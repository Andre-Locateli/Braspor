using Main.Helper;
using Main.Model;
using Main.Model.EtiquetaFolder;
using Main.View.PopupFolder;
using Org.BouncyCastle.Utilities.Encoders;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace Main.View.PagesFolder.Configuration
{
    public partial class SystemForm : Form
    {
        EtiquetaClass _etiqueta = new EtiquetaClass();
        public SystemForm()
        {
            InitializeComponent();
            LoadEstacoes();
            LoadImpressoras();
            LoadEtiquetas();
            loadDgvEtiqueta();
            LoadStringSQL();

            //lbl_time.Text = $"{DateTime.Now.Day.ToString("D2")}/{DateTime.Now.Month.ToString("D2")}/{DateTime.Now.Year}";
            //lblUsuario.Text = Program._usuarioLogado.Nome;
            //lblAcesso.Text = Program._usuarioLogado.Acesso;

            if (Convert.ToBoolean(Program.CFG._modoOperacao))
            {
                ch_producao.Checked = true;
            }

        }
        private void LoadImpressoras()
        {
            cbImpressora.Items.Clear();
            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("@parent", Program.CFG.Estação);
            List<Object> result = Program.SQL.SelectList("select full_name from Rede where tipo='Impressora' and (parent = @parent or parent = '-')", "Rede", "full_name", parametros);
            if(result.Count > 0)
            {
                foreach (var item in result)
                {
                    cbImpressora.Items.Add(item);
                }
            }
            parametros = new Dictionary<string, object>();
            parametros.Add("@estacao", Program.CFG.Estação);
            result = Program.SQL.SelectList("select * from Configuracao where estacao=@estacao", "Configuracao", values: parametros);
            if (result.Count > 0)
            {
                ConfiguracaoClass Config = (ConfiguracaoClass)result[0];
                parametros.Clear(); parametros.Add("@parent", Program.CFG.Estação);
                result = Program.SQL.SelectList("select * from Rede where tipo='Impressora' and (parent = @parent or parent = '-')", "Rede", values:parametros);
                foreach (RedeClass item in result)
                {
                    if (item.Id == Config.id_Impressora)
                    {
                        cbImpressora.Texts = item.full_name;
                    }
                }
                cbCopias.Texts = Config.copias.ToString();
            }
            else
            {
                parametros = new Dictionary<string, object>();
                parametros.Add("@estacao", Program.CFG.Estação);
                parametros.Add("@id_Impressora", 0);
                parametros.Add("@id_Etiqueta", 0);
                parametros.Add("@copias", 1);
                parametros.Add("@dateinsert", DateTime.Today);
                Program.SQL.CRUDCommand("insert into Configuracao (estacao, id_Impressora, id_Etiqueta, copias, dateinsert) values (@estacao, @id_Impressora, @id_Etiqueta, @copias, @dateinsert)", "Configuracao", parametros);
            }
        }

        private void LoadEtiquetas()
        {
            cbEtiqueta.Items.Clear();
            List<Object> result = Program.SQL.SelectList("select nome_etiqueta from Etiqueta", "Etiqueta", "nome_etiqueta");
            if (result.Count > 0)
            {
                foreach (var item in result)
                {
                    cbEtiqueta.Items.Add(item);
                }
            }
            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("@estacao", Program.CFG.Estação);
            result = Program.SQL.SelectList("select * from Configuracao where estacao=@estacao", "Configuracao", values: parametros);
            if (result.Count > 0)
            {
                ConfiguracaoClass Config = (ConfiguracaoClass)result[0];
                result = Program.SQL.SelectList("select * from Etiqueta", "Etiqueta");
                foreach (EtiquetaClass item in result)
                {
                    if (item.id == Config.id_Etiqueta)
                    {
                        cbEtiqueta.Texts = item.nome_etiqueta;
                    }
                }
            }
        }
        private void LoadEstacoes()
        {
            cbEstacoes.Items.Clear();
            List<Object> result = Program.SQL.SelectList("select distinct(parent) from Rede where parent <> '-'", "Rede", "parent");
            bool b_Exist = false;
            if(result.Count != 0)
            {
                foreach (var item in result)
                {
                    if (item.ToString() == Program.CFG.Estação.ToString()) b_Exist = true;
                    cbEstacoes.Items.Add(item);
                }
            }
            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("@estacao", Program.CFG.Estação);
            result = Program.SQL.SelectList("select * from Configuracao where estacao=@estacao", "Configuracao", values: parametros);
            if (result.Count == 0)
            {
                parametros.Clear();
                parametros.Add("@estacao", Program.CFG.Estação);
                parametros.Add("@dateinsert", DateTime.Today);
                bool result1 = Program.SQL.CRUDCommand("insert into Configuracao (estacao, id_Impressora, id_Etiqueta, copias, dateinsert) values (@estacao, '0', '0', 1, @dateinsert)", "Etiqueta", parametros);
                if (result1)
                {

                }
                else
                {
                    MessageBox.Show($"Erro ao adicionar Estação nas configurações!", "VC Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

            if (b_Exist == true) cbEstacoes.Texts = Program.CFG.Estação;
        }

        private void btnSalvarEstacao_Click(object sender, EventArgs e)
        {
            XmlDocument doc = new XmlDocument();
            string caminhoCompleto = Path.Combine(Application.StartupPath, Program.CFG.CaminhoConfig);
            doc.Load(caminhoCompleto);
            XmlElement elem = (XmlElement)doc.SelectSingleNode("//Estacao");
            if(elem != null)
            {
                elem.InnerText = cbEstacoes.Texts;
                Program.CFG.Estação = cbEstacoes.Texts;
            }
            try
            {
                doc.Save(caminhoCompleto);
                MessageBox.Show("Configuração de Estação salva com sucesso!", "VC Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao salvar Configuração da Estação! {ex}", "VC Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnArquivo_Click(object sender, EventArgs e)
        {
            OpenFileDialog fdArquivo = new OpenFileDialog();
            fdArquivo.Filter = "Arquivos de texto (*.txt)|*.txt";
            if (fdArquivo.ShowDialog() == DialogResult.OK)
            {
                _etiqueta.arquivo = "";
                string caminhoArquivo = fdArquivo.FileName;

                StreamReader reader = new StreamReader(caminhoArquivo);
                _etiqueta.arquivo = reader.ReadToEnd();
                reader.Close();

                lbTextoArquivo.Text = $"Arquivo escolhido: {caminhoArquivo}";
            }
        }

        private void loadDgvEtiqueta()
        {
           dgvEtiquetas.DataSource = Program.SQL.SelectDataGrid("select id, nome_etiqueta as 'Nome Etiqueta', arquivo as 'Arquivo', dateinsert as 'DataInsert', dateupdate as 'DateUpdate' from Etiqueta", "Etiqueta");
        }

        private void btnSalvarEtq_Click(object sender, EventArgs e)
        {
            if (txtNomeEtiqueta.Text != "" && _etiqueta.arquivo != null)
            {
                if (ValidarCamposEtiqueta() == false)
                {
                    MessageBox.Show("Nome de Etiqueta digitado já existe, escolha outro!", "VC Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (_etiqueta.id != 0)
                {
                    //UPDATE
                    Dictionary<string, object> parametros = new Dictionary<string, object>();
                    parametros.Add("@id", _etiqueta.id);
                    parametros.Add("@nome_etiqueta", txtNomeEtiqueta.Text);
                    parametros.Add("@arquivo", _etiqueta.arquivo);
                    parametros.Add("@dateupdate", DateTime.Today);
                    bool result = Program.SQL.CRUDCommand("update Etiqueta set nome_etiqueta = @nome_etiqueta, arquivo = @arquivo, dateupdate = @dateupdate where id=@id", "Etiqueta", parametros);
                    if (result)
                    {
                        MessageBox.Show("Etiqueta editada com sucesso!", "VC Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        loadDgvEtiqueta();
                        LoadEtiquetas();
                    }
                }
                else
                {
                    //INSERT
                    Dictionary<string, object> parametros = new Dictionary<string, object>();
                    parametros.Add("@nome_etiqueta", txtNomeEtiqueta.Text);
                    parametros.Add("@arquivo", _etiqueta.arquivo);
                    parametros.Add("@dateinsert", DateTime.Today);
                    bool result =  Program.SQL.CRUDCommand("insert into Etiqueta (nome_etiqueta, arquivo, dateinsert) values (@nome_etiqueta, @arquivo, @dateinsert)", "Etiqueta", parametros);
                    if (result)
                    {
                        MessageBox.Show("Etiqueta adicionada com sucesso!", "VC Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        loadDgvEtiqueta();
                        LoadEtiquetas();
                    }
                }
                LimparEtiqueta();
            }
            else
            {
                MessageBox.Show("Preencha os campos de etiqueta corretamente!", "VC Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void txtNomeEtiqueta_TextChanged(object sender, EventArgs e)
        {
            _etiqueta.nome_etiqueta = txtNomeEtiqueta.Text;
        }

        private void btnSalvarImpressora_Click(object sender, EventArgs e)
        {
            if(cbEtiqueta.Texts != "" && cbImpressora.Texts != "")
            {
                Dictionary<string, object> parametros = new Dictionary<string, object>();
                parametros.Add("@full_name", cbImpressora.Texts);
                List<Object> result = Program.SQL.SelectList("select Id from Rede where full_name=@full_name", "Rede", "Id", parametros);
                int id_Impressora = 0, id_Etiqueta = 0;
                if(result.Count > 0)
                {
                    id_Impressora = Convert.ToInt32(result[0]);
                }
                else{
                    MessageBox.Show($"Erro na obtenção do ID da impressora {cbImpressora.Texts}!", "VC Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                parametros.Clear(); parametros.Add("@nome_etiqueta", cbEtiqueta.Texts);
                result = Program.SQL.SelectList("select id from Etiqueta where nome_etiqueta=@nome_etiqueta", "Etiqueta", "id", parametros);
                if (result.Count > 0)
                {
                    id_Etiqueta = Convert.ToInt32(result[0]);
                }
                else
                {
                    MessageBox.Show($"Erro na obtenção do ID da etiqueta {cbEtiqueta.Texts}!", "VC Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                parametros.Clear(); parametros.Add("@estacao", Program.CFG.Estação);
                parametros.Add("@id_Impressora", id_Impressora);
                parametros.Add("@id_Etiqueta", id_Etiqueta);
                parametros.Add("@copias", Convert.ToInt32(cbCopias.Texts));
                parametros.Add("@dateupdate", DateTime.Today);
                bool b_result = Program.SQL.CRUDCommand("update Configuracao set id_Impressora = @id_Impressora, id_Etiqueta = @id_Etiqueta, copias = @copias, dateupdate = @dateupdate where estacao=@estacao", "Configuracao", parametros);
                if (b_result)
                {
                    MessageBox.Show("Configuração salva com sucesso!", "VC Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    loadDgvEtiqueta();
                }
                else
                {
                    MessageBox.Show("Erro ao salvar as configurações!", "VC Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void dgvEtiquetas_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 0)
                {
                    _etiqueta.id = Convert.ToInt32(dgvEtiquetas.CurrentRow.Cells["id"].Value);
                    txtNomeEtiqueta.Text = (string)dgvEtiquetas.CurrentRow.Cells["Nome Etiqueta"].Value;
                    _etiqueta.nome_etiqueta = (string)dgvEtiquetas.CurrentRow.Cells["Nome Etiqueta"].Value;
                    _etiqueta.arquivo = (string)dgvEtiquetas.CurrentRow.Cells["Arquivo"].Value;
                    lbTextoArquivo.Text = "Arquivo escolhido: Banco de Dados";
                    btnNovaEtiqueta.Visible = true;
                }

                if (e.ColumnIndex == 1)
                {
                    YesOrNo question = new YesOrNo("Você tem certeza que deseja deletar a etiqueta selecionada?");
                    question.ShowDialog();

                    if (question.RESPOSTA)
                    {
                        if (Program.SQL.CRUDCommand("DELETE FROM Etiqueta WHERE id = @id", "Etiqueta", new Dictionary<string, object>() { { "@id", dgvEtiquetas.CurrentRow.Cells["id"].Value } }))
                        {
                            loadDgvEtiqueta();
                            LoadEtiquetas();
                            LimparEtiqueta();
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void LimparEtiqueta()
        {
            _etiqueta = new EtiquetaClass();
            txtNomeEtiqueta.Text = "";
            lbTextoArquivo.Text = "Nenhum arquivo...";
            btnNovaEtiqueta.Visible = false;
        }
        private void btnNovaEtiqueta_Click(object sender, EventArgs e)
        {
            LimparEtiqueta();
        }

        private bool ValidarCamposEtiqueta()
        {
            Dictionary<string, object>  parametros = new Dictionary<string, object>();
            parametros.Add("@nome_etiqueta", txtNomeEtiqueta.Text);
            List<Object> result = Program.SQL.SelectList("select * from Etiqueta where nome_etiqueta=@nome_etiqueta", "Etiqueta", values: parametros);
            if(result.Count > 0)
            {
                if(result.Count > 1)
                {
                    return false;
                }
                else
                {
                    EtiquetaClass etiqueta = (EtiquetaClass)result[0];
                    if (etiqueta.id != _etiqueta.id)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }
            else
            {
                return true;
            }
        }

        private void LoadStringSQL()
        {
            XmlDocument doc = new XmlDocument();
            string caminhoCompleto = Path.Combine(Application.StartupPath, Program.CFG.CaminhoConfig);
            doc.Load(caminhoCompleto);
            XmlElement elem = (XmlElement)doc.SelectSingleNode("//SQLConnection");
            if (elem != null)
            {
                txtStringSQL.Text = elem.InnerText;
            }
        }

        private void btnSalvarBancoSQL_Click(object sender, EventArgs e)
        {
            if(txtStringSQL.Text != "")
            {
                XmlDocument doc = new XmlDocument();
                string caminhoCompleto = Path.Combine(Application.StartupPath, Program.CFG.CaminhoConfig);
                doc.Load(caminhoCompleto);
                XmlElement elem = (XmlElement)doc.SelectSingleNode("//SQLConnection");
                if (elem != null)
                {
                    elem.InnerText = txtStringSQL.Text;
                }
                try
                {
                    doc.Save(caminhoCompleto);
                    MessageBox.Show("String de Conexão SQL salva com sucesso!", "VC Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro ao salvar String de Conexão SQL da Estação! {ex}", "VC Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
        }

        private void SystemForm_VisibleChanged(object sender, EventArgs e)
        {
            List<object> _result = Program.SQL.SelectList("SELECT * FROM Configuracao WHERE estacao = @estacao", "Configuracao", null, new Dictionary<string, object>() { { "@estacao", Environment.MachineName.Trim() } });
            Program.Configuracao = (ConfiguracaoClass)_result[0];
            if (Program.Configuracao != null)
            {
                if (Program.Configuracao.id_Etiqueta != 0)
                {
                    _result = Program.SQL.SelectList("SELECT * FROM Etiqueta WHERE id = @id", "Etiqueta", null, new Dictionary<string, object>() { { "@id", Program.Configuracao.id_Etiqueta } });
                    if (_result.Count > 0) { Program.Etiqueta = (EtiquetaClass)_result[0]; }
                    
                }
            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            StyleSheet.panel_Paint(sender, e);
        }

        private void SystemForm_Resize(object sender, EventArgs e)
        {
            try
            {
                StyleSheet.RedrawAll(this);
            }
            catch (Exception)
            {
            }
        }

        private void tbnEstacao_Click(object sender, EventArgs e)
        {
            tbMenu.Visible = false;
            pnEstacao.Visible = true;
        }

        private void btnImpressora_Click(object sender, EventArgs e)
        {
            tbMenu.Visible = false;
            pnImpressora.Visible = true;
        }

        private void btnBanco_Click(object sender, EventArgs e)
        {
            tbMenu.Visible = false;
            pnBanco.Visible = true;
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            tbMenu.Visible = true;
            pnEstacao.Visible = false;
            pnImpressora.Visible = false;
            pnBanco.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // teste();

               // Program.com.ImpressoraPrint(new EtiquetaInfo() {   earn = "123456789122", packCaixa = "88", partNumber = "123456", produtoProduzido = "AB-CA-111011", quantidadePecas = "138", date = Convert.ToString(GetJulianDay(DateTime.Now)) }, 2);
            }
            catch (Exception)
            {
            }
        }

        static string CompressLZ77(Bitmap bitmap)
        {
            byte[] data = ConvertBitmapToByteArray(bitmap);
            StringBuilder compressed = new StringBuilder();

            int inputLength = data.Length;
            int searchBuffer = 64;  // Tamanho do buffer de busca
            int lookaheadBuffer = 16;  // Tamanho do buffer de visualização

            int currentIndex = 0;

            while (currentIndex < inputLength)
            {
                int maxMatchLength = 0;
                int matchIndex = -1;

                for (int i = Math.Max(currentIndex - searchBuffer, 0); i < currentIndex; i++)
                {
                    int matchLength = 0;

                    while (currentIndex + matchLength < inputLength &&
                           data[i + matchLength] == data[currentIndex + matchLength] &&
                           matchLength < lookaheadBuffer)
                    {
                        matchLength++;
                    }

                    if (matchLength > maxMatchLength)
                    {
                        maxMatchLength = matchLength;
                        matchIndex = i;
                    }
                }

                if (maxMatchLength > 0)
                {
                    compressed.Append($"<{currentIndex - matchIndex},{maxMatchLength}>");
                    currentIndex += maxMatchLength;
                }
                else
                {
                    compressed.Append(data[currentIndex]);
                    currentIndex++;
                }
            }

            return compressed.ToString();
        }

        static byte[] ConvertBitmapToByteArray(Bitmap bitmap)
        {
            using (var stream = new System.IO.MemoryStream())
            {
                bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Bmp);
                return stream.ToArray();
            }
        }


        private int GetJulianDay(DateTime date)
        {
            // Creates an instance of the JulianCalendar.
            JulianCalendar myCal = new JulianCalendar();
            DateTime currentDate = DateTime.Now;
            int julianDay = currentDate.DayOfYear;

            return julianDay;
        }

        private void SystemForm_Load(object sender, EventArgs e)
        {

        }

        private void ch_producao_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                string caminhoCompleto = Path.Combine(Application.StartupPath, Program.CFG.CaminhoConfig);
                doc.Load(caminhoCompleto);
                XmlElement elem = (XmlElement)doc.SelectSingleNode("//ModoDeOperacao");

                if (elem != null)
                {
                    Program.CFG._modoOperacao = ch_producao.Checked.ToString();
                    elem.InnerText = ch_producao.Checked.ToString();
                }
                try
                {
                    doc.Save(caminhoCompleto);
                    // MessageBox.Show("Configuração de Estação salva com sucesso!", "VC Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    // MessageBox.Show($"Erro ao salvar Configuração da Estação! {ex}", "VC Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception)
            {
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            XmlDocument doc = new XmlDocument();
            string caminhoCompleto = Path.Combine(Application.StartupPath, Program.CFG.CaminhoConfig);
            doc.Load(caminhoCompleto);
            XmlElement elem = (XmlElement)doc.SelectSingleNode("//EnderecoRef");
            if (elem != null)
            {
                elem.InnerText = txtEnderecoReferencia.Text;
                Program.Endereco_Referencia = Convert.ToInt32(txtEnderecoReferencia.Text);
            }
            try
            {
                doc.Save(caminhoCompleto);
                MessageBox.Show("Configuração de Estação salva com sucesso!", "VC Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao salvar Configuração da Estação! {ex}", "VC Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void txtEnderecoReferencia_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            }
            catch (Exception)
            {
            }
        }
    }
}

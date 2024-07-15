using Main.Helper;
using Main.Model;
using Main.Model.EtiquetaFolder;
using Main.View.PopupFolder;
using Org.BouncyCastle.Utilities.Encoders;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using ZPL;
using ZXing;

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
        //private void LoadImpressoras()
        //{
        //    cbImpressora.Items.Clear();
        //    Dictionary<string, object> parametros = new Dictionary<string, object>();
        //    parametros.Add("@parent", Program.CFG.Estação);
        //    List<Object> result = Program.SQL.SelectList("select full_name from Rede where tipo='Impressora' and (parent = @parent or parent = '-')", "Rede", "full_name", parametros);
        //    if(result.Count > 0)
        //    {
        //        foreach (var item in result)
        //        {
        //            cbImpressora.Items.Add(item);
        //        }
        //    }
        //    parametros = new Dictionary<string, object>();
        //    parametros.Add("@estacao", Program.CFG.Estação);
        //    result = Program.SQL.SelectList("select * from Configuracao where estacao=@estacao", "Configuracao", values: parametros);
        //    if (result.Count > 0)
        //    {
        //        ConfiguracaoClass Config = (ConfiguracaoClass)result[0];
        //        parametros.Clear(); parametros.Add("@parent", Program.CFG.Estação);
        //        result = Program.SQL.SelectList("select * from Rede where tipo='Impressora' and (parent = @parent or parent = '-')", "Rede", values:parametros);
        //        foreach (RedeClass item in result)
        //        {
        //            if (item.Id == Config.id_Impressora)
        //            {
        //                cbImpressora.Texts = item.full_name;
        //            }
        //        }
        //        cbCopias.Texts = Config.copias.ToString();
        //    }
        //    else
        //    {
        //        parametros = new Dictionary<string, object>();
        //        parametros.Add("@estacao", Program.CFG.Estação);
        //        parametros.Add("@id_Impressora", 0);
        //        parametros.Add("@id_Etiqueta", 0);
        //        parametros.Add("@copias", 1);
        //        parametros.Add("@dateinsert", DateTime.Today);
        //        Program.SQL.CRUDCommand("insert into Configuracao (estacao, id_Impressora, id_Etiqueta, copias, dateinsert) values (@estacao, @id_Impressora, @id_Etiqueta, @copias, @dateinsert)", "Configuracao", parametros);
        //    }
        //}

        public void LoadImpressoras()
        {
            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("@parent", "-");
            _impressoras = Program.SQL.SelectList("select * from Rede where parent = @parent and tipo = 'Impressora'", "Rede", values: parametros);
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


        private List<Object> _impressoras = new List<Object>();


        public async void ImpressoraPrint(EtiquetaInfo etiqueta, int type)
        {
            try
            {
                foreach (RedeClass impressora in _impressoras)
                {
                    string zplCode = "";

                    //Criar novo layout da etiqueta.

                    ZXing.BarcodeWriter brcode = new ZXing.BarcodeWriter();

                    string lbl_op = "N° Op:";
                    string lbl_op_r = "000000000";       //-//ALTERAR
                    string lbl_cli = "Cliente:";
                    string lbl_cli_r = "Cliente Padrão"; //-//ALTERAR
                    string lbl_peso = "Peso:";
                    string lbl_peso_r = "1000";          //-//ALTERAR
                    string lbl_qtfl = "Qtd Folhas:";
                    string lbl_qtfl_r = "100";           //-//ALTERAR
                    string lbl_tppl = "Tipo Papel:";
                    string lbl_tppl_r = "Sulfite";    //-//ALTERAR
                    string lbl_fmt = "Formato:";
                    string lbl_fmt_r = "A4";  //-//ALTERAR
                    string lbl_gr = "Gram:";
                    string lbl_gr_r = "0,0046";      //-//ALTERAR
                    string lbl_dtin = "Data Início:";
                    string lbl_dtin_r = "21/06/2024"; //-//ALTERAR
                    string lbl_dtfm = "Data Término:";
                    string lbl_dtfm_r = "21/06/2024"; //-//ALTERAR
                    string lbl_hrin = "Horário inicial:";
                    string lbl_hrin_r = "11:30:00"; //-//ALTERAR
                    string lbl_hrfm = "Horário final:";
                    string lbl_hrfm_r = "12:00:00";   //-//ALTERAR
                    string lbl_opr = "Operador:";
                    string lbl_opr_r = "Operador Padrão";      //-//ALTERAR
                    string lbl_trn = "Turno:";
                    string lbl_trn_r = "Matutino";      //-//ALTERAR
                    string lbl_obs = "Obs:";
                    string lbl_obs_r = @"3.14159265358979323846264338327950288419716939937510582097494459230781640628620899862803482534211706798214808651328230664709384460955058223172535940812848111745028410270";      //-//ALTERAR

                    string split1 = "";
                    string split2 = "";
                    string split3 = "";

                    if (lbl_obs_r.Length <= 60)
                    {
                        split1 = lbl_obs_r;
                    }
                    else if (lbl_obs_r.Length > 60 & lbl_obs_r.Length < 120)
                    {
                        split1 = lbl_obs_r.Substring(0, 60);
                        split2 = lbl_obs_r.Substring(60, lbl_obs_r.Length - 60);
                    }
                    else if (lbl_obs_r.Length > 60)
                    {
                        split1 = lbl_obs_r.Substring(0, 60);
                        split2 = lbl_obs_r.Substring(60, 60);
                        split3 = lbl_obs_r.Substring(120);
                    }


                    System.Drawing.Font f1 = new System.Drawing.Font("Arial", 18, FontStyle.Regular, GraphicsUnit.Pixel);
                    System.Drawing.Font fmn = new System.Drawing.Font("Arial", 15, FontStyle.Bold, GraphicsUnit.Pixel);

                    System.Drawing.Brush brush = System.Drawing.Brushes.Black;


                    int x = (int)(148 * (96 / 25.4f));
                    int y = (int)(105 * (96 / 25.4f));

                    Bitmap bitmap = new Bitmap(x, y);

                    int wid = (int)(35 * 96 / 25.4f);
                    int hei = (int)(12 * 96 / 25.4f);


                    System.Drawing.Pen blackPen = new System.Drawing.Pen(System.Drawing.Color.Black, 2);

                    using (Graphics graphics = Graphics.FromImage(bitmap))
                    {
                        graphics.Clear(System.Drawing.Color.White);

                        //Draw Black lines


                        //op
                        graphics.DrawLine(blackPen, 72f, 30f, 200f, 30f);

                        //cliente
                        graphics.DrawLine(blackPen, 280f, 30f, 540f, 30f);

                        //peso
                        graphics.DrawLine(blackPen, 65f, 80f, 200f, 80f);

                        //qtfl
                        graphics.DrawLine(blackPen, 309f, 80f, 440f, 80f);

                        //tipo papel
                        graphics.DrawLine(blackPen, 110f, 130f, 225f, 130f);

                        //formato
                        graphics.DrawLine(blackPen, 314f, 130f, 388f, 130f);

                        //gram
                        graphics.DrawLine(blackPen, 449f, 130f, 540f, 130f);

                        //data inicio
                        graphics.DrawLine(blackPen, 110f, 180f, 225f, 180f);

                        //data termino
                        graphics.DrawLine(blackPen, 355f, 180f, 470f, 180f);

                        //horario inicial
                        graphics.DrawLine(blackPen, 132f, 230f, 225f, 230f);

                        //horario final
                        graphics.DrawLine(blackPen, 343f, 230f, 470f, 230f);

                        //operador
                        graphics.DrawLine(blackPen, 100f, 280f, 340f, 280f);

                        //turno
                        graphics.DrawLine(blackPen, 404f, 280f, 540f, 280f);





                        //op
                        graphics.DrawString(lbl_op, f1, brush, new PointF(12, 10));
                        graphics.DrawString(lbl_op_r, f1, brush, new PointF(74, 10));
                        
                        //cliente
                        graphics.DrawString(lbl_cli, f1, brush, new PointF(210, 10));
                        graphics.DrawString(lbl_cli_r, f1, brush, new PointF(281, 10));

                        //peso
                        graphics.DrawString(lbl_peso, f1, brush, new PointF(12, 60));
                        graphics.DrawString(lbl_peso_r, f1, brush, new PointF(65, 60));

                        //qt folhas
                        graphics.DrawString(lbl_qtfl, f1, brush, new PointF(208, 60));
                        graphics.DrawString(lbl_qtfl_r, f1, brush, new PointF(308, 60));

                        //tp papel
                        graphics.DrawString(lbl_tppl, f1, brush, new PointF(12, 110));
                        graphics.DrawString(lbl_tppl_r, f1, brush, new PointF(112, 110));

                        //formato
                        graphics.DrawString(lbl_fmt, f1, brush, new PointF(236, 110));
                        graphics.DrawString(lbl_fmt_r, f1, brush, new PointF(316, 110));

                        //gram
                        graphics.DrawString(lbl_gr, f1, brush, new PointF(393, 110));
                        graphics.DrawString(lbl_gr_r, f1, brush, new PointF(450, 110));

                        //data inicio
                        graphics.DrawString(lbl_dtin, f1, brush, new PointF(12, 160));
                        graphics.DrawString(lbl_dtin_r, f1, brush, new PointF(112, 160));

                        //data Término
                        graphics.DrawString(lbl_dtfm, f1, brush, new PointF(236, 160));
                        graphics.DrawString(lbl_dtfm_r, f1, brush, new PointF(356, 160));

                        //horário inicial
                        graphics.DrawString(lbl_hrin, f1, brush, new PointF(12, 210));
                        graphics.DrawString(lbl_hrin_r, f1, brush, new PointF(132, 210));

                        //horário final
                        graphics.DrawString(lbl_hrfm, f1, brush, new PointF(236, 210));
                        graphics.DrawString(lbl_hrfm_r, f1, brush, new PointF(346, 210));

                        graphics.DrawString(lbl_opr, f1, brush, new PointF(12, 260));
                        graphics.DrawString(lbl_opr_r, f1, brush, new PointF(100, 260));

                        graphics.DrawString(lbl_trn, f1, brush, new PointF(346, 260));
                        graphics.DrawString(lbl_trn_r, f1, brush, new PointF(406, 260));

                        graphics.DrawString(lbl_obs, f1, brush, new PointF(12, 305));
                        graphics.DrawString(split1, fmn, brush, new PointF(56, 308));
                        graphics.DrawString(split2, fmn, brush, new PointF(56, 333));
                        graphics.DrawString(split3, fmn, brush, new PointF(56, 358));


                    }

                    ZPLPrintingService prnSvc = new ZPLPrintingService();
                    //Bitmap bmp = RotateBitmap(bitmap, 90);
                    zplCode = await prnSvc.GetImageZPLEncoded(bitmap);
                    zplCode = zplCode.Replace("#barcode#", lbl_op);
                    //Console.WriteLine(zplCode);

                    PrintDocument documento = new PrintDocument();
                    PrinterSettings configImpressora = new PrinterSettings();
                    PageSettings pageSettings = documento.DefaultPageSettings;

                    string[] impressoras = PrinterSettings.InstalledPrinters.Cast<string>().ToArray();
                    string name = "";
                    foreach (string item in impressoras)
                    {

                        if (item == impressora.impressora) { name = impressora.impressora; }
                    }

                    //bitmap.RotateFlip(RotateFlipType.Rotate90FlipNone);
                    PrintPopup print = new PrintPopup(bitmap);
                    print.ShowDialog();

                    if (!string.IsNullOrWhiteSpace(name))
                    {
                        configImpressora.PrinterName = name;
                        documento.PrinterSettings = configImpressora;
                        documento.DefaultPageSettings.Landscape = true;
                    }

                    documento.PrintPage += (sender, args) =>
                    {
                        args.Graphics.DrawImage(bitmap, 0, 0); // Ajuste a posição conforme necessário
                    };

                    documento.Print();
                }


            }
            catch (Exception)
            {

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // teste();

                ImpressoraPrint(new EtiquetaInfo() { op = "", cliente = "", peso = "", qtd_folhas = "", tipo_papel = "", formato = "", gramatura = "", data_inicio = "", data_termino = "", horario_inicial = "", horario_final = "", operador = "", turno = "", obs = "" }, 2);
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

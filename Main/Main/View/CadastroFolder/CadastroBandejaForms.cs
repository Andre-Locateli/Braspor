using Main.Model;
using Main.Properties;
using Main.View.PopupFolder;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Markup.Localizer;

namespace Main.View.CadastroFolder
{
    public partial class CadastroBandejaForms : Form
    {
        public CadastroBandejaForms()
        {
            InitializeComponent();

            lbl_time.Text = $"{DateTime.Now.Day.ToString("D2")}/{DateTime.Now.Month.ToString("D2")}/{DateTime.Now.Year}";
            lblUsuario.Text = Program._usuarioLogado.Nome;
            lblAcesso.Text = Program._usuarioLogado.Acesso;

            btnEditarUser.Enabled = Program._permissaoUsuario.Bandeja_edit  ;
            btnSalvarUser.Enabled = Program._permissaoUsuario.Bandeja_add;
            //btnNovoUser.Enabled = Program._permissaoUsuario.Bandeja_edit;

            LoadBandejaDB();
        }

        public void LoadBandejaDB()
        {
            try
            {
                dgv_dados.DataSource = Program.SQL.SelectDataGrid("SELECT Id, Codigo AS [Código], descricao AS [Descrição], Peso_alvo as [Peso Alvo], Quantidade_Produtos AS [Quantidade Produtos], Tolerancia AS [Tolerância] FROM Bandeja", "Bandeja");
                dgv_dados.Columns[2].Visible = false;
            }
            catch (Exception)
            {
            }
        }

        private void btnSalvarUser_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtCodigo.Text)) { btnPaintBorder_Click(txtCodigo); return; }
                if (string.IsNullOrWhiteSpace(txtPesoAlvo.Text)) { btnPaintBorder_Click(txtPesoAlvo); return; }
                if (string.IsNullOrWhiteSpace(txtTolerancia.Text)) { btnPaintBorder_Click(txtTolerancia); return; }
                if (string.IsNullOrWhiteSpace(txtDescricao.Text)) { btnPaintBorder_Click(txtDescricao); return; }
                if (string.IsNullOrWhiteSpace(txtQuantidadeProduto.Text)) { btnPaintBorder_Click(txtQuantidadeProduto); return; }
                
                this.Refresh();

                using (MemoryStream ms = new MemoryStream())
                {
                    pcbImage.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    byte[] imagemBytes = ms.ToArray();

                    if (Program.SQL.CRUDCommand("INSERT INTO Bandeja " +
                        "(Codigo,descricao,Peso_alvo,Quantidade_Produtos,Tolerancia,Foto,dateinsert) " +
                        "VALUES" +
                        " (@Codigo,@descricao,@Peso_alvo,@Quantidade_Produtos,@Tolerancia,@Foto,@dateinsert)",
                        "Usuario", new Dictionary<string, object>()
                        {
                            {"@Codigo", txtCodigo.Text },
                            {"@descricao", txtDescricao.Text },
                            {"@Peso_alvo", Convert.ToSingle(txtPesoAlvo.Text) },
                            {"@Quantidade_Produtos", Convert.ToDecimal(txtQuantidadeProduto.Text) },
                            {"@Tolerancia", Convert.ToSingle(txtTolerancia.Text) },
                            {"@Foto",  Convert.ToBase64String(imagemBytes)},
                            {"@dateinsert", DateTime.Now}}))
                    {
                        btnNovoUser_Click(sender, e);
                        LoadBandejaDB();
                    }
                    else
                    {
                        MessageBox.Show("Erro ao adicionar item");
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void btnPaintBorder_Click(object sender)
        {
            // Obtém a cor selecionada para a borda
            Color borderColor = Color.Red; // Exemplo: vermelho

            // Pinta a borda do item desejado
            Control item = (Control)sender; // Substitua "textBox1" pelo nome do controle desejado
            using (Graphics g = item.CreateGraphics())
            {
                Pen pen = new Pen(borderColor, 1); // Largura da borda = 2
                Rectangle rect = new Rectangle(0, 0, item.Width - 1, item.Height - 1);
                g.DrawRectangle(pen, rect);
            }
        }

        private void btnNovoUser_Click(object sender, EventArgs e)
        {
            try
            {
                pcbImage.Image = Resources.NoImage;
                txtCodigo.Text = "";
                txtPesoAlvo.Text = "";
                txtTolerancia.Text = "";
                txtDescricao.Text = "";
                btnEditarUser.Visible = false;
                btnNovoUser.Visible = false;
                btnSalvarUser.Visible = true;
                LoadBandejaDB();
            }
            catch (Exception)
            {
            }
        }

        private void btnEditarUser_Click(object sender, EventArgs e)
        {
            try
            {
                {
                    if (string.IsNullOrWhiteSpace(txtCodigo.Text)) { btnPaintBorder_Click(txtCodigo); return; }
                    if (string.IsNullOrWhiteSpace(txtPesoAlvo.Text)) { btnPaintBorder_Click(txtPesoAlvo); return; }
                    if (string.IsNullOrWhiteSpace(txtTolerancia.Text)) { btnPaintBorder_Click(txtTolerancia); return; }
                    if (string.IsNullOrWhiteSpace(txtDescricao.Text)) { btnPaintBorder_Click(txtDescricao); return; }
                    if (string.IsNullOrWhiteSpace(txtQuantidadeProduto.Text)) { btnPaintBorder_Click(txtQuantidadeProduto); return; }

                    this.Refresh();

                    using (MemoryStream ms = new MemoryStream())
                    {
                        pcbImage.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                        byte[] imagemBytes = ms.ToArray();

                        if (Program.SQL.CRUDCommand("UPDATE Bandeja SET " +
                            "Codigo=@Codigo,descricao=@descricao,Peso_alvo=@Peso_alvo,Quantidade_Produtos=@Quantidade_Produtos,Tolerancia=@Tolerancia,Foto=@Foto " +
                            "WHERE Id = @Id",
                            "Bandeja", new Dictionary<string, object>()
                            {
                                {"@Id", dgv_dados.CurrentRow.Cells["Id"].Value},
                                {"@Codigo", txtCodigo.Text },
                                {"@descricao", txtDescricao.Text },
                                {"@Peso_alvo", Convert.ToSingle(txtPesoAlvo.Text) },
                                {"@Quantidade_Produtos", Convert.ToDecimal(txtQuantidadeProduto.Text)},
                                {"@Tolerancia", Convert.ToSingle(txtTolerancia.Text) },
                                {"@Foto",  Convert.ToBase64String(imagemBytes)} 
                            }))
                        {
                            btnNovoUser_Click(sender, e);
                            LoadBandejaDB();
                        }
                        else
                        {
                            MessageBox.Show("Erro ao adicionar item");
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void txt_search_TextChanged(object sender, EventArgs e)
        {
            try
            {
                var ds = new DataSet();
                DataTable dt;
                dt = (DataTable)dgv_dados.DataSource;
                dt.DefaultView.RowFilter = string.Format("[{0}] LIKE '%" + txt_search.Text + "%'", "Descrição");
            }
            catch (Exception)
            {
            }
        }

        private void dgv_dados_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 0)
                {
                    BandejaClass bandeja = (BandejaClass)Program.SQL.SelectObject("SELECT id, Codigo, descricao,Peso_alvo, Quantidade_Produtos,Tolerancia,Foto, dateinsert  FROM Bandeja WHERE Id = @Id", "Bandeja", new Dictionary<string, object>()
                    {
                        {"@Id", dgv_dados.CurrentRow.Cells["Id"].Value}
                    });

                    if (bandeja.Foto.Length > 0) 
                    {
                        using (MemoryStream ms = new MemoryStream(bandeja.Foto))
                        {
                            pcbImage.Image = System.Drawing.Image.FromStream(ms);
                        }
                    }

                    btnEditarUser.Visible = true;
                    btnSalvarUser.Visible = false;
                    btnNovoUser.Visible = true;

                    txtCodigo.Text = bandeja.Codigo;
                    txtPesoAlvo.Text = bandeja.PesoAlvo.ToString();
                    txtQuantidadeProduto.Text = bandeja.Quantidade_produtos.ToString();
                    txtTolerancia.Text = bandeja.Tolerancia.ToString();
                    txtDescricao.Text = bandeja.Descricao;

                }

                if (e.ColumnIndex == 1)
                {

                    var list_receitas = Program.SQL.SelectList("SELECT * FROM Receita Where id_bandeja = @id_bandeja", "Receita",
                                        null, new Dictionary<string, object>()
                                        {
                                                                {"@id_bandeja", dgv_dados.CurrentRow.Cells["Id"].Value}
                                        });

                    if (list_receitas.Count <= 0 || Program._usuarioLogado.Acesso == "Administrador")
                    {

                        YesOrNo question = new YesOrNo("Você tem certeza que deseja remover o bandeja selecionado ?");
                        question.ShowDialog();
                        if (question.RESPOSTA && Program._permissaoUsuario.Bandeja_remove)
                        {
                            if (Program.SQL.CRUDCommand("DELETE FROM Bandeja WHERE Id = @Id", "Bandeja", new Dictionary<string, object>() { { "@Id", dgv_dados.CurrentRow.Cells["Id"].Value } }))
                            {
                                btnNovoUser_Click(new object(), new EventArgs());
                            }
                        }
                    }
                    else 
                    {
                        InfoPopup question = new InfoPopup("Bandeja com movimentações encontradas.","Não é possível remover uma bandeja vinculado a uma Receita em execução ou já executada.");
                        question.ShowDialog();
                    }



                }
            }
            catch (Exception)
            {
            }
        }

        private void pcbImage_Click(object sender, EventArgs e)
        {
            try
            {
                System.Windows.Forms.OpenFileDialog file = new System.Windows.Forms.OpenFileDialog();
                file.Filter = "Arquivo png (*.png)|*.png";
                file.Title = "Escolha a imagem do produto:";
                file.DefaultExt = "png";
                file.Multiselect = false;
                file.ShowDialog();

                if (file.CheckFileExists)
                {
                    Bitmap mybitmap = new Bitmap(file.FileName);
                    pcbImage.Image = mybitmap;
                }
            }
            catch (Exception)
            {
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Panel pn = (Panel)sender;
            System.Drawing.Color COLOR_BORDER = System.Drawing.Color.FromArgb(237, 237, 237);
            int borderSize = 2;
            ControlPaint.DrawBorder(e.Graphics, pn.ClientRectangle, COLOR_BORDER, borderSize, ButtonBorderStyle.Solid, COLOR_BORDER, borderSize, ButtonBorderStyle.Solid, COLOR_BORDER, borderSize, ButtonBorderStyle.Solid, COLOR_BORDER, borderSize, ButtonBorderStyle.Solid);
        }

        private void txtCodigo_Leave(object sender, EventArgs e)
        {
            try
            {
                BandejaClass band = (BandejaClass)Program.SQL.SelectObject("SELECT * FROM Bandeja WHERE Codigo = @Codigo", "Bandeja",
                    new Dictionary<string, object>()
                    {
                        {"@Codigo", txtCodigo.Text}
                    });

                if (band != null) 
                {
                    txtCodigo.Text = "";
                }
            }
            catch (Exception)
            {
            }
        }
    }
}

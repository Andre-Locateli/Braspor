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
using System.Windows.Controls.Primitives;
using System.Windows.Forms;

namespace Main.View.CadastroFolder
{
    public partial class CadastroRecipienteForms : Form
    {
        public CadastroRecipienteForms()
        {
            InitializeComponent();

            lbl_time.Text = $"{DateTime.Now.Day.ToString("D2")}/{DateTime.Now.Month.ToString("D2")}/{DateTime.Now.Year}";
            lblUsuario.Text = Program._usuarioLogado.Nome;
            lblAcesso.Text = Program._usuarioLogado.Acesso;

            btnEditarUser.Enabled = Program._permissaoUsuario.Recipiente_edit;
            btnSalvarUser.Enabled = Program._permissaoUsuario.Recipiente_add;

            LoadRecipiente();
        }

        public void LoadRecipiente()
        {
            try
            {
                dgv_dados.DataSource = Program.SQL.SelectDataGrid("SELECT Id, Package AS [Package], descricao AS [Descrição], Peso_alvo AS [Peso Alvo], Tolerancia AS [Tolerância] FROM Recipiente", "Recipiente");
                dgv_dados.Columns[2].Visible = false;
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

        private void btnSalvarUser_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtPackage.Text)) { btnPaintBorder_Click(txtPackage); return; }
                if (string.IsNullOrWhiteSpace(txtPesoAlvo.Text)) { btnPaintBorder_Click(txtPesoAlvo); return; }
                if (string.IsNullOrWhiteSpace(txtTolerancia.Text)) { btnPaintBorder_Click(txtTolerancia); return; }
                if (string.IsNullOrWhiteSpace(txtDescricao.Text)) { btnPaintBorder_Click(txtDescricao); return; }
                this.Refresh();

                using (MemoryStream ms = new MemoryStream())
                {
                    pcbImage.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    byte[] imagemBytes = ms.ToArray();

                    if (Program.SQL.CRUDCommand("INSERT INTO Recipiente " +
                        "(Package, descricao, Peso_alvo, Tolerancia,Foto, dateinsert) " +
                        "VALUES" +
                        " (@Package, @descricao, @Peso_alvo, @Tolerancia, @Foto, @dateinsert)",
                        "Recipiente", new Dictionary<string, object>()
                        {
                            {"@Package", txtPackage.Text },
                            {"@descricao", txtDescricao.Text },
                            {"@Peso_alvo", Convert.ToSingle(txtPesoAlvo.Text) },
                            {"@Tolerancia", Convert.ToSingle(txtTolerancia.Text) },
                            {"@Foto",  Convert.ToBase64String(imagemBytes)},
                            {"@dateinsert", DateTime.Now}}))
                    {
                        btnNovoUser_Click(sender, e);
                        LoadRecipiente();
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

        private void btnEditarUser_Click(object sender, EventArgs e)
        {
            try
            {
                
                    if (string.IsNullOrWhiteSpace(txtPackage.Text)) { btnPaintBorder_Click(txtPackage); return; }
                    if (string.IsNullOrWhiteSpace(txtPesoAlvo.Text)) { btnPaintBorder_Click(txtPesoAlvo); return; }
                    if (string.IsNullOrWhiteSpace(txtTolerancia.Text)) { btnPaintBorder_Click(txtTolerancia); return; }
                    if (string.IsNullOrWhiteSpace(txtDescricao.Text)) { btnPaintBorder_Click(txtDescricao); return; }
                    this.Refresh();

                    using (MemoryStream ms = new MemoryStream())
                    {
                        pcbImage.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                        byte[] imagemBytes = ms.ToArray();

                        if (Program.SQL.CRUDCommand("UPDATE Recipiente SET " +
                            "Package = @Package, descricao = @descricao, Peso_alvo = @Peso_alvo, Tolerancia = @Tolerancia,Foto = @Foto " +
                            "WHERE Id = @Id",
                            "Usuario", new Dictionary<string, object>()
                            {
                                {"@Id", dgv_dados.CurrentRow.Cells["Id"].Value},
                                {"@Package", txtPackage.Text },
                                {"@descricao", txtDescricao.Text },
                                {"@Peso_alvo", Convert.ToSingle(txtPesoAlvo.Text) },
                                {"@Tolerancia", Convert.ToSingle(txtTolerancia.Text) },
                                {"@Foto",  Convert.ToBase64String(imagemBytes)},
                                {"@dateinsert", DateTime.Now}}))
                        {
                            btnNovoUser_Click(sender, e);
                            LoadRecipiente();
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

        private void btnNovoUser_Click(object sender, EventArgs e)
        {
            try
            {
                pcbImage.Image = Resources.NoImage;
                txtPackage.Text = "";
                txtPesoAlvo.Text = "";
                txtTolerancia.Text = "";
                txtDescricao.Text = "";
                btnEditarUser.Visible = false;
                btnNovoUser.Visible = false;
                btnSalvarUser.Visible = true;
                LoadRecipiente();
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

        private void dgv_dados_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 0)
                {
                    RecipienteClass recipiente = (RecipienteClass)Program.SQL.SelectObject("SELECT id, Package, descricao,Peso_alvo, Tolerancia,Foto, dateinsert  FROM Recipiente WHERE Id = @Id", "Recipiente", new Dictionary<string, object>()
                    {
                        {"@Id", dgv_dados.CurrentRow.Cells["Id"].Value}
                    });

                    if (recipiente.Foto.Length > 0) 
                    {
                        using (MemoryStream ms = new MemoryStream(recipiente.Foto))
                        {
                            pcbImage.Image = System.Drawing.Image.FromStream(ms);
                        }
                    }

                    btnEditarUser.Visible = true;
                    btnSalvarUser.Visible = false;
                    btnNovoUser.Visible = true;

                    txtPackage.Text = recipiente.Package;
                    txtPesoAlvo.Text = recipiente.PesoAlvo.ToString();
                    txtTolerancia.Text = recipiente.Tolerancia.ToString();
                    txtDescricao.Text = recipiente.Descricao;

                }

                if (e.ColumnIndex == 1)
                {
                    var list_receitas = Program.SQL.SelectList("SELECT * FROM Receita Where id_recipiente = @id_recipiente", "Receita",
                               null, new Dictionary<string, object>()
                               {
                                                                {"@id_recipiente", dgv_dados.CurrentRow.Cells["Id"].Value}
                               });


                    if (list_receitas.Count <= 0 || Program._usuarioLogado.Acesso == "Administrador")
                    {
                        YesOrNo question = new YesOrNo("Você tem certeza que deseja remover o recipiente selecionado ?");
                        question.ShowDialog();
                        
                        if (question.RESPOSTA && Program._permissaoUsuario.Recipiente_remove)
                        {
                            if (Program.SQL.CRUDCommand("DELETE FROM Recipiente WHERE Id = @Id", "Recipiente", new Dictionary<string, object>() { { "@Id", dgv_dados.CurrentRow.Cells["Id"].Value } }))
                            {
                                btnNovoUser_Click(new object(), new EventArgs());
                            }
                        }
                    }
                    else
                    {
                        InfoPopup question = new InfoPopup("Recipiente com movimentações encontradas.", "Não é possível remover um recipiente vinculado a uma Receita em execução ou já executada.");
                        question.ShowDialog();
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
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Panel pn = (Panel)sender;
            System.Drawing.Color COLOR_BORDER = System.Drawing.Color.FromArgb(237, 237, 237);
            int borderSize = 2;
            ControlPaint.DrawBorder(e.Graphics, pn.ClientRectangle, COLOR_BORDER, borderSize, ButtonBorderStyle.Solid, COLOR_BORDER, borderSize, ButtonBorderStyle.Solid, COLOR_BORDER, borderSize, ButtonBorderStyle.Solid, COLOR_BORDER, borderSize, ButtonBorderStyle.Solid);
        }

        private void CadastroRecipienteForms_Resize(object sender, EventArgs e)
        {
            this.Update();
            this.Refresh();
        }

        private void txtPackage_Leave(object sender, EventArgs e)
        {
            try
            {
                RecipienteClass recipi = (RecipienteClass)Program.SQL.SelectObject("SELECT * FROM Recipiente WHERE  Package = @Package", "Recipiente", new Dictionary<string, object>()
                {
                    {"@Package", txtPackage.Text}
                });

                if (recipi != null) 
                {
                    txtPackage.Text = "";
                }
            }
            catch (Exception)
            {
            }
        }
    }
}

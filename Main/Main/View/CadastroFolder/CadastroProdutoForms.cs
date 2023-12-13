using Main.Helper;
using Main.Model;
using Main.Properties;
using Main.View.PopupFolder;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using System.Windows.Ink;
using static System.Net.Mime.MediaTypeNames;

namespace Main.View.CadastroFolder
{
    public partial class CadastroProdutoForms : Form
    {
        public CadastroProdutoForms()
        {
            InitializeComponent();

            lbl_time.Text = $"{DateTime.Now.Day.ToString("D2")}/{DateTime.Now.Month.ToString("D2")}/{DateTime.Now.Year}";
            lblUsuario.Text = Program._usuarioLogado.Nome;
            lblAcesso.Text = Program._usuarioLogado.Acesso;

            btnEditarUser.Enabled = Program._permissaoUsuario.Produto_edit;
            btnSalvarUser.Enabled = Program._permissaoUsuario.Produto_add;
            // btnEditarUser.Enabled = Program._permissaoUsuario.Produto_add;

            LoadProductDB();
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
                if (string.IsNullOrWhiteSpace(txtPartNumber.Text)) { btnPaintBorder_Click(txtPartNumber); return; }
                if (string.IsNullOrWhiteSpace(txtPesoAlvo.Text)) { btnPaintBorder_Click(txtPesoAlvo); return; }
                if (string.IsNullOrWhiteSpace(txtTolerancia.Text)) { btnPaintBorder_Click(txtTolerancia); return; }
                if (string.IsNullOrWhiteSpace(txtDescricao.Text)) { btnPaintBorder_Click(txtDescricao); return; }
                if (string.IsNullOrWhiteSpace(txtPtnCliente.Text)) { btnPaintBorder_Click(txtPtnCliente); return; }
                if (string.IsNullOrWhiteSpace(txtCodigoBarra.Text)) { btnPaintBorder_Click(txtCodigoBarra); return; }
                this.Refresh();

                using (MemoryStream ms = new MemoryStream())
                {
                    pcbImage.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    byte[] imagemBytes = ms.ToArray();

                    if (Program.SQL.CRUDCommand("INSERT INTO Produto " +
                        "(part_number, descricao, Peso_alvo, Tolerancia,Foto, dateinsert,part_number_cliente, CodigoEarn) " +
                        "VALUES" +
                        " (@part_number, @descricao, @Peso_alvo, @Tolerancia, @Foto, @dateinsert,@part_number_cliente, @CodigoEarn)",
                        "Produto", new Dictionary<string, object>()
                        {
                            {"@part_number", txtPartNumber.Text },
                            {"@descricao", txtDescricao.Text },
                            {"@part_number_cliente", txtPtnCliente.Text },
                            {"@CodigoEarn", Convert.ToString(txtCodigoBarra.Text) },
                            {"@Peso_alvo", Convert.ToSingle(txtPesoAlvo.Text) },
                            {"@Tolerancia", Convert.ToSingle(txtTolerancia.Text) },
                            {"@Foto",  Convert.ToBase64String(imagemBytes)},
                            {"@dateinsert", DateTime.Now}}))
                    {
                        btnNovoUser_Click(sender, e);
                        LoadProductDB();
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
                Pen pen = new Pen(borderColor,1); // Largura da borda = 2
                Rectangle rect = new Rectangle(0, 0, item.Width - 1, item.Height - 1);
                g.DrawRectangle(pen, rect);
            }
        }

        private void btnNovoUser_Click(object sender, EventArgs e)
        {
            try
            {
                pcbImage.Image = Resources.NoImage;
                txtPartNumber.Text = "";
                txtPesoAlvo.Text = "";
                txtTolerancia.Text = "";
                txtDescricao.Text = "";
                txtPtnCliente.Text = "";
                btnEditarUser.Visible = false;
                btnNovoUser.Visible = false;
                btnSalvarUser.Visible = true;
                txtCodigoBarra.Text = "";
                LoadProductDB();
            }
            catch (Exception)
            {
            }
        }

        public void LoadProductDB()
        {
            try
            {
                dgv_dados.DataSource = Program.SQL.SelectDataGrid("SELECT Id, part_number AS [Part Number], descricao AS [Descrição], Peso_alvo AS [Peso Alvo], Tolerancia AS [Tolerância], part_number_cliente AS [Part Number Cliente], CodigoEarn AS [Código EARN]  FROM Produto", "Produto");
                dgv_dados.Columns[2].Visible = false;
            }
            catch (Exception)
            {
            }
        }

        private void dgv_dados_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //try
            //{
            //    if (e.ColumnIndex == 0)
            //    {
            //        ProdutoClass produto = (ProdutoClass)Program.SQL.SelectObject("SELECT id, part_number, descricao,Peso_alvo, Tolerancia,Foto, dateinsert, part_number_cliente, CodigoEarn  FROM Produto WHERE Id = @Id", "Produto", new Dictionary<string, object>()
            //        {
            //            {"@Id", dgv_dados.CurrentRow.Cells["Id"].Value}
            //        });

            //        if (produto.Foto.Length > 0)
            //        {
            //            using (MemoryStream ms = new MemoryStream(produto.Foto))
            //            {
            //                pcbImage.Image = System.Drawing.Image.FromStream(ms);
            //            }
            //        }

            //        btnEditarUser.Visible = true;
            //        btnSalvarUser.Visible = false;
            //        btnNovoUser.Visible = true;

            //        txtPartNumber.Text = produto.Part_number;
            //        txtPesoAlvo.Text = produto.PesoAlvo.ToString();
            //        txtTolerancia.Text = produto.Tolerancia.ToString();
            //        txtDescricao.Text = produto.Descricao;
            //        txtPtnCliente.Text = produto.part_number_cliente;
            //        txtCodigoBarra.Text = produto.CodigoEarn;

            //    }

            //    if (e.ColumnIndex == 1)
            //    {

            //        var list_receitas = Program.SQL.SelectList("SELECT * FROM Receita Where id_produto = @id_produto", "Receita",
            //            null, new Dictionary<string, object>()
            //            {
            //                {"@id_produto", dgv_dados.CurrentRow.Cells["Id"].Value}
            //            });

            //        if (list_receitas.Count <= 0 || Program._usuarioLogado.Acesso == "Administrador")
            //        {
            //            YesOrNo question = new YesOrNo("Você tem certeza que deseja remover o produto selecionado ?");
            //            question.ShowDialog();

            //            if (question.RESPOSTA && Program._permissaoUsuario.Produto_remove)
            //            {
            //                if (Program.SQL.CRUDCommand("DELETE FROM Produto WHERE Id = @Id", "Produto", new Dictionary<string, object>() { { "@Id", dgv_dados.CurrentRow.Cells["Id"].Value } }))
            //                {
            //                    btnNovoUser_Click(new object(), new EventArgs());
            //                }
            //            }
            //        }
            //        else
            //        {
            //            InfoPopup question = new InfoPopup("Produto com movimentações encontradas.", "Não é possível remover um produto vinculado a uma Receita em execução ou já executada.");
            //            question.ShowDialog();
            //        }
            //    }
            //}
            //catch (Exception)
            //{
            //}
        }

        private void btnEditarUser_Click(object sender, EventArgs e)
        {
            try
            {
                {
                    if (string.IsNullOrWhiteSpace(txtPartNumber.Text)) { btnPaintBorder_Click(txtPartNumber); return; }
                    if (string.IsNullOrWhiteSpace(txtPesoAlvo.Text)) { btnPaintBorder_Click(txtPesoAlvo); return; }
                    if (string.IsNullOrWhiteSpace(txtTolerancia.Text)) { btnPaintBorder_Click(txtTolerancia); return; }
                    if (string.IsNullOrWhiteSpace(txtDescricao.Text)) { btnPaintBorder_Click(txtDescricao); return; }
                    if (string.IsNullOrWhiteSpace(txtCodigoBarra.Text)) { btnPaintBorder_Click(txtCodigoBarra); return; }
                    this.Refresh();

                    using (MemoryStream ms = new MemoryStream())
                    {
                        pcbImage.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                        byte[] imagemBytes = ms.ToArray();

                        if (Program.SQL.CRUDCommand("UPDATE Produto SET " +
                            "part_number = @part_number, descricao = @descricao, Peso_alvo = @Peso_alvo, Tolerancia = @Tolerancia,Foto = @Foto, part_number_cliente = @part_number_cliente, CodigoEarn = @CodigoEarn " +
                            "WHERE Id = @Id",
                            "Usuario", new Dictionary<string, object>()
                            {
                                {"@Id", dgv_dados.CurrentRow.Cells["Id"].Value},
                                {"@part_number", txtPartNumber.Text },
                                {"@part_number_cliente", txtPtnCliente.Text },
                                {"@descricao", txtDescricao.Text },
                                {"@Peso_alvo", Convert.ToSingle(txtPesoAlvo.Text) },
                                {"@Tolerancia", Convert.ToSingle(txtTolerancia.Text) },
                                {"@CodigoEarn", Convert.ToString(txtCodigoBarra.Text) },
                                {"@Foto",  Convert.ToBase64String(imagemBytes)},
                                {"@dateinsert", DateTime.Now}}))
                        {
                            btnNovoUser_Click(sender, e);
                            LoadProductDB();
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
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Panel pn = (Panel)sender;
            System.Drawing.Color COLOR_BORDER = System.Drawing.Color.FromArgb(237, 237, 237);
            int borderSize = 2;
            ControlPaint.DrawBorder(e.Graphics, pn.ClientRectangle, COLOR_BORDER, borderSize, ButtonBorderStyle.Solid, COLOR_BORDER, borderSize, ButtonBorderStyle.Solid, COLOR_BORDER, borderSize, ButtonBorderStyle.Solid, COLOR_BORDER, borderSize, ButtonBorderStyle.Solid);
        }

        private void CadastroProdutoForms_Resize(object sender, EventArgs e)
        {
            this.Update();
            this.Refresh();
        }

        private void txtPartNumber_Leave(object sender, EventArgs e)
        {
            try
            {
                //ProdutoClass prod = (ProdutoClass)Program.SQL.SelectObject("SELECT * FROM Produto WHERE part_number = @part_number", "Produto",
                //    new Dictionary<string, object>() 
                //    {
                //        {"@part_number", txtPartNumber.Text}
                //    });

                //if (prod != null) 
                //{
                //    txtPartNumber.Text = "";
                //}

            }
            catch (Exception)
            {
            }
        }

        private void txtTolerancia_TextChanged(object sender, EventArgs e)
        {
            try
            {
                decimal value = Convert.ToDecimal(txtTolerancia.Text);
                if (value > 20) 
                {
                    txtTolerancia.Text = " ";
                }
            }
            catch (Exception)
            {
            }
        }
    }
}

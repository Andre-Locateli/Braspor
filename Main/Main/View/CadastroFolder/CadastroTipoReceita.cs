using Main.Model;
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

namespace Main.View.CadastroFolder
{
    public partial class CadastroTipoReceita : Form
    {
        public CadastroTipoReceita()
        {
            InitializeComponent();

            lbl_time.Text = $"{DateTime.Now.Day.ToString("D2")}/{DateTime.Now.Month.ToString("D2")}/{DateTime.Now.Year}";
            lblUsuario.Text = Program._usuarioLogado.Nome;
            lblAcesso.Text = Program._usuarioLogado.Acesso;

            LoadTipoRecipiente();

            btnEditarUser.Enabled = Program._permissaoUsuario.tipoReceita_edit;
            btnSalvarUser.Enabled = Program._permissaoUsuario.tipoReceita_add;
        }

        public void LoadTipoRecipiente()
        {
            try
            {
                dgv_dados.DataSource = Program.SQL.SelectDataGrid("SELECT Id, Tipo_item [Tipo Receita] FROM tipoReceita", "tipoReceita");
                dgv_dados.Columns[2].Visible = false;
            }
            catch (Exception)
            {
            }
        }

        private void btnEditarUser_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtTipo.Text)) { btnPaintBorder_Click(txtTipo); return; }

                this.Refresh();

                if (Program.SQL.CRUDCommand("UPDATE tipoReceita SET " +
                    "Tipo_item = @Tipo_item " +
                    "WHERE Id = @Id",
                    "Usuario", new Dictionary<string, object>()
                    {
                        {"@Id", dgv_dados.CurrentRow.Cells["Id"].Value},
                        {"@Tipo_item", txtTipo.Text } 
                    }))
                {
                    btnNovoUser_Click(sender, e);
                    LoadTipoRecipiente();
                }
                else
                {
                    MessageBox.Show("Erro ao adicionar item");
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
                if (string.IsNullOrWhiteSpace(txtTipo.Text)) { btnPaintBorder_Click(txtTipo); return; }

                this.Refresh();

                if (Program.SQL.CRUDCommand("INSERT INTO tipoReceita " +
                    "(Tipo_item, dateinsert) " +
                    "VALUES" +
                    " (@Tipo_item, @dateinsert)",
                    "Recipiente", new Dictionary<string, object>()
                    {
                        {"@Tipo_item", txtTipo.Text },
                        {"@dateinsert", DateTime.Now}}))
                {
                    btnNovoUser_Click(sender, e);
                    LoadTipoRecipiente();
                }
                else
                {
                    MessageBox.Show("Erro ao adicionar item");
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
                try
                {
                    btnEditarUser.Visible = false;
                    btnNovoUser.Visible = false;
                    btnSalvarUser.Visible = true;
                    txtTipo.Text = "";
                    LoadTipoRecipiente();
                }
                catch (Exception)
                {
                }
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
                    TipoReceitaClass recipiente = (TipoReceitaClass)Program.SQL.SelectObject("SELECT Id, Tipo_item, dateinsert FROM tipoReceita WHERE Id = @Id", "tipoReceita", new Dictionary<string, object>()
                    {
                        {"@Id", dgv_dados.CurrentRow.Cells["Id"].Value}
                    });

                    btnEditarUser.Visible = true;
                    btnSalvarUser.Visible = false;
                    btnNovoUser.Visible = true;

                    txtTipo.Text = recipiente.TipoItem;

                }

                if (e.ColumnIndex == 1)
                {
                    YesOrNo question = new YesOrNo("Você tem certeza que deseja remover o tipo de receita selecionado ?");
                    question.ShowDialog();
                    if (question.RESPOSTA && Program._permissaoUsuario.tipoReceita_remove)
                    {
                        if (Program.SQL.CRUDCommand("DELETE FROM tipoReceita WHERE Id = @Id", "tipoReceita", new Dictionary<string, object>() { { "@Id", dgv_dados.CurrentRow.Cells["Id"].Value } }))
                        {
                            btnNovoUser_Click(new object(), new EventArgs());
                        }
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

        private void txt_search_TextChanged(object sender, EventArgs e)
        {
            try
            {
                var ds = new DataSet();
                DataTable dt;
                dt = (DataTable)dgv_dados.DataSource;
                dt.DefaultView.RowFilter = string.Format("[{0}] LIKE '%" + txt_search.Text + "%'", "Tipo Receita");
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
        private void CadastroTipoReceita_Resize(object sender, EventArgs e)
        {
            this.Update();
            this.Refresh();
        }
    }
}

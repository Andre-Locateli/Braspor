using Main.Model;
using Main.View.CustomLayout;
using Main.View.MainFolder;
using Main.View.PopupFolder;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Forms;

namespace Main.View.CadastroFolder
{
    public partial class CadastroReceitaForms : Form
    {

        public event EventHandler ItemSelecionadoTrigger;

        private string _itemSelecionado;
        public string ItemSelecionado
        {
            get { return _itemSelecionado; }
            set
            {
                if (_itemSelecionado != value)
                {
                    _itemSelecionado = value;
                    if (ItemSelecionado != null)
                    {
                        if (ItemSelecionadoTrigger != null)
                        {
                            ItemSelecionadoTrigger(this, EventArgs.Empty);
                        }
                    }
                }
            }
        }

        public event EventHandler ItemEditadoTrigger;

        private ReceitaClass _itemEditado;
        public ReceitaClass ItemEditado
        {
            get { return _itemEditado; }
            set
            {
                if (_itemEditado != value)
                {
                    _itemEditado = value;
                    if (ItemEditado != null)
                    {
                        if (ItemEditadoTrigger != null)
                        {
                            ItemEditadoTrigger(this, EventArgs.Empty);
                        }
                    }
                }
            }
        }

        private int minParam = 0;
        private int maxParam = 10;
        private double qRows = 0;
        private double drows = 0;

        private int counter_press = 1;

        public CadastroReceitaForms()
        {

            InitializeComponent();
            lbl_time.Text = $"{DateTime.Now.Day.ToString("D2")}/{DateTime.Now.Month.ToString("D2")}/{DateTime.Now.Year}";
            label1.Text = Program._usuarioLogado.Nome;
            lblAcesso.Text = Program._usuarioLogado.Acesso;
            LoadDatabaseInfo();

            button1.Enabled = Program._permissaoUsuario.receita_add;


        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Panel pn = (Panel)sender;
            System.Drawing.Color COLOR_BORDER = System.Drawing.Color.FromArgb(237, 237, 237);
            int borderSize = 2;
            ControlPaint.DrawBorder(e.Graphics, pn.ClientRectangle, COLOR_BORDER, borderSize, ButtonBorderStyle.Solid, COLOR_BORDER, borderSize, ButtonBorderStyle.Solid, COLOR_BORDER, borderSize, ButtonBorderStyle.Solid, COLOR_BORDER, borderSize, ButtonBorderStyle.Solid);
        }

        private void btnCadastro_Click(object sender, EventArgs e)
        {
            try
            {
                CadastroNovaReceita nova = new CadastroNovaReceita();
                nova.ShowDialog();
            }
            catch (Exception)
            {
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void LoadDatabaseInfo()
        {
            var temp = Program.SQL.SelectList("SELECT * FROM Receita", "Receita",null,new Dictionary<string, object>() {});

            qRows = temp.Count;
            drows = qRows / 10;
            quantityPage.Text = $"{Math.Ceiling(qRows / 10)}";

            dgvDados.DataSource = Program.SQL.SelectDataGrid("SELECT Id, Nome, Codigo AS [Código], Quantidade_pecas AS [Quant.Peças], Quantidade_bandejas AS [Quant.Bandejas] FROM ( SELECT *, ROW_NUMBER() OVER (ORDER BY Id) AS row FROM Receita) temp WHERE row >= @minParam AND row <= @maxParam", "Receita",
                new Dictionary<string, object>() 
                {
                    {"@minParam", minParam},
                    {"@maxParam", maxParam}
                });

            dgvDados.Columns["edit"].DisplayIndex = 7;
            dgvDados.Columns["remove"].DisplayIndex = 7;
            dgvDados.Columns[3].Visible = false;
        }

        private void pcb_next_Click(object sender, EventArgs e)
        {
            if (minParam + 10 < qRows)
            {
                maxParam += 10;
                minParam += 10;
                counter_press += 1;
                ReorderSequence();
                LoadDatabaseInfo();
            }
        }

        private void pcb_return_Click(object sender, EventArgs e)
        {
            if (minParam - 10 >= 0)
            {
                maxParam -= 10;
                minParam -= 10;
                counter_press -= 1;
                ReorderSequence();
                LoadDatabaseInfo();
            }
        }

        private void ReorderSequence() 
        {
            try
            {
                if (counter_press <= 3)
                {
                    btnthree.Text = $"{3}";
                }
                else 
                {
                    btnthree.Text = $"{counter_press}";
                }
            }
            catch (Exception)
            {
            }
        }

        private void btnOne_Click(object sender, EventArgs e)
        {
            try
            {
                Button cbtn = (Button)sender;

                if (((Convert.ToInt32(cbtn.Text) - 1) * 10) <= qRows)
                {
                    maxParam = Convert.ToInt32(cbtn.Text) * 10;
                    minParam = (Convert.ToInt32(cbtn.Text) * 10) - 10;

                    if (((Convert.ToInt32(cbtn.Text) - 1) * 10) < qRows)
                    {
                        counter_press = Convert.ToInt32(cbtn.Text);
                    }
                    ReorderSequence();
                    LoadDatabaseInfo();
                }

            }
            catch (Exception)
            {
            }
        }

        private void txtSearch_Click(object sender, EventArgs e)
        {
            if(txtSearch.Text == "  Pesquisar")
            {
                txtSearch.Text = "";
                txtSearch.ForeColor = Color.FromArgb(100, 111, 114, 113);
            }
        }

        private void txtSearch_Leave(object sender, EventArgs e)
        {
            if (txtSearch.Text == "")
            {
                txtSearch.Text = "  Pesquisar";
                txtSearch.ForeColor = Color.FromArgb(100, 223, 223, 223);
            }
        }

        private void CadastroReceitaForms_Resize(object sender, EventArgs e)
        {
            this.Update();
            this.Refresh();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                ItemSelecionado = "FOI DELE";
            }
            catch (Exception)
            {

            }
        }

        private void dgvDados_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

        }


        private void dgvDados_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 1)
                {
                    //Editar

                    //dgvDados.Columns[3].Visible = false;
                    var rec = dgvDados.CurrentRow.Cells["Id"].Value;
                    ReceitaClass edit = (ReceitaClass)Program.SQL.SelectObject("SELECT * FROM Receita WHERE Id = @Id", "Receita", new Dictionary<string, object>() { 
                        { "@Id", dgvDados.CurrentRow.Cells["Id"].Value } 
                    });

                    ItemEditado = edit;
                }

                if (e.ColumnIndex == 2)
                {
                    //Remover
                    var list_receitas = Program.SQL.SelectList("SELECT * FROM LogReceita Where id_receita = @id_receita", "LogReceita",
                        null, new Dictionary<string, object>()
                        {
                            {"@id_receita", dgvDados.CurrentRow.Cells["Id"].Value}
                        });
                    if (list_receitas.Count <= 0 || Program._usuarioLogado.Acesso == "Administrador")  
                    {
                        YesOrNo question = new YesOrNo("Você tem certeza que deseja remover a receita selecionada ?");
                        question.ShowDialog();

                        if (question.RESPOSTA && Program._permissaoUsuario.receita_remove)
                        {
                            if (Program.SQL.CRUDCommand("DELETE FROM Receita WHERE Id = @Id", "Receita", new Dictionary<string, object>() { { "@Id", dgvDados.CurrentRow.Cells["Id"].Value } }))
                            {
                                LoadDatabaseInfo();
                            }
                        }
                    }
                    else
                    {
                        InfoPopup question = new InfoPopup("Receita com movimentações encontradas.", "Não é possível remover uma receita vinculado a uma Receita em execução ou já executada.");
                        question.ShowDialog();
                    }

                }
            }
            catch (Exception)
            {
            }
        }


    }
}

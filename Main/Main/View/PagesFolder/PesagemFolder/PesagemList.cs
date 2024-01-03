using Main.Model;
using Main.View.PopupFolder;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Main.View.PagesFolder.PesagemFolder
{
    //
    public partial class PesagemList : Form
    {
        private void pcb_next_Click(object sender, EventArgs e)
        {

        }
        //public event EventHandler ItemEditadoTrigger;

        //private ReceitaClass _itemEditado;
        //public ReceitaClass ItemEditado
        //{
        //    get { return _itemEditado; }
        //    set
        //    {
        //        if (_itemEditado != value)
        //        {
        //            _itemEditado = value;
        //            if (ItemEditado != null)
        //            {
        //                if (ItemEditadoTrigger != null)
        //                {
        //                    ItemEditadoTrigger(this, EventArgs.Empty);
        //                }
        //            }
        //        }
        //    }
        //}

        //private ReceitaLogClass _logReceita;
        //public ReceitaLogClass LogReceita
        //{
        //    get { return _logReceita; }
        //    set { _logReceita = value; }
        //}

        //public PesagemList()
        //{
        //    InitializeComponent();

        //    lbl_time.Text = $"{DateTime.Now.Day.ToString("D2")}/{DateTime.Now.Month.ToString("D2")}/{DateTime.Now.Year}";
        //    lblUsuario.Text = Program._usuarioLogado.Nome;
        //    lblAcesso.Text = Program._usuarioLogado.Acesso;


        //    LoadDatabaseInfo();
        //}

        //private int minParam = 0;
        //private int maxParam = 10;
        //private double qRows = 0;
        //private double drows = 0;

        //private int counter_press = 1;

        //private void LoadDatabaseInfo()
        //{
        //    var temp = Program.SQL.SelectList("SELECT Id, Nome, Codigo, Qtd_Pecas AS [Quantidade_pecas], Qtd_Bandeja AS [Quantidade_bandejas], dateinsert AS Data, Status FROM LogReceita", "CustomReceitaInfo", null, new Dictionary<string, object>() { });

        //    qRows = temp.Count;
        //    drows = qRows / 10;
        //    quantityPage.Text = $"{Math.Ceiling(qRows / 10)}";
        //    //double aux_minParam = temp.Count - minParam;
        //    //double aux_maxParam = temp.Count - maxParam;

        //    double aux_minParam =  minParam;
        //    double aux_maxParam =  maxParam;

        //    var items = Program.SQL.SelectList("SELECT Id, Nome, Codigo, Qtd_Pecas AS [Quantidade_pecas], Qtd_Bandeja AS [Quantidade_bandejas], dateinsert AS Data, Status FROM ( SELECT *, ROW_NUMBER() OVER (ORDER BY Status ASC) AS row FROM LogReceita AS LReceita ) temp WHERE row >= @minParam AND row <= @maxParam;", "CustomReceitaInfo", null,
        //        new Dictionary<string, object>()
        //        {
        //            {"@minParam", aux_minParam},
        //            {"@maxParam", aux_maxParam}
        //        });

        //    BindingList<CustomReceitaInfo> lista = new BindingList<CustomReceitaInfo>();

        //    foreach (CustomReceitaInfo elemento in items) { lista.Add(elemento); }

        //    dgvDados.DataSource = lista;

        //    dgvDados.Columns[1].Visible = false;
        //    dgvDados.Columns[6].Visible = false;
        //    dgvDados.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

        //}

        //private void pcb_next_Click(object sender, EventArgs e)
        //{
        //    if(minParam + 10 < qRows)
        //    {
        //        maxParam += 10;
        //        minParam += 10;
        //        counter_press += 1;
        //        ReorderSequence();
        //        LoadDatabaseInfo();
        //    }
        //}

        //private void pcb_return_Click(object sender, EventArgs e)
        //{
        //    if (minParam - 10 >= 0)
        //    {
        //        maxParam -= 10;
        //        minParam -= 10;
        //        counter_press -= 1;
        //        ReorderSequence();
        //        LoadDatabaseInfo();
        //    }
        //}

        //private void ReorderSequence()
        //{
        //    try
        //    {

        //        if (counter_press <= 3)
        //        {
        //            btnthree.Text = $"{3}";
        //        }
        //        else
        //        {
        //            btnthree.Text = $"{counter_press}";
        //        }



        //    }
        //    catch (Exception)
        //    {
        //    }
        //}

        //private void btnOne_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        Button cbtn = (Button)sender;

        //        if (((Convert.ToInt32(cbtn.Text)-1) * 10) <= qRows)
        //        {
        //            maxParam = Convert.ToInt32(cbtn.Text) * 10;
        //            minParam = (Convert.ToInt32(cbtn.Text) * 10) - 10;

        //            if (((Convert.ToInt32(cbtn.Text)-1) * 10) < qRows)
        //            {
        //                counter_press = Convert.ToInt32(cbtn.Text);
        //            }
        //            ReorderSequence();
        //            LoadDatabaseInfo();
        //        }

        //    }
        //    catch (Exception)
        //    {
        //    }
        //}

        //private void btnExecutar_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        PopupLeitura pop = new PopupLeitura();
        //        pop.ShowDialog();
        //        if (pop.RESPONSE > 0) 
        //        {
        //            ReceitaLogClass receitaLOg = (ReceitaLogClass)Program.SQL.SelectObject("SELECT * FROM LogReceita WHERE id = @id", "LogReceita", new Dictionary<string, object>()
        //            {
        //                {"@Id", pop.RESPONSE}
        //            });

        //            if (receitaLOg != null) 
        //            {
        //                ReceitaClass receita = (ReceitaClass)Program.SQL.SelectObject("SELECT * FROM Receita WHERE id = @id", "Receita", new Dictionary<string, object>()
        //                {
        //                    {"@Id", receitaLOg.id_receita}
        //                });

        //                if (receita != null) 
        //                {
        //                    LogReceita = receitaLOg;
        //                    ItemEditado = receita;                            
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception)
        //    {
        //    }
        //}

        //private void dgvDados_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    try
        //    {
        //        var status = dgvDados.Rows[e.RowIndex].Cells["Status"].Value;

        //        if (Convert.ToInt32(status) != 2)
        //        {
        //            ReceitaLogClass receitaLOg = (ReceitaLogClass)Program.SQL.SelectObject("SELECT * FROM LogReceita WHERE id = @id", "LogReceita", new Dictionary<string, object>()
        //            {
        //                {"@Id",   dgvDados.Rows[e.RowIndex].Cells["Id"].Value}
        //            });

        //            if (receitaLOg != null)
        //            {
        //                ReceitaClass receita = (ReceitaClass)Program.SQL.SelectObject("SELECT * FROM Receita WHERE id = @id", "Receita", new Dictionary<string, object>()
        //                {
        //                    {"@Id", receitaLOg.id_receita}
        //                });

        //                if (receita != null)
        //                {
        //                    LogReceita = receitaLOg;
        //                    ItemEditado = receita;
        //                }
        //            }
        //        }
        //        else 
        //        {
        //            //
        //        }


        //    }
        //    catch (Exception)
        //    {

        //    }
        //}

        //private void excluirToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        DataGridViewRow linha_selecionada = dgvDados.SelectedRows[0];
        //        Console.WriteLine(linha_selecionada.Cells["Nome"].Value);
        //        int iStatus = Convert.ToInt16(linha_selecionada.Cells["Status"].Value);
        //        if (iStatus >=0 && iStatus <2)
        //        {
        //            YesOrNo question = new YesOrNo("Tem certeza que deseja remover a Receita selecionada ?\n\r O registro será excluido da base de dados e essa ação é irreversível.");
        //            question.ShowDialog();
        //            if (question.RESPOSTA) 
        //            {
        //                bool bDeleteCRUD = Program.SQL.CRUDCommand("DELETE FROM LogReceita WHERE id = @id", "LogReceita",
        //                    new Dictionary<string, object>() { { "@id", linha_selecionada.Cells["Id"].Value.ToString() } });

        //                if (bDeleteCRUD)  
        //                {
        //                    LoadDatabaseInfo();
        //                }

        //            }
        //        }

        //    }
        //    catch (Exception)
        //    {
        //    }
        //}

        //private void metroContextMenu1_Opening(object sender, CancelEventArgs e)
        //{
        //    try
        //    {
        //        Point mousePos = dgvDados.PointToClient(Cursor.Position);

        //        int rowIndex = dgvDados.HitTest(mousePos.X, mousePos.Y).RowIndex;

        //        if (rowIndex >= 0)
        //        {
        //            dgvDados.ClearSelection(); 
        //            dgvDados.Rows[rowIndex].Selected = true;
        //        }
        //        else { e.Cancel = true; }
        //    }
        //    catch (Exception)
        //    {
        //    }

        //}

        //private void dgvDados_CellContextMenuStripChanged(object sender, DataGridViewCellEventArgs e)
        //{

        //}

    }
}

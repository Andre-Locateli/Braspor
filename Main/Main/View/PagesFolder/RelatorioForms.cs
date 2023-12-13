using Main.Helper;
using Main.Model;
using Main.View.PopupFolder;
using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media;

namespace Main.View.PagesFolder
{
    public partial class RelatorioForms : Form
    {
        private string where_condition = "";
        private Dictionary<string, object> where_parameter = new Dictionary<string, object>();

        public RelatorioForms()
        {
            InitializeComponent();

            lbl_time.Text = $"{DateTime.Now.Day.ToString("D2")}/{DateTime.Now.Month.ToString("D2")}/{DateTime.Now.Year}";
            lblUsuario.Text = Program._usuarioLogado.Nome;
            lblAcesso.Text = Program._usuarioLogado.Acesso;

            LoadComboBox(cbReceita, "SELECT DISTINCT Nome, * FROM Receita", "Receita", new Dictionary<string, object>() { }, "Nome");
            LoadComboBox(cbProduto, "SELECT DISTINCT part_number, * FROM Produto", "Produto", new Dictionary<string, object>() { }, "part_number");

            where_condition = "";
            where_parameter.Clear();

            string day = DateTime.Now.Day.ToString().Length == 2 ? DateTime.Now.Day.ToString() : $"0{DateTime.Now.Day.ToString()}";
            string month = DateTime.Now.Month.ToString().Length == 2 ? DateTime.Now.Month.ToString() : $"0{DateTime.Now.Month.ToString()}";

            mtxtDate.Text = $"{day}/{month}/{DateTime.Now.Year}";
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            StyleSheet.panel_Paint(sender, e);
        }

        private void dataGridView1_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            try
            {
                if (e.RowIndex % 2 == 0)
                {
                    //dgv_dados.Rows[e.RowIndex].DefaultCellStyle.BackColor = System.Drawing.Color.White;
                }
                else
                {
                    //dgv_dados.Rows[e.RowIndex].DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(41, 46, 84);
                }
            }
            catch (Exception)
            {
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (dgv_dados.SelectedRows.Count > 0)
                {
                    dgv_dados.SelectedRows[0].Selected = false;
                }
            }
            catch (Exception)
            {
            }
        }

        public void LoadComboBox(ComboBox cb, string consulta, string tabela, Dictionary<string, object> parameter, string parameter_name)
        {
            try
            {
                List<object> items = Program.SQL.SelectList(consulta, tabela, null, parameter);

                cb.DataSource = items;
                cb.DisplayMember = parameter_name;
                cb.SelectedItem = null;
            }
            catch (Exception)
            {
            }
        }

        private void cbReceita_SelectedIndexChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    ComboBox cb = (ComboBox)sender;
            //    string valor = "";

            //    if (cb.Tag.ToString() == "Receita")
            //    {
            //        ReceitaClass receita = (ReceitaClass)cb.SelectedItem;
            //        if (receita == null) { return; }
            //        if (where_condition.Length == 0) { where_condition += $" WHERE Nome = @Nome"; where_parameter.Add("@Nome", receita.Nome); }
            //        else
            //        {
            //            if (where_condition.Contains("@Nome"))
            //            {
            //                where_parameter["@Nome"] = receita.Nome;
            //            }
            //            else
            //            {
            //                where_condition += $" AND Nome = @Nome ";
            //                where_parameter["@Nome"] = receita.Nome;
            //            }
            //        }
            //    }
            //    if (cb.Tag.ToString() == "Produto")
            //    {
            //        ProdutoClass produto = (ProdutoClass)cb.SelectedItem;
            //        if (produto == null) { return; }
            //        if (where_condition.Length == 0) { where_condition += $" WHERE id_Produto = @id_Produto "; where_parameter.Add("@id_Produto", produto.Id); }
            //        else
            //        {
            //            if (where_condition.Contains("@id_Produto"))
            //            {
            //                where_parameter["@id_Produto"] = produto.Id;
            //            }
            //            else
            //            {
            //                where_condition += $" AND id_Produto = @id_Produto ";
            //                where_parameter["@id_Produto"] = produto.Id;
            //            }
            //        }
            //    }

            //    dgv_dados.DataSource = Program.SQL.SelectDataGrid("SELECT Id, Nome, Codigo," +
            //        "Qtd_Pecas_Pesado AS [QTD Peças], " +
            //        "Peso_Pecas AS [Peças Peso], " +
            //        "Peso_Pecas_Pesado AS [Peso Peças Final], " +
            //        "Estacao, " +
            //        "Operador," +
            //        "datefim AS [Data Fim] FROM LogReceita " +
            //        $"{where_condition}", "LogReceita",
            //        where_parameter);

            //    dgv_dados.Columns[1].Visible = false;
            //}
            //catch (Exception)
            //{
            //    where_condition = "";
            //    where_parameter.Clear();
            //}
        }

        private void dgv_dados_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //try
            //{
            //    if (e.ColumnIndex == 0) 
            //    {
            //        //Exportar relatório desse Log.

            //        ReceitaLogClass receita_log = (ReceitaLogClass)Program.SQL.SelectObject("SELECT * FROM LogReceita WHERE Id = @Id", "LogReceita", new Dictionary<string, object>()
            //        {
            //            {"@Id", dgv_dados.CurrentRow.Cells["Id"].Value}
            //        });

            //        if (receita_log != null) 
            //        {

            //            ReceitaClass receita = (ReceitaClass)Program.SQL.SelectObject("SELECT * FROM Receita WHERE Id = @Id", "Receita", new Dictionary<string, object>()
            //            {
            //                {"@Id", receita_log.id_receita}
            //            });

            //            ProdutoClass produto = (ProdutoClass)Program.SQL.SelectObject("SELECT * FROM Produto WHERE id = @id", "Produto", new Dictionary<string, object>()
            //            {
            //                {"@id", receita_log.id_Produto}
            //            });

            //            if (receita_log.Id != 0 && receita.Id != 0 && produto.Id != 0)
            //            {
            //                Microsoft.Office.Interop.Excel.Application xlexcel = new Microsoft.Office.Interop.Excel.Application();
            //                Microsoft.Office.Interop.Excel.Workbook xlWorkBook = xlexcel.Workbooks.Add(AppDomain.CurrentDomain.BaseDirectory + "modelo.xlsx");
            //                Microsoft.Office.Interop.Excel.Worksheet xlWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
            //                Microsoft.Office.Interop.Excel.Worksheet xlWorkSheet2 = xlWorkBook.Worksheets.get_Item(2);
            //                object misValue = System.Reflection.Missing.Value;

            //                xlWorkSheet.Activate();
            //                xlexcel.ActiveSheet.Range("A6").Value = produto.Part_number;
            //                xlexcel.ActiveSheet.Range("D6").Value = produto.part_number_cliente;
            //                xlexcel.ActiveSheet.Range("J6").Value = receita.PKSKF;
            //                xlexcel.ActiveSheet.Range("A8").Value = DateTime.Now;
            //                xlexcel.ActiveSheet.Range("D8").Value = Convert.ToString(receita_log.Peso_Bandejas_Pesado + receita_log.Peso_Recipiente_Pesado + receita_log.Peso_Pecas_Pesado) + "kg";
            //                var seq_list = Program.SQL.SelectList("SELECT * FROM LogReceita WHERE id_receita = @id_receita", "LogReceita",null, new Dictionary<string, object>()
            //                {
            //                    {"@id_receita", receita_log.id_receita}
            //                });
            //                xlexcel.ActiveSheet.Range("J8").Value = "Nº " + (seq_list.Count + 1).ToString(); //Como pegar a sequencia

            //                dataGridView1.DataSource = Program.SQL.SelectDataGrid("SELECT Peso_Pecas_Pesado AS [Peso], * FROM LogReceita WHERE id_Produto = @id_Produto " +
            //                    "AND Peso_Pecas_Pesado IS NOT NULL", "LogReceita",
            //                    new Dictionary<string, object>() 
            //                    {
            //                        {"@id_Produto", produto.Id}
            //                    });

            //                copyAlltoClipboard();
            //                xlWorkSheet2.Activate();
            //                Microsoft.Office.Interop.Excel.Range CR = (Microsoft.Office.Interop.Excel.Range)xlWorkSheet2.Cells[1, 1];
            //                CR.Select();
            //                xlWorkSheet2.PasteSpecial(CR, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, true);

            //                Microsoft.Office.Interop.Excel.ChartObjects chartObjects = (Microsoft.Office.Interop.Excel.ChartObjects)xlWorkSheet.ChartObjects();
            //                Microsoft.Office.Interop.Excel.ChartObject chartObject = chartObjects.Add(35, 520, 2300, 780);
            //                Microsoft.Office.Interop.Excel.Chart chart = chartObject.Chart;
            //                Microsoft.Office.Interop.Excel.Range chartRange = xlWorkSheet2.get_Range("A1:A" + dataGridView1.Rows.Count);
            //                chart.SetSourceData(chartRange);
            //                chart.ChartType = Microsoft.Office.Interop.Excel.XlChartType.xlLine;

            //                xlexcel.Visible = true;
            //                xlWorkSheet.Activate();
            //                xlWorkBook = null;
            //                xlWorkSheet = null;
            //                releaseObject(xlexcel);
            //                releaseObject(xlWorkBook);
            //                releaseObject(xlWorkSheet);
            //            }
            //            else 
            //            {
            //                InfoPopup info = new InfoPopup("Erro na exportação","não foi possível puxar todos os dados do banco, verifique a base de dados.");
            //            }
                    
            //        }

         


            //    }
            //}
            //catch (Exception ex)
            //{
            //    InfoPopup InfoError = new InfoPopup("Erro na exportação", "Verifique se existem registros para esse processo selecionado. Caso contrário não é possível exportar os dados.");
            //    InfoError.ShowDialog();
            //}
        }

        private void copyAlltoClipboard()
        {
            dataGridView1.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
            dataGridView1.MultiSelect = true;
            dataGridView1.SelectAll();
            DataObject dataObj = dataGridView1.GetClipboardContent();
            if (dataObj != null)
                Clipboard.SetDataObject(dataObj);
        }

        private void mtxtDate_Leave(object sender, EventArgs e)
        {
            try
            {
                if (mtxtDate.Text.Length >= 10)
                {
                    if (where_condition.Length == 0) { where_condition += $" WHERE CONVERT(DATE, dateinsert) = @dateinsert "; where_parameter.Add("@dateinsert", mtxtDate.Text); }
                    else
                    {
                        if (where_condition.Contains("@dateinsert"))
                        {
                            where_parameter["@dateinsert"] = mtxtDate.Text;
                        }
                        else
                        {
                            where_condition += $" AND CONVERT(DATE, dateinsert) = @dateinsert ";
                            where_parameter["@dateinsert"] = mtxtDate.Text;
                        }
                    }

                    dgv_dados.DataSource = Program.SQL.SelectDataGrid("SELECT Id, Nome, Codigo," +
                        "Qtd_Pecas_Pesado AS [QTD Peças], " +
                        "Peso_Pecas AS [Peças Peso], " +
                        "Peso_Pecas_Pesado AS [Peso Peças Final], " +
                        "Estacao, " +
                        "Operador," +
                        "datefim AS [Data Fim] FROM LogReceita " +
                        $"{where_condition}", "LogReceita",
                        where_parameter);

                    dgv_dados.Columns[1].Visible = false;
                }
            }
            catch (Exception)
            {
            }
        }

        private void panel1_Paint_1(object sender, PaintEventArgs e)
        {
            System.Windows.Forms.Panel pn = (System.Windows.Forms.Panel)sender;
            System.Drawing.Color COLOR_BORDER = System.Drawing.Color.FromArgb(237, 237, 237);
            int borderSize = 2;
            ControlPaint.DrawBorder(e.Graphics, pn.ClientRectangle, COLOR_BORDER, borderSize, ButtonBorderStyle.Solid, COLOR_BORDER, borderSize, ButtonBorderStyle.Solid, COLOR_BORDER, borderSize, ButtonBorderStyle.Solid, COLOR_BORDER, borderSize, ButtonBorderStyle.Solid);
        }

        private void releaseObject(object obj)
        {
            try
            {
                if (obj != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                    obj = null;
                }
            }
            catch (Exception ex)
            {
                obj = null;
            }
            finally
            {
                GC.Collect();
            }

        }

        private void InitSearch() 
        {
            try
            {
                dgv_dados.DataSource = Program.SQL.SelectDataGrid("SELECT Id, Nome, Codigo," +
                    "Qtd_Pecas_Pesado AS [QTD Peças], " +
                    "Peso_Pecas AS [Peças Peso], " +
                    "Peso_Pecas_Pesado AS [Peso Peças Final], " +
                    "Estacao, " +
                    "Operador," +
                    "datefim AS [Data Fim] FROM LogReceita", "LogReceita");

                dgv_dados.Columns[1].Visible = false;
            }
            catch (Exception)
            {
            }
        }

        private void RelatorioForms_Load(object sender, EventArgs e)
        {
            try
            {
                InitSearch();
            }
            catch (Exception)
            {
            }
        }
    }
}

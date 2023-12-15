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
        private Microsoft.Office.Interop.Excel.Application excelApp { get; set; }
        private Microsoft.Office.Interop.Excel.Workbook workbook { get; set; }
        private Microsoft.Office.Interop.Excel.Worksheet worksheet { get; set; }

        private string where_condition = "";
        private Dictionary<string, object> where_parameter = new Dictionary<string, object>();

        public RelatorioForms()
        {
            InitializeComponent();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            StyleSheet.panel_Paint(sender, e);
        }

        private void dataGridView1_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            try
            {
                if (dgv_dados.Rows.Count > 0) 
                {
                    lblNoDATA.Visible = false;
                }
                else 
                {
                    lblNoDATA.Visible = true;
                }

                //if (e.RowIndex % 2 == 0)
                //{
                //    //dgv_dados.Rows[e.RowIndex].DefaultCellStyle.BackColor = System.Drawing.Color.White;
                //}
                //else
                //{
                //    //dgv_dados.Rows[e.RowIndex].DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(41, 46, 84);
                //}
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
            dgv_dados.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            dgv_dados.MultiSelect = true;
            dgv_dados.SelectAll();
            DataObject dataObj = dgv_dados.GetClipboardContent();
            if (dataObj != null)
                Clipboard.SetDataObject(dataObj);
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
                mtxtDateInicio.Text = $"{fixTime(DateTime.Now.Day)}/{fixTime(DateTime.Now.Month)}/{DateTime.Now.Year} 00:00";
                mtxtDateFim.Text = $"{fixTime(DateTime.Now.Day)}/{fixTime(DateTime.Now.Month)}/{DateTime.Now.Year} 23:59";
                LoadComboBox(cbProduto,"SELECT * FROM MateriaPrima", "MateriaPrima", new Dictionary<string, object>() { },
                    "Descricao");

                LoadComboBox(cbUsuario, "SELECT * FROM Usuario", "Usuario", new Dictionary<string, object>() { },
                    "Nome");

                mtxtDateInicio.Tag = label2;
                mtxtDateFim.Tag = label7;
            }
            catch (Exception)
            {
            }
        }

        private string fixTime(int inputdata) 
        {
            try
            {
                if (inputdata.ToString().Length == 1) { return string.Format("0{0}", inputdata); }
                else if(inputdata.ToString().Length == 2) { return inputdata.ToString(); }

                return "";
            }
            catch (Exception ex)
            {
                return "";
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

        private void cbProduto_Leave(object sender, EventArgs e)
        {
            try
            {
                MateriaPrimaClass mtp = (MateriaPrimaClass)Program.SQL.SelectObject("SELECT * FROM MateriaPrima WHERE Descricao = @Descricao", "MateriaPrima",
                    new Dictionary<string, object>() 
                    {
                        {"@Descricao", cbProduto.Text}
                    });

                if (mtp == null) 
                {
                    cbProduto.Text = "";
                    cbProduto.SelectedItem = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cbUsuario_Leave(object sender, EventArgs e)
        {
            try
            {
                UsuarioClass mtp = (UsuarioClass)Program.SQL.SelectObject("SELECT * FROM Usuario WHERE Nome = @Nome", "Usuario",
                    new Dictionary<string, object>()
                    {
                        {"@Nome", cbUsuario.Text}
                    });

                if (mtp == null)
                {
                    cbUsuario.Text = "";
                    cbUsuario.SelectedItem = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void mtxtDate_LeaveEvent(object sender, EventArgs e)
        {
            try
            {
                MaskedTextBox currentTXT = (MaskedTextBox)sender;
                System.Windows.Forms.Label lblRed = (System.Windows.Forms.Label)currentTXT.Tag;

                int qtd = currentTXT.Text.Count(c => ' ' == c);

                if (qtd > 1)
                {
                    lblRed.ForeColor = System.Drawing.Color.Red;
                }
                else 
                {
                    lblRed.ForeColor = System.Drawing.Color.Black;
                }

                //Console.WriteLine($"Quantidade de espaços em branco. {qtd}");

            }
            catch (Exception ex)
            {
            }
        }


        private void btnPaintBorder_Click(object sender)
        {
            System.Drawing.Color borderColor = System.Drawing.Color.Red;
            Control item = (Control)sender;
            using (Graphics g = item.CreateGraphics())
            {
                System.Drawing.Pen pen = new System.Drawing.Pen(borderColor, 1);
                System.Drawing.Rectangle rect = new System.Drawing.Rectangle(0, 0, item.Width - 1, item.Height - 1);
                g.DrawRectangle(pen, rect);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                YesOrNo question = new YesOrNo("Você tem certeza?\nA exportação de todos os dados pode levar alguns instantes.");
                question.ShowDialog();
                if (question.RESPOSTA)
                {
                    dgv_dados.DataSource = Program.SQL.SelectDataGrid("SELECT M.Descricao AS 'Matéria Prima', U.Nome AS 'Nome Operador', P.Descricao AS 'Descrição Produto', P.Tempo_execucao AS 'Tempo execução', P.Total_contagem AS 'Contagem total de itens' , P.Peso_total AS 'Peso total do processo', P.dateinsert AS 'Data Inicio', P.dateend AS 'Data Fim' FROM Processos P INNER JOIN Usuario U ON P.Id_usuario = U.Id INNER JOIN MateriaPrima M ON P.Id_produto = M.Id", "Processos");

                    ExportToExcel(dgv_dados);
                }
            }
            catch (Exception)
            {
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ExportToExcel(dgv_dados);
        }

        private void ExportToExcel(DataGridView dataGridView)
        {
            try
            {

                dataGridView.SelectAll();
                copyAlltoClipboard();

                if (dataGridView.SelectedCells.Count > 0)
                {
                    DataObject dataObject = dataGridView.GetClipboardContent();
                    Clipboard.SetDataObject(dataObject);

                    excelApp = new Microsoft.Office.Interop.Excel.Application();
                    workbook = excelApp.Workbooks.Add(AppDomain.CurrentDomain.BaseDirectory + "modelo.xlsx");
                    worksheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Worksheets.get_Item(1);
                    Microsoft.Office.Interop.Excel.Range CR = (Microsoft.Office.Interop.Excel.Range)worksheet.Cells[9, 1];
                    CR.Select();
                    worksheet.PasteSpecial(CR, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, true);

                    excelApp.ActiveSheet.Range("F3").Value = Program._usuarioLogado.Nome;
                    excelApp.ActiveSheet.Range("G3").Value = DateTime.Now;

                    excelApp.Visible = true;
                    workbook = null;
                    worksheet = null;
                    releaseObject(excelApp);
                    releaseObject(workbook);
                }
            }
            catch (Exception ex)
            {
                excelApp.Visible = true;
                workbook = null;
                worksheet = null;
                releaseObject(excelApp);
                releaseObject(workbook);
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                resetLayoutColor();
                if (mtxtDateInicio.Text.Count(c => ' ' == c) > 1) { label2.ForeColor = System.Drawing.Color.Red;return; }
                if (mtxtDateFim.Text.Count(c => ' ' == c) > 1) { label7.ForeColor = System.Drawing.Color.Red; return; }

                if (cbProduto.SelectedItem == null) { label4.ForeColor = System.Drawing.Color.Red; return; }
                if (cbUsuario.SelectedItem == null) { label3.ForeColor = System.Drawing.Color.Red; return; }

                if (mtxtDateInicio.Text.Count(c => ' ' == c) > 1) { label2.ForeColor = System.Drawing.Color.Red;return; }


                UsuarioClass USER = (UsuarioClass)cbUsuario.SelectedItem;
                MateriaPrimaClass MATERIA = (MateriaPrimaClass)cbProduto.SelectedItem;

                //Terminar esse CÓDIGO PARA BUSCAR OS ITENS COM OS PARAMETROS.
                where_condition = $"P.dateinsert >= '{mtxtDateInicio.Text}' AND P.dateend <= '{mtxtDateFim.Text}' AND P.Id_usuario = {USER.Id} AND P.Id_produto = {MATERIA.Id}";

                dgv_dados.DataSource = Program.SQL.SelectDataGrid($"SELECT M.Descricao AS 'Matéria Prima', U.Nome AS 'Nome Operador', P.Descricao AS 'Descrição Produto', P.Tempo_execucao AS 'Tempo execução', P.Total_contagem AS 'Contagem total de itens' , P.Peso_total AS 'Peso total do processo', P.dateinsert AS 'Data Inicio', P.dateend AS 'Data Fim' FROM Processos P INNER JOIN Usuario U ON P.Id_usuario = U.Id INNER JOIN MateriaPrima M ON P.Id_produto = M.Id WHERE {where_condition}", "Processos");
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void resetLayoutColor() 
        {
            try
            {
                label2.ForeColor = System.Drawing.Color.Black;
                label7.ForeColor = System.Drawing.Color.Black;
                label4.ForeColor = System.Drawing.Color.Black;
                label3.ForeColor = System.Drawing.Color.Black;
                label2.ForeColor = System.Drawing.Color.Black;
            }
            catch (Exception ex)
            {
            }
        }



    }
}

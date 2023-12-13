﻿using LiveCharts.Wpf;
using LiveCharts;
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
using System.Linq.Expressions;
using System.Drawing.Drawing2D;
using Main.View.MainFolder;
using Main.Helper;
using Main.View.PopupFolder;
using Main.View.PagesFolder.PesagemFolder;
using Main.View.CadastroFolder;
using Main.View.PagesFolder.Configuration;

namespace Main.View.PagesFolder
{
    public partial class MainInfoForms : Form
    {

        public event EventHandler QuickButtonEventClick;

        private Form _quickButtonPress;
        public Form QuickButtonPress
        {
            get { return _quickButtonPress; }
            set
            {
                _quickButtonPress = value;
                if (QuickButtonPress != null)
                {
                    QuickButtonEventClick(this, EventArgs.Empty);
                }
            }
        }


        public MainInfoForms()
        {
            InitializeComponent();

            try
            {
                lbl_time.Text = $"{DateTime.Now.Day.ToString("D2")}/{DateTime.Now.Month.ToString("D2")}/{DateTime.Now.Year} {DateTime.Now.Hour.ToString("D2")}:{DateTime.Now.Minute.ToString("D2")}";
                dgv_avisos.RowTemplate.Height = 90;
                lblUsuario.Text = Program._usuarioLogado.Nome;
                lblAcesso.Text = Program._usuarioLogado.Acesso;
                tmTime.Enabled = true;

                var qtd_pesagens = Program.SQL.SelectList($"SELECT * FROM LogReceita WHERE CONVERT(DATE, dateinsert) = '{DateTime.Now.Year}-{DateTime.Now.Month}-{DateTime.Now.Day}' AND Status = 2", "LogReceita");
                lbl_hoje.Text = $"{qtd_pesagens.Count()}";

                var qtd_estornada = Program.SQL.SelectList($"SELECT * FROM LogReceita WHERE CONVERT(DATE, dateinsert) = '{DateTime.Now.Year}-{DateTime.Now.Month}-{DateTime.Now.Day}' AND Status = 0", "LogReceita");
                lblPendente.Text = $"{qtd_estornada.Count()}";

                var qtd_mes = Program.SQL.SelectList($"SELECT * FROM LogReceita WHERE convert(datetime, convert(int, getdate())) >= '{DateTime.Now.Year}-{DateTime.Now.Month}-01' AND convert(datetime, convert(int, getdate())) <= '{DateTime.Now.Year}-{DateTime.Now.Month}-31'", "LogReceita");
                lbl_prevista.Text = $"{qtd_mes.Count()}";

                double[] valores_dias = new double[33];

                for (int i = 0; i <= DateTime.Now.Day; i++)
                {
                    var temp = Program.SQL.SelectList($"SELECT * FROM LogReceita WHERE CONVERT(DATE, dateinsert) = '{DateTime.Now.Year}-{DateTime.Now.Month}-{i + 1}'", "LogReceita");

                    valores_dias[i + 1] = temp.Count();
                }

                SeriesCollection series = new SeriesCollection
                {
                    new LineSeries
                    {
                        Title = " ",
                        Values = new ChartValues<double>(valores_dias),
                        Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(39,213,67)),
                        PointGeometry = DefaultGeometries.Circle,
                        PointGeometrySize = 15,
                        Fill = System.Windows.Media.Brushes.Transparent,
                        LineSmoothness =0,
                        StrokeThickness = 2,
                        IsHitTestVisible = false,
                    },
                };

                cartesianChart1.AxisX = new AxesCollection { new Axis { Separator = new Separator { IsEnabled = false, Step = 1 }, Title = "", FontSize = 15, Foreground = System.Windows.Media.Brushes.Gray, MinValue = 1, MaxValue = 31 } };
                cartesianChart1.AxisY = new AxesCollection { new Axis { Separator = new Separator { IsEnabled = true, Step = 1 }, Title = "", FontSize = 15, Foreground = System.Windows.Media.Brushes.Gray, } };
                cartesianChart1.Series = series;

                button1.Enabled = Program._permissaoUsuario.Pesagem_View;
                button2.Enabled = Program._permissaoUsuario.Relatorio_View;
                //button3.Enabled = Program._permissaoUsuario.receita_view;
                button4.Enabled = Program._permissaoUsuario.Usuario_View;
                button5.Enabled = Program._permissaoUsuario.Produto_view;
            }
            catch (Exception ex)
            {
            }

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void MainInfoForms_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                tmTime.Enabled = false;
           }
            catch (Exception)
            {
            }
        }

        private void tmTime_Tick(object sender, EventArgs e)
        {
            try
            {
                lbl_time.Text = $"{DateTime.Now.Day.ToString("D2")}/{DateTime.Now.Month.ToString("D2")}/{DateTime.Now.Year} {DateTime.Now.Hour.ToString("D2")}:{DateTime.Now.Minute.ToString("D2")}";
                dgv_avisos.DataSource = Program.SQL.SelectDataGrid("SELECT Id, Mensagem  FROM Avisos", "Avisos");

                if (Program._usuarioLogado.Acesso != "Administrador") 
                {
                    dgv_avisos.Columns[1].Visible = false;
                }
            }
            catch (Exception)
            {
            }
        }

        private void dgv_avisos_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            dgv_avisos.Columns[2].Visible = false;

            dgv_avisos.Columns[0].DisplayIndex = 2;
            dgv_avisos.Columns[1].DisplayIndex = 3;
            dgv_avisos.Columns[3].HeaderText = "";
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Panel pn = (Panel)sender;
            System.Drawing.Color COLOR_BORDER = System.Drawing.Color.FromArgb(237, 237, 237);
            int borderSize = 2;
            ControlPaint.DrawBorder(e.Graphics, pn.ClientRectangle, COLOR_BORDER, borderSize, ButtonBorderStyle.Solid, COLOR_BORDER, borderSize, ButtonBorderStyle.Solid, COLOR_BORDER, borderSize, ButtonBorderStyle.Solid, COLOR_BORDER, borderSize, ButtonBorderStyle.Solid);
        }

        private void label2_Click(object sender, EventArgs e)
        {
            try
            {
                //string UserAnswer = Microsoft.VisualBasic.Interaction.InputBox("A mensagem será enviada para todos os colaboradores conectados no sistema.", "Informe a mensagem que deseja enviar", "");

                QuestionPopup question = new QuestionPopup("Informe a mensagem que deseja enviar\nA mensagem será enviada para todos os colaboradores conectados no sistema.");
                question.ShowDialog();
                if (question.RESPOSTA)
                {
                    Program.SQL.CRUDCommand("INSERT INTO Avisos (Mensagem, dateinsert) VALUES (@Mensagem, @dateinsert)", "Avisos",
                        new Dictionary<string, object>() { { "@Mensagem", question.Message }, { "@dateinsert", DateTime.Now } });

                    dgv_avisos.DataSource = Program.SQL.SelectDataGrid("SELECT Id, Mensagem  FROM Avisos", "Avisos");

                    if (Program._usuarioLogado.Acesso != "Administrador")
                    {
                        dgv_avisos.Columns[1].Visible = false;
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void dgv_avisos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 1)
                {
                    YesOrNo question = new YesOrNo("Deseja remover a a mensagem selecionada ?");
                    question.ShowDialog();

                    if (question.RESPOSTA)
                    {
                        Program.SQL.CRUDCommand("DELETE FROM Avisos WHERE Id = @Id", "Avisos", new Dictionary<string, object>() { { "@Id", dgv_avisos.Rows[e.RowIndex].Cells["Id"].Value } });
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void MainInfoForms_Resize(object sender, EventArgs e)
        {
            try
            {
                StyleSheet.RedrawAll(this);
            }
            catch (Exception)
            {
            }
        }

        private void AtalhoClick(object sender, EventArgs e)
        {
            try
            {
                Button btn = (Button)sender;

                if (btn.Tag.ToString() == "Pesagem") 
                {
                    QuickButtonPress = new PesagemList(); 
                }

                if (btn.Tag.ToString() == "Relatorio")
                {
                    QuickButtonPress = new RelatorioForms();
                }

                if (btn.Tag.ToString() == "Receita")
                {
                    QuickButtonPress = new CadastroNovaReceita();
                }

                if (btn.Tag.ToString() == "Usuario")
                {
                    QuickButtonPress = new UserForm();
                }

                if (btn.Tag.ToString() == "Produto")
                {
                    QuickButtonPress = new CadastroProdutoForms();
                }

            }
            catch (Exception)
            {
            }
        }
    }
}

using iTextSharp.text.html.simpleparser;
using Main.Model;
using Main.Service;
using Main.Helper;
using Main.View.MainFolder;
using Main.View.PopupFolder;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using StyleSheet = Main.Helper.StyleSheet;

namespace Main.View.PagesFolder.ProcessFolder
{
    public partial class PesoProcessForms : Form
    {
        int idUsuario = 0;
        string nomeUsuario = "";
        int idProduto = 0;
        int idProcesso = 1;
        int qtMinima = 0;

        Image[] imgs_peso;

        string tempoExecucao = "";
        int statusProcesso = 0;

        string descProcesso = "";

        int countMensagens = 0;

        int timeSec, timeMin, timeH;
        string tempoContagem = "";

        Boolean tmpExectAtivo;

        decimal pesoReferencia;
        decimal qtContab = 0;
        decimal qtContabTotal = 0;
        decimal valorPesoTotal = 0;

        //Comunicação
        public System.Timers.Timer tmRead = new System.Timers.Timer();
        public bool availableStatus = true;
        //

        int indiceReferencia = 0;
        int indiceContador = 0;

        Decimal valorSuporte = 0;
        Decimal valorSecSup = 0;

        int valorTotal = 0;

        int bloqueiaValor = 0;
        int bloqueiaLoop = 1;
        int bloqueiaContag = 1;
        int bloqueiaBotaoContag = 0;

        //
        bool isTrue = true;
        bool isAtivo = true;

        Stopwatch stopSup = new Stopwatch();
        Stopwatch stopValor = new Stopwatch();

        List<object> selectProcessos = new List<object>();

        public PesoProcessForms(int id_Usuario, string nome_Usuario, int id_MateriaPrima, int qt_Minima, string dsc_MateriaPrima, int id_Processo)
        {
            InitializeComponent();

            imgs_peso = new Image[]
            {
                Properties.Resources.await,
                Properties.Resources.industrial_scales_connecting_99px,
                Properties.Resources.industrial_scales_connected_filled_99px__1_
            };

            idUsuario = id_Usuario;
            nomeUsuario = nome_Usuario;
            idProcesso = id_Processo;
            qtMinima = qt_Minima;
        }

        public PesoProcessForms(int id_Processo)
        {
            InitializeComponent();

            imgs_peso = new Image[]
            {
                Properties.Resources.await,
                Properties.Resources.industrial_scales_connecting_99px,
                Properties.Resources.industrial_scales_connected_filled_99px__1_
            };

            idProcesso = id_Processo;
        }

        private async void ProcessForms_Load(object sender, EventArgs e)
        {
            selectProcessos = Program.SQL.SelectList("SELECT * FROM Processos WHERE Id = @Id", "Processos", null,
            new Dictionary<string, object>()
            {
                {"@Id", idProcesso }
            });

            foreach (ProcessosModel proc in selectProcessos)
            {
                idProduto = proc.Id_Produto;
                descProcesso = proc.Descricao;
                lbl_Descricao.Text = proc.Descricao.ToString();
                tempoExecucao = proc.TempoExecucao;
                valorTotal = proc.TotalContagem;
                pesoReferencia = Convert.ToDecimal(proc.PesoReferencia);
                valorPesoTotal = Convert.ToDecimal(proc.PesoTotal);
                tempoContagem = proc.TempoExecucao;
                statusProcesso = proc.StatusProcesso;
                lbl_DataInsert.Text = Convert.ToString(proc.dateinsert);
                lbl_Horario.Text = tempoExecucao;
            }

            lbl_PesoReferencia.Text = Convert.ToString(pesoReferencia);

            if(lbl_Horario.Text == "")
            {
                lbl_Horario.Text = "00:00:00";
            }

            var selectProduto = Program.SQL.SelectList("SELECT * FROM MateriaPrima WHERE Id = @Id", "MateriaPrima", null,
            new Dictionary<string, object>()
            {
                {"@Id", idProduto }
            });

            foreach (MateriaPrimaClass materia in selectProduto)
            {
                qtMinima = materia.Quantidade_minima;
                lbl_qtMinima.Text = materia.Quantidade_minima.ToString();
                lbl_MateriaPrima.Text = materia.Codigo + " - " + materia.Descricao;
            }

            lbl_ValorReal.Text = valorTotal.ToString();

            lbl_Descricao.Text = descProcesso;

            if (statusProcesso == 0)
            {
                // PAUSADO SEM REFERÊNCIA

            }
            else if (statusProcesso == 1)
            {
                // PAUSADO COM REFERÊNCIA

                btn_IniciarContagem.Enabled = true;
                btn_IniciarContagem.ForeColor = Color.Green;
                btn_IniciarContagem.BackColor = Color.FromArgb(192, 255, 192);

                btn_SalvarReferencia.Enabled = false;
                btn_SalvarReferencia.ForeColor = Color.FromArgb(64, 64, 64);
                btn_SalvarReferencia.BackColor = Color.Silver;

                pict_Status.Image = imgs_peso[1];

                // STATUS
                lbl_Status.Invoke(new MethodInvoker(() =>
                {
                    lbl_Status.Text = "";
                    lbl_Status.Text = "REFERÊNCIA REGISTRADA. INICIE A CONTAGEM.";
                    lbl_Status.Refresh();
                }));

                bloqueiaBotaoContag = 1;
                bloqueiaContag = 0;
            }
            else if (statusProcesso == 2)
            {
                // PAUSADO CONTAGEM INICIADA

                btn_IniciarContagem.Enabled = true;
                btn_IniciarContagem.ForeColor = Color.Green;
                btn_IniciarContagem.BackColor = Color.FromArgb(192, 255, 192);

                btn_SalvarReferencia.Enabled = false;
                btn_SalvarReferencia.ForeColor = Color.FromArgb(64, 64, 64);
                btn_SalvarReferencia.BackColor = Color.Silver;

                tempoContagem = tempoContagem.Replace(":", "");
                timeH = Convert.ToInt32(tempoContagem.Substring(0, 2));
                timeMin = Convert.ToInt32(tempoContagem.Substring(2, 2));
                timeSec = Convert.ToInt32(tempoContagem.Substring(4, 2));

                //STATUS
                lbl_Status.Invoke(new MethodInvoker(() =>
                {
                    lbl_Status.Text = "";
                    lbl_Status.Text = "AGUARDANDO RETOMADA DE PROCESSO...";
                }));
                //

                bloqueiaBotaoContag = 1;
                bloqueiaContag = 0;
                btn_IniciarContagem.Text = "RETOMAR PROCESSO";
            }


            int i = 0;

  
            foreach (IndicadorClass ind in SerialCommunicationService.indicador_addr)
            {
                if (ind.indicador.addr == Program.Endereco_Referencia)
                {
                    indiceReferencia = i;
                }
                else
                {
                    indiceContador = i;
                }

                i++;
            }


            if (lbl_Descricao.Text == "")
            {
                lbl_Descricao.Text = "Processo sem descrição.";
            }


            if (SerialCommunicationService.SERIALPORT1.IsOpen)
            {
                taraReferencia.Tag = Program.Endereco_Referencia;
                zeroReferencia.Tag = Program.Endereco_Referencia;

                taraContagem.Tag = Program.Endereco_Referencia + 1;
                zeroContagem.Tag = Program.Endereco_Referencia + 1;
            }


            await Task.Delay(500);

            string peso = $"{SerialCommunicationService.indicador_addr[indiceReferencia].PS}";
            YesOrNo question = new YesOrNo("Deseja efetuar tara antes de iniciar a pesagem? \n (Aviso: Caso haja um produto na balança, retire-o antes de efetuar a tara)");
            question.ShowDialog();

            if (question.RESPOSTA)
            {
                SerialCommunicationService.SendCommand(Convert.ToInt32(taraContagem.Tag), 0);

                await Task.Delay(250);

                SerialCommunicationService.SendCommand(Convert.ToInt32(taraReferencia.Tag), 0);
            }

            Task.Run(() =>
            {
                try
                {
                    while (isTrue)
                    {
                        if (bloqueiaContag == 1)
                        {
                            this.Invoke(new MethodInvoker(() =>
                            {
                                if ($"{SerialCommunicationService.indicador_addr[indiceReferencia].PS}".Contains("-"))
                                {
                                    valorReferencia.Text = "0.000";
                                }
                                else
                                {
                                    valorReferencia.Text = $"{SerialCommunicationService.indicador_addr[indiceReferencia].PS}";
                                    valorReferencia.Text = valorReferencia.Text.Replace(",", ".");
                                }
                            }));
                        }


                        if (bloqueiaContag == 0)
                        {
                            this.Invoke(new MethodInvoker(() =>
                            {
                            if ($"{SerialCommunicationService.indicador_addr[indiceContador].PS}".Contains("-"))
                            {
                                valorContagem.Text = "0.000";
                            }
                            else
                            {
                                valorContagem.Text = $"{SerialCommunicationService.indicador_addr[indiceContador].PS}";
                                valorContagem.Text = valorContagem.Text.Replace(",", ".");
                            }
                            }));
                        }


                        if (btn_IniciarContagem.Text == "FINALIZAR PROCESSO")
                        {
                            qtContab = Convert.ToDecimal(valorContagem.Text) / pesoReferencia;
                            qtContab = Convert.ToDecimal($"{SerialCommunicationService.indicador_addr[indiceContador].PS}") / pesoReferencia;

                            valorSuporte = Convert.ToDecimal($"{SerialCommunicationService.indicador_addr[indiceContador].PS}");

                            if (qtContab <= 0)
                            {
                                qtContab = 0;
                            }

                            this.Invoke(new MethodInvoker(() =>
                            {
                                lbl_QtContab.Text = Convert.ToInt32(qtContab).ToString();
                            }));
                        }


                        if (valorSuporte > 0 && btn_IniciarContagem.Text == "FINALIZAR PROCESSO")
                        {
                            if (bloqueiaValor == 0)
                            {
                                valorSecSup = valorSuporte;
                                stopValor.Start();

                                //STATUS
                                lbl_Status.Invoke(new MethodInvoker(() =>
                                {
                                    pict_Status.Image = imgs_peso[0];

                                    lbl_Status.Text = "";
                                    lbl_Status.Text = "PESANDO...";
                                }));
                                //
                            }

                            valorSuporte = Convert.ToDecimal($"{SerialCommunicationService.indicador_addr[indiceContador].PS}");

                            if (stopValor.ElapsedMilliseconds > 5000)
                            {
                                if (valorSecSup == valorSuporte)
                                {
                                    //STATUS
                                    this.Invoke(new MethodInvoker(() =>
                                    {
                                        pict_Status.Image = imgs_peso[1];

                                        lbl_Status.Text = "";
                                        lbl_Status.Text = "PESO ESTABILIZADO. RETIRE A MATÉRIA-PRIMA.";
                                    }));
                                    //

                                    bloqueiaLoop = 0;
                                    bloqueiaValor = 1;
                                }

                                stopValor.Reset();
                            }
                        }


                        if (bloqueiaLoop == 0)
                        {
                            valorSuporte = Convert.ToDecimal($"{SerialCommunicationService.indicador_addr[indiceContador].PS}");

                            if (valorSuporte <= 0)
                            {
                                stopSup.Start();
                            }

                            if (stopSup.ElapsedMilliseconds > 5000)
                            {
                                if (valorSuporte <= 0)
                                {
                                    qtContabTotal = valorSecSup / pesoReferencia;
                                    valorTotal += Convert.ToInt32(qtContabTotal);

                                    this.Invoke(new MethodInvoker(() =>
                                    {
                                        lbl_ValorReal.Text = valorTotal.ToString();
                                    }));


                                    //STATUS
                                    this.Invoke(new MethodInvoker(() =>
                                    {
                                        pict_Status.Image = imgs_peso[2];

                                        lbl_Status.Text = "";
                                        lbl_Status.Text = "PESO REGISTRADO. AGUARDANDO MATÉRIA-PRIMA...";
                                    }));
                                    //

                                    SerialCommunicationService.SendCommand(Convert.ToInt32(taraContagem.Tag), 0);

                                    valorPesoTotal = valorPesoTotal + Convert.ToDecimal(valorContagem.Text);

                                    var insertLog = Program.SQL.CRUDCommand("INSERT INTO Log_Processos (Id_processo, Peso_temporeal, Peso_total, Tempo_execucao, dateinsert) VALUES (@Id_processo, @Peso_temporeal, @Peso_total, @Tempo_execucao, @dateinsert)", "Log_Processos",
                                    new Dictionary<string, object>()
                                    {
                                        {"@Id_processo", idProcesso },
                                        {"@Peso_temporeal", qtContabTotal },
                                        {"@Peso_total", valorTotal },
                                        {"@Tempo_execucao", lbl_Horario.Text },
                                        {"@dateinsert", DateTime.Now}
                                    });


                                    var UpdateProcesso = Program.SQL.CRUDCommand("UPDATE Processos SET Descricao = @Descricao, Tempo_execucao = @Tempo_execucao, Total_contagem = @Total_contagem, Peso_Referencia = @Peso_Referencia, Peso_total = @Peso_total, Status_processo = @Status_processo WHERE Id = @Id\r\n", "Processos",
                                    new Dictionary<string, object>()
                                    {
                                        {"@Id", idProcesso },
                                        {"@Descricao", descProcesso },
                                        {"@Tempo_execucao", lbl_Horario.Text },
                                        {"@Total_contagem", valorTotal },
                                        {"@Peso_Referencia", pesoReferencia },
                                        {"@Peso_total", valorPesoTotal },
                                        {"@Status_processo", statusProcesso },
                                    });

                                    bloqueiaLoop = 1;
                                    bloqueiaValor = 0;

                                    if (stopSup.ElapsedMilliseconds > 8)
                                    {
                                        stopSup.Reset();
                                    }

                                    countMensagens = 1;
                                }
                                else
                                {
                                    stopSup.Reset();
                                }
                            }

                            this.Invoke(new MethodInvoker(() =>
                            {
                                lbl_Status.Refresh();
                            }));
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            });
        }

        private void CommandButtonEventClick(object sender, EventArgs e)
        {
            try
            {

                PictureBox piCommand = (PictureBox)sender;

                if (piCommand.Name.Contains("tara"))
                {
                    SerialCommunicationService.SendCommand(Convert.ToInt32(piCommand.Tag), 0);
                }
                else if (piCommand.Name.Contains("zero"))
                {
                    SerialCommunicationService.SendCommand(Convert.ToInt32(piCommand.Tag), 1);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ProcessForms_SizeChanged(object sender, EventArgs e)
        {
            StyleSheet.RedrawAll(this);
            this.Invalidate();
            this.Refresh();
            this.Update();
        }

        private async void btn_IniciarContagem_Click(object sender, EventArgs e)
        {
            try
            {
                if (bloqueiaBotaoContag == 1)
                {
                    if (btn_IniciarContagem.Text == "INICIAR CONTAGEM")
                    {
                        YesOrNo question = new YesOrNo("Deseja iniciar a contagem?");
                        question.ShowDialog();

                        if (question.RESPOSTA)
                        {
                            statusProcesso = 2;
                            tmpExectAtivo = true;
                            TimerRelogio.Start();

                            //STATUS
                            //lbl_Status.Invoke(new MethodInvoker(() =>
                            //{
                            lbl_Status.Text = "";
                            lbl_Status.Text = "PESANDO...";
                            //}));
                            //

                            btn_IniciarContagem.Text = "";
                            btn_IniciarContagem.Text = "FINALIZAR PROCESSO";

                            btn_IniciarContagem.Enabled = true;
                            btn_IniciarContagem.ForeColor = Color.White;
                            btn_IniciarContagem.BackColor = Color.FromArgb(255, 0, 0);

                            btn_IniciarContagem.Refresh();
                        }
                    }

                    else if (btn_IniciarContagem.Text == "RETOMAR PROCESSO")
                    {
                        YesOrNo question = new YesOrNo("Deseja retomar a contagem?");
                        question.ShowDialog();

                        if (question.RESPOSTA)
                        {
                            statusProcesso = 2;
                            tmpExectAtivo = true;
                            TimerRelogio.Start();

                            //////////////////SerialCommunicationService.SendCommand(Convert.ToInt32(taraContagem.Tag), 0);

                            //STATUS
                            //lbl_Status.Invoke(new MethodInvoker(() =>
                            //{
                            lbl_Status.Text = "";
                            lbl_Status.Text = "AGUARDANDO MATÉRIA-PRIMA...";
                            //}));
                            //

                            btn_IniciarContagem.Enabled = true;
                            btn_IniciarContagem.ForeColor = Color.White;
                            btn_IniciarContagem.BackColor = Color.FromArgb(255, 0, 0);

                            this.Invoke(new MethodInvoker(() =>
                            {
                                btn_IniciarContagem.Text = "FINALIZAR PROCESSO";
                            }));
                        }
                    }

                    else if (btn_IniciarContagem.Text == "FINALIZAR PROCESSO")
                    {
                        YesOrNo question = new YesOrNo("Deseja finalizar esse processo?");
                        question.ShowDialog();

                        if (question.RESPOSTA)
                        {
                            statusProcesso = 3;
                            isTrue = false;

                            TimerRelogio.Stop();

                            var UpdateProcesso = Program.SQL.CRUDCommand("UPDATE Processos SET Descricao = @Descricao, Tempo_execucao = @Tempo_execucao, Total_contagem = @Total_contagem, Peso_Referencia = @Peso_Referencia, Peso_total = @Peso_total, Status_processo = @Status_processo, dateinsert = @dateinsert WHERE Id = @Id\r\n", "Log_Processos",
                            new Dictionary<string, object>()
                            {
                                {"@Id", idProcesso },
                                {"@Descricao", descProcesso },
                                {"@Tempo_execucao", lbl_Horario.Text },
                                {"@Total_contagem", valorTotal },
                                {"@Peso_Referencia", pesoReferencia },
                                {"@Peso_total", valorTotal },
                                {"@Status_processo", statusProcesso },
                                {"@dateinsert", DateTime.Now }
                            });

                            await Task.Delay(1000);

                            InfoPopup info = new InfoPopup("Parabéns!", "Processo e contagem registrados com sucesso!", Properties.Resources._299110_check_sign_icon);
                            info.ShowDialog();

                            MainInfoForms form = new MainInfoForms(idUsuario, nomeUsuario);

                            foreach (Form openForm in Application.OpenForms)
                            {
                                if (openForm is MainForms)
                                {
                                    MainForms main = (MainForms)openForm;
                                    main.OpenPage(form);
                                    this.Close();
                                    return;
                                }
                            }
                        }
                    }
                }
                else
                {
                    InfoPopup info = new InfoPopup("Erro", "Registre o peso de Referência antes de iniciar a contagem!", Properties.Resources.errorIcon);
                    info.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            VisualizarLogForms visu = new VisualizarLogForms(idProcesso);
            visu.ShowDialog();
        }

        private void PesoProcessForms_Resize(object sender, EventArgs e)
        {
            this.Invoke(new MethodInvoker(() =>
            {
                panel6.Width = this.Size.Width;
                panel6.Height = this.Size.Height;
            }));

            tableLayoutPanel1.Width = this.Size.Width;
            tableLayoutPanel1.Height = this.Size.Height;

            StyleSheet.RedrawAll(this);
            this.Invalidate();
            this.Refresh();
            this.Update();
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void TimerRelogio_Tick_1(object sender, EventArgs e)
        {
            if (tmpExectAtivo)
            {
                timeSec++;

                if (timeSec > 59)
                {
                    timeMin++;
                    timeSec = 0;

                    if (timeMin > 59)
                    {
                        timeH++;
                        timeMin = 0;
                    }
                }
            }

            lbl_Horario.Invoke(new MethodInvoker(() =>
            {
                lbl_Horario.Text = (String.Format("{0:00}", timeH)) + ":" + (String.Format("{0:00}", timeMin)) + ":" + (String.Format("{0:00}", timeSec));
            }));
        }

        private void btn_SalvarReferencia_Click(object sender, EventArgs e)
        {
            try
            {
                if (bloqueiaBotaoContag == 0)
                {
                    YesOrNo question = new YesOrNo("Confirmar que a quantidade para referência (" + qtMinima + ") está correta na balança? Começar contagem?");
                    question.ShowDialog();

                    if (question.RESPOSTA)
                    {
                        statusProcesso = 1;

                        pict_Status.Image = imgs_peso[1];

                        lbl_Status.Text = "";
                        lbl_Status.Text = "REFERÊNCIA REGISTRADA. INICIE A CONTAGEM.";

                        var UpdateProcesso = Program.SQL.CRUDCommand("UPDATE Processos SET Peso_Referencia = @Peso_Referencia, Status_processo = @Status_processo WHERE Id = @Id\r\n", "Log_Processos",
                        new Dictionary<string, object>()
                        {
                            {"@Id", idProcesso },
                            {"@Peso_Referencia", pesoReferencia },
                            {"@Status_processo", statusProcesso },
                        });

                        lbl_Status.Refresh();

                        pesoReferencia = Convert.ToDecimal($"{SerialCommunicationService.indicador_addr[indiceReferencia].PS}") / Convert.ToDecimal(lbl_qtMinima.Text);
                        lbl_PesoReferencia.Text = pesoReferencia.ToString();

                        lbl_PesoReferencia.Refresh();

                        bloqueiaContag = 0;
                        bloqueiaBotaoContag = 1;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}

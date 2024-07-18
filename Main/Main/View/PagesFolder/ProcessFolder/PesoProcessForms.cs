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
using System.Numerics;
using Microsoft.VisualBasic;
using Main.Model.EtiquetaFolder;
using System.Drawing.Printing;
using ZPL;
using ZXing;

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

        decimal Gramatura;
        decimal qtContab = 0;
        decimal qtContabTotal = 0;
        decimal valorPesoTotal = 0;
        decimal valorPesoTotalSup = 0;

        //Comunicação
        public System.Timers.Timer tmRead = new System.Timers.Timer();
        public bool availableStatus = true;
        //

        int indiceReferencia = 0;
        int indiceContador = 0;

        Decimal valorSuporte = 0;
        Decimal valorSecSup = 0;

        int valorTotal = 0;

        int bloqueiaLoop = 1;
        int bloqueiaValor = 0;
        int bloqueiaCheck = 0;
        int bloqueiaContag = 1;
        int bloqueiaBotaoContag = 0;

        //
        bool isTrue = true;
        bool isAtivo = true;

        int startSupTimer = 0;

        Stopwatch stopSup = new Stopwatch();
        Stopwatch stopValor = new Stopwatch();
        Stopwatch stopCheck = new Stopwatch();

        List<object> selectProcessos = new List<object>();

        private List<Object> _impressoras = new List<Object>();


        public PesoProcessForms(int id_Usuario, string nome_Usuario, int qt_Folhas, int id_Processo)
        {
            InitializeComponent();
            this.DoubleBuffered = true;

            imgs_peso = new Image[]
            {
                Properties.Resources.await,
                Properties.Resources.industrial_scales_connecting_99px,
                Properties.Resources.industrial_scales_connected_filled_99px__1_
            };

            idUsuario = id_Usuario;
            nomeUsuario = nome_Usuario;
            idProcesso = id_Processo;
            qtMinima = qt_Folhas;
        }

        public PesoProcessForms(int id_Processo)
        {
            InitializeComponent();
            this.DoubleBuffered = true;

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
            try
            {
                LoadImpressoras();

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
                    Gramatura = Convert.ToDecimal(proc.Gramatura);
                    valorPesoTotal = Convert.ToDecimal(proc.PesoTotal);
                    tempoContagem = proc.TempoExecucao;
                    statusProcesso = proc.StatusProcesso;
                    lbl_DataInsert.Text = Convert.ToString(proc.dateinsert);
                    lbl_Horario.Text = tempoExecucao;

                    // Novo

                    qtMinima = proc.Quantidade;
                    lbl_qtMinima.Text = proc.Quantidade.ToString();
                    lbl_MateriaPrima.Text = proc.Numero + " - " + proc.Papel + " " + proc.Formato;
                    infosprocesso_txt.Text = proc.Cliente + " - " + proc.Op;
                }


                lbl_PesoReferencia.Text = "1 folha ≅ " + Convert.ToString(Gramatura);

                if (lbl_Horario.Text == "")
                {
                    lbl_Horario.Text = "00:00:00";
                }

                //var selectProduto = Program.SQL.SelectList("SELECT * FROM MateriaPrima WHERE Id = @Id", "MateriaPrima", null,
                //new Dictionary<string, object>()
                //{
                //    {"@Id", idProduto }
                //});

                //foreach (MateriaPrimaClass materia in selectProduto)
                //{
                //    qtMinima = materia.Quantidade_minima;
                //    lbl_qtMinima.Text = materia.Quantidade_minima.ToString();
                //    lbl_MateriaPrima.Text = materia.Codigo + " - " + materia.Descricao;
                //}

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
                    lbl_Descricao.Text = "Processo sem observação.";
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

                try
                {
                    if (question.RESPOSTA)
                    {
                        SerialCommunicationService.SendCommand(Convert.ToInt32(taraContagem.Tag), 0);

                        await Task.Delay(250);

                        SerialCommunicationService.SendCommand(Convert.ToInt32(taraReferencia.Tag), 0);
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }



                Task.Run(async () =>
                {
                    try
                    {
                        while (isTrue)
                        {
                            string val = $"{SerialCommunicationService.indicador_addr[indiceReferencia].PS}";
                            string val2 = $"{SerialCommunicationService.indicador_addr[indiceContador].PS}";


                            if (SerialCommunicationService.SERIALPORT1.IsOpen == true)
                            {

                            }
                            else
                            {
                                this.Invoke(new MethodInvoker(() =>
                                {
                                    PesagemForms form = new PesagemForms(idUsuario, nomeUsuario);

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
                                }));

                                InfoPopup info = new InfoPopup("Erro!", "A conexão com a balança foi interrompida. Reconecte para retornar ao processo! \n Fechando...", Properties.Resources.errorIcon);
                                info.ShowDialog();
                            }


                            await Task.Delay(1);

                            this.Invoke(new MethodInvoker(() =>
                            {
                                valorReferencia.Text = val;
                                valorReferencia.Text = valorReferencia.Text.Replace(",", ".");
                                valorReferencia.Refresh();
                            }));

                            this.Invoke(new MethodInvoker(() =>
                            {
                                valorContagem.Text = val2;
                                valorContagem.Text = valorContagem.Text.Replace(",", ".");
                                valorContagem.Refresh();
                            }));

                            if (btn_IniciarContagem.Text == "FINALIZAR PROCESSO")
                            {
                                qtContab = Convert.ToDecimal(valorContagem.Text) / Gramatura;
                                qtContab = Convert.ToDecimal($"{SerialCommunicationService.indicador_addr[indiceContador].PS}") / Gramatura;

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


                            if (btn_IniciarContagem.Text == "FINALIZAR PROCESSO")
                            {
                                if (bloqueiaValor == 0 && valorSuporte > 0)
                                {
                                    valorSecSup = valorSuporte;
                                    stopValor.Start();

                                    //STATUS
                                    lbl_Status.Invoke(new MethodInvoker(() =>
                                    {
                                        pict_Status.Image = imgs_peso[0];

                                        pict_Status.BackColor = Color.DarkOrange;
                                        panel12.BackColor = Color.DarkOrange;
                                        panel20.BackColor = Color.DarkOrange;
                                        lbl_Status.BackColor = Color.DarkOrange;

                                        lbl_Status.Text = "";
                                        lbl_Status.Text = "PESANDO...";
                                    }));
                                    //
                                }
                                else if (bloqueiaValor == 0 && valorSuporte <= 0)
                                {
                                    if (valorTotal == 0)
                                    {
                                        //STATUS
                                        lbl_Status.Invoke(new MethodInvoker(() =>
                                        {
                                            pict_Status.BackColor = Color.FromArgb(41, 46, 84);
                                            panel12.BackColor = Color.FromArgb(41, 46, 84);
                                            panel20.BackColor = Color.FromArgb(41, 46, 84);
                                            lbl_Status.BackColor = Color.FromArgb(41, 46, 84);

                                            lbl_Status.Text = "";
                                            lbl_Status.Text = "AGUARDANDO MATÉRIA-PRIMA...";
                                        }));
                                        //
                                    }
                                    else
                                    {
                                        pict_Status.BackColor = Color.SeaGreen;
                                        panel12.BackColor = Color.SeaGreen;
                                        panel20.BackColor = Color.SeaGreen;
                                        lbl_Status.BackColor = Color.SeaGreen;

                                        //STATUS
                                        this.Invoke(new MethodInvoker(() =>
                                        {
                                            pict_Status.Image = imgs_peso[2];

                                            lbl_Status.Text = "";
                                            lbl_Status.Text = "PESO REGISTRADO. AGUARDANDO MATÉRIA-PRIMA...";
                                        }));
                                        //
                                    }
                                }

                                valorSuporte = Convert.ToDecimal($"{SerialCommunicationService.indicador_addr[indiceContador].PS}");

                                if (stopValor.ElapsedMilliseconds > 3000)
                                {
                                    await Task.Delay(1000);

                                    valorSuporte = Convert.ToDecimal($"{SerialCommunicationService.indicador_addr[indiceContador].PS}");

                                    if (valorSecSup == valorSuporte)
                                    {
                                        valorPesoTotalSup = valorPesoTotalSup + Convert.ToDecimal(val2);

                                        pict_Status.BackColor = Color.FromArgb(41, 46, 84);
                                        panel12.BackColor = Color.FromArgb(41, 46, 84);
                                        panel20.BackColor = Color.FromArgb(41, 46, 84);
                                        lbl_Status.BackColor = Color.FromArgb(41, 46, 84);

                                        //STATUS
                                        this.Invoke(new MethodInvoker(() =>
                                        {
                                            pict_Status.Image = imgs_peso[1];

                                            lbl_Status.Text = "";
                                            lbl_Status.Text = "PESO ESTABILIZADO. RETIRE OU ADICIONE MAIS MATÉRIA-PRIMA.";
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

                                if (valorSecSup != valorSuporte)
                                {
                                    stopCheck.Start();

                                    if (valorSuporte == 0)
                                    {
                                        pict_Status.BackColor = Color.SteelBlue;
                                        panel12.BackColor = Color.SteelBlue;
                                        panel20.BackColor = Color.SteelBlue;
                                        lbl_Status.BackColor = Color.SteelBlue;

                                        this.Invoke(new MethodInvoker(() =>
                                        {
                                            lbl_Status.Text = "";
                                            lbl_Status.Text = "REGISTRANDO PESO. AGUARDE...";
                                        }));
                                    }
                                    else
                                    {
                                        pict_Status.BackColor = Color.DarkOrange;
                                        panel12.BackColor = Color.DarkOrange;
                                        panel20.BackColor = Color.DarkOrange;
                                        lbl_Status.BackColor = Color.DarkOrange;

                                        this.Invoke(new MethodInvoker(() =>
                                        {
                                            pict_Status.Image = imgs_peso[0];
                                            lbl_Status.Text = "";
                                            lbl_Status.Text = "PESANDO...";
                                        }));
                                    }
                                }


                                if (stopCheck.ElapsedMilliseconds > 4000)
                                {
                                    valorSuporte = Convert.ToDecimal($"{SerialCommunicationService.indicador_addr[indiceContador].PS}");

                                    if (valorSuporte > 0)
                                    {
                                        bloqueiaLoop = 1;
                                        bloqueiaValor = 0;

                                        valorPesoTotalSup = 0;

                                        stopCheck.Reset();
                                    }
                                    else
                                    {
                                        pict_Status.BackColor = Color.SteelBlue;
                                        panel12.BackColor = Color.SteelBlue;
                                        panel20.BackColor = Color.SteelBlue;
                                        lbl_Status.BackColor = Color.SteelBlue;

                                        this.Invoke(new MethodInvoker(() =>
                                        {
                                            lbl_Status.Text = "";
                                            lbl_Status.Text = "REGISTRANDO PESO. AGUARDE...";
                                        }));

                                        valorPesoTotal = valorPesoTotal + valorPesoTotalSup;

                                        stopSup.Start();
                                        stopCheck.Reset();
                                    }
                                }


                                if (stopSup.ElapsedMilliseconds > 3500)
                                {
                                    if (valorSuporte <= 0)
                                    {
                                        try
                                        {

                                            qtContabTotal = valorSecSup / Gramatura;
                                            valorTotal += Convert.ToInt32(qtContabTotal);

                                            this.Invoke(new MethodInvoker(() =>
                                            {
                                                lbl_ValorReal.Text = valorTotal.ToString();
                                            }));

                                            string tempoexec = lbl_Horario.Text;

                                            SerialCommunicationService.SendCommand(Convert.ToInt32(taraContagem.Tag), 0);

                                            var insertLog = Program.SQL.CRUDCommand("INSERT INTO Log_Processos (Id_processo, Peso_temporeal, Peso_total, Tempo_execucao, dateinsert) VALUES (@Id_processo, @Peso_temporeal, @Peso_total, @Tempo_execucao, @dateinsert)", "Log_Processos",
                                            new Dictionary<string, object>()
                                            {
                                                {"@Id_processo", idProcesso },
                                                {"@Peso_temporeal", Convert.ToInt32(qtContabTotal) },
                                                {"@Peso_total", valorTotal },
                                                {"@Tempo_execucao", tempoexec },
                                                {"@dateinsert", DateTime.Now}
                                            });

                                            //=================================================================================================================//

                                            double pesoTotal = valorTotal * Convert.ToDouble(Gramatura);

                                            var UpdateProcesso = Program.SQL.CRUDCommand("UPDATE Processos SET Descricao = @Descricao, Tempo_execucao = @Tempo_execucao, Total_contagem = @Total_contagem, Gramatura = @Gramatura, Peso_total = @Peso_total, Status_processo = @Status_processo WHERE Id = @Id", "Processos",
                                            new Dictionary<string, object>()
                                            {
                                                {"@Id", idProcesso },
                                                {"@Descricao", descProcesso },
                                                {"@Tempo_execucao", tempoexec },
                                                {"@Total_contagem", valorTotal },
                                                {"@Gramatura", Gramatura },
                                                {"@Peso_total", pesoTotal },
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
                                        catch (Exception ex)
                                        {
                                            Console.WriteLine(ex.Message);
                                        }
                                    }
                                    else
                                    {
                                        //stopSup.Stop();
                                        //if (valorSecSup != valorSuporte && valorSuporte > 0)
                                        //{
                                        //    bloqueiaLoop = 0;
                                        //    bloqueiaValor = 0;
                                        //}
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
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

        //private void ProcessForms_SizeChanged(object sender, EventArgs e)
        //{
        //    StyleSheet.RedrawAll(this);
        //    this.Invalidate();
        //    this.Refresh();
        //    this.Update();
        //}

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

                            pict_Status.BackColor = Color.FromArgb(41, 46, 84);
                            panel12.BackColor = Color.FromArgb(41, 46, 84);
                            panel20.BackColor = Color.FromArgb(41, 46, 84);
                            lbl_Status.BackColor = Color.FromArgb(41, 46, 84);

                            lbl_Status.Text = "";
                            lbl_Status.Text = "AGUARDANDO MATÉRIA-PRIMA...";
                            
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

                            //SerialCommunicationService.SendCommand(Convert.ToInt32(taraContagem.Tag), 0);

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
                            if (lbl_QtContab.Text != "0")
                            {
                                YesOrNo pergunta = new YesOrNo("Ainda há matéria-prima na balança, gostaria de contabilizar essa matéria-prima na contagem final?");
                                pergunta.ShowDialog();

                                if (pergunta.RESPOSTA)
                                {
                                    valorTotal += Convert.ToInt32(qtContabTotal);
                                }
                            }

                            statusProcesso = 3;
                            isTrue = false;
                            TimerRelogio.Stop();

                            double pesoTotal = valorTotal * Convert.ToDouble(Gramatura);


                            var UpdateProcesso = Program.SQL.CRUDCommand("UPDATE Processos SET Descricao = @Descricao, Tempo_execucao = @Tempo_execucao, Total_contagem = @Total_contagem, Gramatura = @Gramatura, Peso_total = @Peso_total, Status_processo = @Status_processo, dateend = @dateend WHERE Id = @Id", "Processos",
                            new Dictionary<string, object>()
                            {
                                {"@Id", idProcesso },
                                {"@Descricao", descProcesso },
                                {"@Tempo_execucao", lbl_Horario.Text },
                                {"@Total_contagem", valorTotal },
                                {"@Gramatura", Gramatura },
                                {"@Peso_total", pesoTotal },
                                {"@Status_processo", statusProcesso },
                                {"@dateend", DateTime.Now }
                            });

                            await Task.Delay(1000);

                            InfoPopup info = new InfoPopup("Parabéns!", "Processo e contagem registrados com sucesso!", Properties.Resources._299110_check_sign_icon);
                            info.ShowDialog();

                            ImpressoraPrint(new EtiquetaInfo() { op = "", cliente = "", peso = "", qtd_folhas = "", tipo_papel = "", formato = "", gramatura = "", data_inicio = "", data_termino = "", horario_inicial = "", horario_final = "", operador = "", turno = "", obs = "" }, 2);



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

        private void valorContagem_Click(object sender, EventArgs e)
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
                string valorRef = $"{SerialCommunicationService.indicador_addr[indiceReferencia].PS}";

                if (Convert.ToDecimal(valorRef) > 0.0005M)
                {
                    if (bloqueiaBotaoContag == 0)
                    {
                        YesOrNo question = new YesOrNo("Confirmar que a quantidade para referência (" + qtMinima + ") está correta na balança?");
                        question.ShowDialog();

                        if (question.RESPOSTA)
                        {
                            statusProcesso = 1;

                            pict_Status.Image = imgs_peso[1];

                            Gramatura = Convert.ToDecimal($"{SerialCommunicationService.indicador_addr[indiceReferencia].PS}") / Convert.ToDecimal(lbl_qtMinima.Text);

                            lbl_Status.Text = "";
                            lbl_Status.Text = "REFERÊNCIA REGISTRADA. INICIE A CONTAGEM.";

                            var UpdateProcesso = Program.SQL.CRUDCommand("UPDATE Processos SET Gramatura = @Gramatura, Status_processo = @Status_processo WHERE Id = @Id", "Processos",
                            new Dictionary<string, object>()
                            {
                                {"@Id", idProcesso },
                                {"@Gramatura", Gramatura },
                                {"@Status_processo", statusProcesso },
                            });

                            lbl_Status.Refresh();

                            zeroReferencia.Enabled = false;
                            taraReferencia.Enabled = false;

                            lbl_PesoReferencia.Text = "1 folha ≅ " + Gramatura.ToString();

                            lbl_PesoReferencia.Refresh();


                            YesOrNo iniciarContagem = new YesOrNo("Deseja iniciar a contagem?");
                            iniciarContagem.ShowDialog();

                            if (iniciarContagem.RESPOSTA)
                            {
                                statusProcesso = 2;
                                tmpExectAtivo = true;
                                TimerRelogio.Start();



                                //STATUS
                                pict_Status.BackColor = Color.FromArgb(41, 46, 84);
                                panel12.BackColor = Color.FromArgb(41, 46, 84);
                                panel20.BackColor = Color.FromArgb(41, 46, 84);
                                lbl_Status.BackColor = Color.FromArgb(41, 46, 84);

                                lbl_Status.Text = "";
                                lbl_Status.Text = "AGUARDANDO MATÉRIA-PRIMA...";



                                btn_IniciarContagem.Enabled = true;
                                btn_IniciarContagem.ForeColor = Color.White;
                                btn_IniciarContagem.BackColor = Color.FromArgb(255, 0, 0);

                                btn_IniciarContagem.Text = "";
                                btn_IniciarContagem.Text = "FINALIZAR PROCESSO";



                                btn_IniciarContagem.Enabled = true;
                                btn_IniciarContagem.ForeColor = Color.White;
                                btn_IniciarContagem.BackColor = Color.FromArgb(255, 0, 0);

                                btn_IniciarContagem.Refresh();
                            }
                            else
                            {
                                btn_IniciarContagem.Enabled = true;
                                btn_IniciarContagem.ForeColor = Color.Green;
                                btn_IniciarContagem.BackColor = Color.FromArgb(192, 255, 192);
                            }


                            this.Invoke(new MethodInvoker(() =>
                            {
                                btn_SalvarReferencia.Enabled = false;
                                btn_SalvarReferencia.ForeColor = Color.FromArgb(64, 64, 64);
                                btn_SalvarReferencia.BackColor = Color.Silver;
                            }));


                            bloqueiaContag = 0;
                            bloqueiaBotaoContag = 1;

                            btn_SalvarReferencia.Refresh();
                            btn_SalvarReferencia.Enabled = false;
                        }
                    }
                }
                else
                {
                    InfoPopup info = new InfoPopup("Erro", "O peso de referência não pode ser zero ou negativo!", Properties.Resources.errorIcon);
                    info.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void PesoProcessForms_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (btn_IniciarContagem.Text == "FINALIZAR PROCESSO")
            {
                string tempoexec = lbl_Horario.Text;

                //=================================================================================================================//

                double pesoTotal = valorTotal * Convert.ToDouble(Gramatura);

                var UpdateProcesso = Program.SQL.CRUDCommand("UPDATE Processos SET Descricao = @Descricao, Tempo_execucao = @Tempo_execucao, Total_contagem = @Total_contagem, Gramatura = @Gramatura, Peso_total = @Peso_total, Status_processo = @Status_processo WHERE Id = @Id", "Processos",
                new Dictionary<string, object>()
                {
                    {"@Id", idProcesso },
                    {"@Descricao", descProcesso },
                    {"@Tempo_execucao", tempoexec },
                    {"@Total_contagem", valorTotal },
                    {"@Gramatura", Gramatura },
                    {"@Peso_total", pesoTotal },
                    {"@Status_processo", statusProcesso },
                });
            }
        }

        private void lbl_PesoReferencia_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        public void LoadImpressoras()
        {
            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("@parent", "-");
            _impressoras = Program.SQL.SelectList("select * from Rede where parent = @parent and tipo = 'Impressora'", "Rede", values: parametros);
        }

        public async void ImpressoraPrint(EtiquetaInfo etiqueta, int type)
        {
            try
            {
                foreach (RedeClass impressora in _impressoras)
                {
                    string zplCode = "";

                    ZXing.BarcodeWriter brcode = new ZXing.BarcodeWriter();


                    selectProcessos = Program.SQL.SelectList("SELECT * FROM Processos WHERE Id = @Id", "Processos", null,
                    new Dictionary<string, object>()
                    {
                        {"@Id", idProcesso }
                    });


                    string lbl_op = "N° Op:";
                    string lbl_op_r = "000000000";

                    string lbl_cli = "Cliente:";
                    string lbl_cli_r = "Cliente Padrão";

                    string lbl_peso = "Peso:";
                    string lbl_peso_r = lbl_ValorReal.Text;

                    string lbl_qtfl = "Qtd Folhas:";
                    string lbl_qtfl_r = lbl_qtMinima.Text;

                    string lbl_tppl = "Tipo Papel:";
                    string lbl_tppl_r = "";

                    string lbl_fmt = "Formato:";
                    string lbl_fmt_r = "";

                    string lbl_gr = "Gram:";
                    string lbl_gr_r = Gramatura.ToString();

                    string lbl_dtin = "Data Início:";
                    string lbl_dtin_r = "";

                    string lbl_dtfm = "Data Término:";
                    string lbl_dtfm_r = DateTime.Now.Date.ToString().Substring(0, 10);

                    string lbl_hrin = "Horário inicial:";
                    string lbl_hrin_r = "";

                    string lbl_hrfm = "Horário final:";
                    string lbl_hrfm_r = DateTime.Now.ToString().Substring(11, 8);

                    string lbl_opr = "Operador:";
                    string lbl_opr_r = Program._usuarioLogado.Nome;

                    if (lbl_opr_r.Length <= 21)
                    {
                        lbl_opr_r = Program._usuarioLogado.Nome.Substring(0, lbl_opr_r.Length);
                    }
                    else if (lbl_opr_r.Length >= 22)
                    {
                        lbl_opr_r = Program._usuarioLogado.Nome.Substring(0, 22);
                    }

                    string lbl_trn = "Turno:";
                    string lbl_trn_r = "";
                    if(DateTime.Now.Hour<=12){lbl_trn_r="Matutino";}else if(DateTime.Now.Hour>12){lbl_trn_r="Vespertino";}

                    string lbl_obs = "Obs:";
                    string lbl_obs_r = lbl_Descricao.Text;

                    string split1 = "";
                    string split2 = "";
                    string split3 = "";

                    if (lbl_obs_r.Length <= 60)
                    {
                        split1 = lbl_obs_r;
                    }
                    else if (lbl_obs_r.Length > 60 & lbl_obs_r.Length < 120)
                    {
                        split1 = lbl_obs_r.Substring(0, 60);
                        split2 = lbl_obs_r.Substring(60, lbl_obs_r.Length - 60);
                    }
                    else if (lbl_obs_r.Length > 60)
                    {
                        split1 = lbl_obs_r.Substring(0, 60);
                        split2 = lbl_obs_r.Substring(60, 60);
                        split3 = lbl_obs_r.Substring(120);
                    }



                    foreach (ProcessosModel proc in selectProcessos)
                    {
                        lbl_peso_r = proc.PesoTotal.ToString();
                        if (lbl_peso_r.Length <= 12)
                        {
                            lbl_peso_r = proc.PesoTotal.ToString().Substring(0, lbl_peso_r.Length);
                        }
                        else if (lbl_peso_r.Length >= 13)
                        {
                            lbl_peso_r = proc.PesoTotal.ToString().Substring(0, 13);
                        }


                        lbl_opr_r = proc.Op;
                        if (lbl_opr_r.Length <= 21)
                        {
                            lbl_opr_r = Program._usuarioLogado.Nome.Substring(0, lbl_opr_r.Length);
                        }
                        else if (lbl_opr_r.Length >= 22)
                        {
                            lbl_opr_r = Program._usuarioLogado.Nome.Substring(0, 22);
                        }


                        lbl_cli_r = proc.Cliente;
                        if (lbl_cli_r.Length <= 21)
                        {
                            lbl_cli_r = proc.Cliente.ToString().Substring(0, lbl_cli_r.Length);
                        }
                        else if (lbl_cli_r.Length >= 22)
                        {
                            lbl_cli_r = proc.Cliente.ToString().Substring(0, 22);
                        }


                        lbl_dtin_r = proc.dateinsert.Date.ToString();
                        if (lbl_dtin_r.Length <= 9)
                        {
                            lbl_dtin_r = proc.dateinsert.Date.ToString().Substring(0, lbl_dtin_r.Length);
                        }
                        else if (lbl_dtin_r.Length >= 10)
                        {
                            lbl_dtin_r = proc.dateinsert.Date.ToString().Substring(0, 10);
                        }


                        lbl_hrin_r = proc.dateinsert.ToString();
                        if (lbl_hrin_r.Length <= 17)
                        {
                            lbl_hrin_r = proc.dateinsert.ToString().Substring(11, 8);
                        }
                        else if (lbl_hrin_r.Length >= 18)
                        {
                            lbl_hrin_r = proc.dateinsert.ToString().Substring(11, 8);
                        }


                        lbl_tppl_r = proc.Tipo.ToString();
                        lbl_fmt_r = proc.Formato.ToString();
                        lbl_qtfl_r = proc.TotalContagem.ToString();
                    }


                    System.Drawing.Font f1 = new System.Drawing.Font("Arial", 18, FontStyle.Regular, GraphicsUnit.Pixel);
                    System.Drawing.Font fmn = new System.Drawing.Font("Arial", 15, FontStyle.Bold, GraphicsUnit.Pixel);

                    System.Drawing.Brush brush = System.Drawing.Brushes.Black;

                    int x = (int)(148 * (96 / 25.4f));
                    int y = (int)(105 * (96 / 25.4f));

                    Bitmap bitmap = new Bitmap(x, y);

                    int wid = (int)(35 * 96 / 25.4f);
                    int hei = (int)(12 * 96 / 25.4f);

                    System.Drawing.Pen blackPen = new System.Drawing.Pen(System.Drawing.Color.Black, 2);

                    using (Graphics graphics = Graphics.FromImage(bitmap))
                    {
                        graphics.Clear(System.Drawing.Color.White);

                        //Draw Black lines


                        //op
                        graphics.DrawLine(blackPen, 72f, 30f, 200f, 30f);

                        //cliente
                        graphics.DrawLine(blackPen, 280f, 30f, 540f, 30f);

                        //peso
                        graphics.DrawLine(blackPen, 65f, 80f, 200f, 80f);

                        //qtfl
                        graphics.DrawLine(blackPen, 309f, 80f, 440f, 80f);

                        //tipo papel
                        graphics.DrawLine(blackPen, 110f, 130f, 225f, 130f);

                        //formato
                        graphics.DrawLine(blackPen, 314f, 130f, 388f, 130f);

                        //gram
                        graphics.DrawLine(blackPen, 449f, 130f, 540f, 130f);

                        //data inicio
                        graphics.DrawLine(blackPen, 110f, 180f, 225f, 180f);

                        //data termino
                        graphics.DrawLine(blackPen, 355f, 180f, 470f, 180f);

                        //horario inicial
                        graphics.DrawLine(blackPen, 132f, 230f, 225f, 230f);

                        //horario final
                        graphics.DrawLine(blackPen, 343f, 230f, 470f, 230f);

                        //operador
                        graphics.DrawLine(blackPen, 100f, 280f, 340f, 280f);

                        //turno
                        graphics.DrawLine(blackPen, 404f, 280f, 540f, 280f);





                        //op
                        graphics.DrawString(lbl_op, f1, brush, new PointF(12, 10));
                        graphics.DrawString(lbl_op_r, f1, brush, new PointF(74, 10));

                        //cliente
                        graphics.DrawString(lbl_cli, f1, brush, new PointF(210, 10));
                        graphics.DrawString(lbl_cli_r, f1, brush, new PointF(281, 10));

                        //peso
                        graphics.DrawString(lbl_peso, f1, brush, new PointF(12, 60));
                        graphics.DrawString(lbl_peso_r, f1, brush, new PointF(65, 60));

                        //qt folhas
                        graphics.DrawString(lbl_qtfl, f1, brush, new PointF(208, 60));
                        graphics.DrawString(lbl_qtfl_r, f1, brush, new PointF(308, 60));

                        //tp papel
                        graphics.DrawString(lbl_tppl, f1, brush, new PointF(12, 110));
                        graphics.DrawString(lbl_tppl_r, f1, brush, new PointF(112, 110));

                        //formato
                        graphics.DrawString(lbl_fmt, f1, brush, new PointF(236, 110));
                        graphics.DrawString(lbl_fmt_r, f1, brush, new PointF(316, 110));

                        //gram
                        graphics.DrawString(lbl_gr, f1, brush, new PointF(393, 110));
                        graphics.DrawString(lbl_gr_r, f1, brush, new PointF(450, 110));

                        //data inicio
                        graphics.DrawString(lbl_dtin, f1, brush, new PointF(12, 160));
                        graphics.DrawString(lbl_dtin_r, f1, brush, new PointF(112, 160));

                        //data Término
                        graphics.DrawString(lbl_dtfm, f1, brush, new PointF(236, 160));
                        graphics.DrawString(lbl_dtfm_r, f1, brush, new PointF(356, 160));

                        //horário inicial
                        graphics.DrawString(lbl_hrin, f1, brush, new PointF(12, 210));
                        graphics.DrawString(lbl_hrin_r, f1, brush, new PointF(132, 210));

                        //horário final
                        graphics.DrawString(lbl_hrfm, f1, brush, new PointF(236, 210));
                        graphics.DrawString(lbl_hrfm_r, f1, brush, new PointF(346, 210));

                        graphics.DrawString(lbl_opr, f1, brush, new PointF(12, 260));
                        graphics.DrawString(lbl_opr_r, f1, brush, new PointF(100, 260));

                        graphics.DrawString(lbl_trn, f1, brush, new PointF(346, 260));
                        graphics.DrawString(lbl_trn_r, f1, brush, new PointF(406, 260));

                        graphics.DrawString(lbl_obs, f1, brush, new PointF(12, 305));
                        graphics.DrawString(split1, fmn, brush, new PointF(56, 308));
                        graphics.DrawString(split2, fmn, brush, new PointF(56, 333));
                        graphics.DrawString(split3, fmn, brush, new PointF(56, 358));


                    }

                    ZPLPrintingService prnSvc = new ZPLPrintingService();
                    //Bitmap bmp = RotateBitmap(bitmap, 90);
                    zplCode = await prnSvc.GetImageZPLEncoded(bitmap);
                    zplCode = zplCode.Replace("#barcode#", lbl_op);
                    //Console.WriteLine(zplCode);

                    PrintDocument documento = new PrintDocument();
                    PrinterSettings configImpressora = new PrinterSettings();
                    PageSettings pageSettings = documento.DefaultPageSettings;

                    string[] impressoras = PrinterSettings.InstalledPrinters.Cast<string>().ToArray();
                    string name = "";
                    foreach (string item in impressoras)
                    {

                        if (item == impressora.impressora) { name = impressora.impressora; }
                    }

                    //bitmap.RotateFlip(RotateFlipType.Rotate90FlipNone);
                    PrintPopup print = new PrintPopup(bitmap);
                    print.ShowDialog();

                    if (!string.IsNullOrWhiteSpace(name))
                    {
                        configImpressora.PrinterName = name;
                        documento.PrinterSettings = configImpressora;
                        documento.DefaultPageSettings.Landscape = true;
                    }

                    documento.PrintPage += (sender, args) =>
                    {
                        args.Graphics.DrawImage(bitmap, 0, 0); // Ajuste a posição conforme necessário
                    };

                    documento.Print();
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}

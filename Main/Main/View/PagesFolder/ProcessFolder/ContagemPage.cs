using Main.Model;
using Main.Service;
using Main.View.MainFolder;
using Main.View.PopupFolder;
using Org.BouncyCastle.Asn1.Cms;
using Org.BouncyCastle.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media.Animation;

namespace Main.View.PagesFolder.ProcessFolder
{
    public partial class ContagemPage : Form
    {
        private ProcessosModel processo_atual { get; set; }

        int tickCount = 0;
        int salvarfolhas = 0;
        int contagemTotal = 0;
        int indiceContador = 0;
        int indiceReferencia = 0;
        int salvarEstabilizacao = 0;

        double Peso = 0;
        double Gramatura = 0;
        double pesoTotal = 0;
        double Peso_Salvo = 0;

        int retorno0 = 0;
        int statusProcesso = 0;
        int folhasRegistradas = 0;

        Boolean Estado = true;
        Boolean Estabilizou = false;
        Boolean contagemAtiva = false;

        long tempo_salvo = new long();
        long tempo_salvo_retorno = new long();

        Image[] imgs_peso = new Image[0];
        Stopwatch executation_time = new Stopwatch();
        System.Timers.Timer tm_process = new System.Timers.Timer();


        public ContagemPage(int id_Processo)
        {
            InitializeComponent();

            processo_atual = (ProcessosModel)Program.SQL.SelectObject("SELECT * FROM Processos WHERE id = @id", "Processos",
                new Dictionary<string, object>()
                {
                    {"@id", id_Processo}
                });


            contagemTotal = processo_atual.TotalContagem;
            pesoTotal = processo_atual.PesoTotal;


            imgs_peso = new Image[]
            {
                Properties.Resources.await,
                Properties.Resources.industrial_scales_connecting_99px,
                Properties.Resources.industrial_scales_connected_filled_99px__1_
            };

            tm_process.Interval = 250;
            tm_process.Elapsed += Tm_process_Elapsed;

            //processo_atual.GramaturaDigitada = Math.Round(processo_atual.GramaturaDigitada, 3);
        }


        private async void ContagemPage_Load(object sender, EventArgs e)
        {
            try
            {
                statusProcesso = processo_atual.StatusProcesso;
                Gramatura = Convert.ToDouble(processo_atual.Gramatura);
                lbl_DataInsert.Text = Convert.ToString(processo_atual.dateinsert);

                lbl_Horario.Text = ConvertMillisecondsToTime(Convert.ToInt64(processo_atual.TempoExecucao));

                lbl_qtMinima.Text = processo_atual.Quantidade.ToString();
                lbl_MateriaPrima.Text = processo_atual.Numero + " - " + processo_atual.Papel + " " + processo_atual.Formato;
                infosprocesso_txt.Text = processo_atual.Cliente + " - " + processo_atual.Op;
                lbl_DataInsert.Text = processo_atual.dateinsert.ToString();

                lbl_PesoReferencia.Text = "1 folha ≅ " + Convert.ToString(Math.Round(Gramatura, 4));

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

                    //STATUS
                    lbl_Status.Invoke(new MethodInvoker(() =>
                    {
                        lbl_Status.Text = "";
                        lbl_Status.Text = "AGUARDANDO RETOMADA DE PROCESSO...";
                    }));
                    
                    btn_IniciarContagem.Text = "RETOMAR PROCESSO";
                }

                if (lbl_Horario.Text == "")
                {
                    lbl_Horario.Text = "00:00:00";
                }

                lbl_ValorReal.Text = processo_atual.TotalContagem.ToString();

                if (processo_atual.Descricao == "")
                {
                    lbl_Descricao.Text = "Processo sem observação.";
                }
                else 
                {
                    lbl_Descricao.Text = processo_atual.Descricao.ToString();
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

                if (SerialCommunicationService.SERIALPORT1.IsOpen)
                {
                    taraReferencia.Tag = Program.Endereco_Referencia;
                    zeroReferencia.Tag = Program.Endereco_Referencia;

                    var slctContagem = Program.SQL.SelectList("SELECT * FROM Rede WHERE Tipo = 'Balança' AND parent = @parent AND addr <> @addr", "Rede", null,
                    new Dictionary<string, object>()
                    {
                        {"@parent", Program.CFG.Estação },
                        {"@addr", Program.Endereco_Referencia }
                    });

                    foreach (RedeClass rd in slctContagem)
                    {
                        taraContagem.Tag = rd.addr;
                        zeroContagem.Tag = rd.addr;
                    }

                }

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

                tm_process.Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }


        public static string ConvertMillisecondsToTime(long milliseconds)
        {
            TimeSpan timeSpan = TimeSpan.FromMilliseconds(milliseconds);
            return timeSpan.ToString(@"hh\:mm\:ss");
        }


        private async void Tm_process_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                string referencia = $"{SerialCommunicationService.indicador_addr[indiceReferencia].PS}";
                string contador = $"{SerialCommunicationService.indicador_addr[indiceContador].PS}";
                tickCount++;

                valorReferencia.Invoke(new MethodInvoker(delegate 
                {
                    valorReferencia.Text = $"{referencia}";
                }));

                valorContagem.Invoke(new MethodInvoker(delegate
                {
                    valorContagem.Text = $"{contador}";
                }));

                if (tickCount >= 4)
                {

                    Int64 tempo_execucao = Convert.ToInt64(processo_atual.TempoExecucao) + Convert.ToInt64(executation_time.ElapsedMilliseconds);

                    string timeFormatted = ConvertMillisecondsToTime(tempo_execucao);


                    lbl_Horario.Invoke(new MethodInvoker(delegate
                    {
                        lbl_Horario.Text = timeFormatted;
                    }));

                    tm_process.Stop();

                    var UpdateProcesso = Program.SQL.CRUDCommand("UPDATE Processos SET Tempo_execucao = @Tempo_execucao WHERE Id = @Id", "Processos",
                    new Dictionary<string, object>()
                    {
                        {"@Id", processo_atual.Id },
                        {"@Tempo_execucao", tempo_execucao }
                    }); 
                    
                    tickCount = 0;

                    tm_process.Start();
                }



                if (SerialCommunicationService.SERIALPORT1.IsOpen == true)
                {

                }
                else
                {
                    this.Invoke(new MethodInvoker(() =>
                    {
                        PesagemForms form = new PesagemForms(Program._usuarioLogado.Id, Program._usuarioLogado.Nome);

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

                if (contagemAtiva) 
                {
                    Peso = Convert.ToDouble($"{SerialCommunicationService.indicador_addr[indiceContador].PS}");
                    Estado = Convert.ToBoolean($"{SerialCommunicationService.indicador_addr[indiceContador].Estavel}");

                    var chk3folhas = Gramatura * 3;
                    double gram_formatada = Peso / Math.Round(Gramatura, 4);

                    if (gram_formatada != 0)
                    {
                        this.Invoke(new MethodInvoker(() =>
                        {
                            lbl_QtContab.Text = Convert.ToInt32(gram_formatada).ToString();
                        }));
                    }
                    else
                    {
                        this.Invoke(new MethodInvoker(() =>
                        {
                            lbl_QtContab.Text = "0";
                        }));
                    }

                    Console.WriteLine(Estado.ToString());

                    if (Peso > chk3folhas)
                     {
                        try
                        {
                            if (Estado == true)
                            {
                                if (Peso != Peso_Salvo)
                                {

                                    if (Peso > chk3folhas && Estabilizou == true)
                                    {
                                        await Task.Delay(3000);
                                        if (Peso > chk3folhas)
                                        {
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

                                            salvarEstabilizacao = 0;
                                        }
                                        else
                                        {
                                            pict_Status.BackColor = Color.FromArgb(41, 46, 84);
                                            panel12.BackColor = Color.FromArgb(41, 46, 84);
                                            panel20.BackColor = Color.FromArgb(41, 46, 84);
                                            lbl_Status.BackColor = Color.FromArgb(41, 46, 84);

                                            pict_Status.Image = imgs_peso[1];

                                            lbl_Status.Invoke(new MethodInvoker(() =>
                                            {
                                                lbl_Status.Text = "";
                                                lbl_Status.Text = "PESO ESTABILIZADO. RETIRE OU ADICIONE MAIS MATÉRIA-PRIMA.";
                                            }));

                                            Estabilizou = true;
                                            salvarEstabilizacao = 1;
                                        }
                                    }
                                }

                                if (salvarEstabilizacao == 0)
                                {
                                    tempo_salvo = executation_time.ElapsedMilliseconds;
                                    Peso_Salvo = Peso;
                                    Estabilizou = false;
                                    salvarEstabilizacao = 1;
                                }

                                if (Estabilizou == false)
                                {

                                    if (executation_time.ElapsedMilliseconds > (tempo_salvo + 1000))
                                    {
                                        if (Estado == true && Peso_Salvo == Peso)
                                        {
                                            if (executation_time.ElapsedMilliseconds > (tempo_salvo + 2000))
                                            {
                                                if (Estado == true)
                                                {
                                                    if (executation_time.ElapsedMilliseconds > (tempo_salvo + 3000))
                                                    {
                                                        if (Estado == true)
                                                        {
                                                            Peso = Convert.ToDouble($"{SerialCommunicationService.indicador_addr[indiceContador].PS}");

                                                            pict_Status.BackColor = Color.FromArgb(41, 46, 84);
                                                            panel12.BackColor = Color.FromArgb(41, 46, 84);
                                                            panel20.BackColor = Color.FromArgb(41, 46, 84);
                                                            lbl_Status.BackColor = Color.FromArgb(41, 46, 84);

                                                            pict_Status.Image = imgs_peso[1];

                                                            lbl_Status.Invoke(new MethodInvoker(() =>
                                                            {
                                                                lbl_Status.Text = "";
                                                                lbl_Status.Text = "PESO ESTABILIZADO. RETIRE OU ADICIONE MAIS MATÉRIA-PRIMA.";
                                                            }));

                                                            if (Peso == Peso_Salvo)
                                                            {
                                                                Estabilizou = true;
                                                            }
                                                            else
                                                            {
                                                                Peso_Salvo = Peso;
                                                                salvarEstabilizacao = 0;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            Peso_Salvo = Peso;
                                                            salvarEstabilizacao = 0;

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

                                                            salvarfolhas = 0;
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    Peso_Salvo = Peso;
                                                    salvarEstabilizacao = 0;

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

                                                    salvarfolhas = 0;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            Peso_Salvo = Peso;
                                            salvarEstabilizacao = 0;

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

                                            salvarfolhas = 0;
                                        }
                                    }
                                }
                            }
                            else if (Estado == false && Estabilizou != true)
                            {
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

                                salvarfolhas = 0;
                            }
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                    else if (Peso <= chk3folhas)
                    {
                        //SerialCommunicationService.SendCommand(Convert.ToInt32(taraContagem.Tag), 1);

                        if (Peso != Peso_Salvo && Peso_Salvo > chk3folhas && Estabilizou == true)
                        {
                            if (salvarfolhas == 0)
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

                                tempo_salvo = executation_time.ElapsedMilliseconds;
                                salvarfolhas = 1;
                            }
                            else
                            {
                                if(executation_time.ElapsedMilliseconds > (tempo_salvo + 2000))
                                {
                                    folhasRegistradas = Convert.ToInt32(Peso_Salvo / Math.Round(Gramatura, 5));

                                    this.Invoke(new MethodInvoker(() =>
                                    {
                                        lbl_ValorReal.Text = folhasRegistradas.ToString();
                                    }));

                                    string tempoexec = lbl_Horario.Text;




                                    this.Invoke(new MethodInvoker(() =>
                                    {
                                        pesoTotal += (folhasRegistradas * Convert.ToDouble(Gramatura));
                                        contagemTotal = contagemTotal + folhasRegistradas;
                                        lbl_ValorReal.Text = contagemTotal.ToString();

                                    }));

                                    salvarEstabilizacao = 0;

                                    var insertLog = Program.SQL.CRUDCommand("INSERT INTO Log_Processos (Id_processo, qtd_temporeal, qtd_total, Tempo_execucao, dateinsert, Peso) VALUES (@Id_processo, @qtd_temporeal, @qtd_total, @Tempo_execucao, @dateinsert, @Peso)", "Log_Processos",
                                    new Dictionary<string, object>()
                                    {
                                        {"@Id_processo", processo_atual.Id },
                                        {"@qtd_temporeal", folhasRegistradas },
                                        {"@qtd_total", contagemTotal },
                                        {"@Peso", Peso_Salvo },
                                        {"@Tempo_execucao", tempoexec },
                                        {"@dateinsert", DateTime.Now}
                                    });

                                    if (insertLog) { Peso_Salvo = 0; }

                                    //=================================================================================================================//


                                    Int64 tempo_execucao = Convert.ToInt64(processo_atual.TempoExecucao) + Convert.ToInt64(executation_time.ElapsedMilliseconds);

                                    var UpdateProcesso = Program.SQL.CRUDCommand("UPDATE Processos SET Descricao = @Descricao, Tempo_execucao = @Tempo_execucao, Total_contagem = @Total_contagem, Peso_Total = @Peso_Total, Status_processo = @Status_processo WHERE Id = @Id", "Processos",
                                    new Dictionary<string, object>()
                                    {
                                        {"@Id", processo_atual.Id },
                                        {"@Descricao", processo_atual.Descricao },
                                        {"@Tempo_execucao", tempo_execucao},
                                        {"@Total_contagem", contagemTotal },
                                        {"@Peso_Total", Math.Round(pesoTotal) },
                                        {"@Status_processo", statusProcesso },
                                    });

                                    Peso_Salvo = Peso;
                                    salvarfolhas = 0;
                                }
                            }
                        }
                        else
                        {
                            if (folhasRegistradas == 0)
                            {
                                //Referencia registrada, aguardando Matéria Prima
                                this.Invoke(new MethodInvoker(() => {
                                    pict_Status.BackColor = Color.FromArgb(41, 46, 84);
                                    panel12.BackColor = Color.FromArgb(41, 46, 84);
                                    panel20.BackColor = Color.FromArgb(41, 46, 84);
                                    lbl_Status.BackColor = Color.FromArgb(41, 46, 84);
                                    lbl_Status.Text = "";
                                    lbl_Status.Text = "AGUARDANDO MATÉRIA-PRIMA...";
                                }));
                            }
                            else
                            {
                                //Peso registrado, aguardando matéria Prima
                                this.Invoke(new MethodInvoker(() => {
                                    pict_Status.BackColor = Color.SeaGreen;
                                    panel12.BackColor = Color.SeaGreen;
                                    panel20.BackColor = Color.SeaGreen;
                                    lbl_Status.BackColor = Color.SeaGreen;
                                    pict_Status.Image = imgs_peso[2];
                                    lbl_Status.Text = "";
                                    lbl_Status.Text = "PESO REGISTRADO. AGUARDANDO MATÉRIA-PRIMA...";
                                }));
                            }

                            //statusProcesso = 0;
                        }                   
                    }




                }

                //if (executation_time.Elapsed.Seconds > 5) 
                //{
                //    bool updateTime = Program.SQL.CRUDCommand("UPDATE Processos SET Tempo_execucao = @temp, Status_processo = @Status_processo  WHERE id = @id", "Processos",
                //        new Dictionary<string, object>()
                //        {
                //            {"@temp", $"{executation_time.ElapsedMilliseconds}"},
                //            {"@Status_processo", statusProcesso},
                //            {"@id", processo_atual.Id},
                //        });
                //}
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private void btn_SalvarReferencia_Click(object sender, EventArgs e)
        {
            try
            {
                double valor_referencia = Convert.ToDouble(SerialCommunicationService.indicador_addr[indiceReferencia].PS);

                if (Convert.ToDouble(valor_referencia) > 0.0005)
                {
                    YesOrNo question = new YesOrNo("Confirmar que a quantidade para referência (" + processo_atual.Quantidade + ") está correta na balança?");
                    question.ShowDialog();

                    if (question.RESPOSTA) 
                    {
                        pict_Status.Image = imgs_peso[1];
                        statusProcesso = 1;
                        Gramatura = Convert.ToDouble($"{SerialCommunicationService.indicador_addr[indiceReferencia].PS}") / Convert.ToDouble(lbl_qtMinima.Text);

                        lbl_Status.Text = "";
                        lbl_Status.Text = "REFERÊNCIA REGISTRADA. INICIE A CONTAGEM.";

                        var UpdateProcesso = Program.SQL.CRUDCommand("UPDATE Processos SET Gramatura = @Gramatura, Status_processo = @Status_processo WHERE Id = @Id", "Processos",
                        new Dictionary<string, object>()
                    {
                            {"@Id", processo_atual.Id },
                            {"@Gramatura", Gramatura },
                            {"@Status_processo", statusProcesso },
                        });

                        lbl_Status.Refresh();

                        zeroReferencia.Enabled = false;
                        taraReferencia.Enabled = false;

                        lbl_PesoReferencia.Text = "1 folha ≅ " + Math.Round(Gramatura, 4).ToString();

                        lbl_PesoReferencia.Refresh();

                        YesOrNo iniciarContagem = new YesOrNo("Deseja iniciar a contagem?");
                        iniciarContagem.ShowDialog();

                        if (iniciarContagem.RESPOSTA)
                        {
                            statusProcesso = 2;
                            executation_time.Start();

                            //STATUS
                            pict_Status.BackColor = Color.FromArgb(41, 46, 84);
                            panel12.BackColor = Color.FromArgb(41, 46, 84);
                            panel20.BackColor = Color.FromArgb(41, 46, 84);
                            lbl_Status.BackColor = Color.FromArgb(41, 46, 84);

                            lbl_Status.Text = "";
                            lbl_Status.Text = "AGUARDANDO MATÉRIA-PRIMA...";

                            contagemAtiva = true;
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
                            contagemAtiva = false;
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


                        btn_SalvarReferencia.Refresh();
                        btn_SalvarReferencia.Enabled = false;
                    }

                }
                else
                {
                    InfoPopup info = new InfoPopup("Erro", "O peso de referência não pode ser zero ou negativo!", Properties.Resources.errorIcon);
                    info.ShowDialog();
                }
            }
            catch (Exception)
            {
            }
        }

        private void btn_IniciarContagem_Click(object sender, EventArgs e)
        {
            try
            {
                if (statusProcesso > 0)
                {
                    if (btn_IniciarContagem.Text == "INICIAR CONTAGEM")
                    {
                        YesOrNo question = new YesOrNo("Deseja iniciar a contagem?");
                        question.ShowDialog();

                        if (question.RESPOSTA)
                        {
                            contagemAtiva = true;
                            statusProcesso = 2;
                            executation_time.Start();

                            pict_Status.BackColor = Color.FromArgb(41, 46, 84);
                            panel12.BackColor = Color.FromArgb(41, 46, 84);
                            panel20.BackColor = Color.FromArgb(41, 46, 84);
                            lbl_Status.BackColor = Color.FromArgb(41, 46, 84);

                            lbl_Status.Text = "";
                            lbl_Status.Text = "AGUARDANDO MATÉRIA-PRIMA...";

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
                            contagemAtiva = true;
                            executation_time.Start();

                            lbl_Status.Text = "";
                            lbl_Status.Text = "AGUARDANDO MATÉRIA-PRIMA...";

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
                                    //valorTotal += Convert.ToInt32(qtContabTotal);
                                }
                            }

                            statusProcesso = 3;
                            executation_time.Stop();

                            var soma = Program.SQL.SelectList("SELECT SUM(Peso) AS SOMA FROM Log_Processos Where Id_processo = @Id", "Log_Processos", "SOMA", new Dictionary<string, object>()
                            {
                                {"@Id", processo_atual.Id }
                            });


                            double pesoTotal = 0;
                            if (soma.Count > 0)
                            {
                                pesoTotal = Math.Round(Convert.ToDouble(soma[0]), 4);
                            }
                            else
                            {
                                //pesoTotal = valorTotal * Convert.ToDouble(Gramatura);
                            }



                            Int64 tempo_execucao = Convert.ToInt64(processo_atual.TempoExecucao) + Convert.ToInt64(executation_time.ElapsedMilliseconds);

                            var UpdateProcesso = Program.SQL.CRUDCommand("UPDATE Processos SET Descricao = @Descricao, Tempo_execucao = @Tempo_execucao, Total_contagem = @Total_contagem, Gramatura = @Gramatura, Peso_total = @Peso_total, Status_processo = @Status_processo, dateend = @dateend WHERE Id = @Id", "Processos",
                            new Dictionary<string, object>()
                            {
                                {"@Id", processo_atual.Id },
                                {"@Descricao", processo_atual.Descricao},
                                {"@Tempo_execucao", tempo_execucao},
                                {"@Total_contagem", contagemTotal},
                                {"@Gramatura", Gramatura },
                                {"@Peso_total", pesoTotal },
                                {"@Status_processo", 3 },
                                {"@dateend", DateTime.Now }
                            });


                            InfoPopup info = new InfoPopup("Parabéns!", "Processo e contagem registrados com sucesso!", Properties.Resources._299110_check_sign_icon);
                            info.ShowDialog();

                            SerialCommunicationService.LoadImpressoras();
                            SerialCommunicationService.ImpressoraPrint(processo_atual);
                            //ImpressoraPrint();

                            MainInfoForms form = new MainInfoForms(Program._usuarioLogado.Id, Program._usuarioLogado.Nome);

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
            catch (Exception)
            {

                throw;
            }
        }

        private void valorContagem_TextChanged(object sender, EventArgs e)
        {

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

        private void button2_Click(object sender, EventArgs e)
        {
            VisualizarLogForms visu = new VisualizarLogForms(processo_atual.Id);
            visu.ShowDialog();
        }
    }
}

using LiveCharts.Charts;
using Main.Model;
using Main.Service;
using Main.View.CommunicationFolder;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Main.View.PagesFolder.ProcessFolder
{
    public partial class ProcessForms : Form
    {
        int timeSec, timeMin, timeH;
        Boolean tmpExectAtivo;

        bool isAtivo = true;

        decimal pesoReferencia;
        decimal qtContab = 0;

        //Comunicação
        public System.Timers.Timer tmRead = new System.Timers.Timer();
        public bool availableStatus = true;

        int indiceReferencia = 0;
        int indiceContador = 0;

        int contadorValor = 0;

        int valorGuardado = 0;
        int valorAntes = 0;
        int valorContabilizado = 0;

        public ProcessForms(int id_Usuario, int id_MateriaPrima, int qt_Minima, string dsc_MateriaPrima)
        {
            InitializeComponent();

            //SerialCommunicationService.InitWithAutoConnect();

            lbl_qtMinima.Text = qt_Minima.ToString();
            lbl_Descricao.Text = dsc_MateriaPrima;
        }

        private void ProcessForms_Load(object sender, EventArgs e)
        {
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

            Task.Run(async () =>
            {
                try
                {
                    while (true)
                    {
                        valorReferencia.Invoke(new MethodInvoker(() =>
                        {
                            valorReferencia.Text = $"{SerialCommunicationService.indicador_addr[indiceReferencia].PS}";
                            valorReferencia.Text = valorReferencia.Text.Replace(",", ".");
                        }));

                        valorContagem.Invoke(new MethodInvoker(() =>
                        {
                            valorContagem.Text = $"{SerialCommunicationService.indicador_addr[indiceContador].PS}";
                            valorContagem.Text = valorContagem.Text.Replace(",", ".");
                        }));

                        if (btn_IniciarContagem.Text == "Terminar Contagem")
                        {
                            qtContab = Convert.ToDecimal(valorContagem.Text) / pesoReferencia;
                            qtContab = Convert.ToDecimal($"{SerialCommunicationService.indicador_addr[indiceContador].PS}") / pesoReferencia;

                            this.Invoke(new MethodInvoker(() =>
                            {
                                if (contadorValor == 0)
                                {
                                    valorAntes = Convert.ToInt32(qtContab);
                                }

                                lbl_QtContab.Text = Convert.ToInt32(qtContab).ToString();

                                valorGuardado = Convert.ToInt32(qtContab);

                                contadorValor++;
                            }));
                        }

                        if (Convert.ToInt32(qtContab) == 0)
                        {
                            this.Invoke(new MethodInvoker(() =>
                            {
                                pctg10.BackColor = Color.Gray;
                                pctg20.BackColor = Color.Gray;
                                pctg30.BackColor = Color.Gray;
                                pctg40.BackColor = Color.Gray;
                                pctg50.BackColor = Color.Gray;
                                pctg60.BackColor = Color.Gray;
                                pctg70.BackColor = Color.Gray;
                                pctg80.BackColor = Color.Gray;
                                pctg90.BackColor = Color.Gray;
                                pctg100.BackColor = Color.Gray;
                            }));
                        }
                        else if (Convert.ToInt32(qtContab) == 1) 
                        {
                            this.Invoke(new MethodInvoker(() =>
                            {
                                pctg10.BackColor = Color.Green;
                                pctg20.BackColor = Color.Gray;
                                pctg30.BackColor = Color.Gray;
                                pctg40.BackColor = Color.Gray;
                                pctg50.BackColor = Color.Gray;
                                pctg60.BackColor = Color.Gray;
                                pctg70.BackColor = Color.Gray;
                                pctg80.BackColor = Color.Gray;
                                pctg90.BackColor = Color.Gray;
                                pctg100.BackColor = Color.Gray;
                            }));
                        }
                        else if (Convert.ToInt32(qtContab) == 2)
                        {
                            this.Invoke(new MethodInvoker(() =>
                            {
                                pctg10.BackColor = Color.Green;
                                pctg20.BackColor = Color.Green;
                                pctg30.BackColor = Color.Gray;
                                pctg40.BackColor = Color.Gray;
                                pctg50.BackColor = Color.Gray;
                                pctg60.BackColor = Color.Gray;
                                pctg70.BackColor = Color.Gray;
                                pctg80.BackColor = Color.Gray;
                                pctg90.BackColor = Color.Gray;
                                pctg100.BackColor = Color.Gray;
                            }));
                        }
                        else if (Convert.ToInt32(qtContab) == 3)
                        {
                            this.Invoke(new MethodInvoker(() =>
                            {
                                pctg10.BackColor = Color.Green;
                                pctg20.BackColor = Color.Green;
                                pctg30.BackColor = Color.Green;
                                pctg40.BackColor = Color.Gray;
                                pctg50.BackColor = Color.Gray;
                                pctg60.BackColor = Color.Gray;
                                pctg70.BackColor = Color.Gray;
                                pctg80.BackColor = Color.Gray;
                                pctg90.BackColor = Color.Gray;
                                pctg100.BackColor = Color.Gray;
                            }));
                        }
                        else if (Convert.ToInt32(qtContab) == 4)
                        {
                            this.Invoke(new MethodInvoker(() =>
                            {
                                pctg10.BackColor = Color.Green;
                                pctg20.BackColor = Color.Green;
                                pctg30.BackColor = Color.Green;
                                pctg40.BackColor = Color.Green;
                                pctg50.BackColor = Color.Gray;
                                pctg60.BackColor = Color.Gray;
                                pctg70.BackColor = Color.Gray;
                                pctg80.BackColor = Color.Gray;
                                pctg90.BackColor = Color.Gray;
                                pctg100.BackColor = Color.Gray;
                            }));
                        }
                        else if (Convert.ToInt32(qtContab) == 5)
                        {
                            this.Invoke(new MethodInvoker(() =>
                            {
                                pctg10.BackColor = Color.Green;
                                pctg20.BackColor = Color.Green;
                                pctg30.BackColor = Color.Green;
                                pctg40.BackColor = Color.Green;
                                pctg50.BackColor = Color.Green;
                                pctg60.BackColor = Color.Gray;
                                pctg70.BackColor = Color.Gray;
                                pctg80.BackColor = Color.Gray;
                                pctg90.BackColor = Color.Gray;
                                pctg100.BackColor = Color.Gray;
                            }));
                        }
                        else if (Convert.ToInt32(qtContab) == 6)
                        {
                            this.Invoke(new MethodInvoker(() =>
                            {
                                pctg10.BackColor = Color.Green;
                                pctg20.BackColor = Color.Green;
                                pctg30.BackColor = Color.Green;
                                pctg40.BackColor = Color.Green;
                                pctg50.BackColor = Color.Green;
                                pctg60.BackColor = Color.Green;
                                pctg70.BackColor = Color.Gray;
                                pctg80.BackColor = Color.Gray;
                                pctg90.BackColor = Color.Gray;
                                pctg100.BackColor = Color.Gray;
                            }));
                        }
                        else if (Convert.ToInt32(qtContab) == 7)
                        {
                            this.Invoke(new MethodInvoker(() =>
                            {
                                pctg10.BackColor = Color.Green;
                                pctg20.BackColor = Color.Green;
                                pctg30.BackColor = Color.Green;
                                pctg40.BackColor = Color.Green;
                                pctg50.BackColor = Color.Green;
                                pctg60.BackColor = Color.Green;
                                pctg70.BackColor = Color.Green;
                                pctg80.BackColor = Color.Gray;
                                pctg90.BackColor = Color.Gray;
                                pctg100.BackColor = Color.Gray;
                            }));
                        }
                        else if (Convert.ToInt32(qtContab) == 8)
                        {
                            this.Invoke(new MethodInvoker(() =>
                            {
                                pctg10.BackColor = Color.Green;
                                pctg20.BackColor = Color.Green;
                                pctg30.BackColor = Color.Green;
                                pctg40.BackColor = Color.Green;
                                pctg50.BackColor = Color.Green;
                                pctg60.BackColor = Color.Green;
                                pctg70.BackColor = Color.Green;
                                pctg80.BackColor = Color.Green;
                                pctg90.BackColor = Color.Gray;
                                pctg100.BackColor = Color.Gray;
                            }));
                        }
                        else if (Convert.ToInt32(qtContab) == 9)
                        {
                            this.Invoke(new MethodInvoker(() =>
                            {
                                pctg10.BackColor = Color.Green;
                                pctg20.BackColor = Color.Green;
                                pctg30.BackColor = Color.Green;
                                pctg40.BackColor = Color.Green;
                                pctg50.BackColor = Color.Green;
                                pctg60.BackColor = Color.Green;
                                pctg70.BackColor = Color.Green;
                                pctg80.BackColor = Color.Green;
                                pctg90.BackColor = Color.Green;
                                pctg100.BackColor = Color.Gray;
                            }));
                        }
                        else if (Convert.ToInt32(qtContab) >= 10)
                        {
                            this.Invoke(new MethodInvoker(() =>
                            {
                                pctg10.BackColor = Color.Green;
                                pctg20.BackColor = Color.Green;
                                pctg30.BackColor = Color.Green;
                                pctg40.BackColor = Color.Green;
                                pctg50.BackColor = Color.Green;
                                pctg60.BackColor = Color.Green;
                                pctg70.BackColor = Color.Green;
                                pctg80.BackColor = Color.Green;
                                pctg90.BackColor = Color.Green;
                                pctg100.BackColor = Color.Green;
                            }));
                        }


                        //await Task.Delay(3000);

                        //if (valorAntes != valorGuardado)
                        //{
                        //    if (valorGuardado == 0)
                        //    {
                        //        valorContabilizado += valorGuardado;

                        //        lbl_ValorReal.Text = valorContabilizado.ToString();

                        //        contadorValor = 0;
                        //    }
                        //}
                    }
                }
                catch (Exception)
                {
                }
            });

            //Console.WriteLine("Teste");

            if (SerialCommunicationService.SERIALPORT1.IsOpen)
            {
                taraReferencia.Tag = Program.Endereco_Referencia;
                zeroReferencia.Tag = Program.Endereco_Referencia;

                taraContagem.Tag = Program.Endereco_Referencia + 1;
                zeroContagem.Tag = Program.Endereco_Referencia + 1;
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

        private void ProcessForms_SizeChanged(object sender, EventArgs e)
        {
            this.Invalidate();
            this.Refresh();
            this.Update();
        }

        private void btn_IniciarContagem_Click(object sender, EventArgs e)
        {
            try
            {
                tmpExectAtivo = true;
                TimerRelogio.Start();
                lbl_Status.Text = "Em andamento";

                this.Invoke(new MethodInvoker(() =>
                {
                    btn_IniciarContagem.Text = "Terminar Contagem";
                }));
            }
            catch (Exception ex)
            {

            }
        }

        private void TimerRelogio_Tick_1(object sender, EventArgs e)
        {
            if (tmpExectAtivo)
            {
                timeSec++;

                if (timeSec > 60)
                {
                    timeMin++;
                    timeSec = 0;

                    if (timeMin > 60)
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

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btn_SalvarReferencia_Click(object sender, EventArgs e)
        {
            try
            {
                btn_IniciarContagem.Enabled = true;
                btn_IniciarContagem.ForeColor = Color.Green;
                btn_IniciarContagem.BackColor = Color.FromArgb(192, 255, 192);

                btn_SalvarReferencia.Enabled = false;
                btn_SalvarReferencia.ForeColor = Color.FromArgb(64, 64, 64);
                btn_SalvarReferencia.BackColor = Color.Silver;

                pesoReferencia = Convert.ToDecimal($"{SerialCommunicationService.indicador_addr[indiceReferencia].PS}") / Convert.ToDecimal(lbl_qtMinima.Text);
                Load_Referencia.Visible = false;

            }
            catch (Exception ex) 
            { 
            }
        }
    }
}
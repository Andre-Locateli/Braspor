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

        double pesoReferencia;
        decimal qtContab = 0;

        //Comunicação
        public System.Timers.Timer tmRead = new System.Timers.Timer();
        public bool availableStatus = true;

        public ProcessForms(int id_Usuario, int id_MateriaPrima, int qt_Minima, string dsc_MateriaPrima)
        {
            InitializeComponent();

            SerialCommunicationService.InitWithAutoConnect();

            lbl_qtMinima.Text = qt_Minima.ToString();
            lbl_Descricao.Text = dsc_MateriaPrima;
        }

        private void ProcessForms_Load(object sender, EventArgs e)
        {
            Task.Run(() =>
            {
                try
                {
                    while (true)
                    {
                        valorReferencia.Invoke(new MethodInvoker(() =>
                        {
                            if (SerialCommunicationService.indicadores_info.ContainsKey(Program.COMNAME_01))
                            {
                                valorReferencia.Text = $"{SerialCommunicationService.indicadores_info[Program.COMNAME_01].PS}";
                                valorReferencia.Text = valorReferencia.Text.Replace(",", ".");
                            }
                        }));

                        valorContagem.Invoke(new MethodInvoker(() =>
                        {
                            if (SerialCommunicationService.indicadores_info.ContainsKey(Program.COMNAME_02))
                            {
                                valorContagem.Text = $"{SerialCommunicationService.indicadores_info[Program.COMNAME_02].PS}";
                                valorContagem.Text = valorContagem.Text.Replace(",", ".");
                            }
                        }));

                        if(btn_IniciarContagem.Text == "Terminar Contagem")
                        {
                            //qtContab = Convert.ToDouble(valorContagem.Text) / pesoReferencia;
                            qtContab = Convert.ToDecimal($"{SerialCommunicationService.indicadores_info[Program.COMNAME_02].PS}") / Convert.ToDecimal(0.004795);

                            this.Invoke(new MethodInvoker(() =>
                            {
                                lbl_QtContab.Text = Convert.ToInt32(qtContab).ToString();
                            }));
                        }

                        if (qtContab == 0)
                        {
                            this.Invoke(new MethodInvoker(() =>
                            {
                                lbl_QtContab.Text = Convert.ToInt32(qtContab).ToString();
                            }));
                        }
                        else if (qtContab == 1) 
                        {
                            this.Invoke(new MethodInvoker(() =>
                            {
                                lbl_QtContab.Text = Convert.ToInt32(qtContab).ToString();
                            }));
                        }
                    }
                }
                catch (Exception)
                {
                }
            });
            Console.WriteLine("Teste");
            if (SerialCommunicationService.portInfo.ContainsKey(SerialCommunicationService.SERIALPORT1) && SerialCommunicationService.portInfo[SerialCommunicationService.SERIALPORT1].indicador.addr == Program.Endereco_Referencia)
            {
                taraReferencia.Tag = SerialCommunicationService.SERIALPORT1;
                zeroReferencia.Tag = SerialCommunicationService.SERIALPORT1;

                taraContagem.Tag = SerialCommunicationService.SERIALPORT2;
                zeroContagem.Tag = SerialCommunicationService.SERIALPORT2;
            }
            else if (SerialCommunicationService.portInfo.ContainsKey(SerialCommunicationService.SERIALPORT2) && SerialCommunicationService.portInfo[SerialCommunicationService.SERIALPORT2].indicador.addr == Program.Endereco_Referencia)
            {
                taraReferencia.Tag = SerialCommunicationService.SERIALPORT2;
                zeroReferencia.Tag = SerialCommunicationService.SERIALPORT2;

                taraContagem.Tag = SerialCommunicationService.SERIALPORT1;
                zeroContagem.Tag = SerialCommunicationService.SERIALPORT1;
            }

        }

        private void CommandButtonEventClick(object sender, EventArgs e)
        {
            try
            {

                PictureBox piCommand = (PictureBox)sender;

                if (piCommand.Name.Contains("tara"))
                {
                    SerialCommunicationService.SendCommand((SerialPort)piCommand.Tag, 0);
                }
                else if (piCommand.Name.Contains("zero"))
                {
                    SerialCommunicationService.SendCommand((SerialPort)piCommand.Tag, 1);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void taraContagem_Click(object sender, EventArgs e)
        {
            try
            {
                SerialCommunicationService.SendCommand(SerialCommunicationService.SERIALPORT2, 1);
            }
            catch (Exception)
            {

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

                pesoReferencia = Convert.ToDouble($"{SerialCommunicationService.indicadores_info[Program.COMNAME_01].PS}") / Convert.ToDouble(lbl_qtMinima.Text);
                Load_Referencia.Visible = false;

            }
            catch (Exception ex) 
            { 
            }
        }
    }
}
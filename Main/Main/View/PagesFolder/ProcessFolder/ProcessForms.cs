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

        //Comunicação
        public System.Timers.Timer tmRead = new System.Timers.Timer();
        public bool availableStatus = true;

        public ProcessForms()
        {
            InitializeComponent();

            SerialCommunicationService.InitWithAutoConnect();
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
    }
}
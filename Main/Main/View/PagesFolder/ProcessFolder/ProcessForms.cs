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
        SerialPort teste = new SerialPort();
        public bool availableStatus = true;

        public ProcessForms()
        {
            InitializeComponent();

            SerialCommunicationService.InitWithAutoConnect();
        }

        private void ProcessForms_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                label1.Text = $"Peso do Indicador 01: {SerialCommunicationService.indicadores_info[Program.COMNAME_01].PS}";
            }
            catch (Exception)
            {
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                label2.Text = $"Peso do Indicador 02: {SerialCommunicationService.indicadores_info[Program.COMNAME_01].PS}";
            }
            catch (Exception)
            {

            }
        }

    }
}

﻿using Main.Model;
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
            Task.Run(() =>
            {
                try
                {
                    while (true)
                    {
                        valorReferencia.Invoke(new MethodInvoker(() =>
                        {
                            valorReferencia.Text = $"{SerialCommunicationService.indicadores_info[Program.COMNAME_01].PS}";
                            valorReferencia.Text =  valorReferencia.Text.Replace(",", ".");
                        }));

                        valorContagem.Invoke(new MethodInvoker(() =>
                        {
                            valorContagem.Text = $"{SerialCommunicationService.indicadores_info[Program.COMNAME_02].PS}";
                            valorContagem.Text = valorContagem.Text.Replace(",", ".");
                        }));
                    }
                }
                catch (Exception)
                {
                }
            });
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
            }
            catch (Exception)
            {
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
            }
            catch (Exception)
            {

            }
        }

    }
}

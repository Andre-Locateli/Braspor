using Main.Helper;
using Main.View.MainFolder;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Media;
using System.Xml;
using Color = System.Drawing.Color;
using Pen = System.Drawing.Pen;

namespace Main.View.PagesFolder.Configuration
{
    public partial class SerialForm : Form
    {
        public SerialForm()
        {
            InitializeComponent();

            System.Management.ManagementEventWatcher watcher = new System.Management.ManagementEventWatcher();
            watcher.Query = new System.Management.WqlEventQuery("SELECT * FROM Win32_DeviceChangeEvent WHERE EventType = 2");
            watcher.EventArrived += new System.Management.EventArrivedEventHandler(DeviceChangeEvent);
            watcher.Start();
            chkAutoconnect.Checked = Program._autoconnect_1;
            chkAutoconnect2.Checked = Program._autoconnect_2;
        }

        private void SerialForm_Load(object sender, EventArgs e)
        {
            if (Program.SERIALPORT.IsOpen)
            {
                btnSalvar.Text = "Desconectar";
            }
            else
            {
                btnSalvar.Text = "Conectar";
            }
            if (Program.IMPRESSORAPORT.IsOpen)
            {
                btnConectar02.Text = "Desconectar";
            }
            else
            {
                btnConectar02.Text = "Conectar";
            }

            cbPortaSerial.Items.Clear();
            cbPortaSerial2.Items.Clear();
            cbBaudRate.Items.Clear();
            cbBaudRate2.Items.Clear();
            cbStopBit.Items.Clear();
            cbStopBit2.Items.Clear();
            cbParidade2.Items.Clear();

            string[] portNames = SerialPort.GetPortNames();
            cbPortaSerial.Items.AddRange(portNames); 
            cbPortaSerial2.Items.AddRange(portNames);

            cbBaudRate.Items.Add(1200);
            cbBaudRate.Items.Add(2400);
            cbBaudRate.Items.Add(4800);
            cbBaudRate.Items.Add(9600);
            cbBaudRate.Items.Add(19200);
            cbBaudRate.Items.Add(38400);
            cbBaudRate.Items.Add(57600);
            cbBaudRate.Items.Add(115200);
            cbBaudRate2.Items.Add(1200);
            cbBaudRate2.Items.Add(2400);
            cbBaudRate2.Items.Add(4800);
            cbBaudRate2.Items.Add(9600);
            cbBaudRate2.Items.Add(19200);
            cbBaudRate2.Items.Add(38400);
            cbBaudRate2.Items.Add(57600);
            cbBaudRate2.Items.Add(115200);

            cbStopBit.Items.Add(0);
            cbStopBit.Items.Add(1);
            cbStopBit.Items.Add(1.5);
            cbStopBit.Items.Add(2);
            cbStopBit2.Items.Add(0);
            cbStopBit2.Items.Add(1);
            cbStopBit2.Items.Add(1.5);
            cbStopBit2.Items.Add(2);

            cbParidade2.Items.Add("Nenhuma");
            cbParidade2.Items.Add("Par");
            cbParidade2.Items.Add("Ímpar");

            cbPortaSerial.Texts = Program.SERIALPORT.PortName;
            cbBaudRate.Texts = Program.SERIALPORT.BaudRate.ToString();
            cbStopBit.Texts = GeralClass.v_StopBit(Program.SERIALPORT.StopBits);

            cbPortaSerial2.Texts = Program.IMPRESSORAPORT.PortName;
            cbBaudRate2.Texts = Program.IMPRESSORAPORT.BaudRate.ToString();
            cbStopBit2.Texts = GeralClass.v_StopBit(Program.IMPRESSORAPORT.StopBits);

            if (Program.IMPRESSORAPORT.Parity == Parity.Even)
            {
                cbParidade2.Texts = "Par";
            }else if (Program.IMPRESSORAPORT.Parity == Parity.Odd)
            {
                cbParidade2.Texts = "Ímpar";
            }
            else
            {
                cbParidade2.Texts = "Nenhuma";
            }
        }
        private void DeviceChangeEvent(object sender, System.Management.EventArrivedEventArgs e)
        {
            // Atualiza a lista de portas disponíveis
            string[] portNames = SerialPort.GetPortNames();
            cbPortaSerial.Invoke(new Action(() => cbPortaSerial.Items.Clear()));
            cbPortaSerial.Invoke(new Action(() => cbPortaSerial.Items.AddRange(portNames)));
            cbPortaSerial2.Invoke(new Action(() => cbPortaSerial2.Items.Clear()));
            cbPortaSerial2.Invoke(new Action(() => cbPortaSerial2.Items.AddRange(portNames)));
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SerialForm_Paint(object sender, PaintEventArgs e)
        {
            Pen pen = new Pen(Color.FromArgb(100, 45, 45, 97), 3);
            e.Graphics.DrawRectangle(pen, new Rectangle(0, 0, this.Width, this.Height));
        }


        private void v_SerialPort1_ConfChange()
        {
            XmlDocument doc = new XmlDocument();
            string caminhoCompleto = Path.Combine(Application.StartupPath, Program.CFG.CaminhoConfig);
            doc.Load(caminhoCompleto);
            XmlElement xml_PortaSerial_1 = (XmlElement)doc.SelectSingleNode("//PortaSerial_1");
            XmlElement xml_BaudRate_1 = (XmlElement)doc.SelectSingleNode("//BaudRate_1");
            XmlElement xml_StopBit_1 = (XmlElement)doc.SelectSingleNode("//StopBit_1");
            XmlElement xml_Autoconnect_1 = (XmlElement)doc.SelectSingleNode("//AutoConnect_1");
            if (xml_PortaSerial_1 != null && xml_BaudRate_1 != null && xml_StopBit_1 != null && xml_Autoconnect_1 != null)
            {
                xml_PortaSerial_1.InnerText = Program.SERIALPORT.PortName;
                xml_BaudRate_1.InnerText = Program.SERIALPORT.BaudRate.ToString();
                xml_StopBit_1.InnerText = GeralClass.v_StopBit(Program.SERIALPORT.StopBits);
                if(chkAutoconnect.Checked == true) xml_Autoconnect_1.InnerText = "1";
                else xml_Autoconnect_1.InnerText = "0";
            }
            try
            {
                doc.Save(caminhoCompleto);
                //MessageBox.Show("Configuração de Estação salva com sucesso!", "VC Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                //MessageBox.Show($"Erro ao salvar Configurãção da Estação! {ex}", "VC Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void v_SerialPort2_ConfChange()
        {
            XmlDocument doc = new XmlDocument();
            string caminhoCompleto = Path.Combine(Application.StartupPath, Program.CFG.CaminhoConfig);
            doc.Load(caminhoCompleto);
            XmlElement xml_PortaSerial_1 = (XmlElement)doc.SelectSingleNode("//PortaSerial_2");
            XmlElement xml_BaudRate_1 = (XmlElement)doc.SelectSingleNode("//BaudRate_2");
            XmlElement xml_StopBit_1 = (XmlElement)doc.SelectSingleNode("//StopBit_2");
            XmlElement xml_Paridade_1 = (XmlElement)doc.SelectSingleNode("//Paridade_2");
            XmlElement xml_Autoconnect_1 = (XmlElement)doc.SelectSingleNode("//AutoConnect_2");
            if (xml_PortaSerial_1 != null && xml_BaudRate_1 != null && xml_StopBit_1 != null && xml_Autoconnect_1 != null)
            {
                xml_PortaSerial_1.InnerText = Program.IMPRESSORAPORT.PortName;
                xml_BaudRate_1.InnerText = Program.IMPRESSORAPORT.BaudRate.ToString();
                xml_StopBit_1.InnerText = GeralClass.v_StopBit(Program.IMPRESSORAPORT.StopBits);
                if (chkAutoconnect2.Checked == true) xml_Autoconnect_1.InnerText = "1";
                else xml_Autoconnect_1.InnerText = "0";
            }
            try
            {
                doc.Save(caminhoCompleto);
                //MessageBox.Show("Configuração de Estação salva com sucesso!", "VC Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                //MessageBox.Show($"Erro ao salvar Configurãção da Estação! {ex}", "VC Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnSalvarEtq_Click(object sender, EventArgs e)
        {
            if (!Program.SERIALPORT.IsOpen)
            {
                Program.SERIALPORT.PortName = cbPortaSerial.Texts;
                Program.SERIALPORT.BaudRate = Convert.ToInt32(cbBaudRate.Texts);
                Program.SERIALPORT.StopBits = GeralClass.v_StopBit(cbStopBit.Texts);

                try
                {
                    v_SerialPort1_ConfChange();
                    Program.SERIALPORT.Open();
                    btnSalvar.Text = "Desconectar";
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    btnSalvar.Text = "Conectar";
                }
            }
            else
            {
                v_SerialPort1_ConfChange();
                Program.SERIALPORT.Close();
                btnSalvar.Text = "Conectar";
            }
            if (Application.OpenForms.OfType<MainForms>().Any())
            {
                MainForms MainForm = Application.OpenForms.OfType<MainForms>().FirstOrDefault();
                MainForm.UpdateStatusSerial();
            }
        }

        private void btnConectar02_Click(object sender, EventArgs e)
        {
            if (!Program.IMPRESSORAPORT.IsOpen)
            {
                Program.IMPRESSORAPORT.PortName = cbPortaSerial2.Texts;
                Program.IMPRESSORAPORT.BaudRate = Convert.ToInt32(cbBaudRate2.Texts);
                Program.IMPRESSORAPORT.StopBits = GeralClass.v_StopBit(cbStopBit2.Texts);
                if (cbParidade2.Texts == "Par")
                {
                    Program.IMPRESSORAPORT.Parity = Parity.Even;
                }
                else if (cbParidade2.Texts == "Ímpar")
                {
                    Program.IMPRESSORAPORT.Parity = Parity.Odd;
                }
                else
                {
                    Program.IMPRESSORAPORT.Parity = Parity.None;
                }

                try
                {
                    v_SerialPort2_ConfChange();
                    Program.IMPRESSORAPORT.Open();
                    btnConectar02.Text = "Desconectar";
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    btnConectar02.Text = "Conectar";
                }
            }
            else
            {
                v_SerialPort2_ConfChange();
                Program.IMPRESSORAPORT.Close();
                btnConectar02.Text = "Conectar";
            }
            if (Application.OpenForms.OfType<MainForms>().Any())
            {
                MainForms MainForm = Application.OpenForms.OfType<MainForms>().FirstOrDefault();
                MainForm.UpdateStatusSerial();
            }
        }
    }
}

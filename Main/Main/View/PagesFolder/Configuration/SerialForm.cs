using Main.Helper;
using Main.Model;
using Main.Service;
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

            chkAutoconnect01.Checked = Program._autoconnect_1;
            chkAutoconnect2.Checked = Program._autoconnect_2;
            //chkAutoconnect02.Checked = Program._autoconnect_3;
        }

        private async void SerialForm_Load(object sender, EventArgs e)
        {
            try
            {
                if (SerialCommunicationService.SERIALPORT1.IsOpen)
                {
                    btnSalvar01.Text = "Desconectar";
                }
                else
                {
                    btnSalvar01.Text = "Conectar";
                }
                //if (SerialCommunicationService.SERIALPORT2.IsOpen)
                //{
                //    btnSalvar02.Text = "Desconectar";
                //}
                //else
                //{
                //    btnSalvar02.Text = "Conectar";
                //}
                if (Program.IMPRESSORAPORT.IsOpen)
                {
                    btnConectar02.Text = "Desconectar";
                }
                else
                {
                    btnConectar02.Text = "Conectar";
                }

                cbPortaSerial01.Items.Clear();
                cbPortaSerial02.Items.Clear();
                cbPortaSerial2.Items.Clear();
                cbBaudRate01.Items.Clear();
                cbBaudRate02.Items.Clear();
                cbBaudRate2.Items.Clear();
                cbStopBit01.Items.Clear();
                cbStopBit02.Items.Clear();
                cbStopBit2.Items.Clear();
                cbParidade2.Items.Clear();

                string[] portNames = SerialPort.GetPortNames();
                cbPortaSerial01.Items.AddRange(portNames);
                cbPortaSerial02.Items.AddRange(portNames);
                cbPortaSerial2.Items.AddRange(portNames);

                cbBaudRate01.Items.Add(1200);
                cbBaudRate01.Items.Add(2400);
                cbBaudRate01.Items.Add(4800);
                cbBaudRate01.Items.Add(9600);
                cbBaudRate01.Items.Add(19200);
                cbBaudRate01.Items.Add(38400);
                cbBaudRate01.Items.Add(57600);
                cbBaudRate01.Items.Add(115200);

                cbBaudRate02.Items.Add(1200);
                cbBaudRate02.Items.Add(2400);
                cbBaudRate02.Items.Add(4800);
                cbBaudRate02.Items.Add(9600);
                cbBaudRate02.Items.Add(19200);
                cbBaudRate02.Items.Add(38400);
                cbBaudRate02.Items.Add(57600);
                cbBaudRate02.Items.Add(115200);

                cbBaudRate2.Items.Add(1200);
                cbBaudRate2.Items.Add(2400);
                cbBaudRate2.Items.Add(4800);
                cbBaudRate2.Items.Add(9600);
                cbBaudRate2.Items.Add(19200);
                cbBaudRate2.Items.Add(38400);
                cbBaudRate2.Items.Add(57600);
                cbBaudRate2.Items.Add(115200);

                cbStopBit01.Items.Add(0);
                cbStopBit01.Items.Add(1);
                cbStopBit01.Items.Add(1.5);
                cbStopBit01.Items.Add(2);

                cbStopBit02.Items.Add(0);
                cbStopBit02.Items.Add(1);
                cbStopBit02.Items.Add(1.5);
                cbStopBit02.Items.Add(2);

                cbStopBit2.Items.Add(0);
                cbStopBit2.Items.Add(1);
                cbStopBit2.Items.Add(1.5);
                cbStopBit2.Items.Add(2);

                cbParidade2.Items.Add("Nenhuma");
                cbParidade2.Items.Add("Par");
                cbParidade2.Items.Add("Ímpar");

                //Indicador 01
                cbPortaSerial01.Texts = SerialCommunicationService.SERIALPORT1.PortName;
                cbBaudRate01.Texts = SerialCommunicationService.SERIALPORT1.BaudRate.ToString();
                cbStopBit01.Texts = GeralClass.v_StopBit(SerialCommunicationService.SERIALPORT1.StopBits);

                //Impressora
                cbPortaSerial2.Texts = Program.IMPRESSORAPORT.PortName;
                cbBaudRate2.Texts = Program.IMPRESSORAPORT.BaudRate.ToString();
                cbStopBit2.Texts = GeralClass.v_StopBit(Program.IMPRESSORAPORT.StopBits);

                //Indicador 02
                //cbPortaSerial02.Texts = SerialCommunicationService.SERIALPORT2.PortName;
                //cbBaudRate02.Texts = SerialCommunicationService.SERIALPORT2.BaudRate.ToString();
                //cbStopBit02.Texts = GeralClass.v_StopBit(SerialCommunicationService.SERIALPORT2.StopBits);

                if (Program.IMPRESSORAPORT.Parity == Parity.Even)
                {
                    cbParidade2.Texts = "Par";
                }
                else if (Program.IMPRESSORAPORT.Parity == Parity.Odd)
                {
                    cbParidade2.Texts = "Ímpar";
                }
                else
                {
                    cbParidade2.Texts = "Nenhuma";
                }

                await Task.Run(() =>
                {
                    var Balancas = Program.SQL.SelectList("SELECT * FROM REDE WHERE tipo = 'Balança'", "Rede", null,
                    new Dictionary<string, object>() { });

                    foreach (RedeClass item in Balancas)
                    {
                        cbBalanca01.Invoke(new MethodInvoker(delegate
                        {
                            cbBalanca01.Items.Add(item);
                            cbBalanca01.DisplayMember = "nome";
                            cbBalanca02.Items.Add(item);
                            cbBalanca02.DisplayMember = "nome";
                        }));
                    }

                    if (Balancas.Count > 0)
                    {
                        cbBalanca01.Invoke(new MethodInvoker(delegate { cbBalanca01.SelectedIndex = 0; }));
                        cbBalanca02.Invoke(new MethodInvoker(delegate { cbBalanca02.SelectedIndex = 0; }));
                    }
                });

                //cbBalanca01.Texts = Program.CFG.balanca_1;
                //cbBalanca02.Texts = Program.CFG.balanca_2;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void DeviceChangeEvent(object sender, System.Management.EventArrivedEventArgs e)
        {
            // Atualiza a lista de portas disponíveis
            string[] portNames = SerialPort.GetPortNames();
            cbPortaSerial01.Invoke(new Action(() => cbPortaSerial01.Items.Clear()));
            cbPortaSerial01.Invoke(new Action(() => cbPortaSerial01.Items.AddRange(portNames)));
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


        private void v_SerialPort1_ConfChange(string indicador)
        {

            try
            {
                if (indicador == "Indicador 01")
                {
                    XmlDocument doc = new XmlDocument();
                    string caminhoCompleto = Path.Combine(Application.StartupPath, Program.CFG.CaminhoConfig);
                    doc.Load(caminhoCompleto);
                    XmlElement xml_Balanca_1 = (XmlElement)doc.SelectSingleNode("//Balanca_1");
                    XmlElement xml_PortaSerial_1 = (XmlElement)doc.SelectSingleNode("//PortaSerial_1");
                    XmlElement xml_BaudRate_1 = (XmlElement)doc.SelectSingleNode("//BaudRate_1");
                    XmlElement xml_StopBit_1 = (XmlElement)doc.SelectSingleNode("//StopBit_1");
                    XmlElement xml_Autoconnect_1 = (XmlElement)doc.SelectSingleNode("//AutoConnect_1");
                    if (xml_PortaSerial_1 != null && xml_BaudRate_1 != null && xml_StopBit_1 != null && xml_Autoconnect_1 != null)
                    {
                        xml_Balanca_1.InnerText = cbBalanca01.Texts;
                        //Program.CFG.balanca_1 = cbBalanca01.Texts;
                        xml_PortaSerial_1.InnerText = SerialCommunicationService.SERIALPORT1.PortName;
                        xml_BaudRate_1.InnerText = SerialCommunicationService.SERIALPORT1.BaudRate.ToString();
                        xml_StopBit_1.InnerText = GeralClass.v_StopBit(SerialCommunicationService.SERIALPORT1.StopBits);
                        if (chkAutoconnect01.Checked == true) xml_Autoconnect_1.InnerText = "1";
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
                else if (indicador == "Indicador 02")
                {
                    XmlDocument doc = new XmlDocument();
                    string caminhoCompleto = Path.Combine(Application.StartupPath, Program.CFG.CaminhoConfig);
                    doc.Load(caminhoCompleto);
                    XmlElement xml_Balanca_3 = (XmlElement)doc.SelectSingleNode("//Balanca_2");
                    XmlElement xml_PortaSerial_3 = (XmlElement)doc.SelectSingleNode("//PortaSerial_3");
                    XmlElement xml_BaudRate_3 = (XmlElement)doc.SelectSingleNode("//BaudRate_3");
                    XmlElement xml_StopBit_3 = (XmlElement)doc.SelectSingleNode("//StopBit_3");
                    XmlElement xml_Autoconnect_3 = (XmlElement)doc.SelectSingleNode("//AutoConnect_3");
                    if (xml_PortaSerial_3 != null && xml_BaudRate_3 != null && xml_StopBit_3 != null && xml_Autoconnect_3 != null)
                    {
                        xml_Balanca_3.InnerText = cbBalanca02.Texts;
                        //Program.CFG.balanca_2 = cbBalanca02.Texts;
                        //xml_PortaSerial_3.InnerText = SerialCommunicationService.SERIALPORT2.PortName;
                        //xml_BaudRate_3.InnerText = SerialCommunicationService.SERIALPORT2.BaudRate.ToString();
                        //xml_StopBit_3.InnerText = GeralClass.v_StopBit(SerialCommunicationService.SERIALPORT2.StopBits);
                        if (chkAutoconnect02.Checked == true) xml_Autoconnect_3.InnerText = "1";
                        else xml_Autoconnect_3.InnerText = "0";
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

                Program.ReadAgain();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
            try
            {
                System.Windows.Forms.Button btn = (System.Windows.Forms.Button)sender;
                if (btn.Tag.ToString() == "Indicador 01")
                {

                    if (!SerialCommunicationService.SERIALPORT1.IsOpen)
                    {
                        SerialCommunicationService.SERIALPORT1.PortName = cbPortaSerial01.Texts;
                        SerialCommunicationService.SERIALPORT1.BaudRate = Convert.ToInt32(cbBaudRate01.Texts);
                        SerialCommunicationService.SERIALPORT1.StopBits = GeralClass.v_StopBit(cbStopBit01.Texts);

                        try
                        {
                            v_SerialPort1_ConfChange(btn.Tag.ToString());
                            SerialCommunicationService.SERIALPORT1.Close();
                            SerialCommunicationService.SERIALPORT1.Open();
                            btnSalvar01.Text = "Desconectar";
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.ToString());
                            btnSalvar01.Text = "Conectar";
                        }

                        SerialCommunicationService.InitWithoutAutoConnect();
                    }
                    else
                    {
                        v_SerialPort1_ConfChange(btn.Tag.ToString());
                        SerialCommunicationService.SERIALPORT1.Close();
                        btnSalvar01.Text = "Conectar";
                    }
                    if (Application.OpenForms.OfType<MainForms>().Any())
                    {
                        MainForms MainForm = Application.OpenForms.OfType<MainForms>().FirstOrDefault();
                        MainForm.UpdateStatusSerial();
                    }
                }
                else if (btn.Tag.ToString() == "Indicador 02") 
                {

                    //if (!SerialCommunicationService.SERIALPORT2.IsOpen)
                    //{
                    //    SerialCommunicationService.SERIALPORT2.PortName = cbPortaSerial02.Texts;
                    //    SerialCommunicationService.SERIALPORT2.BaudRate = Convert.ToInt32(cbBaudRate02.Texts);
                    //    SerialCommunicationService.SERIALPORT2.StopBits = GeralClass.v_StopBit(cbStopBit02.Texts);

                    //    try
                    //    {
                    //        v_SerialPort1_ConfChange(btn.Tag.ToString());
                    //        SerialCommunicationService.SERIALPORT2.Close();
                    //        SerialCommunicationService.SERIALPORT2.Open();
                    //        btnSalvar02.Text = "Desconectar";
                    //    }
                    //    catch (Exception ex)
                    //    {
                    //        Console.WriteLine(ex.ToString());
                    //        btnSalvar02.Text = "Conectar";
                    //    }
                    //    SerialCommunicationService.InitWithoutAutoConnect();
                    //}
                    //else
                    //{
                    //    v_SerialPort1_ConfChange(btn.Tag.ToString());
                    //    SerialCommunicationService.SERIALPORT2.Close();
                    //    btnSalvar02.Text = "Conectar";
                    //}
                    //if (Application.OpenForms.OfType<MainForms>().Any())
                    //{
                    //    MainForms MainForm = Application.OpenForms.OfType<MainForms>().FirstOrDefault();
                    //    MainForm.UpdateStatusSerial();
                    //}
                }

                if (Program.com != null) 
                {
                    //Program.com.TryAgainCommunication();
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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

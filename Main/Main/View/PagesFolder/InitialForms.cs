using Main.Helper.SerialFolder;
using Main.Properties;
using Main.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using System.Threading;
using Main.Helper;

namespace Main.View.PagesFolder
{
    public partial class InitialForms : Form
    {

        private bool time_running = true;
        public TCPContext TCP_2;
        public TCPContext TCP_ZEBRA;

        public InitialForms()
        {
            InitializeComponent();

            Task.Run(async() => 
            {
                while (time_running)
                {
                    string day = DateTime.Now.Day.ToString();
                    if (day.Length == 1)
                        day = $"0{day}";
                    string month = DateTime.Now.Month.ToString();
                    if (month.Length == 1)
                        month = $"0{month}";
                    string hour = DateTime.Now.Hour.ToString();
                    if (hour.Length == 1)
                        hour = $"0{hour}";
                    string minute = DateTime.Now.Minute.ToString();
                    if (minute.Length == 1)
                        minute = $"0{minute}";

                    lbl_time.Text = $"{day}/{month}/{DateTime.Now.Year}\n{hour}:{minute}";
                    await Task.Delay(60000);
                    loadCOM();
                }
            });
            loadCOM();

            cb_baud.SelectedIndex = 0;
            cb_bit.SelectedIndex = 1;
        }

        private void loadCOM() 
        {
            try
            {
                cbCom.Items.Clear();
                foreach (string item in System.IO.Ports.SerialPort.GetPortNames())
                {
                    cbCom.Items.Add(item);
                }
                cbCom.SelectedIndex = 0;
            }
            catch (Exception)
            {
            }
        }

        private async void btnSerial_Click(object sender, EventArgs e)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(txt_addr.Text))
                {
                    lbl_error.Text = "O campo endereço deve ser preenchido";
                    await Task.Delay(6000);
                    lbl_error.Text = "";
                    return;
                }

                if (string.IsNullOrWhiteSpace(cbCom.SelectedItem.ToString()))
                {
                    lbl_error.Text = "O campo porta serial deve ser preenchido";
                    await Task.Delay(6000);
                    lbl_error.Text = "";
                    return;
                }

                if (string.IsNullOrWhiteSpace(cb_baud.SelectedItem.ToString()))
                {
                    lbl_error.Text = "O campo velocidade transmissão deve ser preenchido";
                    await Task.Delay(6000);
                    lbl_error.Text = "";
                    return;
                }

                if (string.IsNullOrWhiteSpace(cb_bit.SelectedItem.ToString()))
                {
                    lbl_error.Text = "O campo bit de parada deve ser preenchido";
                    await Task.Delay(6000);
                    lbl_error.Text = "";
                    return;
                }


                if (!Program.SERIALPORT.IsOpen)
                {
                    Program.SERIALPORT.PortName = cbCom.SelectedItem.ToString();
                    Program.SERIALPORT.BaudRate = Convert.ToInt32(cb_baud.SelectedItem.ToString());

                    if (cb_bit.SelectedIndex == 0)
                    {
                        Program.SERIALPORT.DataBits = 8;
                        Program.SERIALPORT.StopBits = System.IO.Ports.StopBits.One;
                    }
                    else
                    {
                        Program.SERIALPORT.DataBits = 8;
                        Program.SERIALPORT.StopBits = System.IO.Ports.StopBits.Two;
                    }

                    Program._Controle = new ControleCIndicadores(Program.SERIALPORT, 1, Convert.ToInt32(txt_addr.Text), this);
                    //Program._Controle.Set_Label(label1, Convert.ToInt32(txt_addr.Text));
                    Program.SERIALPORT.Open();

                    if (Program.SERIALPORT.IsOpen)
                    {
                        Program._Controle.Request_Inicialize("FULL_SPEED");
                        this.btnSerial.Image = Properties.Resources.errorIcon;
                    }
                    else
                    {
                        //Mostar erro
                    }
                }
                else
                {
                    Program.SERIALPORT.Close();
                    Program._Controle.Request_Abort("THREAD");
                    Program._Controle = null;
                    this.btnSerial.Image = Properties.Resources._299110_check_sign_icon;
                }
            }
            catch (Exception)
            {
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                    (e.KeyChar != '.'))
                {
                    e.Handled = true;
                }
            }
            catch (Exception)
            {
            }
        }

        private void cb_baud_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                    (e.KeyChar != '.'))
                {
                    e.Handled = true;
                }
            }
            catch (Exception)
            {
            }
        }

        private void cbCom_Click(object sender, EventArgs e)
        {
            try
            {
                loadCOM();
            }
            catch (Exception)
            {
            }
        }

        private async void btnEthernet_Click(object sender, EventArgs e)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(txt_ip.Text))
                {
                    lbl_error.Text = "O campo ip deve ser preenchido";
                    await Task.Delay(6000);
                    lbl_error.Text = "";
                    return;
                }

                if (String.IsNullOrWhiteSpace(txt_porta.Text))
                {
                    lbl_error.Text = "O campo porta deve ser preenchido";
                    await Task.Delay(6000);
                    lbl_error.Text = "";
                    return;
                }

                //if (String.IsNullOrWhiteSpace(txt_mascara.Text))
                //{
                //    lbl_error.Text = "O campo mascara deve ser preenchido";
                //    await Task.Delay(6000);
                //    lbl_error.Text = "";
                //    return;
                //}

                //if (String.IsNullOrWhiteSpace(txt_gateway.Text))
                //{
                //    lbl_error.Text = "O campo gateway deve ser preenchido";
                //    await Task.Delay(6000);
                //    lbl_error.Text = "";
                //    return;
                //}

                Program.TCP = new Helper.TCPContext(txt_ip.Text, txt_porta.Text);
                //if (Program.TCP.tcpClient.Client.Available > 0)
                //{
                //    this.btnEthernet.Image = Properties.Resources.errorIcon;
                //}
                //else 
                //{
                //    this.btnSerial.Image = Properties.Resources._299110_check_sign_icon;
                //}

                //Task.Run(() => 
                //{
                //    try
                //    {
                //       // BACK_2:
                //        while (true)
                //        {
                //            Program.TCP.ReadClient();
                //            //await Task.Delay(500);
                //            Thread.Sleep(500);
                //            // foreach (var bt in Program.TCP.data)
                //            //{
                //            //    Console.Write(Encoding.ASCII.get(bt));
                //            // }

                //            Console.WriteLine(Encoding.ASCII.GetString(Program.TCP.data));
                //            Program.TCP.data = new byte[1];
                //        }
                //    }
                //    catch (Exception)
                //    {
                //       // goto BACK_2;
                //    }

                //});

                TimerEthernet.Enabled = true;

            }
            catch (Exception ex)
            {
            }
        }

        private void pcb_sendCommand_Click(object sender, EventArgs e)
        {
            try
            {
                var value = Interaction.InputBox(" ", "Informe o comando", "");

                if (!string.IsNullOrWhiteSpace(value))
                {
                    Program.TCP.WriteClient(Encoding.ASCII.GetBytes(value));
                }
            }
            catch (Exception)
            {
            }
        }

        private void TimerEthernet_Tick(object sender, EventArgs e)
        {
            Program.TCP.ReadClient();
            Thread.Sleep(50);

            if (Program.TCP.data != null) 
            {
                if (Program.TCP.data.Length >= 17)
                {
                    Console.WriteLine(Encoding.ASCII.GetString(Program.TCP.data));
                    string Peso = Encoding.ASCII.GetString(Program.TCP.data);
                    double peso_d = 0;
                    if (Peso.Length >= 17)
                    {
                        Peso = Peso.Substring(4, 6);
                        peso_d = Convert.ToDouble(Peso) * 0.001;
                        label16.Text = String.Format("{0} kg", peso_d.ToString());
                    }
                }
            }


            Program.TCP.data = new byte[1];
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            try
            {
                //Program.TCP = new Helper.TCPContext(txt_ip.Text, txt_porta.Text);

                TCP_2 = new Helper.TCPContext(textBox5.Text, textBox4.Text);
                TimerEthernet2.Enabled = true;
            }
            catch (Exception)
            {
            }
        }

        private void TimerEthernet2_Tick(object sender, EventArgs e)
        {
            try
            {
                TCP_2.ReadClient();
                Thread.Sleep(50);

                if (TCP_2.data != null)
                {
                    if (TCP_2.data.Length >= 17)
                    {
                        Console.WriteLine(Encoding.ASCII.GetString(TCP_2.data));
                        string Peso = Encoding.ASCII.GetString(TCP_2.data);
                        double peso_d = 0;
                        if (Peso.Length >= 17)
                        {
                            Peso = Peso.Substring(5, 11);
                            peso_d = Convert.ToDouble(Peso);
                            label22.Text = String.Format("{0} g", peso_d.ToString());
                        }
                    }
                }


                TCP_2.data = new byte[1];
            }
            catch (Exception)
            {
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            try
            {
                TCP_ZEBRA = new Helper.TCPContext(textBox8.Text, textBox7.Text);
                Thread.Sleep(100);
                //TCP_ZEBRA.WriteClient();
                StringBuilder str = new StringBuilder();
                //Enviar comando de impressão
                str.AppendLine("^XA");

                str.AppendLine("^FX Top section with logo, name and address.");
                str.AppendLine("^CF0,60");
                str.AppendLine("^FO50,50^GB100,100,100^FS");
                str.AppendLine("^FO75,75^FR^GB100,100,100^FS");  
                str.AppendLine("^FO93,93^GB40,40,40^FS");
                str.AppendLine("^FO200,50^FD PESO ^FS");
                str.AppendLine("^FO180,120^FD 10,00kg ^FS");
                str.AppendLine("^CF0,30");

                str.AppendLine("^XZ");
                TCP_ZEBRA.WriteClient(str);
            }
            catch (Exception)
            {
            }
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }
    }
}

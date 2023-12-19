using Main.Model;
using Main.View.CommunicationFolder;
using Main.View.PopupFolder;
using Microsoft.Office.Interop.Excel;
using Org.BouncyCastle.Crypto.Engines;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Ports;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms.DataVisualization.Charting;
using ZXing.OneD;

namespace Main.Service
{

    public static class SerialCommunicationService
    {
        public static SerialPort SERIALPORT1 = new SerialPort();

        public static SerialPort teste = new SerialPort();

        private static List<Object> _indicadores = new List<Object>();

        public static List<IndicadorClass> indicador_addr = new List<IndicadorClass>();

        //private static Dictionary<int, Stopwatch> indicador_watchdogTime = new Dictionary<int, Stopwatch>();

        private static int global_counter = 0;

        private static System.Timers.Timer tmRead = new System.Timers.Timer();

        private static Stopwatch global_stopwatch = new Stopwatch();

        private static bool canSend = true;

        //Conecta no inicio do programa, caso esteja configurado para conexão automatica.
        public static void InitWithAutoConnect() 
        {
            try
            {

                if (Program._autoconnect_1)
                {
                    if (!SERIALPORT1.IsOpen) { SERIALPORT1.Open(); }
                }

                SERIALPORT1.DataReceived += Value_DataReceived;

                Dictionary<string, object> parametros = new Dictionary<string, object>()
                {
                    { "@parent", Environment.MachineName}
                };

                _indicadores = Program.SQL.SelectList("select * from Rede where parent = @parent and tipo = 'Balança'", "Rede", values: parametros);

                if (_indicadores.Count > 0)
                {
                    foreach (RedeClass indicador in _indicadores)
                    {

                        IndicadorClass indicador01 = new IndicadorClass(SERIALPORT1, indicador);
                        indicador_addr.Add(indicador01);
                    }
                }

                if (indicador_addr.Count <= 0)
                {
                   // InfoPopup info = new InfoPopup("Configuração de indicadores necessária !!", "Atenção, Pressione o botão F1 na página principal para configurar os indicadores.");
                   // info.Show();
                }
                else 
                {
                    tmRead.Interval = 1;
                    tmRead.Elapsed += TmRead_Elapsed;
                    tmRead.Start();
                }

            }
            catch (Exception ex)
            {

            }
        }
        //Conecta com o Indicador quando o botão de conectar é pressionado na tela de Conexão Serial.
        public static void InitWithoutAutoConnect()
        {
            try
            {
                //indicador_watchdogTime.Clear();
                try
                {
                    if (!SERIALPORT1.IsOpen) { SERIALPORT1.Open(); }
                }
                catch (Exception)
                {
                }

                SERIALPORT1.DataReceived += Value_DataReceived;

                Dictionary<string, object> parametros = new Dictionary<string, object>()
                {
                    { "@parent", Environment.MachineName}
                };

                _indicadores = Program.SQL.SelectList("select * from Rede where parent = @parent and tipo = 'Balança'", "Rede", values: parametros);

                if (_indicadores.Count > 0)
                {
                    foreach (RedeClass indicador in _indicadores)
                    {
                        IndicadorClass indicador01 = new IndicadorClass(SERIALPORT1, indicador);
                        indicador_addr.Add(indicador01);
                    }
                }

                if (indicador_addr.Count <= 0)
                {
                    //InfoPopup info = new InfoPopup("Configuração de indicadores necessária !!", "Atenção, Pressione o botão F1 na página principal para configurar os indicadores.");
                    //info.Show();
                }
                else
                {
                    tmRead.Interval = 60;
                    tmRead.Elapsed += TmRead_Elapsed;
                    tmRead.Start();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        //Função para requisição de peso no Indicador Modbus.
        private static void TmRead_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                if (canSend == false) { return; }
                
                if (global_counter == indicador_addr.Count) { global_counter = 0; }
                
                if (SERIALPORT1.IsOpen && indicador_addr[global_counter].availableStatus == true && global_counter < indicador_addr.Count)
                {
                    Console.WriteLine($"Valor do counter: {global_counter}");

                    byte[] command = new byte[]
                    {
                        Convert.ToByte(indicador_addr[global_counter].indicador.addr),
                        0x03,
                        0x00,
                        0x0A,
                        0x00,
                        0x02,
                        0x00,
                        0x00
                    };

                    byte[] crc_calc = CommunicationFormsHelper.CRC(command);
                    command[6] = crc_calc[0];
                    command[7] = crc_calc[1];

                    SERIALPORT1.Write(command, 0, command.Length);
                    indicador_addr[global_counter].availableStatus = false;
                    global_stopwatch.Reset();
                }
                else
                {
                    if (!global_stopwatch.IsRunning) { global_stopwatch.Start(); }
                    if (global_stopwatch.ElapsedMilliseconds > 50)
                    {
                        global_stopwatch.Reset();
                        indicador_addr[global_counter].availableStatus = true;
                    }
                }

            }
            catch (Exception ex)
            {

            }
        }


        //Função que vai tratar o retorno dos dados do Indicador.
        private static void Value_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                SerialPort received = (SerialPort)sender;

                var bytes_ler = received.BytesToRead;

                if (bytes_ler > 0)
                {
                    Thread.Sleep(20);
                    bytes_ler = received.BytesToRead;
                }
                else 
                {
                    return;
                }


                if (received.BytesToRead >= 9 && received.BytesToRead < 11)
                {
                    byte[] bruto = new byte[received.BytesToRead];
                    received.Read(bruto, 0, received.BytesToRead);

                    byte[] bruto_check = (byte[])bruto.Clone();
                    bruto_check[7] = 0;
                    bruto_check[8] = 0;

                    byte[] crc_calc = CommunicationFormsHelper.CRC(bruto_check);

                    if (bruto[7] == crc_calc[0] && bruto[8] == crc_calc[1])
                    {
                        if (bruto[0] == Convert.ToByte(indicador_addr[global_counter].indicador.addr) && bruto[1] == 0x03)
                        {
                            indicador_addr[global_counter].PS = CommunicationFormsHelper.PesoConverted(bruto[3], bruto[4], bruto[5], bruto[6]);
                            indicador_addr[global_counter].PB = Convert.ToDouble(indicador_addr[global_counter].PS);

                            //Console.WriteLine($"Endereço: {indicador_addr[global_counter].indicador.addr}\nPeso: {indicador_addr[global_counter].PS}");

                            indicador_addr[global_counter].availableStatus = true;
                            if (global_counter + 1 > indicador_addr.Count) 
                            {
                                return; 
                            }
                            global_counter += 1;

                            if (global_counter == 2) 
                            {
                                global_counter = 0;
                            }
                        }
                        else
                        {

                        }
                    }
                    else
                    {

                    }
                }

                else
                {
                    byte[] clear = new byte[received.BytesToRead];
                    received.Read(clear, 0, received.BytesToRead);
                }

            }
            catch (Exception ex)
            {
            }
        }


        public async static void SendCommand(int addr, int typeSend)
        {
            try
            {
                //Para comunicação de peso.
                canSend = false;
                await Task.Delay(100);
                tmRead.Stop();

                await Task.Delay(100);

                //Tara Command
                if (typeSend == 0)
                {
                    byte[] command = new byte[]
                    {
                        Convert.ToByte(addr),
                        0x06,
                        0x00,
                        0x04,
                        0x00,
                        0x01,
                        0x00,
                        0x00
                    };
                    byte[] crc_calc = CommunicationFormsHelper.CRC(command);
                    command[6] = crc_calc[0];
                    command[7] = crc_calc[1];
                    SERIALPORT1.Write(command, 0, command.Length);
                }

                //Zero Command
                else if (typeSend == 1) 
                {
                    byte[] command = new byte[]
                    {
                        Convert.ToByte(addr),
                        0x06,
                        0x00,
                        0x04,
                        0x00,
                        0x02,
                        0x00,
                        0x00
                    };

                    byte[] crc_calc = CommunicationFormsHelper.CRC(command);
                    command[6] = crc_calc[0];
                    command[7] = crc_calc[1];
                    SERIALPORT1.Write(command, 0, command.Length);
                }

                //Retoma a comunicação de peso.

                canSend = true;
                await Task.Delay(100);
                tmRead.Start();
            }
            catch (Exception ex)
            {
            }
        }

    }
}

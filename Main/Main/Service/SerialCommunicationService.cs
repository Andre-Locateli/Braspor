using Main.Model;
using Main.View.CommunicationFolder;
using Main.View.PopupFolder;
using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms.DataVisualization.Charting;
using ZXing.OneD;

namespace Main.Service
{

    public static class SerialCommunicationService
    {
        public static SerialPort SERIALPORT1 = new SerialPort();
        public static SerialPort SERIALPORT2 = new SerialPort();

        public static SerialPort teste = new SerialPort();

        private static List<Object> _indicadores = new List<Object>();

        public static Dictionary<string, IndicadorClass> indicadores_info = new Dictionary<string, IndicadorClass>();
        public static Dictionary<SerialPort, IndicadorClass> portInfo = new Dictionary<SerialPort, IndicadorClass>();
        private static Dictionary<String, Stopwatch> indicador_watchdogTime = new Dictionary<string, Stopwatch>();



        private static System.Timers.Timer tmRead = new System.Timers.Timer();

        public static void InitWithAutoConnect() 
        {
            try
            {

                if (Program._autoconnect_1)
                {
                    if (!SERIALPORT1.IsOpen) { SERIALPORT1.Open(); }
                }
                if (Program._autoconnect_3)
                {
                    if (!SERIALPORT2.IsOpen) { SERIALPORT2.Open(); }
                }

                SERIALPORT1.DataReceived += Value_DataReceived;
                SERIALPORT2.DataReceived += Value_DataReceived;

                Dictionary<string, object> parametros = new Dictionary<string, object>()
                {
                    { "@parent", Environment.MachineName}
                };

                _indicadores = Program.SQL.SelectList("select * from Rede where parent = @parent and tipo = 'Balança'", "Rede", values: parametros);

                if (_indicadores.Count > 0)
                {
                    foreach (RedeClass indicador in _indicadores)
                    {

                        if (indicador.nome == Program.CFG.balanca_1 && Program._autoconnect_1)
                        {
                            IndicadorClass indicador01 = new IndicadorClass(SERIALPORT1, indicador);
                            indicador01.nome = Program.CFG.balanca_1;
                            indicadores_info.Add(SERIALPORT1.PortName, indicador01);
                            portInfo.Add(SERIALPORT1, indicador01);

                        }
                        else if (indicador.nome == Program.CFG.balanca_2 && Program._autoconnect_3)
                        {
                            IndicadorClass indicador02 = new IndicadorClass(SERIALPORT2, indicador);
                            indicador02.nome = Program.CFG.balanca_2;
                            indicadores_info.Add(SERIALPORT2.PortName, indicador02);
                            portInfo.Add(SERIALPORT2, indicador02);
                        }
                    }
                }

                if (indicadores_info.Count <= 0)
                {
                    InfoPopup info = new InfoPopup("Configuração de indicadores necessária !!", "Atenção, Pressione o botão F1 na página principal para configurar os indicadores.");
                    info.Show();
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

            }
        }

        public static void InitWithoutAutoConnect()
        {

        }



        //FUNÇÕES

        private static void TmRead_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {

                //if (CommandFlag) { return; }

                if (indicadores_info.ContainsKey(SERIALPORT1.PortName)) 
                {
                    if (indicadores_info[SERIALPORT1.PortName] != null && indicadores_info[SERIALPORT1.PortName].availableStatus)
                    {
                        byte[] command = new byte[]
                        {
                        //Estou confundindo as portas.
                            Convert.ToByte(indicadores_info[SERIALPORT1.PortName].indicador.addr),
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
                        //Console.WriteLine($"Estou mando no {SERIALPORT1.PortName}");
                        SERIALPORT1.Write(command, 0, command.Length);
                        indicadores_info[SERIALPORT1.PortName].availableStatus = false;

                        if (!indicador_watchdogTime.ContainsKey(SERIALPORT1.PortName))
                        {
                            indicador_watchdogTime.Add(SERIALPORT1.PortName, new Stopwatch());
                            indicador_watchdogTime[SERIALPORT1.PortName].Start();
                        }

                    }
                    else
                    {
                        if (!indicador_watchdogTime.ContainsKey(SERIALPORT1.PortName))
                        {
                            if (!indicador_watchdogTime.ContainsKey(SERIALPORT1.PortName))
                            {
                                indicador_watchdogTime.Add(SERIALPORT1.PortName, new Stopwatch());
                                indicador_watchdogTime[SERIALPORT1.PortName].Start();
                            }
                        }
                        if (indicador_watchdogTime[SERIALPORT1.PortName].ElapsedMilliseconds >= 80)
                        {
                            indicadores_info[SERIALPORT1.PortName].availableStatus = true;
                            indicador_watchdogTime[SERIALPORT1.PortName].Stop();
                            indicador_watchdogTime.Remove(SERIALPORT1.PortName);
                        }
                    }
                }

                if (indicadores_info.ContainsKey(SERIALPORT2.PortName)) 
                {
                    if (indicadores_info[SERIALPORT2.PortName] != null && indicadores_info[SERIALPORT2.PortName].availableStatus)
                    {
                        byte[] command = new byte[]
                        {
                        //Estou confundindo as portas.
                            Convert.ToByte(indicadores_info[SERIALPORT2.PortName].indicador.addr),
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
                        //Console.WriteLine($"Estou mando no {SERIALPORT2.PortName}");
                        SERIALPORT2.Write(command, 0, command.Length);
                        indicadores_info[SERIALPORT2.PortName].availableStatus = false;

                        if (!indicador_watchdogTime.ContainsKey(SERIALPORT2.PortName))
                        {
                            indicador_watchdogTime.Add(SERIALPORT2.PortName, new Stopwatch());
                            indicador_watchdogTime[SERIALPORT2.PortName].Start();
                        }
                    }
                    else
                    {
                        if (!indicador_watchdogTime.ContainsKey(SERIALPORT2.PortName))
                        {
                            if (!indicador_watchdogTime.ContainsKey(SERIALPORT2.PortName))
                            {
                                indicador_watchdogTime.Add(SERIALPORT2.PortName, new Stopwatch());
                                indicador_watchdogTime[SERIALPORT2.PortName].Start();
                            }
                        }
                        if (indicador_watchdogTime[SERIALPORT2.PortName].ElapsedMilliseconds >= 80)
                        {
                            indicadores_info[SERIALPORT2.PortName].availableStatus = true;
                            indicador_watchdogTime[SERIALPORT2.PortName].Stop();
                            indicador_watchdogTime.Remove(SERIALPORT2.PortName);
                        }
                    }
                }

            }
            catch (Exception ex)
            {

            }
        }

        private static void Value_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                SerialPort received = (SerialPort)sender;

                if (received.BytesToRead >= 9 && received.BytesToRead < 11)
                {
                    //Preciso criar uma validação anti ruido.
                    //Ele está dando saltos de forma errada no valor.
                    //Preciso validar o crc
                    byte[] bruto = new byte[received.BytesToRead];
                    received.Read(bruto, 0, received.BytesToRead);

                    byte[] bruto_check = (byte[])bruto.Clone();
                    bruto_check[7] = 0;
                    bruto_check[8] = 0;

                    byte[] crc_calc = CommunicationFormsHelper.CRC(bruto_check);

                    if (bruto[7] == crc_calc[0] && bruto[8] == crc_calc[1])
                    {
                        if (bruto[0] == Convert.ToByte(portInfo[received].indicador.addr) && bruto[1] == 0x03)
                        {
                            indicadores_info[received.PortName].PS = CommunicationFormsHelper.PesoConverted(bruto[3], bruto[4], bruto[5], bruto[6]);
                            indicadores_info[received.PortName].PB = Convert.ToDouble(indicadores_info[received.PortName].PS);

                            Console.WriteLine($"{received.PortName}: {indicadores_info[received.PortName].PS}");
                            indicadores_info[received.PortName].availableStatus = true;
                            indicador_watchdogTime.Remove(received.PortName);
                        }
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


        public static void SendCommand(SerialPort serialCOM, int typeSend)
        {
            try
            {
                tmRead.Stop();
                //CommandFlag = true;

                ///await Task.Delay(1000);
               
                //Tara Command
                if (typeSend == 0)
                {
                    //portInfo[serialCOM].availableStatus = false;

                    byte[] command = new byte[]
                    {
                        Convert.ToByte(indicadores_info[SERIALPORT1.PortName].indicador.addr),
                        0x06,
                        0x00,
                        0x02,
                        0x00,
                        0x01,
                        0x00,
                        0x00
                    };

                    byte[] crc_calc = CommunicationFormsHelper.CRC(command);
                    command[6] = crc_calc[0];
                    command[7] = crc_calc[1];
                    serialCOM.Write(command, 0, command.Length);
                }
                //Zero Command
                else if (typeSend == 1) 
                {
                    //portInfo[serialCOM].availableStatus = false;

                    byte[] command = new byte[]
                    {
                        Convert.ToByte(indicadores_info[SERIALPORT1.PortName].indicador.addr),
                        0x06,
                        0x00,
                        0x02,
                        0x00,
                        0x02,
                        0x00,
                        0x00
                    };

                    byte[] crc_calc = CommunicationFormsHelper.CRC(command);
                    command[6] = crc_calc[0];
                    command[7] = crc_calc[1];
                    serialCOM.Write(command, 0, command.Length);
                }

                //CommandFlag = false;
                tmRead.Start();
            }
            catch (Exception ex)
            {
            }
        }

    }
}

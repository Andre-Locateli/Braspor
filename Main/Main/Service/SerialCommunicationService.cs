using Main.Model;
using Main.Model.EtiquetaFolder;
using Main.View.CommunicationFolder;
using Main.View.PopupFolder;
using Microsoft.Office.Interop.Excel;
using Org.BouncyCastle.Crypto.Engines;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using ZPL;
using ZXing.OneD;

namespace Main.Service
{

    public static class SerialCommunicationService
    {
        private static List<Object> _impressoras = new List<Object>();

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

                indicador_addr.Clear();

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

                indicador_addr.Clear();

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
                //MessageBox.Show(ex.Message);
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
                    //Console.WriteLine($"Valor do counter: {global_counter}");

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
                            
                            var estavel = bruto[3] & 0b00010000;
                            if (estavel == 0x00) { indicador_addr[global_counter].Estavel = false; }
                            else { indicador_addr[global_counter].Estavel = true; }

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


        public async static void ImpressoraPrint(EtiquetaInfo etiqueta, int type)
        {
            try
            {

                var listPrinter = Program.SQL.SelectList("SELECT * FROM Rede WHERE Parent = @Parent AND tipo = 'Impressora'", "Rede",
                    null, new Dictionary<string, object>()
                    {
                        {"@Parent", Environment.MachineName}
                    });

                if (listPrinter.Count >= 1)
                {
                    //Pegar ela e executar

                    foreach (RedeClass impressora in listPrinter)
                    {
                        if (Program.Configuracao.id_Impressora == impressora.Id)
                        {
                            Console.WriteLine(Program.Etiqueta.arquivo);

                            if (impressora.tipo_impressao == 2)
                            {
                                Task task = Task.Run(async () =>
                                {
                                    string ipAddress = impressora.IP;
                                    int port = impressora.porta;
                                    int timeoutMilliseconds = 1000;
                                    using (TcpClient tcpClient = new TcpClient())
                                    {
                                        try
                                        {
                                            using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource(timeoutMilliseconds))
                                            {
                                                Task connectTask = tcpClient.ConnectAsync(ipAddress, port);
                                                await Task.WhenAny(connectTask, Task.Delay(Timeout.Infinite, cancellationTokenSource.Token));
                                                cancellationTokenSource.Token.ThrowIfCancellationRequested();

                                                //Program.DebbugPage.InsertLog($"Dispositivo conectado com sucesso! {impressora.full_name}");

                                                using (NetworkStream stream = tcpClient.GetStream())
                                                {
                                                    byte[] bufferWrite = Encoding.UTF8.GetBytes(Program.Etiqueta.arquivo);
                                                    stream.Write(bufferWrite, 0, bufferWrite.Length);
                                                    //Program.DebbugPage.InsertLog($"Dados enviados: {bufferWrite}");
                                                }
                                            }
                                        }
                                        catch (OperationCanceledException)
                                        {
                                            //Program.DebbugPage.InsertLog($"Tempo limite de conexão atingido. {impressora.full_name}");
                                        }
                                        catch (IOException ex)
                                        {
                                            //Program.DebbugPage.InsertLog($"Erro na escrita dos dados: {ex.Message}" + $" {impressora.full_name}");
                                        }
                                        catch (Exception ex)
                                        {
                                            //Program.DebbugPage.InsertLog($"Erro ao tentar conectar: {ex.Message}" + $" {impressora.full_name}");
                                        }
                                    }
                                });
                            }

                            //Serial
                            else if (impressora.tipo_impressao == 1)
                            {
                                Task taskSerial = Task.Run(() =>
                                {

                                    try
                                    {
                                        if (Program.IMPRESSORAPORT.IsOpen)
                                        {
                                            // Program.IMPRESSORAPORT.Open();
                                            //Program.DebbugPage.InsertLog"Dispositivo conectado com sucesso! {0}", impressora.full_name);

                                            byte[] bufferWrite = Encoding.UTF8.GetBytes(Program.Etiqueta.arquivo);
                                            Program.IMPRESSORAPORT.Write(bufferWrite, 0, bufferWrite.Length);
                                            //Program.DebbugPage.InsertLog$"Dados enviados: {Encoding.UTF8.GetString(bufferWrite)}");
                                            //MessageBox.Show("Enviado para impressora");
                                        }

                                    }
                                    catch (TimeoutException)
                                    {
                                        //Program.DebbugPage.InsertLog($"Tempo limite de conexão atingido. {impressora.full_name}");
                                    }
                                    catch (UnauthorizedAccessException ex)
                                    {
                                        //Program.DebbugPage.InsertLog($"Erro de acesso à porta serial: {ex.Message} {impressora.full_name}");
                                    }
                                    catch (IOException ex)
                                    {
                                        //Program.DebbugPage.InsertLog($"Erro na escrita dos dados: {ex.Message} {impressora.full_name}");
                                    }
                                    catch (Exception ex)
                                    {
                                       // Program.DebbugPage.InsertLog($"Erro ao tentar conectar: {ex.Message} {impressora.full_name}");
                                    }
                                    finally
                                    {
                                        //if (Program.IMPRESSORAPORT.IsOpen)
                                        //    Program.IMPRESSORAPORT.Close();
                                    }

                                });
                            }

                            //Impressora normal
                            else if (impressora.tipo_impressao == 0)
                            {
                                Program.Etiqueta.arquivo = Program.Etiqueta.arquivo.Replace("#Balanca#", "1");
                                Program.Etiqueta.arquivo = Program.Etiqueta.arquivo.Replace("#Referencia#", "1");
                                Program.Etiqueta.arquivo = Program.Etiqueta.arquivo.Replace("#PLiq#", "1");
                                Program.Etiqueta.arquivo = Program.Etiqueta.arquivo.Replace("#Data#", $"{DateTime.Now.Day}/{DateTime.Now.Month}/{DateTime.Now.Year.ToString().Substring(2, 2)}");
                                Program.Etiqueta.arquivo = Program.Etiqueta.arquivo.Replace("#Hora#", $"{DateTime.Now.Second}:{DateTime.Now.Minute}:{DateTime.Now.Hour}");

                                ZXing.BarcodeWriter brcode = new ZXing.BarcodeWriter();

                                string lbl_1 = $"Balança: {1}";
                                string lbl_2 = $"Ref: {1}";

                                string lbl_3 = $"Data:  {format_string(DateTime.Now.Day.ToString())}/{format_string(DateTime.Now.Month.ToString())}/{DateTime.Now.Year.ToString().Substring(2, 2)} ";
                                string lbl_4 = $"Hora: {format_string(DateTime.Now.Hour.ToString())}:{format_string(DateTime.Now.Minute.ToString())}:{format_string(DateTime.Now.Second.ToString())}";

                                string lbl_5 = $"Peso Liquido:";
                                string lbl__5 = $"{1}";

                                string lbl_6 = $"{1}";

                                System.Drawing.Font font = new System.Drawing.Font("Arial", 10, System.Drawing.FontStyle.Bold, GraphicsUnit.Pixel);
                                System.Drawing.Font fontSmall = new System.Drawing.Font("Arial", 8, System.Drawing.FontStyle.Bold, GraphicsUnit.Pixel);
                                System.Drawing.Font fontBig = new System.Drawing.Font("Arial", 18, System.Drawing.FontStyle.Bold, GraphicsUnit.Pixel);

                                //Font fontLarge = new Font("SKF Sans", 36, FontStyle.Regular, GraphicsUnit.Pixel);
                                Brush brush = Brushes.Black;

                                int x = (int)(100 * (96 / 25.4f)); //100 mm
                                int y = (int)(030 * (96 / 25.4f)); //30 mm

                                Bitmap bitmap = new Bitmap(x, y);

                                int wid = (int)(40 * 96 / 25.4f);
                                int hei = (int)(07 * 96 / 25.4f);

                                //brcode.Format = BarcodeFormat.PDF_417;
                                //brcode.Options = new ZXing.Common.EncodingOptions()
                                //{
                                //    Margin = 0,
                                //    Height = hei,
                                //    Width = wid,
                                //};

                                //Bitmap barcodeBitmap = new Bitmap(brcode.Write(1), wid, hei);

                                using (Graphics graphics = Graphics.FromImage(bitmap))
                                {
                                    graphics.Clear(Color.White);
                                    graphics.DrawString(lbl_1, font, brush, new PointF(10, 20));
                                    graphics.DrawString(lbl_2, font, brush, new PointF(10, 35));

                                    graphics.DrawString(lbl_3, font, brush, new PointF(290, 20));
                                    graphics.DrawString(lbl_4, font, brush, new PointF(290, 35));

                                    //graphics.DrawImage(barcodeBitmap, new PointF(10, 70));
                                    //graphics.DrawImage(barcodeBitmap, new PointF(250, 70));

                                    graphics.DrawString(lbl_5, font, brush, new PointF(155, 60));
                                    graphics.DrawString(lbl__5, fontBig, brush, new PointF(155, 75));

                                    graphics.DrawString(lbl_6, fontSmall, brush, new PointF(50, 90));
                                    graphics.DrawString(lbl_6, fontSmall, brush, new PointF(300, 90));
                                }

                                PrintDocument documento = new PrintDocument();
                                PrinterSettings configImpressora = new PrinterSettings();
                                PageSettings pageSettings = documento.DefaultPageSettings;

                                string[] impressoras = PrinterSettings.InstalledPrinters.Cast<string>().ToArray();
                                string name = "";
                                foreach (string item in impressoras)
                                {
                                    //if (item.Contains("Citizen CL-S631")) { name = item; }
                                    //if (item.Contains("ZDesigner ZD220-203dpi ZPL")) { name = item; }

                                    if (item == impressora.impressora) { name = impressora.impressora; }
                                }
                                ZPLPrintingService prnSvc = new ZPLPrintingService();
                                string zplCode = await prnSvc.GetImageZPLEncoded(bitmap);

                                bitmap.RotateFlip(RotateFlipType.Rotate90FlipNone);
                                //PrintPopup print = new PrintPopup(bitmap);
                                //print.ShowDialog();

                                if (!string.IsNullOrWhiteSpace(name))
                                {
                                    configImpressora.PrinterName = name;
                                    documento.PrinterSettings = configImpressora;
                                    documento.DefaultPageSettings.Landscape = true;
                                    //bitmap.Dispose();
                                }

                                documento.PrintPage += (sender, args) =>
                                {
                                    // Desenhe a imagem no objeto Graphics do evento
                                    args.Graphics.DrawImage(bitmap, 0, 0); // Ajuste a posição conforme necessário
                                };

                                //PrintPreviewDialog previewDialog = new PrintPreviewDialog();
                                //previewDialog.Document = documento;
                                //previewDialog.ShowDialog();

                                documento.Print();
                            }

                            //   }
                        }
                    }

                    // RedeClass impressora = (RedeClass)listPrinter[0];

                }
                else
                {
                    //System.Windows.MessageBox.Show("Nenhuma impressora foi encontrada na base de dados.\n\rCadastre uma na tela de rede!!", "VC Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

            }
            catch (Exception ex)
            {
            }
        }


        public static string format_string(string s)
        {
            try
            {
                if (s.Length == 1)
                {
                    return "0" + s;
                }

                return s;
            }
            catch (Exception)
            {
                return s;
            }
        }

        public static void LoadImpressoras()
        {
            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("@parent", "-");
            _impressoras = Program.SQL.SelectList("select * from Rede where parent = @parent and tipo = 'Impressora'", "Rede", values: parametros);
        }

        public static async void ImpressoraPrint(ProcessosModel processoAtual)
        {
            try
            {
                foreach (RedeClass impressora in _impressoras)
                {
                    //if (Program.Configuracao.id_Impressora != impressora.Id) { return; }

                    ConfiguracaoClass confg = (ConfiguracaoClass)Program.SQL.SelectObject("SELECT * FROM Configuracao WHERE estacao = @estacao", "Configuracao", new Dictionary<string, object>()
                    {
                        {"@estacao", Environment.MachineName}
                    });

                    if (impressora.Id != confg.id_Impressora)
                    { }
                    else
                    {

                    }

                    string zplCode = "";

                    ZXing.BarcodeWriter brcode = new ZXing.BarcodeWriter();

                    string lbl_op = "N° Op:";
                    string lbl_op_r = processoAtual.Op.ToString();

                    string lbl_cli = "Cliente:";
                    string lbl_cli_r = processoAtual.Cliente.ToString();

                    var soma = Program.SQL.SelectList("SELECT SUM(Peso) AS SOMA FROM Log_Processos Where Id_processo = @Id", "Log_Processos", "SOMA", new Dictionary<string, object>()
                    {
                        {"@Id", processoAtual.Id }
                    });

                    string lbl_peso = "";
                    string lbl_peso_r = "";
                    float dSoma = 0;

                    if (soma.Count > 0)
                    {
                        dSoma = (float)Math.Round(Convert.ToDouble(soma[0]), 4);
                        lbl_peso = "Peso:";
                        lbl_peso_r = dSoma.ToString();
                        //lbl_peso_r = dSoma.ToString().Substring(0, 7);
                    }
                    else
                    {
                        lbl_peso = "Peso:";
                        lbl_peso_r = "0,00";
                    }

                    string lbl_qtfl = "Qtd Folhas:";
                    string lbl_qtfl_r = processoAtual.Quantidade.ToString();

                    string lbl_tppl = "Tipo Papel:";
                    string lbl_tppl_r = processoAtual.Tipo.ToString();

                    string lbl_fmt = "Formato:";
                    string lbl_fmt_r = processoAtual.Formato.ToString();

                    string lbl_gr = "Gram:";
                    string lbl_gr_r = processoAtual.GramaturaDigitada.ToString();

                    string lbl_dtin = "Data Início:";
                    string lbl_dtin_r = "";

                    string lbl_dtfm = "Data Término:";
                    string lbl_dtfm_r = DateTime.Now.Date.ToString().Substring(0, 10);

                    string lbl_hrin = "Horário inicial:";
                    string lbl_hrin_r = "";

                    string lbl_hrfm = "Horário final:";
                    string lbl_hrfm_r = DateTime.Now.ToString().Substring(11, 8);

                    string lbl_opr = "Operador:";
                    string lbl_opr_r = Program._usuarioLogado.Nome;

                    if (lbl_opr_r.Length <= 21)
                    {
                        lbl_opr_r = Program._usuarioLogado.Nome.Substring(0, lbl_opr_r.Length);
                    }
                    else if (lbl_opr_r.Length >= 22)
                    {
                        lbl_opr_r = Program._usuarioLogado.Nome.Substring(0, 22);
                    }

                    string lbl_trn = "Turno:";
                    string lbl_trn_r = "";
                    if (DateTime.Now.Hour <= 12) { lbl_trn_r = "Matutino"; } else if (DateTime.Now.Hour > 12) { lbl_trn_r = "Vespertino"; }

                    string lbl_obs = "Obs:";
                    string lbl_obs_r = processoAtual.Descricao;

                    string split1 = "";
                    string split2 = "";
                    string split3 = "";

                    if (lbl_obs_r.Length <= 60)
                    {
                        split1 = lbl_obs_r;
                    }
                    else if (lbl_obs_r.Length > 60 & lbl_obs_r.Length < 120)
                    {
                        split1 = lbl_obs_r.Substring(0, 60);
                        split2 = lbl_obs_r.Substring(60, lbl_obs_r.Length - 60);
                    }
                    else if (lbl_obs_r.Length > 60)
                    {
                        split1 = lbl_obs_r.Substring(0, 60);
                        split2 = lbl_obs_r.Substring(60, 60);
                        split3 = lbl_obs_r.Substring(120);
                    }



                    lbl_opr_r = processoAtual.Op;
                    if (lbl_opr_r.Length <= 21)
                    {
                        lbl_opr_r = Program._usuarioLogado.Nome.Substring(0, lbl_opr_r.Length);
                    }
                    else if (lbl_opr_r.Length >= 22)
                    {
                        lbl_opr_r = Program._usuarioLogado.Nome.Substring(0, 22);
                    }


                    lbl_cli_r = processoAtual.Cliente;
                    if (lbl_cli_r.Length <= 21)
                    {
                        lbl_cli_r = processoAtual.Cliente.ToString().Substring(0, lbl_cli_r.Length);
                    }
                    else if (lbl_cli_r.Length >= 22)
                    {
                        lbl_cli_r = processoAtual.Cliente.ToString().Substring(0, 22);
                    }


                    lbl_dtin_r = processoAtual.dateinsert.Date.ToString();
                    if (lbl_dtin_r.Length <= 9)
                    {
                        lbl_dtin_r = processoAtual.dateinsert.Date.ToString().Substring(0, lbl_dtin_r.Length);
                    }
                    else if (lbl_dtin_r.Length >= 10)
                    {
                        lbl_dtin_r = processoAtual.dateinsert.Date.ToString().Substring(0, 10);
                    }


                    lbl_hrin_r = processoAtual.dateinsert.ToString();
                    if (lbl_hrin_r.Length <= 17)
                    {
                        lbl_hrin_r = processoAtual.dateinsert.ToString().Substring(11, 8);
                    }
                    else if (lbl_hrin_r.Length >= 18)
                    {
                        lbl_hrin_r = processoAtual.dateinsert.ToString().Substring(11, 8);
                    }


                    lbl_tppl_r = processoAtual.Tipo.ToString();
                    lbl_fmt_r = processoAtual.Formato.ToString();
                    lbl_qtfl_r = processoAtual.TotalContagem.ToString();


                    System.Drawing.Font f1_small = new System.Drawing.Font("Arial", 10, System.Drawing.FontStyle.Regular, GraphicsUnit.Pixel);
                    System.Drawing.Font f1 = new System.Drawing.Font("Arial", 18, System.Drawing.FontStyle.Regular, GraphicsUnit.Pixel);
                    System.Drawing.Font fmn = new System.Drawing.Font("Arial", 15, System.Drawing.FontStyle.Bold, GraphicsUnit.Pixel);

                    System.Drawing.Brush brush = System.Drawing.Brushes.Black;

                    int x = (int)(148 * (96 / 25.4f));
                    int y = (int)(105 * (96 / 25.4f));

                    Bitmap bitmap = new Bitmap(x, y);

                    int wid = (int)(35 * 96 / 25.4f);
                    int hei = (int)(12 * 96 / 25.4f);

                    System.Drawing.Pen blackPen = new System.Drawing.Pen(System.Drawing.Color.Black, 2);

                    using (Graphics graphics = Graphics.FromImage(bitmap))
                    {
                        graphics.Clear(System.Drawing.Color.White);

                        //Draw Black lines


                        //op
                        graphics.DrawLine(blackPen, 72f, 30f, 200f, 30f);

                        //cliente
                        graphics.DrawLine(blackPen, 280f, 30f, 540f, 30f);

                        //peso
                        graphics.DrawLine(blackPen, 65f, 80f, 200f, 80f);

                        //qtfl
                        graphics.DrawLine(blackPen, 309f, 80f, 440f, 80f);

                        //tipo papel
                        graphics.DrawLine(blackPen, 110f, 130f, 225f, 130f);

                        //formato
                        graphics.DrawLine(blackPen, 314f, 130f, 388f, 130f);

                        //gram
                        graphics.DrawLine(blackPen, 449f, 130f, 540f, 130f);

                        //data inicio
                        graphics.DrawLine(blackPen, 110f, 180f, 225f, 180f);

                        //data termino
                        graphics.DrawLine(blackPen, 355f, 180f, 470f, 180f);

                        //horario inicial
                        graphics.DrawLine(blackPen, 132f, 230f, 225f, 230f);

                        //horario final
                        graphics.DrawLine(blackPen, 343f, 230f, 470f, 230f);

                        //operador
                        graphics.DrawLine(blackPen, 100f, 280f, 340f, 280f);

                        //turno
                        graphics.DrawLine(blackPen, 404f, 280f, 540f, 280f);

                        //op
                        graphics.DrawString(lbl_op, f1, brush, new PointF(12, 10));
                        graphics.DrawString(lbl_op_r, f1, brush, new PointF(74, 10));

                        //cliente
                        graphics.DrawString("Etiqueta não automatizada.", f1_small, brush, new PointF(400, 0));
                        graphics.DrawString(lbl_cli, f1, brush, new PointF(210, 10));
                        graphics.DrawString(lbl_cli_r, f1, brush, new PointF(281, 10));

                        //peso
                        graphics.DrawString(lbl_peso, f1, brush, new PointF(12, 60));
                        graphics.DrawString(lbl_peso_r, f1, brush, new PointF(65, 60));

                        //qt folhas
                        graphics.DrawString(lbl_qtfl, f1, brush, new PointF(208, 60));
                        graphics.DrawString(lbl_qtfl_r, f1, brush, new PointF(308, 60));

                        //tp papel
                        graphics.DrawString(lbl_tppl, f1, brush, new PointF(12, 110));
                        graphics.DrawString(lbl_tppl_r, f1, brush, new PointF(112, 110));

                        //formato
                        graphics.DrawString(lbl_fmt, f1, brush, new PointF(236, 110));
                        graphics.DrawString(lbl_fmt_r, f1, brush, new PointF(316, 110));

                        //gram
                        graphics.DrawString(lbl_gr, f1, brush, new PointF(393, 110));
                        graphics.DrawString(lbl_gr_r, f1, brush, new PointF(450, 110));

                        //data inicio
                        graphics.DrawString(lbl_dtin, f1, brush, new PointF(12, 160));
                        graphics.DrawString(lbl_dtin_r, f1, brush, new PointF(112, 160));

                        //data Término
                        graphics.DrawString(lbl_dtfm, f1, brush, new PointF(236, 160));
                        graphics.DrawString(lbl_dtfm_r, f1, brush, new PointF(356, 160));

                        //horário inicial
                        graphics.DrawString(lbl_hrin, f1, brush, new PointF(12, 210));
                        graphics.DrawString(lbl_hrin_r, f1, brush, new PointF(132, 210));

                        //horário final
                        graphics.DrawString(lbl_hrfm, f1, brush, new PointF(236, 210));
                        graphics.DrawString(lbl_hrfm_r, f1, brush, new PointF(346, 210));

                        graphics.DrawString(lbl_opr, f1, brush, new PointF(12, 260));
                        graphics.DrawString(lbl_opr_r, f1, brush, new PointF(100, 260));

                        graphics.DrawString(lbl_trn, f1, brush, new PointF(346, 260));
                        graphics.DrawString(lbl_trn_r, f1, brush, new PointF(406, 260));

                        graphics.DrawString(lbl_obs, f1, brush, new PointF(12, 305));
                        graphics.DrawString(split1, fmn, brush, new PointF(56, 308));
                        graphics.DrawString(split2, fmn, brush, new PointF(56, 333));
                        graphics.DrawString(split3, fmn, brush, new PointF(56, 358));


                    }

                    ZPLPrintingService prnSvc = new ZPLPrintingService();
                    zplCode = await prnSvc.GetImageZPLEncoded(bitmap);
                    zplCode = zplCode.Replace("#barcode#", lbl_op);

                    PrintDocument documento = new PrintDocument();
                    PrinterSettings configImpressora = new PrinterSettings();
                    PageSettings pageSettings = documento.DefaultPageSettings;

                    string[] impressoras = PrinterSettings.InstalledPrinters.Cast<string>().ToArray();
                    string name = "";
                    foreach (string item in impressoras)
                    {

                        if (item == impressora.impressora) { name = impressora.impressora; }
                    }

                    //bitmap.RotateFlip(RotateFlipType.Rotate90FlipNone);
                    PrintPopup print = new PrintPopup(bitmap);
                    print.ShowDialog();

                    if (!string.IsNullOrWhiteSpace(name))
                    {
                        configImpressora.PrinterName = name;
                        documento.PrinterSettings = configImpressora;
                        documento.DefaultPageSettings.Landscape = true;
                    }

                    documento.PrintPage += (sender, args) =>
                    {
                        args.Graphics.DrawImage(bitmap, 0, 0);
                    };

                    try
                    {
                        documento.Print();
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

    }
}

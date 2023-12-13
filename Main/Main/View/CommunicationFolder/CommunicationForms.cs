using iTextSharp.text;
using iTextSharp.text.pdf;
using Main.Model;
using Main.Model.EtiquetaFolder;
using Main.View.PagesFolder.Configuration;
using Main.View.PopupFolder;
using Microsoft.Office.Interop.Excel;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Xml.Linq;
using ZPL;
using ZXing;
using ZXing.Common;
using static System.Net.Mime.MediaTypeNames;
using Brushes = System.Drawing.Brushes;

namespace Main.View.CommunicationFolder
{
    public partial class CommunicationForms : Form
    {
        private List<Object> _indicadores = new List<Object>();
        private List<Object> _impressoras = new List<Object>();
        private List<IndicadorClass> _indClass = new List<IndicadorClass>();
        private List<double> _Pesos = new List<double>();
        public CommunicationForms()
        {
            InitializeComponent();
            LoadAll();
            Program.SERIALPORT.DataReceived += serialPort_DataReceived;
        }

        public void LoadAll()
        {
            LoadIndicadores();
            /*if (_indicadores.Count > 0)
            {
                foreach (RedeClass indicador in _indicadores)
                {
                    IndicadorClass indClass = new IndicadorClass();
                    indClass.indicador = indicador;
                    _indClass.Add(indClass);
                }
                //foreach (IndicadorClass indicador in _indClass)
                //{
                //    indicador.Start();
                //}

                //_indClass.Add(new IndicadorClass());
            }*/
            LoadImpressoras();
            //ImpressoraPrint("100000", "DESCRICAO TESTE", "06.04.22.A", "0015", 10.51);
        }

        public void ReLoadIndicadores()
        {
            Program.Registradores.Clear();
            LoadIndicadores();
        }
        public void LoadIndicadores()
        {
            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("@parent", Environment.MachineName);
            _indicadores = Program.SQL.SelectList("select * from Rede where parent = @parent and tipo = 'Balança'", "Rede", values: parametros);
            _indClass.Clear();
            foreach (RedeClass indicador in _indicadores)
            {
                IndicadorClass indClass = new IndicadorClass();
                indClass.indicador = indicador;
                _indClass.Add(indClass);
            }

            if (Program.Registradores.Count == 0)
            {
                int i = 0;
                foreach (RedeClass indicador in _indicadores)
                {
                    Program.Registradores.Add(indicador.Id, _indClass[i]);
                    i++;
                }
            }
            else
            {
                int i = 0;
                foreach (RedeClass indicador in _indicadores)
                {
                    if (!Program.Registradores.ContainsKey(indicador.Id))
                    {
                        Program.Registradores.Add(indicador.Id, _indClass[i]);
                    }
                    i++;
                }
            }
            
        }

        public void LoadImpressoras()
        {
            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("@parent", "-");
            _impressoras = Program.SQL.SelectList("select * from Rede where parent = @parent and tipo = 'Impressora'", "Rede", values: parametros);
        }

        public async void ImpressoraPrint(EtiquetaInfo etiqueta, int type)
        {
            foreach (RedeClass impressora in _impressoras)
            {
                if (Program.Configuracao.id_Impressora == impressora.Id)
                {
                    if(Program.Etiqueta != null)
                    {
                        //Código para criar etiqueta.
                        ZXing.BarcodeWriter brcode = new ZXing.BarcodeWriter();

                        string lbl_1 = etiqueta.quantidadePecas;
                        string lbl_2 = etiqueta.date + "W";
                        string lbl_3 = etiqueta.produtoProduzido;
                        string lbl_4 = "80 - MADE IN BRAZIL";
                        string lbl_pack = etiqueta.packCaixa;
                        string lbl_5 = etiqueta.partNumber;
                        string lbl_6 = "weight cheked approved";
                        string lbl_7 = $"^PR7\n\r^BY3,2,56^FT50,107^BER,,Y,N\r\n^FH\\^FD{etiqueta.earn}^FS\r\n^PQ1,0,1,Y";

                        System.Drawing.Font fontRegularR = new System.Drawing.Font("SKF Sans", 13, FontStyle.Regular, GraphicsUnit.Pixel);
                        System.Drawing.Font fontLargeB = new System.Drawing.Font("SKF Sans", 20, FontStyle.Bold, GraphicsUnit.Pixel);
                        System.Drawing.Font fontMediumB = new System.Drawing.Font("SKF Sans", 17, FontStyle.Bold, GraphicsUnit.Pixel);
                        System.Drawing.Font fontSmallB = new System.Drawing.Font("SKF Sans", 13, FontStyle.Bold, GraphicsUnit.Pixel);
                        System.Drawing.Brush brush = System.Drawing.Brushes.Black;

                        int x = (int)(60 * (96 / 25.4f));  //60 mm
                        int y = (int)(050 * (96 / 25.4f)); //50 mm

                        Bitmap bitmap = new Bitmap(x, y);

                        //Terminar de arrumar o tamanho da etiqueta.
                        int wid = (int)(51 * 96 / 25.4f);
                        int hei = (int)(13 * 96 / 25.4f);

                        brcode.Format = BarcodeFormat.EAN_13;
                        brcode.Options = new ZXing.Common.EncodingOptions()
                        {
                            Margin = 0,
                            Height = hei,
                            Width = wid,
                            NoPadding = false,
                        };

                        Bitmap barcodeBitmap = new Bitmap(brcode.Write(etiqueta.earn), wid, hei);

                        using (Graphics graphics = Graphics.FromImage(bitmap))
                        {
                            graphics.Clear(System.Drawing.Color.White);

                            graphics.DrawString(lbl_1, fontLargeB, brush, new PointF(90, 2));
                            graphics.DrawString(lbl_2, fontRegularR, brush, new PointF(180, 2));
                            graphics.DrawString(lbl_3, fontMediumB, brush, new PointF(55, 25));
                            graphics.DrawString(lbl_4, fontSmallB, brush, new PointF(24, 50));
                            graphics.DrawString(lbl_pack, fontRegularR, brush, new PointF(198, 50));
                            ////Baixo da linha vermelha
                            graphics.DrawString(lbl_6, fontSmallB, brush, new PointF(32, 65));
                            graphics.DrawString(lbl_5, fontRegularR, brush, new PointF(80, 90));
                            graphics.DrawImage(barcodeBitmap, new PointF(15, 110));
                        }

                        ZPLPrintingService prnSvc = new ZPLPrintingService();
                        //Bitmap bmp = RotateBitmap(bitmap, 90);
                        string zplCode = await prnSvc.GetImageZPLEncoded(bitmap);
                        zplCode = zplCode.Replace("#barcode#", lbl_7);
                        //Console.WriteLine(zplCode);

                        //TCP
                        if (type == 0)
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

                                            Console.WriteLine("Dispositivo conectado com sucesso! {0}", impressora.full_name);

                                            using (NetworkStream stream = tcpClient.GetStream())
                                            {
                                                byte[] bufferWrite = Encoding.UTF8.GetBytes(zplCode);
                                                stream.Write(bufferWrite, 0, bufferWrite.Length);
                                                Console.WriteLine($"Dados enviados: {bufferWrite}");
                                            }
                                        }
                                    }
                                    catch (OperationCanceledException)
                                    {
                                        Console.WriteLine("Tempo limite de conexão atingido. {0}", impressora.full_name);
                                    }
                                    catch (IOException ex)
                                    {
                                        Console.WriteLine($"Erro na escrita dos dados: {ex.Message}" + " {0}", impressora.full_name);
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine($"Erro ao tentar conectar: {ex.Message}" + " {0}", impressora.full_name);
                                    }
                                }
                            });
                        }
                        //Serial
                        else if (type == 1)
                        {
                            Task taskSerial = Task.Run(() =>
                            {

                                try
                                {
                                    if (Program.IMPRESSORAPORT.IsOpen)
                                    {
                                        // Program.IMPRESSORAPORT.Open();
                                        //Console.WriteLine("Dispositivo conectado com sucesso! {0}", impressora.full_name);

                                        byte[] bufferWrite = Encoding.UTF8.GetBytes(zplCode);
                                        Program.IMPRESSORAPORT.Write(bufferWrite, 0, bufferWrite.Length);
                                        //Console.WriteLine($"Dados enviados: {Encoding.UTF8.GetString(bufferWrite)}");
                                        //MessageBox.Show("Enviado para impressora");
                                    }

                                }
                                catch (TimeoutException)
                                {
                                    Console.WriteLine("Tempo limite de conexão atingido. {0}", impressora.full_name);
                                }
                                catch (UnauthorizedAccessException ex)
                                {
                                    Console.WriteLine($"Erro de acesso à porta serial: {ex.Message} {impressora.full_name}");
                                }
                                catch (IOException ex)
                                {
                                    Console.WriteLine($"Erro na escrita dos dados: {ex.Message} {impressora.full_name}");
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine($"Erro ao tentar conectar: {ex.Message} {impressora.full_name}");
                                }
                                finally
                                {
                                    //if (Program.IMPRESSORAPORT.IsOpen)
                                    //    Program.IMPRESSORAPORT.Close();
                                }

                            });
                        }
                        //Impressora normal
                        else if (type == 2) 
                        {

                            PrintDocument documento = new PrintDocument();
                            PrinterSettings configImpressora = new PrinterSettings();
                            PageSettings pageSettings = documento.DefaultPageSettings;

                            string[] impressoras = PrinterSettings.InstalledPrinters.Cast<string>().ToArray();
                            string name = "";
                            foreach (string item in impressoras)
                            {

                                if (item == impressora.impressora) { name = impressora.impressora; }
                            }

                            bitmap.RotateFlip(RotateFlipType.Rotate90FlipNone);
                            //PrintPopup print = new PrintPopup(bitmap);
                            //print.ShowDialog();

                            if (!string.IsNullOrWhiteSpace(name))
                            {
                                configImpressora.PrinterName = name;
                                documento.PrinterSettings = configImpressora;
                                documento.DefaultPageSettings.Landscape = true;
                            }

                            documento.PrintPage += (sender, args) =>
                            {
                                args.Graphics.DrawImage(bitmap, 0, 0); // Ajuste a posição conforme necessário
                            };

                            documento.Print();


                            //TPrint(etiqueta, barcodeBitmap, impressora);
                            //PrintDocument documento = new PrintDocument();
                            //PrinterSettings configImpressora = new PrinterSettings();
                            //PageSettings pageSettings = documento.DefaultPageSettings;
                            //if (!string.IsNullOrWhiteSpace(impressora.impressora))
                            //{
                            //    configImpressora.PrinterName = impressora.impressora;
                            //    documento.PrinterSettings = configImpressora;
                            //    documento.DefaultPageSettings.Landscape = true;
                            //    //documento.Print();
                            //    //bitmap.Dispose();
                            //}

                            ////PrinterResolution Resolution = new PrinterResolution();
                            ////Resolution.X = 2000;
                            ////Resolution.Y = 2000;
                            ////Resolution.Kind = PrinterResolutionKind.High;
                            ////pageSettings.PrinterResolution = Resolution;

                            //try
                            //{
                            //    //System.Drawing.Font fontSmall_ = new System.Drawing.Font("SKF Sans", 10, FontStyle.Regular, GraphicsUnit.Pixel);
                            //    //System.Drawing.Font font_ = new System.Drawing.Font("SKF Sans", 15, FontStyle.Regular, GraphicsUnit.Pixel);
                            //    //int conste = 1;
                            //    ////Graphics GR = Resolution.Kind
                            //    //Bitmap bitMapImage = new Bitmap(250, 250);

                            //    //using (Graphics graphics = Graphics.FromImage(bitMapImage))
                            //    //{
                            //    //    graphics.Clear(System.Drawing.Color.White);
                            //    //    graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBilinear;
                            //    //    graphics.DrawString(lbl_1, font_, brush, new PointF(120 * conste, 79 * conste));
                            //    //    graphics.DrawString(lbl_2, fontSmall_, brush, new PointF(182 * conste, 80 * conste));
                            //    //    graphics.DrawString(lbl_3, fontSmall_, brush, new PointF(119 * conste, 95 * conste));
                            //    //    graphics.DrawString(lbl_4, fontSmall_, brush, new PointF(97 * conste, 110 * conste));
                            //    //    graphics.DrawString(lbl_pack, fontSmall_, brush, new PointF(192 * conste, 110 * conste));
                            //    //    ///Baixo da linha vermelha
                            //    //    graphics.DrawString(lbl_5, fontSmall_, brush, new PointF(119 * conste, 143 * conste));
                            //    //    graphics.DrawString(lbl_6, fontSmall_, brush, new PointF(96 * conste, 125 * conste));
                            //    //    graphics.DrawImage(barcodeBitmap, new PointF(60 * conste, 160 * conste));
                            //    //}

                            //    //Bitmap fixedBitmap = RotateBitmap(bitMapImage, 180);
                            //    PrintPopup print = new PrintPopup(bitmap);
                            //    print.ShowDialog();

                            //    documento.PrintPage += (sender, e) =>
                            //    {
                            //        ///Imprimir a imagem rotacionada.
                            //        e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                            //        e.Graphics.DrawImage(bitmap, 0, 0);
                            //    };

                            //    documento.PrinterSettings = configImpressora;
                            //    documento.Print();
                            //}
                            //catch (Exception ex)
                            //{
                            //}

                        }

                    }
                }
            }
            
        }

        private void TPrint(EtiquetaInfo etiqueta, Bitmap barcodeBitmap, RedeClass impressora)
        {
            string lbl_1 = etiqueta.quantidadePecas;
            string lbl_2 = etiqueta.date + "W";
            string lbl_3 = etiqueta.produtoProduzido;
            string lbl_4 = "80 - MADE IN BRAZIL";
            string lbl_pack = etiqueta.packCaixa;
            string lbl_5 = etiqueta.partNumber;
            string lbl_6 = "weight cheked approved";
            string lbl_7 = $"^PR7\n\r^BY3,2,56^FT50,107^BER,,Y,N\r\n^FH\\^FD{etiqueta.earn}^FS\r\n^PQ1,0,1,Y";
            //using (Bitmap bitmap = new Bitmap(800, 600))
            using (Bitmap bitmap = new Bitmap(250, 250))
            {
                System.Drawing.Font fontSmall_ = new System.Drawing.Font("SKF Sans", 10, FontStyle.Regular, GraphicsUnit.Pixel);
                System.Drawing.Font font_ = new System.Drawing.Font("SKF Sans", 15, FontStyle.Regular, GraphicsUnit.Pixel);
                System.Drawing.Brush brush = System.Drawing.Brushes.Black;
                int conste = 1;
                using (Graphics graphics = Graphics.FromImage(bitmap))
                {
                    // Desenhe os elementos na tela, como labels, etc.
                    // Exemplo:
                    //graphics.DrawString("Exemplo de Texto", new System.Drawing.Font("Arial", 12), Brushes.Black, new PointF(50, 50));
                    graphics.Clear(System.Drawing.Color.White);
                    graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBilinear;
                    graphics.DrawString(lbl_1, font_, brush, new PointF(120 * conste, 79 * conste));
                    graphics.DrawString(lbl_2, fontSmall_, brush, new PointF(182 * conste, 80 * conste));
                    graphics.DrawString(lbl_3, fontSmall_, brush, new PointF(119 * conste, 95 * conste));
                    graphics.DrawString(lbl_4, fontSmall_, brush, new PointF(97 * conste, 110 * conste));
                    graphics.DrawString(lbl_pack, fontSmall_, brush, new PointF(192 * conste, 110 * conste));
                    ///Baixo da linha vermelha
                    graphics.DrawString(lbl_5, fontSmall_, brush, new PointF(119 * conste, 143 * conste));
                    graphics.DrawString(lbl_6, fontSmall_, brush, new PointF(96 * conste, 125 * conste));
                    graphics.DrawImage(barcodeBitmap, new PointF(60 * conste, 160 * conste));
                }

                // Salvar a imagem em um arquivo temporário
                string tempImagePath = Path.GetTempFileName() + ".png";
                bitmap.Save(tempImagePath, ImageFormat.Png);

                // Criar um documento PDF
                Document doc = new Document();

                // Definir o caminho para o arquivo PDF de saída
                string pdfFilePath = "output.pdf";
                PdfWriter.GetInstance(doc, new FileStream(pdfFilePath, FileMode.Create));

                doc.Open();

                // Adicionar a imagem ao PDF
                iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(tempImagePath);
                img.Rotation = 180;
                img.RotationDegrees = 180;
                img.Rotate();
                doc.Add(img);

                doc.Close();

                Thread.Sleep(500);

                PrintDocument documento = new PrintDocument();
                PrinterSettings configImpressora = new PrinterSettings();
                PageSettings pageSettings = documento.DefaultPageSettings;

                if (!string.IsNullOrWhiteSpace(impressora.impressora))
                {
                    configImpressora.PrinterName = impressora.impressora;
                    documento.PrinterSettings = configImpressora;
                    documento.DefaultPageSettings.Landscape = true;
                    bitmap.Dispose();
                }

                documento.PrintPage += (sender, args) =>
                {
                    using (FileStream fs = new FileStream(pdfFilePath, FileMode.Open))
                    {
                        byte[] png = Freeware.Pdf2Png.Convert(fs, 1);
                        System.Drawing.Image x = (Bitmap)((new ImageConverter()).ConvertFrom(png));

                        //Bitmap fixedBitmap = RotateBitmap(x, 180);
                        //System.Drawing.Image i = System.Drawing.Image.FromStream(fs);
                        //x.RotateFlip(RotateFlipType.Rotate180FlipNone);
                        args.Graphics.DrawImage(x, -90, -120);
                    }
                };

                documento.Print();

                // Redimensionar a imagem para 250x250
                //using (Bitmap resizedBitmap = new Bitmap(bitmap, new Size(250, 250)))
                //{
                //    // Salvar a imagem redimensionada
                //    string resizedImagePath = "resized.png";
                //    resizedBitmap.Save(resizedImagePath, ImageFormat.Png);
                //    Console.WriteLine("Imagem redimensionada salva em: " + resizedImagePath);
                //}

                // Excluir o arquivo temporário
                File.Delete(tempImagePath);
            }

            Console.WriteLine("PDF criado com sucesso!");
            Console.ReadLine();
        }

        private void vUpdatePBvalue(double PB)
        {
            if(_Pesos.Count == 3)
            {
                _Pesos.RemoveAt(0);
                _Pesos.Add(PB);
            }
            else
            {
                _Pesos.Add(PB);
            }
            double SomaPesos = 0, MediaPesos = 0;
            foreach (double peso in _Pesos)
            {
                SomaPesos += peso;
            }
            MediaPesos = SomaPesos / _Pesos.Count;
            MediaPesos = Math.Round(MediaPesos, 3);
            if (MediaPesos == _Pesos[0])
            {
                ((IndicadorClass)Program.Registradores.ElementAt(0).Value).PB = MediaPesos;
            }
        }

        private void serialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            // Lógica para tratamento dos dados recebidos pela porta serial
            if (Program.SERIALPORT.BytesToRead > 0)
            {
                try
                {
                    string data = Program.SERIALPORT.ReadLine();
                //    Console.WriteLine(data);
                    //((IndicadorClass)Program.Registradores[0]).indicador;
                    if (Program.Registradores.Count > 0)
                    {
                        RedeClass redeClass = new RedeClass();
                        redeClass = ((IndicadorClass)Program.Registradores.ElementAt(0).Value).indicador;
                        if (redeClass.fabricante == "AEPH do Brasil" && redeClass.protocolo == "TCA")
                        {
                            if (data.Trim() == "SOBRE" || data.Trim() == "SATURA")
                            {

                            }else
                            {
                                int indexPB = data.IndexOf("PB:");
                                int indexPL = data.IndexOf("PL:");
                                int indexT = data.IndexOf("T:");

                                string _PB = data.Substring(indexPB + 3, indexPL - (indexPB + 3)).Trim();
                                _PB = _PB.Replace("kg", "").Trim();
                                _PB = _PB.Replace(".", ",").Trim();
                                string _PL = data.Substring(indexPL + 3, indexT - (indexPL + 3)).Trim();
                                _PL = _PL.Replace("kg", "").Trim();
                                _PL = _PL.Replace(".", ",").Trim();
                                string _T = data.Substring(indexT + 3).Trim();
                                _T = _T.Replace("kg", "").Trim();
                                _T = _T.Replace(".", ",").Trim();

                                if (double.TryParse(_PB, out double resultPB))
                                {
                                  //  Console.WriteLine("PB: " + resultPB);
                                    //((IndicadorClass)Program.Registradores.ElementAt(0).Value).PB = resultPB;
                                    vUpdatePBvalue(resultPB);
                                }
                                if (double.TryParse(_PL, out double resultPL))
                                {
                               //     Console.WriteLine("PL: " + resultPL);
                                    ((IndicadorClass)Program.Registradores.ElementAt(0).Value).PL = resultPL;
                                }
                                if (double.TryParse(_T, out double resultT))
                                {
                                 //   Console.WriteLine("T: " + resultT);
                                    ((IndicadorClass)Program.Registradores.ElementAt(0).Value).T = resultT;
                                }
                            }

                        }
                        else if (redeClass.fabricante == "Toledo do Brasil" && redeClass.protocolo == "P03")
                        {
                            string _Peso = data.Substring(4, 6);
                            string _T = data.Substring(10, 6);
                            string hex1 = data.Substring(1, 1);
                            string hex2 = data.Substring(2, 1);
                            string hex3 = data.Substring(3, 1);

                            byte[] bytes = System.Text.Encoding.ASCII.GetBytes(hex1);
                            string binaryString = string.Empty;
                            foreach (byte b in bytes)
                            {
                                binaryString += Convert.ToString(b, 2).PadLeft(8, '0');
                            }
                            string sDecimal = $"{binaryString[2]}{binaryString[1]}{binaryString[0]}";
                            double dDecimal = 1;
                            switch (sDecimal)
                            {
                                case "001":
                                    dDecimal = 10;
                                    break;
                                case "010":
                                    dDecimal = 1;
                                    break;
                                case "011":
                                    dDecimal = 0.1;
                                    break;
                                case "100":
                                    dDecimal = 0.01;
                                    break;
                                case "101":
                                    dDecimal = 0.001;
                                    break;
                                case "110":
                                    dDecimal = 0.0001;
                                    break;
                                default:
                                    dDecimal = 1;
                                    break;
                            }
                            bytes = System.Text.Encoding.ASCII.GetBytes(hex2);
                            foreach (byte b in bytes)
                            {
                                binaryString += Convert.ToString(b, 2).PadLeft(8, '0');
                            }
                            double d_negativo = 1;
                            if (binaryString[1] == '1')
                            {
                                d_negativo = -1;
                            }


                            if (double.TryParse(_Peso, out double resultPB))
                            {
                              //  Console.WriteLine("PB: " + resultPB * dDecimal * d_negativo);
                                //((IndicadorClass)Program.Registradores.ElementAt(0).Value).PB = resultPB * dDecimal * d_negativo;
                                vUpdatePBvalue(resultPB * dDecimal * d_negativo);
                            }
                            if (double.TryParse(_T, out double resultT))
                            {
                             //   Console.WriteLine("T: " + resultT * dDecimal * d_negativo);
                                ((IndicadorClass)Program.Registradores.ElementAt(0).Value).T = resultT * dDecimal * d_negativo;
                            }

                        }
                        else if (redeClass.fabricante == "Alfa Instrumentos" && redeClass.protocolo == "TRC")
                        {
                            if (data.Trim() == "SOBRE" || data.Trim() == "SATURA" || data.Trim() == "S<BRE")
                            {

                            }
                            else
                            {
                                int indexPB = data.IndexOf("PB:");
                                if(indexPB == -1) indexPB = data.IndexOf("**:");
                                if (indexPB == -1) indexPB = data.IndexOf("PL:");
                                int indexT = data.IndexOf("T:");
                                if (indexT == -1) indexT = data.IndexOf(" *:")+1;
                                // Extrair os valores de PB e T
                                string _PB = data.Substring(indexPB + 3, indexT - indexPB - 4).Trim();
                                _PB = _PB.Replace(".", ",").Trim();
                                string _T = data.Substring(indexT + 2).Trim();
                                _T = _T.Replace(".", ",").Trim();

                                if (double.TryParse(_PB, out double resultPB))
                                {
                                  //  Console.WriteLine("PB: " + resultPB);
                                    //((IndicadorClass)Program.Registradores.ElementAt(0).Value).PB = resultPB;
                                    vUpdatePBvalue(resultPB);
                                }
                                if (double.TryParse(_T, out double resultT))
                                {
                                //    Console.WriteLine("T: " + resultT);
                                    ((IndicadorClass)Program.Registradores.ElementAt(0).Value).T = resultT;
                                }
                            }
                        }

                    }
                }
                catch (Exception ex)
                {
                }
            }
        }

        private Bitmap RotateBitmap(Bitmap bitmap, float angle)
        {
            //Bitmap rotatedBitmap = new Bitmap(bitmap.Height, bitmap.Width);
            Bitmap rotatedBitmap = new Bitmap(bitmap.Width, bitmap.Height);
            using (Graphics graphics = Graphics.FromImage(rotatedBitmap))
            {
                graphics.Clear(System.Drawing.Color.White);
                graphics.InterpolationMode = InterpolationMode.HighQualityBilinear;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                System.Drawing.Point rotationPoint = new System.Drawing.Point(bitmap.Width / 2, bitmap.Height / 2);
                graphics.TranslateTransform(rotationPoint.X, rotationPoint.Y);
                graphics.RotateTransform(angle);
                graphics.TranslateTransform(-rotationPoint.X, -rotationPoint.Y);

                graphics.DrawImage(bitmap, new System.Drawing.Point(0, 0));
            }

            return rotatedBitmap;
        }

        public static Bitmap RotateImage(System.Drawing.Image image, PointF offset, float angle)
        {
            if (image == null)
                throw new ArgumentNullException("image");

            //create a new empty bitmap to hold rotated image
            Bitmap rotatedBmp = new Bitmap(image.Width, image.Height);
            rotatedBmp.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            //make a graphics object from the empty bitmap
            Graphics g = Graphics.FromImage(rotatedBmp);

            //Put the rotation point in the center of the image
            g.TranslateTransform(offset.X, offset.Y);

            //rotate the image
            g.RotateTransform(angle);

            //move the image back
            g.TranslateTransform(-offset.X, -offset.Y);

            //draw passed in image onto graphics object
            g.DrawImage(image, new PointF(0, 0));

            return rotatedBmp;
        }

    }
}

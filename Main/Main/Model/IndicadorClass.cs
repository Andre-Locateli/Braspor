using Main.Helper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
//using Timer = System.Timers.Timer;

namespace Main.Model
{
    internal class IndicadorClass
    {
        private double _PB = 0;

        public double PB
        {
            get { return _PB; }
            set { _PB = value; }
        }

        private double _PL = 0;

        public double PL
        {
            get { return _PL; }
            set { _PL = value; }
        }

        private double _T = 0;

        public double T
        {
            get { return _T; }
            set { _T = value; }
        }

        public RedeClass indicador { get; set; }
        public TCPContext TCPContext { get; set; }

        public int contador_1 { get; set; }
        public int contador_2 { get; set; }
        public int contador_3 { get; set; }

        public async Task Start()
        {
            while (true)
            {
                //Console.WriteLine($"Contador 1 = {contador_1}\n" +
                //    $"Contador 2 = {contador_2}\n" +
                //    $"Contador 3 = {contador_3}\n" +
                //    $"Indicador = {indicador.full_name}");

                string ipAddress = indicador.IP;
                int port = indicador.porta;
                int timeoutMilliseconds = 2000;
                int bufferSize = 1024;
                using (TcpClient tcpClient = new TcpClient())
                {
                    try
                    {
                        using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource(timeoutMilliseconds))
                        {
                            Task connectTask = tcpClient.ConnectAsync(ipAddress, port);
                            await Task.WhenAny(connectTask, Task.Delay(Timeout.Infinite, cancellationTokenSource.Token));
                            cancellationTokenSource.Token.ThrowIfCancellationRequested();

                            Console.WriteLine("Dispositivo conectado com sucesso! {0}", indicador.full_name);

                            using (NetworkStream stream = tcpClient.GetStream())
                            {
                                byte[] bufferWrite = new byte[1];
                                bufferWrite[0] = 0x00;
                                stream.Write(bufferWrite, 0, 1);
                                byte[] buffer = new byte[bufferSize];
                                //int bytesRead = await stream.ReadAsync(buffer, 0, 17, cancellationTokenSource.Token);
                                //Thread.Sleep(1000);
                                    int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);

                                    // Converter bytes para string e exibir no console
                                    string receivedData = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                                    Console.WriteLine($"Dados recebidos: {receivedData}");
                                    double d_Peso = 0;
                                    if (indicador.protocolo == "P03" && indicador.modelo == "TI500")
                                    {
                                        if (receivedData.Length >= 15)
                                        {
                                            string Peso = receivedData.Substring(4, 6);
                                            d_Peso = Convert.ToDouble(Peso) / Math.Pow(10, indicador.casasDecimais);
                                            Console.WriteLine($"Peso: {Peso}");
                                            Program.Registradores[indicador.Id] = d_Peso;
                                        }
                                    }
                                    else if (indicador.modelo == "PS6000")
                                    {
                                        if (receivedData.Length >= 15)
                                        {
                                            string Peso = receivedData.Substring(5, 11);
                                            d_Peso = Convert.ToDouble(Peso);
                                            Console.WriteLine($"Peso: {Peso}");
                                            Program.Registradores[indicador.Id] = d_Peso;
                                        }
                                    }

                            }
                        }
                    }
                    catch (OperationCanceledException)
                    {
                        Console.WriteLine("Tempo limite de conexão atingido. {0}", indicador.full_name);
                        contador_1 += 1;
                    }
                    catch (IOException ex)
                    {
                        Console.WriteLine($"Erro na leitura dos dados: {ex.Message}" + " {0}", indicador.full_name);
                        contador_2 += 1;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Erro ao tentar conectar: {ex.Message}" + " {0}", indicador.full_name);
                        contador_3 += 1;
                    }
                }
            }
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Threading;

namespace Main.Helper
{
    public class TCPContext
    {

        private string ip = string.Empty;
        private string porta = string.Empty;
        private string ipMask = string.Empty;
        private string gateway = string.Empty;

        private bool _running = false;

        private IPAddress server;
        public TcpClient tcpClient;

        public byte[] data;

        public TCPContext(string _ip, string _porta, [Optional] string _ipMask, [Optional] string _gateway)
        {
			try
			{
                ip = _ip;
                porta = _porta;
                ipMask = _ipMask;
                gateway = _gateway;
                tcpClient = new TcpClient(ip, Convert.ToInt32(porta));
                tcpClient.ReceiveBufferSize = 8000;
                tcpClient.ReceiveTimeout = 50;
                tcpClient.SendTimeout = 50;
            }
			catch (Exception)
			{
			}
        }

        public bool WriteClient(byte[] data) 
        {
            try
            {
                using (NetworkStream ns = tcpClient.GetStream())
                {
                    using (BufferedStream bs = new BufferedStream(ns))
                    {                       
                        bs.Write(data, 0, data.Length);
                    }
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool WriteClient(string s_data)
        {
            try
            {
                using (NetworkStream ns = tcpClient.GetStream())
                {
                    using (BufferedStream bs = new BufferedStream(ns))
                    {
                        byte[] data = Encoding.UTF8.GetBytes(s_data);
                        bs.Write(data, 0, data.Length);
                    }
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool WriteClient(StringBuilder s_data)
        {
            try
            {
                using (NetworkStream ns = tcpClient.GetStream())
                {
                    using (BufferedStream bs = new BufferedStream(ns))
                    {
                        byte[] data = Encoding.UTF8.GetBytes(s_data.ToString());
                        bs.Write(data, 0, data.Length);
                    }
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void ReadClient() 
        {
            try
            {
                //using (NetworkStream ns = tcpClient.GetStream())
                //{

                tcpClient = new TcpClient();
                tcpClient.ReceiveTimeout = 50;
                tcpClient.SendTimeout = 50;
                tcpClient.Connect(ip, Convert.ToInt32(porta));

                //tcpClient = new TcpClient(ip, Convert.ToInt32(porta));
                Thread.Sleep(100);
                if (tcpClient.Connected && tcpClient.Available != 0)
                {
                    tcpClient.ReceiveBufferSize = 8000;
                    tcpClient.ReceiveTimeout = 50;
                    NetworkStream ns = tcpClient.GetStream();
                    //Thread.Sleep(20);
                    Thread.Sleep(50);
                    ns.ReadTimeout = 50;
                    if (ns.CanRead && ns.DataAvailable)
                    {
                        data = new byte[17];
                        using (BufferedStream bs = new BufferedStream(ns))
                        {
                            bs.Read(data, 0, data.Length);
                        }
                        ns.Close();
                        ns.Dispose();
                    }

                }
                tcpClient.Close();
                //}
            }
            catch (Exception ex)
            {
               // tcpClient.Close();
            }
        }

        public void ClearData(byte[] dt) 
        {
            try
            {
//                {

  //              }
            }
            catch (Exception)
            {
            }
        }

    }
}

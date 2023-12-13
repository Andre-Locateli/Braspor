using AutoUpdaterDotNET;
using Main.Helper;
using Main.Helper.SerialFolder;
using Main.Model;
using Main.View.CommunicationFolder;
using Main.View.LoginFolder;
using Main.View.MainFolder;
using Main.View.PagesFolder.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Windows.Diagnostics;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using System.Windows.Ink;
using System.Xml;

namespace Main
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// 
        public static ConfiguracaoClass Configuracao;
        public static EtiquetaClass Etiqueta;
        public static CFGContext CFG = new CFGContext();
        public static SQLContext SQL;
        public static TCPContext TCP;
        public static SerialPort SERIALPORT = new SerialPort();
        public static SerialPort IMPRESSORAPORT = new SerialPort();
        public static ControleCIndicadores _Controle;
        public static UsuarioClass _usuarioLogado;
        public static PermissaoClass _permissaoUsuario;
        
        public static HttpContext HTTP = new HttpContext();
        //public static Dictionary<int, double> Registradores = new Dictionary<int, double>();
        public static Dictionary<int, object> Registradores = new Dictionary<int, object>();
        public static Stopwatch millis = new Stopwatch();
        public static List<RedeClass> REDE = new List<RedeClass>();
        public static string _ultimoLogin;
        public static string _ultimaSenha;
        public static bool _checkSalvar;
        public static string _usbValue = "";
        public static bool _autoconnect_1 = false;
        public static bool _autoconnect_2 = false;
        public static TimeSpan _time_global;
        public static string software_version = "1.0.0.1";
        public static CommunicationForms com { get; set; }

        [STAThread]
        static void Main()
        {
            millis.Start();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            ConfigRead();
          //  Application.ApplicationExit += Application_ApplicationExit;
            TecladoFilter filtro = new TecladoFilter();
            Application.AddMessageFilter(filtro);
            //Application.Run(new CommunicationForms());
            List<object> _rede = Program.SQL.SelectList("SELECT * FROM Rede WHERE parent = @parent", "Rede", null, new Dictionary<string, object>() { { "@parent", Environment.MachineName.Trim() } });
            REDE = _rede.Cast<RedeClass>().ToList();

            AutoUpdater.InstalledVersion = new Version(software_version);
            AutoUpdater.Synchronous = true;
            AutoUpdater.ShowSkipButton = true;
            AutoUpdater.ShowRemindLaterButton = true;
            AutoUpdater.ClearAppDirectory = true;
            AutoUpdater.Mandatory = true;
            AutoUpdater.UpdateMode = Mode.Forced;
            AutoUpdater.Start("ftp://ftp.aephdobrasil.com.br/SKF/xml_skf.xml", new NetworkCredential("ITO@aephdobrasil.com.br", "3x6lGPv3"));

            Application.Run(new LoginForms());

        }

        private static void ConfigRead()
        {
            //ARQUIVO DE CONFIGURACOES GERAIS DO SOFTWARE
            string caminhoCompleto = Path.Combine(Application.StartupPath, CFG.CaminhoConfig);
            if (!File.Exists(caminhoCompleto))
            {
                XmlDocument xmlDoc = new XmlDocument();
                XmlElement root = xmlDoc.CreateElement("Configuracoes");
                xmlDoc.AppendChild(root);
                XmlElement xml_estacao = xmlDoc.CreateElement("Estacao");
                xml_estacao.InnerText = Environment.MachineName;
                root.AppendChild(xml_estacao);
                XmlElement xml_SQLConnection = xmlDoc.CreateElement("SQLConnection");
                xml_SQLConnection.InnerText = "Data Source=10.0.0.12\\SQLEXPRESS;Initial Catalog=VC_BRASPOR; User ID=sa; Pwd=AEPH1234*#";
                root.AppendChild(xml_SQLConnection);

                XmlElement root_login = xmlDoc.CreateElement("ultimoLogin");
                root.AppendChild(root_login);

                XmlElement root_Senha = xmlDoc.CreateElement("ultimoSenha");
                root.AppendChild(root_Senha);

                XmlElement check_lembrar = xmlDoc.CreateElement("checkLembrar");
                check_lembrar.InnerText = "0";
                root.AppendChild(check_lembrar);

                XmlElement xml_PortaSerial = xmlDoc.CreateElement("PortaSerial_1");
                xml_PortaSerial.InnerText = "";
                root.AppendChild(xml_PortaSerial);

                XmlElement xml_BaudRate = xmlDoc.CreateElement("BaudRate_1");
                xml_BaudRate.InnerText = "19200";
                root.AppendChild(xml_BaudRate);

                XmlElement xml_StopBit = xmlDoc.CreateElement("StopBit_1");
                xml_StopBit.InnerText = "2";
                root.AppendChild(xml_StopBit);

                XmlElement xml_AutoConnect = xmlDoc.CreateElement("AutoConnect_1");
                xml_AutoConnect.InnerText = "0";
                root.AppendChild(xml_AutoConnect);

                XmlElement xml_PortaSerial2 = xmlDoc.CreateElement("PortaSerial_2");
                xml_PortaSerial2.InnerText = "";
                root.AppendChild(xml_PortaSerial2);

                XmlElement xml_BaudRate2 = xmlDoc.CreateElement("BaudRate_2");
                xml_BaudRate2.InnerText = "19200";
                root.AppendChild(xml_BaudRate2);

                XmlElement xml_StopBit2 = xmlDoc.CreateElement("StopBit_2");
                xml_StopBit2.InnerText = "2";
                root.AppendChild(xml_StopBit2);

                XmlElement xml_Paridade2 = xmlDoc.CreateElement("Paridade_2");
                xml_Paridade2.InnerText = "Nenhuma";
                root.AppendChild(xml_Paridade2);

                XmlElement xml_AutoConnect2 = xmlDoc.CreateElement("AutoConnect_2");
                xml_AutoConnect2.InnerText = "0";
                root.AppendChild(xml_AutoConnect2);

                XmlElement xml_X = xmlDoc.CreateElement("x");
                xml_X.InnerText = "0";
                root.AppendChild(xml_X);

                XmlElement xml_Y = xmlDoc.CreateElement("y");
                xml_Y.InnerText = "0";
                root.AppendChild(xml_Y);

                XmlElement xml_width = xmlDoc.CreateElement("width");
                xml_width.InnerText = "1143";
                root.AppendChild(xml_width);

                XmlElement xml_height = xmlDoc.CreateElement("height");
                xml_height.InnerText = "690";
                root.AppendChild(xml_height);

                XmlElement xml_full_screen = xmlDoc.CreateElement("full_screen");
                xml_full_screen.InnerText = "0";
                root.AppendChild(xml_full_screen);

                XmlElement xml_modo = xmlDoc.CreateElement("ModoDeOperacao");
                xml_modo.InnerText = "false";
                root.AppendChild(xml_modo);

                xmlDoc.Save(caminhoCompleto);
                CFG.Estação = Environment.MachineName;
                CFG.sqlConnection = xml_SQLConnection.InnerText;
                CFG._width = 1143;
                CFG._height = 690;
                SQL = new SQLContext(CFG.sqlConnection);
                _ultimoLogin = "";
                _ultimaSenha = "";
                _checkSalvar = false;
            }
            else
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(caminhoCompleto);
                XmlNodeList xmlConf = xmlDoc.GetElementsByTagName("Configuracoes");
                XmlElement xml_estacao = (XmlElement)xmlConf[0].SelectSingleNode("Estacao");
                XmlElement xml_SQLConnection = (XmlElement)xmlConf[0].SelectSingleNode("SQLConnection");

                XmlElement ultimoLogin = (XmlElement)xmlConf[0].SelectSingleNode("ultimoLogin");
                XmlElement ultimaSenha = (XmlElement)xmlConf[0].SelectSingleNode("ultimoSenha");
                XmlElement checkSalvar = (XmlElement)xmlConf[0].SelectSingleNode("checkLembrar");

                XmlElement xml_PortSerial = (XmlElement)xmlConf[0].SelectSingleNode("PortaSerial_1");
                XmlElement xml_BaudRate = (XmlElement)xmlConf[0].SelectSingleNode("BaudRate_1");
                XmlElement xml_StopBit = (XmlElement)xmlConf[0].SelectSingleNode("StopBit_1");
                XmlElement xml_Autoconnect = (XmlElement)xmlConf[0].SelectSingleNode("AutoConnect_1");

                XmlElement xml_PortSerial2 = (XmlElement)xmlConf[0].SelectSingleNode("PortaSerial_2");
                XmlElement xml_BaudRate2 = (XmlElement)xmlConf[0].SelectSingleNode("BaudRate_2");
                XmlElement xml_StopBit2 = (XmlElement)xmlConf[0].SelectSingleNode("StopBit_2");
                XmlElement xml_Paridade2 = (XmlElement)xmlConf[0].SelectSingleNode("Paridade_2");
                XmlElement xml_Autoconnect2 = (XmlElement)xmlConf[0].SelectSingleNode("AutoConnect_2");

                XmlElement xml_X = (XmlElement)xmlConf[0].SelectSingleNode("x");
                XmlElement xml_Y = (XmlElement)xmlConf[0].SelectSingleNode("y");
                XmlElement xml_width = (XmlElement)xmlConf[0].SelectSingleNode("width");
                XmlElement xml_height = (XmlElement)xmlConf[0].SelectSingleNode("height");
                XmlElement xml_full_screen = (XmlElement)xmlConf[0].SelectSingleNode("full_screen");

                XmlElement xml_modo_operacao = (XmlElement)xmlConf[0].SelectSingleNode("ModoDeOperacao");

                //PORTA SERIAL 01
                if (xml_Autoconnect.InnerText == "1")
                {
                    _autoconnect_1 = true;
                }
                else
                {
                    _autoconnect_1 = false;
                }

                if (xml_PortSerial.InnerText != "")
                {
                    try
                    {
                        SERIALPORT.PortName = xml_PortSerial.InnerText;
                    }
                    catch (Exception ex)
                    {
                    }
                }
                SERIALPORT.BaudRate = Convert.ToInt32(xml_BaudRate.InnerText);
                SERIALPORT.DataBits = 8;
                SERIALPORT.Parity = Parity.None;
                SERIALPORT.StopBits = GeralClass.v_StopBit(xml_StopBit.InnerText);
                try
                {
                    if (_autoconnect_1) SERIALPORT.Open();
                }
                catch (Exception ex)
                {
                }

                //PORTA SERIAL 02
                if (xml_Autoconnect2.InnerText == "1")
                {
                    _autoconnect_2 = true;
                }
                else
                {
                    _autoconnect_2 = false;
                }

                if (xml_PortSerial2.InnerText != "")
                {
                    try
                    {
                        IMPRESSORAPORT.PortName = xml_PortSerial2.InnerText;
                    }
                    catch (Exception ex)
                    {
                    }
                }
                IMPRESSORAPORT.BaudRate = Convert.ToInt32(xml_BaudRate2.InnerText);
                IMPRESSORAPORT.DataBits = 8;
                if (xml_Paridade2.InnerText == "Par")
                {
                    IMPRESSORAPORT.Parity = Parity.Even;
                }
                else if (xml_Paridade2.InnerText == "Ímpar")
                {
                    IMPRESSORAPORT.Parity = Parity.Odd;
                }
                else
                {
                    IMPRESSORAPORT.Parity = Parity.None;
                }

                IMPRESSORAPORT.StopBits = GeralClass.v_StopBit(xml_StopBit2.InnerText);
                try
                {
                    if (_autoconnect_2) IMPRESSORAPORT.Open();
                }
                catch (Exception ex)
                {
                }

                CFG.Estação = xml_estacao.InnerText;
                CFG.sqlConnection = xml_SQLConnection.InnerText;
                CFG._x = Convert.ToInt32(xml_X.InnerText);
                CFG._y = Convert.ToInt32(xml_Y.InnerText);
                CFG._width = Convert.ToInt32(xml_width.InnerText);
                CFG._height = Convert.ToInt32(xml_height.InnerText);
                CFG._modoOperacao = xml_modo_operacao.InnerText;
                if (xml_full_screen.InnerText == "1") { CFG._full_screen = true; }
                else { CFG._full_screen = false; }
                SQL = new SQLContext(CFG.sqlConnection);
                _ultimoLogin = ultimoLogin.InnerText;
                _ultimaSenha = ultimaSenha.InnerText;
                _checkSalvar = Convert.ToBoolean(Convert.ToInt16(checkSalvar.InnerText));
            }
            List<object> _result = Program.SQL.SelectList("SELECT * FROM Configuracao WHERE estacao = @estacao", "Configuracao", null, new Dictionary<string, object>() { { "@estacao", Environment.MachineName.Trim() } });
            if(_result.Count != 0) Configuracao = (ConfiguracaoClass)_result[0];
            if(Configuracao != null)
            {
                if(Configuracao.id_Etiqueta != 0)
                {
                    _result = Program.SQL.SelectList("SELECT * FROM Etiqueta WHERE id = @id", "Etiqueta", null, new Dictionary<string, object>() { { "@id",Configuracao.id_Etiqueta } });
                    if(_result.Count() > 0) Etiqueta = (EtiquetaClass)_result[0];
                }
            }
        }
    }
}

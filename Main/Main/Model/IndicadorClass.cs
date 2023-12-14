using Main.Helper;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
//using Timer = System.Timers.Timer;

namespace Main.Model
{
    public class IndicadorClass
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

        private string _PS;

        public string PS
        {
            get { return _PS; }
            set { _PS = value; }
        }


        public RedeClass indicador { get; set; }

        public SerialPort SERIALPORTContext { get; set; }

        private string _nome;
        public string nome
        {
            get { return _nome; }
            set { _nome = value; }
        }


        private bool _availableStatus;
        public bool availableStatus
        {
            get { return _availableStatus; }
            set
            {
                _availableStatus = value;
            }
        }



        public int contador_1 { get; set; }
        public int contador_2 { get; set; }
        public int contador_3 { get; set; }


        public IndicadorClass(SerialPort _serialport, RedeClass _indicador)
        {
            availableStatus = true;
            SERIALPORTContext = _serialport;
            Console.WriteLine($"Configurando porta :{SERIALPORTContext.PortName}");
            indicador = _indicador;
        }

    }
}

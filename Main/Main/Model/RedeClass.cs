using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Model
{
    public class RedeClass
    {
        public int Id { get; set; }
        public string tipo { get; set; }
        public int tipo_impressao { get; set; }
        public string impressora { get; set; }
        public bool simplificar { get; set; }
        public string fabricante { get; set; }
        public string modelo { get; set; }
        public string protocolo { get; set; }
        public string nome { get; set; }
        public int addr {get; set; }
        public int baud_rate { get; set; }
        public string parent { get; set; }
        public string full_name { get; set; }
        public int num_parent { get; set; }
        public string IP { get; set; }
        public int porta { get; set; }
        public string MAC { get; set; }
        public int casasDecimais { get; set; }
        public DateTime dateinsert { get; set; }
        public DateTime dateupdate { get; set; }
    }
}

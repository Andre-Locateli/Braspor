using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Model
{
    public class EtiquetaClass
    {
        public int id { get; set; } = 0;
        public string nome_etiqueta { get; set; }
        public string arquivo { get; set; }
        public DateTime dateinsert { get; set; }
        public DateTime dateupdate { get; set; }
    }
}

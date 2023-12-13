using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Model
{
    public class ConfiguracaoClass
    {
        public int id { get; set; }
        public string estacao { get; set; }
        public int id_Impressora { get; set; }
        public int id_Etiqueta { get; set; }
        public int copias { get; set; }
        public byte[] logo_empresa { get; set; }
        public DateTime dateinsert { get; set; }
        public DateTime dateupdate { get; set; }

    }
}

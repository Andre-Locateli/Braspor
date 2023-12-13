using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Helper
{
    internal class CFGContext
    {
        public string Estação { get; set; }
        public string sqlConnection { get; set; }
        public string CaminhoConfig { get; set; } = "config.xml";
        public int _x { get; set; } = 0;
        public int _y { get; set; } = 0;
        public int _width { get; set; }
        public int _height { get; set; }
        public bool _full_screen { get; set; } = false;
        public string _modoOperacao { get; set; } = "false";
    }
}

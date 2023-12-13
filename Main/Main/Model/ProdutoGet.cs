using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Model
{
    public class ProdutoGet
    {
        public string cod_produto { get; set; }
        public string descricao { get; set; }
        public string lote { get; set; }

        private string _peso_alvo;
        public string peso_alvo
        {
            get { return _peso_alvo; }
            set
            {
                _peso_alvo = value;
                _peso_alvo = _peso_alvo.Replace(".", ",");
                _peso_alvo_d = Decimal.Parse(_peso_alvo, new CultureInfo("pt-BR"));
            }
        }
        public decimal _peso_alvo_d { get; set; }

        private string _tolerancia;
        public string tolerancia
        {
            get { return _tolerancia; }
            set 
            {
                _tolerancia = value;
                _tolerancia = _tolerancia.Replace(".", ",");
                tolerencia_d = Decimal.Parse(_tolerancia, new CultureInfo("pt-BR"));
            }
        }
        public decimal tolerencia_d { get; set; }


        public string pesado { get; set; }
    }
}

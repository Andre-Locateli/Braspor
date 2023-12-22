using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Model
{
    public class LogProcessosClass
    {
        private int _id;
        public int Id 
        { 
            get { return _id; } 
            set { _id = value; }
        }

        private int _id_processo;
        public int Id_processo
        {
            get { return _id_processo; }
            set { _id_processo = value; }
        }

        private decimal _peso_temporeal;
        public decimal Peso_temporeal
        {
            get { return _peso_temporeal; }
            set { _peso_temporeal = value; }
        }

        private decimal _peso_total;
        public decimal Peso_total
        {
            get { return _peso_total; }
            set { _peso_total = value; }
        }

        private string _tempo_execucao;
        public string Tempo_execucao
        {
            get { return _tempo_execucao; }
            set { _tempo_execucao = value; }
        }

        private DateTime _dateinsert;
        public DateTime dateinsert
        {
            get { return _dateinsert; }
            set { _dateinsert = value; }
        }
    }
}

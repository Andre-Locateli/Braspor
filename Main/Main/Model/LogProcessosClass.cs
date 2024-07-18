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

        private decimal _qtd_temporeal;
        public decimal qtd_temporeal
        {
            get { return _qtd_temporeal; }
            set { _qtd_temporeal = value; }
        }

        private decimal _qtd_total;
        public decimal qtd_total
        {
            get { return _qtd_total; }
            set { _qtd_total = value; }
        }

        private float _peso;
        public float Peso
        {
            get { return _peso; }
            set { _peso = value; }
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

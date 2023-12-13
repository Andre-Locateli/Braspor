using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Model
{
    public class MateriaPrimaClass
    {
        private int _id;
        public int Id 
        { 
            get { return _id; } 
            set { _id = value; }
        }


        private string _codigo;
        public string Codigo 
        {
            get { return _codigo; }
            set { _codigo = value; }
        }


        private string _descricao;
        public string Descricao 
        { 
            get { return _descricao; }
            set { _descricao = value; }
        }


        private double _tolerancia_erro;
        public double Tolerancia_erro
        {
            get { return _tolerancia_erro; }
            set { _tolerancia_erro = value; }
        }


        private int _quantidade_minima;
        public int Quantidade_minima
        {
            get { return _quantidade_minima; }
            set { _quantidade_minima = value; }
        }


        private Boolean _status;
        public Boolean Status
        {
            get { return _status; }
            set { _status = value; }
        }


        private DateTime _date_insert;
        public DateTime DateInsert
        {
            get { return _date_insert; }
            set { _date_insert = value; }
        }
    }
}

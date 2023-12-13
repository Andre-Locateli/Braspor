using Main.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Model
{
    public class ReceitaLogClass
    {
		private int _id;
		public int Id
		{
			get { return _id; }
			set { _id = value; }
		}

        private string _nome;
        public string Nome
        {
            get { return _nome; }
            set { _nome = value; }
        }

        private string _codigo;
        public string Codigo
        {
            get { return _codigo; }
            set { _codigo = value; }
        }

        private int _id_receita;
		public int id_receita
        {
			get { return _id_receita; }
			set { _id_receita = value; }
		}

		private int _id_Recipiente;
		public int id_Recipiente
        {
			get { return _id_Recipiente; }
			set { _id_Recipiente = value; }
		}

		private double _Peso_Recipiente;
		public double Peso_Recipiente
        {
			get { return _Peso_Recipiente; }
			set { _Peso_Recipiente = value; }
		}

		private double _Peso_Recipiente_Pesado;
		public double Peso_Recipiente_Pesado
        {
			get { return _Peso_Recipiente_Pesado; }
			set { _Peso_Recipiente_Pesado = value; }
		}

		private int _id_Bandeja;
		public int id_Bandeja
        {
			get { return _id_Bandeja; }
			set { _id_Bandeja = value; }
		}

		private int _Qtd_Bandeja;
		public int Qtd_Bandeja
        {
			get { return _Qtd_Bandeja; }
			set { _Qtd_Bandeja = value; }
		}

		private int _Qtd_Bandeja_Pesado;
		public int Qtd_Bandeja_Pesado
        {
			get { return _Qtd_Bandeja_Pesado; }
			set { _Qtd_Bandeja_Pesado = value; }
		}

		private double _Peso_Bandejas;
		public double Peso_Bandejas
        {
			get { return _Peso_Bandejas; }
			set { _Peso_Bandejas = value; }
		}

		private double _Peso_Bandejas_Pesado;
		public double Peso_Bandejas_Pesado
        {
			get { return _Peso_Bandejas_Pesado; }
			set { _Peso_Bandejas_Pesado = value; }
		}

		private int _id_Produto;
		public int id_Produto
        {
			get { return _id_Produto; }
			set { _id_Produto = value; }
		}

		private int _Qtd_Pecas;
		public int Qtd_Pecas
        {
			get { return _Qtd_Pecas; }
			set { _Qtd_Pecas = value; }
		}

		private int _Qtd_Pecas_Pesado;
		public int Qtd_Pecas_Pesado
        {
			get { return _Qtd_Pecas_Pesado; }
			set { _Qtd_Pecas_Pesado = value; }
		}

		private double _Peso_Pecas;
		public double Peso_Pecas
        {
			get { return _Peso_Pecas; }
			set { _Peso_Pecas = value; }
		}

		private double _Peso_Pecas_Pesado;
		public double Peso_Pecas_Pesado
        {
			get { return _Peso_Pecas_Pesado; }
			set { _Peso_Pecas_Pesado = value; }
		}

        private int _status;
        public int Status
        {
            get { return _status; }
            set
            {
                _status = value;
                if (_status == 0)
                {
                    //Pendente
                    ImageIcon = Resources.pendenteico;

                }

                if (_status == 1)
                {
                    //executando
                    ImageIcon = Resources.executandoIco;
                }

                if (_status == 2)
                {
                    //Finalizada
                    ImageIcon = Resources.finalizadaIco;
                }
            }
        }

        private Bitmap _imageIcon;
        public Bitmap ImageIcon
        {
            get { return _imageIcon; }
            set { _imageIcon = value; }
        }

        private string _Estacao;
		public string Estacao
        {
			get { return _Estacao; }
			set { _Estacao = value; }
		}

		private string _Operador;
		public string Operador
        {
			get { return _Operador; }
			set { _Operador = value; }
		}

		private DateTime _dateinsert;
		public DateTime dateinsert
        {
			get { return _dateinsert; }
			set { _dateinsert = value; }
		}

		private DateTime _datefim;
		public DateTime datefim
        {
			get { return _datefim; }
			set { _datefim = value; }
		}

	}
}

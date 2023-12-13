using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Model
{
    public class ReceitaClass
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

		private string _pkSKF;
		public string PKSKF
		{
			get { return _pkSKF; }
			set { _pkSKF = value; }
		}


		private int _id_produto;
		public int Id_Produto
		{
			get { return _id_produto; }
			set { _id_produto = value; }
		}

		private int _id_bandeja;
		public int Id_Bandeja
		{
			get { return _id_bandeja; }
			set { _id_bandeja = value; }
		}

		private int _id_recipiente;
		public int Id_Recipiente
		{
			get { return _id_recipiente; }
			set { _id_recipiente = value; }
		}

		private int _quantidade_pecas;
		public int Quantidade_pecas
		{
			get { return _quantidade_pecas; }
			set { _quantidade_pecas = value; }
		}

		private int _quantidade_bandejas;
		public int Quantidade_bandejas
		{
			get { return _quantidade_bandejas; }
			set { _quantidade_bandejas = value; }
		}

		private string _operador;
		public string Operador
		{
			get { return _operador; }
			set { _operador = value; }
		}

		private int _status;
		public int Status
		{
			get { return _status; }
			set { _status = value; }
		}

		private DateTime _dateInsert;
		public DateTime DateInsert
		{
			get { return _dateInsert; }
			set { _dateInsert = value; }
		}


	}
}

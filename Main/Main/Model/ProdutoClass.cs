using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Model
{
    public class ProdutoClass
    {

		private int _id;
		public int Id
		{
			get { return _id; }
			set { _id = value; }
		}

		/// <summary>
		/// Código do produto da pesagem.
		/// </summary>
		private string _part_number;
		public string Part_number
		{
			get { return _part_number; }
			set { _part_number = value; }
		}

		private string _descricao;
		public string Descricao
		{
			get { return _descricao; }
			set { _descricao = value; }
		}

		private float _pesoAlvo;
		public float PesoAlvo
		{
			get { return _pesoAlvo; }
			set { _pesoAlvo = value; }
		}

		private float _tolerancia;
		public float Tolerancia
		{
			get { return _tolerancia; }
			set { _tolerancia = value; }
		}

		private byte[] _Foto;
        public byte[] Foto
		{
			get { return _Foto; }
			set { _Foto = value; }
		}

		private string _part_number_cliente;
		public string part_number_cliente
        {
			get { return _part_number_cliente; }
			set { _part_number_cliente = value; }
		}

		private DateTime _dateInsert;
		public DateTime DateInsert
		{
			get { return _dateInsert; }
			set { _dateInsert = value; }
		}

		private string _CodigoEarn;
		public string CodigoEarn
        {
			get { return _CodigoEarn; }
			set { _CodigoEarn = value; }
		}


	}
}

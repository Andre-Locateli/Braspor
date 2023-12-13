using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Model
{
    public class RecipienteClass
    {

		private int _id;
		public int Id
		{
			get { return _id; }
			set { _id = value; }
		}

		private string _package;
		public string Package
		{
			get { return _package; }
			set { _package = value; }
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

		private byte[] _foto;
		public byte[] Foto
		{
			get { return _foto; }
			set { _foto = value; }
		}


		private DateTime _dateInsert;
		public DateTime DateInsert
		{
			get { return _dateInsert; }
			set { _dateInsert = value; }
		}

	}
}

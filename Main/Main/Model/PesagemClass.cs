using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Model
{
    public class PesagemClass
    {

		private int _id;
		public int Id
		{
			get { return _id; }
			set { _id = value; }
		}

		private string _codigoProduto;
		public string CodigoProduto
		{
			get { return _codigoProduto; }
			set { _codigoProduto = value; }
		}

		private string _descricao;
		public string Descricao
		{
			get { return _descricao; }
			set { _descricao = value; }
		}

		private string _pesoAlvo;
		public string PesoAlvo
		{
			get { return _pesoAlvo; }
			set { _pesoAlvo = value; }
		}

		private string _pesoReal;
		public string PesoReal
		{
			get { return _pesoReal; }
			set { _pesoReal = value; }
		}

		private string _tolerencia;
		public string Tolerencia
		{
			get { return _tolerencia; }
			set { _tolerencia = value; }
		}

		private bool _flag_sync;
		/// <summary>
		/// Quando ele estiver em 0 o dado não está sincronizado no Protheus
		/// Quandl ele estiver em 1 o dado está sincronizado com o Protheus
		/// </summary>
		public bool flag_sync
		{
			get { return _flag_sync; }
			set { _flag_sync = value; }
		}

		private DateTime _dateinsert;
		public DateTime dateinsert
        {
			get { return _dateinsert; }
			set { _dateinsert = value; }
		}


	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Model
{
    public class ProcessosModel
    {

		private int _id;
		public int Id
		{
			get { return _id; }
			set { _id = value; }
		}

		private int _idProduto;
		public int Id_Produto
		{
			get { return _idProduto; }
			set { _idProduto = value; }
		}

		private int _idUsuario;
		public int IdUsuario
		{
			get { return _idUsuario; }
			set { _idUsuario = value; }
		}

		private string _descricao;
		public string Descricao
		{
			get { return _descricao; }
			set { _descricao = value; }
		}

		private string _tempoExecucao;
		public string TempoExecucao
		{
			get { return _tempoExecucao; }
			set { _tempoExecucao = value; }
		}

		private int _totalContagem;
		public int TotalContagem
		{
			get { return _totalContagem; }
			set { _totalContagem = value; }
		}

		private double _pesoTotal;
		public double PesoTotal
		{
			get { return _pesoTotal; }
			set { _pesoTotal = value; }
		}

		private int _statusProcesso;
		public int StatusProcesso
		{
			get { return _statusProcesso; }
			set { _statusProcesso = value; }
		}

		private DateTime _dateinsert;
		public DateTime dateinsert
        {
			get { return _dateinsert; }
			set { _dateinsert = value; }
		}

		private DateTime _dateend;
		public DateTime dateend
        {
			get { return _dateend; }
			set { _dateend = value; }
		}

		private DateTime _dateupdate;
		public DateTime dateupdate
        {
			get { return _dateupdate; }
			set { _dateupdate = value; }
		}



	}
}

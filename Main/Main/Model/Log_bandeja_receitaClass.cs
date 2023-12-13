using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Model
{
    public class Log_bandeja_receitaClass
    {
		private int _id;
		public int Id
		{
			get { return _id; }
			set { _id = value; }
		}

		private int _id_log_receita;
		public int Id_Log_Receita
		{
			get { return _id_log_receita; }
			set { _id_log_receita = value; }
		}

		private int _numero_bandejas;
		public int Numero_Bandejas
		{
			get { return _numero_bandejas; }
			set { _numero_bandejas = value; }
		}

		private double _peso_bandeja;
		public double Peso_Bandeja
		{
			get { return _peso_bandeja; }
			set { _peso_bandeja = value; }
		}

		private double _peso_produto;
		public double Peso_Produto
		{
			get { return _peso_produto; }
			set { _peso_produto = value; }
		}

	}
}

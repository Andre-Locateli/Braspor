using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Model
{
    public class TipoReceitaClass
    {

		private int _id;
		public int Id
		{
			get { return _id; }
			set { _id = value; }
		}

		private string _tipoItem;
		public string TipoItem
		{
			get { return _tipoItem; }
			set { _tipoItem = value; }
		}

        private DateTime _dateInsert;
        public DateTime DateInsert
        {
            get { return _dateInsert; }
            set { _dateInsert = value; }
        }

    }
}

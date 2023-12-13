using Main.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Model
{
    public class CustomReceitaInfo
    {
		private int _id;
		public int id
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
        [DisplayName("Código")]
        public string Codigo
		{
			get { return _codigo; }
			set { _codigo = value; }
		}

        
        private int _quantidadePecas;
        [DisplayName("Quantidade Peças")]
        public int QuantidadePecas
		{
			get { return _quantidadePecas; }
			set { _quantidadePecas = value; }
		}

		private int _quantidadeBandejas;
        [DisplayName("Quantidade Bandeijas")]
        public int QuantidadeBandejas
		{
			get { return _quantidadeBandejas; }
			set { _quantidadeBandejas = value; }
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
        [DisplayName("Status")]
        public Bitmap ImageIcon
        {
            get { return _imageIcon; }
            set
            { 
                _imageIcon = value;
            }
        }

        private DateTime _date;
        [DisplayName("Data")]
        public DateTime Date
        {
            get { return _date; }
            set { _date = value; }
        }


    }
}

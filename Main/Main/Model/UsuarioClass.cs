using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Model
{
    public class UsuarioClass
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

		private string _login;
		public string Login
		{
			get { return _login; }
			set { _login = value; }
		}

		private string _senha;
		public string Senha
		{
			get { return _senha; }
			set { _senha = value; }
		}

		private string _acesso;
		public string Acesso
		{
			get { return _acesso; }
			set { _acesso = value; }
		}

	}
}

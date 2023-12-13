using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Model
{
    public class PermissaoClass
    {

		private int _id;
		public int Id
		{
			get { return _id; }
			set { _id = value; }
		}

		private string _acesso;
		public string Acesso
		{
			get { return _acesso; }
			set { _acesso = value; }
		}

		private bool _pesagem_view;
		public bool Pesagem_View
		{
			get { return _pesagem_view; }
			set { _pesagem_view = value; }
		}

        private bool _pesagem_add;
        public bool Pesagem_add
        {
            get { return _pesagem_add; }
            set { _pesagem_add = value; }
        }

        private bool _pesagem_edit;
        public bool Pesagem_edit
        {
            get { return _pesagem_edit; }
            set { _pesagem_edit = value; }
        }

        private bool _pesagem_remove;
        public bool Pesagem_remove
        {
            get { return _pesagem_remove; }
            set { _pesagem_remove = value; }
        }

        private bool _pesagem_search;
        public bool Pesagem_search
        {
            get { return _pesagem_search; }
            set { _pesagem_search = value; }
        }     

        private bool _relatorio_view;
        public bool Relatorio_View
        {
            get { return _relatorio_view; }
            set { _relatorio_view = value; }
        }

        private bool _relatorio_add;
        public bool Relatorio_add
        {
            get { return _relatorio_add; }
            set { _relatorio_add = value; }
        }

        private bool _relatorio_edit;
        public bool Relatorio_edit
        {
            get { return _relatorio_edit; }
            set { _relatorio_edit = value; }
        }

        private bool _relatorio_remove;
        public bool Relatorio_remove
        {
            get { return _relatorio_remove; }
            set { _relatorio_remove = value; }
        }

        private bool _relatorio_search;
        public bool Relatorio_search
        {
            get { return _relatorio_search; }
            set { _relatorio_search = value; }
        }

        private bool _rede_view;
        public bool Rede_View
        {
            get { return _rede_view; }
            set { _rede_view = value; }
        }

        private bool _rede_add;
        public bool Rede_add
        {
            get { return _rede_add; }
            set { _rede_add = value; }
        }

        private bool _rede_edit;
        public bool Rede_edit
        {
            get { return _rede_edit; }
            set { _rede_edit = value; }
        }

        private bool _rede_remove;
        public bool Rede_remove
        {
            get { return _rede_remove; }
            set { _rede_remove = value; }
        }

        private bool _rede_search;
        public bool Rede_search
        {
            get { return _rede_search; }
            set { _rede_search = value; }
        }

        private bool _sistema_view;
        public bool Sistema_View
        {
            get { return _sistema_view; }
            set { _sistema_view = value; }
        }

        private bool _sistema_add;
        public bool Sistema_add
        {
            get { return _sistema_add; }
            set { _sistema_add = value; }
        }

        private bool _sistema_edit;
        public bool Sistema_edit
        {
            get { return _sistema_edit; }
            set { _sistema_edit = value; }
        }

        private bool _sistema_remove;
        public bool Sistema_remove
        {
            get { return _sistema_remove; }
            set { _sistema_remove = value; }
        }

        private bool _sistema_search;
        public bool Sistema_search
        {
            get { return _sistema_search; }
            set { _sistema_search = value; }
        }

        private bool _usuario_view;
        public bool Usuario_View
        {
            get { return _usuario_view; }
            set { _usuario_view = value; }
        }

        private bool _usuario_add;
        public bool Usuario_add
        {
            get { return _usuario_add; }
            set { _usuario_add = value; }
        }

        private bool _usuario_edit;
        public bool Usuario_edit
        {
            get { return _usuario_edit; }
            set { _usuario_edit = value; }
        }

        private bool _usuario_remove;
        public bool Usuario_remove
        {
            get { return _usuario_remove; }
            set { _usuario_remove = value; }
        }

        private bool _usuario_search;
        public bool Usuario_search
        {
            get { return _usuario_search; }
            set { _usuario_search = value; }
        }

        private bool _receita_view;
        public bool receita_view
        {
            get { return _receita_view; }
            set { _receita_view = value; }
        }

        private bool _receita_add;
        public bool receita_add
        {
            get { return _receita_add; }
            set { _receita_add = value; }
        }

        private bool _receita_edit;
        public bool receita_edit
        {
            get { return _receita_edit; }
            set { _receita_edit = value; }
        }

        private bool _receita_remove;
        public bool receita_remove
        {
            get { return _receita_remove; }
            set { _receita_remove = value; }
        }

        private bool _receita_search;
        public bool receita_search
        {
            get { return _receita_search; }
            set { _receita_search = value; }
        }

        private bool _tipoReceita_view;
        public bool tipoReceita_view
        {
            get { return _tipoReceita_view; }
            set { _tipoReceita_view = value; }
        }

        private bool _tipoReceita_add;
        public bool tipoReceita_add
        {
            get { return _tipoReceita_add; }
            set { _tipoReceita_add = value; }
        }

        private bool _tipoReceita_edit;
        public bool tipoReceita_edit
        {
            get { return _tipoReceita_edit; }
            set { _tipoReceita_edit = value; }
        }

        private bool _tipoReceita_remove;
        public bool tipoReceita_remove
        {
            get { return _tipoReceita_remove; }
            set { _tipoReceita_remove = value; }
        }

        private bool _tipoReceita_search;
        public bool tipoReceita_search
        {
            get { return _tipoReceita_search; }
            set { _tipoReceita_search = value; }
        }

        private bool _Recipiente_view;
        public bool Recipiente_view
        {
            get { return _Recipiente_view; }
            set { _Recipiente_view = value; }
        }

        private bool _Recipiente_add;
        public bool Recipiente_add
        {
            get { return _Recipiente_add; }
            set { _Recipiente_add = value; }
        }

        private bool _Recipiente_edit;
        public bool Recipiente_edit
        {
            get { return _Recipiente_edit; }
            set { _Recipiente_edit = value; }
        }

        private bool _Recipiente_remove;
        public bool Recipiente_remove
        {
            get { return _Recipiente_remove; }
            set { _Recipiente_remove = value; }
        }

        private bool _Recipiente_search;
        public bool Recipiente_search
        {
            get { return _Recipiente_search; }
            set { _Recipiente_search = value; }
        }

        private bool _Bandeja_view;
        public bool Bandeja_view
        {
            get { return _Bandeja_view; }
            set { _Bandeja_view = value; }
        }

        private bool _Bandeja_add;
        public bool Bandeja_add
        {
            get { return _Bandeja_add; }
            set { _Bandeja_add = value; }
        }

        private bool _Bandeja_edit;
        public bool Bandeja_edit
        {
            get { return _Bandeja_edit; }
            set { _Bandeja_edit = value; }
        }

        private bool _Bandeja_remove;
        public bool Bandeja_remove
        {
            get { return _Bandeja_remove; }
            set { _Bandeja_remove = value; }
        }

        private bool _Bandeja_search;
        public bool Bandeja_search
        {
            get { return _Bandeja_search; }
            set { _Bandeja_search = value; }
        }

        private bool _Produto_view;
        public bool Produto_view
        {
            get { return _Produto_view; }
            set { _Produto_view = value; }
        }

        private bool _Produto_add;
        public bool Produto_add
        {
            get { return _Produto_add; }
            set { _Produto_add = value; }
        }

        private bool _Produto_edit;
        public bool Produto_edit
        {
            get { return _Produto_edit; }
            set { _Produto_edit = value; }
        }

        private bool _Produto_remove;
        public bool Produto_remove
        {
            get { return _Produto_remove; }
            set { _Produto_remove = value; }
        }

        private bool _Produto_search;
        public bool Produto_search
        {
            get { return _Produto_search; }
            set { _Produto_search = value; }
        }

        private int _id_usuario;
        public int Id_Usuario
        {
            get { return _id_usuario; }
            set { _id_usuario = value; }
        }

    }
}

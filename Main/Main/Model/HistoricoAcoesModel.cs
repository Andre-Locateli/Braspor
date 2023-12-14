using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Model
{
    public class HistoricoAcoesModel
    {
        private int _id;
        public int Id 
        { 
            get { return _id; } 
            set { _id = value; } 
        }

        private int _id_usuario;
        public int Id_usuario 
        { 
            get { return _id_usuario; } 
            set { _id_usuario = value; }
        }

        private string _nome_usuario;
        public string Nome_usuario
        {
            get { return _nome_usuario; }
            set { _nome_usuario = value; }
        }

        private string _acao;
        public string Acao
        {
            get { return _acao; }
            set { _acao = value; }
        }

        private string _descricao;
        public string Descricao
        {
            get { return _descricao; }
            set { _descricao = value; }
        }

        private DateTime _dateinsert;
        public DateTime Dateinsert
        {
            get { return _dateinsert; }
            set { _dateinsert = value; }
        }
    }
}

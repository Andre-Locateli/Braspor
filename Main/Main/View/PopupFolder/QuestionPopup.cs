using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Main.View.PopupFolder
{
    public partial class QuestionPopup : Form
    {

        private bool _resposta;
        public bool RESPOSTA
        {
            get { return _resposta; }
            set { _resposta = value; }
        }

        private string _message;
        public string Message
        {
            get { return _message; }
            set { _message = value; }
        }


        public QuestionPopup(string question)
        {
            InitializeComponent();
            lblMensagem.Text = question;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            RESPOSTA = false;
            //Message = txtMessage.Text;
            this.Close();
        }

        private void btnEnviar_Click(object sender, EventArgs e)
        {
            RESPOSTA = true;
            Message = txtMessage.Text;
            this.Close();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lblMensagem_Click(object sender, EventArgs e)
        {

        }
    }
}

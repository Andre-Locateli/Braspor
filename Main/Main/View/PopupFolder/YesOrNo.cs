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
    public partial class YesOrNo : Form
    {

        private bool _RESPOSTA;
        public bool RESPOSTA
        {
            get { return _RESPOSTA; }
            set { _RESPOSTA = value; }
        }


        public YesOrNo(string question)
        {
            InitializeComponent();

            lblMensagem.Text = question;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RESPOSTA = true;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            RESPOSTA = false;
            this.Close();
        }
    }
}

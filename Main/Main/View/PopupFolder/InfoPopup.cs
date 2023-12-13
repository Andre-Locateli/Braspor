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
    public partial class InfoPopup : Form
    {
        public InfoPopup(string titulo, string message)
        {
            InitializeComponent();

            lblTitulo.Text = titulo;
            lblInformacao.Text = message;
        }

        private void btnEnviar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

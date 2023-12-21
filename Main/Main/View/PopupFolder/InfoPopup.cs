using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Main.View.PopupFolder
{
    public partial class InfoPopup : Form
    {
        public Image icon { get; set; }

        public InfoPopup(string titulo, string message)
        {
            InitializeComponent();

            lblTitulo.Text = titulo;
            lblInformacao.Text = message;
        }

        public InfoPopup(string titulo, string message, Image icone)
        {
            InitializeComponent();

            icon = icone;

            lblTitulo.Text = titulo;
            lblInformacao.Text = message;
        }

        private void btnEnviar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void InfoPopup_Load(object sender, EventArgs e)
        {
            try
            {
                if(icon != null)
                {
                    pictureBox1.Image = icon;
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}

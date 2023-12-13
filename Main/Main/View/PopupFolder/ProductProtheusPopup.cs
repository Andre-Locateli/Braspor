using Main.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media.Animation;

namespace Main.View.PopupFolder
{
    public partial class ProductProtheusPopup : Form
    {
        public ProdutoGet PRODUTO = new ProdutoGet();
        public string NUMERO_OP = "";

        public ProductProtheusPopup()
        {
            InitializeComponent();
            this.Opacity = 0.95;
            panel1.BackColor = Color.White;
            this.TransparencyKey = Color.FromArgb(0, 255, 0);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnNoProtheus_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtNumeroOp.Text)) { return; }
                if (string.IsNullOrWhiteSpace(txtCodigoProduto.Text)) { return; }
                if (string.IsNullOrWhiteSpace(txtDescricao.Text)) { return; }
                if (string.IsNullOrWhiteSpace(txtPesoAlvo.Text)) { return; }
                if (string.IsNullOrWhiteSpace(txtTolerencia.Text)) { return; }

                NUMERO_OP = txtNumeroOp.Text;
                PRODUTO.cod_produto = txtCodigoProduto.Text;
                PRODUTO.descricao = txtDescricao.Text;
                PRODUTO.peso_alvo = txtPesoAlvo.Text;
                PRODUTO.tolerancia = txtTolerencia.Text;
                this.Close();
            }
            catch (Exception)
            {
            }
        }

    }
}

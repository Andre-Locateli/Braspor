using Main.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Main.View.CustomLayout
{
    public partial class RowCardCustom : UserControl
    {
        private int index = 0;
        public object elemento = null;

        public event EventHandler ItemSelecionadoTrigger;

        private object _itemSelecionado;
        public object ItemSelecionado
        {
            get { return _itemSelecionado; }
            set
            {
                if (_itemSelecionado != value)
                {
                    _itemSelecionado = value;
                    if (ItemSelecionado != null)
                    {
                        if (ItemSelecionadoTrigger != null)
                        {
                            ItemSelecionadoTrigger(this, EventArgs.Empty);
                        }
                    }
                }
            }
        }

        private string _tipoVariavel;
        public string TipoVariavel
        {
            get { return _tipoVariavel; }
            set { _tipoVariavel = value; }
        }

        public RowCardCustom(ProdutoClass _produto)
        {
            InitializeComponent();
            TipoVariavel = "Produto";
            lblObjeto.Text = _produto.Part_number.ToString();
            lblObjeto.Tag = _produto;
            lblInfo.Text = _produto.Descricao;
            lblInfo.Tag = _produto;
            index = 0;
            elemento = _produto;
            
        }

        public RowCardCustom(BandejaClass _bandeja)
        {
            InitializeComponent();
            TipoVariavel = "Bandeja";
            lblObjeto.Text = _bandeja.Codigo.ToString();
            lblObjeto.Tag = _bandeja;
            lblInfo.Text = _bandeja.Descricao;
            lblInfo.Tag = _bandeja;
            index = 1;
            elemento = _bandeja;
        }

        public RowCardCustom(RecipienteClass _recipiente)
        {
            InitializeComponent();
            TipoVariavel = "Recipiente";
            lblObjeto.Text = _recipiente.Package.ToString();
            lblObjeto.Tag = _recipiente;
            lblInfo.Text = _recipiente.Descricao;
            lblInfo.Tag = _recipiente;
            index = 2;
            elemento = _recipiente;
        }


        private void lblObjeto_Click(object sender, EventArgs e)
        {
            try
            {
                ItemSelecionado = elemento;
                if (this.Size.Height == 32)
                {
                    this.Invoke(new MethodInvoker(delegate
                    {
                        this.Size = new System.Drawing.Size(239, 86);
                    }));
                }
                else 
                {
                    this.Invoke(new MethodInvoker(delegate
                    {
                        this.Size = new System.Drawing.Size(239, 32);
                    }));
                }        
                this.Invalidate();
            }
            catch (Exception)
            {
            }
        }

        private void lblObjeto_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {

                Label label = (Label)sender;
                label.DoDragDrop(label.Tag, DragDropEffects.Move);

                ItemSelecionado = elemento;
                if (this.Size.Height == 32)
                {
                    this.Invoke(new MethodInvoker(delegate
                    {
                        this.Size = new System.Drawing.Size(239, 86);
                    }));
                }
                else
                {
                    this.Invoke(new MethodInvoker(delegate
                    {
                        this.Size = new System.Drawing.Size(239, 32);
                    }));
                }
                this.Invalidate();
            }
            catch (Exception)
            {
            }
        }


    }
}

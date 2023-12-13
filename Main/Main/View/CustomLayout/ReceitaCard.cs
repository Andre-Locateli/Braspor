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

namespace Main.View.CustomLayout
{
    public partial class ReceitaCard : UserControl
    {
        private ReceitaClass receita = new ReceitaClass();
        public ReceitaCard(ReceitaClass _receita)
        {
            InitializeComponent();

            lblInfo.Text = $"Nome: {_receita.Nome}\nCódigo: {_receita.Codigo}";
            lblQuantidade.Text = $"Quantidade Peças: {_receita.Quantidade_pecas}\nQuantidade Bandejas: {_receita.Quantidade_bandejas}";
        }
    }
}

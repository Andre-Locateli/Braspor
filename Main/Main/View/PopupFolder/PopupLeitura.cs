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
using System.Windows.Resources;

namespace Main.View.PopupFolder
{
    public partial class PopupLeitura : Form
    {

        private int _response;
        public int RESPONSE
        {
            get { return _response; }
            set { _response = value; }
        }


        public PopupLeitura()
        {
            InitializeComponent();
            txtCodigo.Focus();
        }

        //private void pcbClose_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        this.Close();
        //    }
        //    catch (Exception)
        //    {
        //    }
        //}

        //private void txtCodigo_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (txtCodigo.Text.Contains("Digite o código")) 
        //        {
        //            txtCodigo.Text = "";
        //        }
        //    }
        //    catch (Exception)
        //    {
        //    }
        //}

        //private void txtCodigo_Leave(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (string.IsNullOrWhiteSpace(txtCodigo.Text))
        //        {
        //            txtCodigo.Text = "";
        //        }
        //    }
        //    catch (Exception)
        //    {
        //    }
        //}

        //private void txtCodigo_TextChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        TextBox txt = (TextBox)sender;
        //        ReceitaClass rec = (ReceitaClass)Program.SQL.SelectObject("SELECT * FROM Receita WHERE Codigo = @Codigo", "Receita",
        //            new Dictionary<string, object>() 
        //            {
        //                {"@Codigo", txt.Text}
        //            });

        //        if (rec != null) 
        //        {
        //            ProdutoClass prod = (ProdutoClass)Program.SQL.SelectObject("SELECT * FROM Produto WHERE id = @Id", "Produto",
        //                new Dictionary<string, object>() { { "@Id", rec.Id_Produto } });

        //            RecipienteClass recip = (RecipienteClass)Program.SQL.SelectObject("SELECT * FROM Recipiente WHERE id = @Id", "Recipiente",
        //                new Dictionary<string, object>() { { "@Id", rec.Id_Recipiente } });

        //            BandejaClass bande = (BandejaClass)Program.SQL.SelectObject("SELECT * FROM Bandeja WHERE id = @Id", "Bandeja",
        //                new Dictionary<string, object>() { { "@Id", rec.Id_Bandeja } });

        //            if (bande == null) { bande = new BandejaClass(); }

        //            //Fechar e carregar o item.
        //            int result = Program.SQL.InsertAndSelectLasRow("INSERT INTO LogReceita (id_receita,Nome, Codigo, id_Recipiente, Peso_Recipiente, " +
        //                                                "id_Bandeja, Qtd_Bandeja, Peso_Bandejas, " +
        //                                                "id_Produto, Qtd_Pecas, Peso_Pecas, Status, " +
        //                                                "Estacao, Operador, dateinsert) " +
        //                                                " VALUES " +
        //                                                "(@id_receita,@Nome, @Codigo, @id_Recipiente, @Peso_Recipiente,@id_Bandeja, @Qtd_Bandeja, " +
        //                                                "@Peso_Bandejas,@id_Produto, @Qtd_Pecas, @Peso_Pecas, @Status, @Estacao, @Operador, @dateinsert);SELECT SCOPE_IDENTITY() AS Last_Id;", "LogReceita",
        //                                                new Dictionary<string, object>() 
        //                                                {
        //                                                    {"@id_receita", rec.Id},
        //                                                    {"@Nome", rec.Nome},
        //                                                    {"@Codigo", rec.Codigo},
        //                                                    {"@id_Recipiente", rec.Id_Recipiente},
        //                                                    {"@Peso_Recipiente", Convert.ToDecimal(recip.PesoAlvo)},
        //                                                    {"@id_Bandeja", rec.Id_Bandeja},
        //                                                    {"@Qtd_Bandeja", rec.Quantidade_bandejas},
        //                                                    {"@Peso_Bandejas", Convert.ToDecimal(bande.PesoAlvo)},
        //                                                    {"@id_Produto", rec.Id_Produto},
        //                                                    {"@Qtd_Pecas", rec.Quantidade_pecas},
        //                                                    {"@Peso_Pecas", Convert.ToDecimal(prod.PesoAlvo)},
        //                                                    {"@Status", 0},
        //                                                    {"@Estacao", Environment.MachineName},
        //                                                    {"@Operador", Program._usuarioLogado.Nome},
        //                                                    {"@dateinsert", DateTime.Now},
        //                                                });

        //            if (result > 0) 
        //            {

        //                for (int i = 0; i < rec.Quantidade_bandejas; i++)
        //                {
        //                    var log_bandeja_linha = Program.SQL.CRUDCommand("INSERT INTO Log_bandeja_receita " +
        //                            "(id_log_receita, Numero_Bandeja, Peso_Bandeja, Peso_Produto) VALUES " +
        //                            "(@id_log_receita, @Numero_Bandeja, @Peso_Bandeja, @Peso_Produto)", "Log_bandeja_receita",
        //                            new Dictionary<string, object>()
        //                            {
        //                                {"@id_log_receita", result},
        //                                {"@Numero_Bandeja", i+1},
        //                                {"@Peso_Bandeja", 0},
        //                                {"@Peso_Produto", 0},
        //                            });
        //                }

        //                RESPONSE = result;
        //                this.Close();
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}
    
    }
}

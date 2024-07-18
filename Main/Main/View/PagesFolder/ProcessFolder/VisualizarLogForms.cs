using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;

namespace Main.View.PagesFolder.ProcessFolder
{
    public partial class VisualizarLogForms : Form
    {
        int idProcesso = 0;

        public VisualizarLogForms(int id_Processo)
        {
            InitializeComponent();
            idProcesso = id_Processo;
        }

        private void VisualizarLogForms_Load(object sender, EventArgs e)
        {
            var Processos = Program.SQL.SelectList("SELECT * FROM Log_Processos Where Id_processo = @Id AND qtd_temporeal > 0", "Log_Processos", null,
            new Dictionary<string, object>()
            {
                {"@Id", idProcesso }
            });

            var soma = Program.SQL.SelectList("SELECT SUM(Peso) AS SOMA FROM Log_Processos Where Id_processo = @Id", "Log_Processos", "SOMA", new Dictionary<string, object>() 
            {
                {"@Id", idProcesso }
            });

            if (soma.Count > 0) 
            {
                double dSoma = Math.Round(Convert.ToDouble(soma[0]), 4);

                lblTotal.Invoke(new MethodInvoker(delegate 
                {
                    lblTotal.Text = $"Peso total do lote: {dSoma}";
                }));
            }

            //dgvDados.DataSource = Program.SQL.SelectDataGrid("SELECT * FROM Log_Processos Where Id_processo = @Id AND qtd_temporeal > 0", "Log_Processos");

            if (Processos.Count != 0)
            {
                dgvDados.DataSource = Processos;

                dgvDados.Columns["Id"].Visible = false;
                dgvDados.Columns["Id_processo"].Visible = false;
                dgvDados.Columns["qtd_temporeal"].Visible = false;
                dgvDados.Columns["qtd_total"].HeaderText = "Quantidade Total";
                dgvDados.Columns["Tempo_execucao"].HeaderText = "Tempo de execução";
                dgvDados.Columns["dateinsert"].HeaderText = "Data de inserção";
            }
        }

        private void X_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

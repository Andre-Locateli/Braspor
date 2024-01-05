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
            var Processos = Program.SQL.SelectList("SELECT * FROM Log_Processos Where Id_processo = @Id", "Log_Processos", null,
            new Dictionary<string, object>()
            {
                {"@Id", idProcesso }
            });

            dgvDados.DataSource = Processos;    
        }

        private void X_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

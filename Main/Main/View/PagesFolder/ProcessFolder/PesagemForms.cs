using Main.Service;
using Main.View.MainFolder;
using Main.View.PopupFolder;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Main.View.PagesFolder.ProcessFolder
{
    public partial class PesagemForms : Form
    {
        int idUsuario = 0;
        string nomeUsuario = "";

        public PesagemForms(int id_Usuario, string nome_Usuario)
        {
            InitializeComponent();
            idUsuario = id_Usuario;
            nomeUsuario = nome_Usuario;
        }

        private void btnExecutar_Click(object sender, EventArgs e)
        {
            EscolhaPesagemForms escolha = new EscolhaPesagemForms(idUsuario, nomeUsuario);
            escolha.ShowDialog();
        }

        private void PesagemForms_Load(object sender, EventArgs e)
        {
            var Processos = Program.SQL.SelectList("SELECT * FROM Processos", "Processos", null,
            new Dictionary<string, object>());

            dgvDados.DataSource = Processos;

            dgvDados.Columns["Id"].Visible = false;
            dgvDados.Columns["Id_produto"].Visible = false;
            dgvDados.Columns["IdUsuario"].Visible = false;
            dgvDados.Columns["dateend"].Visible = false;
            dgvDados.Columns["dateupdate"].Visible = false;

            dgvDados.Columns["Descricao"].HeaderText = "Descrição";
            dgvDados.Columns["TempoExecucao"].HeaderText = "Tempo de execução";
            dgvDados.Columns["TotalContagem"].HeaderText = "Quantidade";
            dgvDados.Columns["PesoReferencia"].HeaderText = "Peso de referência";
            dgvDados.Columns["PesoTotal"].HeaderText = "Peso total";
            dgvDados.Columns["StatusProcesso"].HeaderText = "Status do processo";
            dgvDados.Columns["dateinsert"].HeaderText = "Data de inserção";
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            var Processos = Program.SQL.SelectList("SELECT * FROM Processos", "Processos", null,
            new Dictionary<string, object>());

            dgvDados.DataSource = null;
            dgvDados.Rows.Clear();

            if(txtSearch.Text == "" || txtSearch.Text == "  Pesquisar")
            {
                dgvDados.DataSource = Processos;
            }
            else
            {
                Processos = Program.SQL.SelectList("SELECT * FROM Processos WHERE Id LIKE '%" + txtSearch.Text + "%' OR Id_produto LIKE '%" + txtSearch.Text + "%' OR Id_usuario LIKE '%" + txtSearch.Text + "%' OR Descricao LIKE '%" + txtSearch.Text + "%' OR Status_processo LIKE '%" + txtSearch.Text + "%'", "Processos", null,
                new Dictionary<string, object>());

                dgvDados.DataSource = Processos;
            }

            if (Processos.Count != 0)
            {
                dgvDados.Columns["Id"].Visible = false;
                dgvDados.Columns["Id_produto"].Visible = false;
                dgvDados.Columns["IdUsuario"].Visible = false;
                dgvDados.Columns["dateend"].Visible = false;
                dgvDados.Columns["dateupdate"].Visible = false;

                dgvDados.Columns["Descricao"].HeaderText = "Descrição";
                dgvDados.Columns["TempoExecucao"].HeaderText = "Tempo de execução";
                dgvDados.Columns["TotalContagem"].HeaderText = "Quantidade";
                dgvDados.Columns["PesoReferencia"].HeaderText = "Peso de referência";
                dgvDados.Columns["PesoTotal"].HeaderText = "Peso total";
                dgvDados.Columns["StatusProcesso"].HeaderText = "Status do processo";
                dgvDados.Columns["dateinsert"].HeaderText = "Data de inserção";
            }
        }

        private void txtSearch_Click(object sender, EventArgs e)
        {
            if(txtSearch.Text == "  Pesquisar")
            {
                txtSearch.Text = "";
            }
        }

        private void txtSearch_Leave(object sender, EventArgs e)
        {
            if(txtSearch.Text == "")
            {
                txtSearch.Text = "  Pesquisar";
            }
        }

        private void dgvDados_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(dgvDados.Rows[e.RowIndex].Cells["Id"].Value);

                YesOrNo question = new YesOrNo("Deseja retomar processo?");
                question.ShowDialog();

                if (question.RESPOSTA)
                {
                    ProcessForms proc = new ProcessForms(id);

                    foreach (Form openForm in Application.OpenForms)
                    {
                        if (openForm is MainForms)
                        {
                            MainForms mainForm = (MainForms)openForm;
                            mainForm.OpenPage(proc);
                            this.Close();
                            return;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}

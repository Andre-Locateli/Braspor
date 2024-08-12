﻿//using Main.Service;
using iTextSharp.xmp;
using Main.Service;
using Main.View.MainFolder;
using Main.View.PopupFolder;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
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

        private int minParam = 0;
        private int maxParam = 15;
        private double qRows = 0;
        private double drows = 0;

        private int counter_press = 1;

        public PesagemForms(int id_Usuario, string nome_Usuario)
        {
            InitializeComponent();
            idUsuario = id_Usuario;
            nomeUsuario = nome_Usuario;
        }

        private void btnExecutar_Click(object sender, EventArgs e)
        {
            //var ProdutoCheck = Program.SQL.SelectList("SELECT * FROM MateriaPrima", "MateriaPrima", null,
            //new Dictionary<string, object>());

            //if (ProdutoCheck.Count == 0)
            //{
            //    InfoPopup info = new InfoPopup("Erro", "Cadastre uma matéria-prima antes de iniciar um processo!", Properties.Resources.errorIcon);
            //    info.ShowDialog();
            //}
            //else
            //{
                EscolhaPesagemForms escolha = new EscolhaPesagemForms(idUsuario, nomeUsuario);
                escolha.ShowDialog();
            //}
        }

        private void PesagemForms_Load(object sender, EventArgs e)
        {
            LoadDatabaseInfo();
        }

        private void LoadDatabaseInfo()
        {
            dgvDados.DataSource = null;
            dgvDados.Rows.Clear();

            var Processos = Program.SQL.SelectList("SELECT * FROM Processos", "Processos", null,
            new Dictionary<string, object>());

            qRows = Processos.Count();
            drows = qRows / 10;
            quantityPage.Text = $"{Math.Ceiling(qRows / 10)}";

            double aux_minParam = minParam;
            double aux_maxParam = maxParam;

            //dgvDados.DataSource = Processos;

            var items = Program.SQL.SelectList("SELECT * FROM ( SELECT *, ROW_NUMBER() OVER (ORDER BY dateinsert DESC) AS row FROM PROCESSOS AS LReceita ) temp WHERE row >= @aux_minParam AND row <= @aux_maxParam", "Processos", null,
            new Dictionary<string, object>()
            {
                {"@aux_minParam", aux_minParam },
                {"@aux_maxParam", aux_maxParam }
            });



            dgvDados.DataSource = items;


            if (items.Count == 0)
            {
                dgvDados.Columns    ["Status"].         Visible         =   false                   ;
            }
            else
            {
                dgvDados.Columns    ["Status"].         Visible         =   true                    ;

                dgvDados.Columns    ["Id"].             Visible         =   false                   ;
                dgvDados.Columns    ["dateend"].        Visible         =   false                   ;
                dgvDados.Columns    ["IdUsuario"].      Visible         =   false                   ;
                dgvDados.Columns    ["Id_produto"].     Visible         =   false                   ;
                dgvDados.Columns    ["dateupdate"].     Visible         =   false                   ;
                dgvDados.Columns    ["StatusProcesso"]. Visible         =   false                   ;

                dgvDados.Columns    ["Op"].             HeaderText      =   "Ordem de Produção"     ;
                dgvDados.Columns    ["Gramatura"].      HeaderText      =   "Gramatura"             ;
                dgvDados.Columns    ["Descricao"].      HeaderText      =   "Descrição"             ;
                dgvDados.Columns    ["PesoTotal"].      HeaderText      =   "Peso total"            ;
                dgvDados.Columns    ["dateinsert"].     HeaderText      =   "Data de inserção"      ;
                dgvDados.Columns    ["TempoExecucao"].  HeaderText      =   "Tempo de execução"     ;
                dgvDados.Columns    ["TotalContagem"].  HeaderText      =   "Quantidade"            ;
                dgvDados.Columns    ["Status"].         DisplayIndex    =   10                      ;

            }

            ReorderButtons();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            var Processos = Program.SQL.SelectList("SELECT * FROM Processos", "Processos", null,
            new Dictionary<string, object>());

            dgvDados.DataSource = null;
            dgvDados.Rows.Clear();

            if(txtSearch.Text == "" || txtSearch.Text == "  Pesquisar")
            {
                LoadDatabaseInfo();
            }
            else
            {
                Processos.Clear();
                Processos = Program.SQL.SelectList("SELECT * FROM Processos WHERE Id LIKE '%" + txtSearch.Text + "%' OR Id_produto LIKE '%" + txtSearch.Text + "%' OR Id_usuario LIKE '%" + txtSearch.Text + "%' OR Descricao LIKE '%" + txtSearch.Text + "%' OR Status_processo LIKE '%" + txtSearch.Text + "%' ORDER BY Status_processo", "Processos", null,
                new Dictionary<string, object>());

                dgvDados.DataSource = Processos;
            }

            if (Processos.Count != 0)
            {
                dgvDados.Columns    ["Id"].             Visible         =   false                   ;
                dgvDados.Columns    ["Id_produto"].     Visible         =   false                   ;
                dgvDados.Columns    ["IdUsuario"].      Visible         =   false                   ;
                dgvDados.Columns    ["dateend"].        Visible         =   false                   ;
                dgvDados.Columns    ["dateupdate"].     Visible         =   false                   ;
                dgvDados.Columns    ["StatusProcesso"]. Visible         =   false                   ;

                dgvDados.Columns    ["Descricao"].      HeaderText      =   "Descrição"             ;
                dgvDados.Columns    ["TempoExecucao"].  HeaderText      =   "Tempo de execução"     ;
                dgvDados.Columns    ["TotalContagem"].  HeaderText      =   "Quantidade"            ;
                dgvDados.Columns    ["Gramatura"].      HeaderText      =   "Gramatura"             ;
                dgvDados.Columns    ["PesoTotal"].      HeaderText      =   "Peso total"            ;
                dgvDados.Columns    ["Op"].             HeaderText      =   "Ordem de Produção"     ;
                dgvDados.Columns    ["dateinsert"].     HeaderText      =   "Data de inserção"      ;
                dgvDados.Columns    ["Status"].         DisplayIndex    =   10                      ; 
            }
            else
            {
                dgvDados.Columns    ["Status"].         Visible         =   false                   ;
            }
        }

        private void txtSearch_Enter(object sender, EventArgs e)
        {
            if (txtSearch.Text == "  Pesquisar")
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
                if (SerialCommunicationService.SERIALPORT1.IsOpen == true)
                {
                    int id = Convert.ToInt32(dgvDados.Rows[e.RowIndex].Cells["Id"].Value);
                    string status = Convert.ToString(dgvDados.Rows[e.RowIndex].Cells["Status"].Value);

                    if (status == "Finalizado")
                    {
                        InfoPopup info = new InfoPopup("Alerta!", "Esse processo já foi finalizado! \n Crie um novo ou retome um já existente para iniciar uma nova pesagem.");
                        info.ShowDialog();
                    }
                    else
                    {
                        YesOrNo question = new YesOrNo("Deseja retomar processo?");
                        question.ShowDialog();

                        if (question.RESPOSTA)
                        {
                            ContagemPage proc = new ContagemPage(id);

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
                }
                else
                {
                    InfoPopup info = new InfoPopup("Erro!", "Você não está conectado a nenhuma balança. Pressiona a tecla F1 e efetue a configuração antes de iniciar o processo!", Properties.Resources.errorIcon);
                    info.ShowDialog();
                }
            }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

        private void pcb_return_Click(object sender, EventArgs e)
        {
            if (minParam > 0)
            {
                if (minParam - 15 >= 0)
                {
                    minParam -= 15;
                    maxParam -= 15;
                    counter_press -= 1;
                    ReorderSequence();
                    LoadDatabaseInfo();
                }
            }
        }

        private void pcb_next_Click(object sender, EventArgs e)
        {
            if (minParam + 15 < qRows)
            {
                minParam += 15;
                maxParam += 15;
                counter_press += 1;
                ReorderSequence();
                LoadDatabaseInfo();
            }
        }

        private void ReorderSequence()
        {
            try
            {
                if (counter_press <= 3)
                {
                    btnthree.Text = $"{3}";
                }
                else
                {
                    btnthree.Text = $"{counter_press}";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void btnOne_Click(object sender, EventArgs e)
        {
            try
            {
                Button cbtn = (Button)sender;

                if (((Convert.ToInt32(cbtn.Text)-1) * 15) <= qRows)
                {
                    maxParam = Convert.ToInt32(cbtn.Text) * 15;
                    minParam = (Convert.ToInt32(cbtn.Text) * 15) - 15;

                    if (((Convert.ToInt32(cbtn.Text) - 1) * 15) < qRows)
                    {
                        counter_press = Convert.ToInt32(cbtn.Text);
                    }

                    ReorderSequence();
                    LoadDatabaseInfo();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void ReorderButtons()
        {
            try
            {
                if (counter_press + 2 > 3 && counter_press < Convert.ToInt32(quantityPage.Text))
                {
                    btnOne.Invoke(new MethodInvoker(delegate { btnOne.Text = (counter_press + 2 - 3).ToString(); }));
                    btnTwo.Invoke(new MethodInvoker(delegate { btnTwo.Text = (counter_press + 2 - 2).ToString(); }));
                    btnthree.Invoke(new MethodInvoker(delegate { btnthree.Text = (counter_press + 2 - 1).ToString(); }));
                }
                else if (counter_press + 1 < 3)
                {
                    btnOne.Invoke(new MethodInvoker(delegate { btnOne.Text = "1"; }));
                    btnTwo.Invoke(new MethodInvoker(delegate { btnTwo.Text = "2"; }));
                    btnthree.Invoke(new MethodInvoker(delegate { btnthree.Text = "3"; }));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void dgvDados_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            int x = 0;

            foreach (DataGridViewRow row in dgvDados.Rows)
            {
                if (dgvDados.Rows[x].Cells[9].Value.ToString() == "0")
                {
                    row.Cells[1].Value = "Sem referência";
                }
                else if (dgvDados.Rows[x].Cells[9].Value.ToString() == "1")
                {
                    row.Cells[1].Value = "Referência calculada";
                }
                else if (dgvDados.Rows[x].Cells[9].Value.ToString() == "2")
                {
                    row.Cells[1].Value = "Contagem iniciada";
                }
                else if (dgvDados.Rows[x].Cells[9].Value.ToString() == "3")
                {
                    row.Cells[1].Value = "Finalizado";
                }

 
                if (dgvDados.Rows[x].Cells[5].Value.ToString() == "")
                {
                    row.Cells[5].Value = "Processo sem descrição.";
                }

                if (dgvDados.Rows[x].Cells[6].Value.ToString() == "")
                {
                    row.Cells[6].Value = "00:00:00";
                }

                x++;
            }
        }

        private void dgvDados_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void excluirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                DataGridViewRow linha_selecionada = dgvDados.SelectedRows[0];

                string Status = Convert.ToString(linha_selecionada.Cells["Status"].Value);
                if (Status != "Finalizado")
                {
                    YesOrNo question = new YesOrNo("Tem certeza que deseja remover o processo selecionado ?\n\r O registro será excluido da base de dados e essa ação é irreversível.");
                    question.ShowDialog();
                    if (question.RESPOSTA)
                    {
                        bool bDeleteCRUD = Program.SQL.CRUDCommand("DELETE FROM Processos WHERE id = @id", "Processos",
                            new Dictionary<string, object>() { { "@id", linha_selecionada.Cells["Id"].Value.ToString() } });

                        if (bDeleteCRUD)
                        {
                            LoadDatabaseInfo();
                        }

                    }
                }

            }
            catch (Exception)
            {
            }
        }
    }
}

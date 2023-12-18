using Main.Model;
using Main.View.PopupFolder;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Main.View.CadastroFolder
{
    public partial class CadastroMateriaPrimaForms : Form
    {
        double pctTolerancia = 0;

        int idUsuario = 0;
        string nomeUsuario = "";

        int idMateriaEdit = 0;

        Regex apenasNumero = new Regex("^[0-9]*$");

        public CadastroMateriaPrimaForms(int id_Usuario, string nome_Usuario)
        {
            InitializeComponent();
            idUsuario = id_Usuario;
            nomeUsuario = nome_Usuario;
        }

        private void CadastroMateriaPrimaForms_Load(object sender, EventArgs e)
        {
            LoadProductDB();
        }

        private void tkbr_Tolerancia_Scroll(object sender, EventArgs e)
        {
            pctTolerancia = tkbr_Tolerancia.Value;

            txtTolerancia.Invoke((MethodInvoker)delegate
            {
                txtTolerancia.Text = tkbr_Tolerancia.Value.ToString() + "%";
            });
        }

        private void txtTolerancia_TextChanged(object sender, EventArgs e)
        {
            pctTolerancia = Convert.ToInt32(txtTolerancia.Text.Replace("%", ""));
            tkbr_Tolerancia.Invoke((MethodInvoker)delegate
            {
                tkbr_Tolerancia.Value = Convert.ToInt32(pctTolerancia);
            });
        }

        private void btnPaintBorder_Click(object sender)
        {
            Color borderColor = Color.Red;

            Control item = (Control)sender;
            using (Graphics g = item.CreateGraphics())
            {
                Pen pen = new Pen(borderColor, 1);
                Rectangle rect = new Rectangle(0, 0, item.Width - 1, item.Height - 1);
                g.DrawRectangle(pen, rect);
            }
        }

        public void LoadProductDB()
        {
            try
            {
                dgv_dados.DataSource = Program.SQL.SelectDataGrid("SELECT Id, Codigo as [Código], Descricao as [Descrição], Tolerancia_erro as [Tolerância de erro(%)], quantidade_minima as [Quantidade Miníma], bit_status as [Disponível?] FROM MateriaPrima", "MateriaPrima");
                dgv_dados.Columns[2].Visible = false;
            }
            catch (Exception)
            {
            }
        }

        private void btnSalvarUser_Click(object sender, EventArgs e)
        {
            try
            {
                int disponivelCk = 0;
                if (disponivelCheck.Checked)
                {
                    disponivelCk = 1;
                }
                else
                {
                    disponivelCk = 0;
                }

                if (string.IsNullOrWhiteSpace(txtDescricao.Text)) { btnPaintBorder_Click(txtDescricao); return; }
                if (string.IsNullOrWhiteSpace(txtCodigo.Text)) { btnPaintBorder_Click(txtCodigo); return; }
                if (string.IsNullOrWhiteSpace(txtQtMinima.Text)) { btnPaintBorder_Click(txtQtMinima); return; }

                if (apenasNumero.IsMatch(txtQtMinima.Text))
                {
                    btnPaintBorder_Click(txtQtMinima);
                    return;
                }

                this.Refresh();

                if (Program.SQL.CRUDCommand("INSERT INTO MateriaPrima (Codigo, Descricao, Tolerancia_erro, quantidade_minima, bit_status, dateinsert) VALUES (@Codigo, @Descricao, @Tolerancia_erro, @quantidade_minima, @bit_status, @dateinsert)", "MateriaPrima", 
                    new Dictionary<string, object>()
                    {
                        {"@Codigo", txtCodigo.Text },
                        {"@Descricao", txtDescricao.Text },
                        {"@Tolerancia_erro", pctTolerancia },
                        {"@quantidade_minima", Convert.ToInt32(txtQtMinima.Text) },
                        {"@bit_status", disponivelCk },
                        {"@dateinsert", DateTime.Now}}))
                {
                    var insertHistorico = Program.SQL.CRUDCommand("INSERT INTO Historico_Acoes (Id_usuario, Nome_usuario, Acao, Descricao, dateinsert) VALUES (@Id_usuario, @Nome_usuario, @Acao, @Descricao, @dateinsert)", "Historico_Acoes",
                    new Dictionary<string, object>()
                    {
                        {"@Id_usuario", idUsuario},
                        {"@Nome_usuario", nomeUsuario},
                        {"@Acao", "Inserção de Matéria-prima"},
                        {"@Descricao", "Matéria-prima: " + txtCodigo.Text + " - " + txtDescricao.Text },
                        {"@dateinsert", DateTime.Now}
                    });

                    btnNovoUser_Click(sender, e);
                    btnEditarUser.Visible = true;
                    btnSalvarUser.Visible = false;
                    btnNovoUser.Visible = true;

                    LoadProductDB();
                }
                else
                {
                    MessageBox.Show("Erro ao adicionar item");
                }
            }
            catch (Exception)
            {

            }
        }

        private void btnNovoUser_Click(object sender, EventArgs e)
        {
            try
            {
                txtDescricao.Text = "";
                txtCodigo.Text = "";
                txtTolerancia.Text = "0%";
                txtQtMinima.Text = "";
                tkbr_Tolerancia.Value = 0;
                disponivelCheck.Checked = false;
                LoadProductDB();
            }
            catch (Exception)
            {
            }
        }

        private void btnEditarUser_Click(object sender, EventArgs e)
        {
            try
            {
                int disponivelCk = 0;
                if (disponivelCheck.Checked)
                {
                    disponivelCk = 1;
                }
                else
                {
                    disponivelCk = 0;
                }

                if (string.IsNullOrWhiteSpace(txtDescricao.Text)) { btnPaintBorder_Click(txtDescricao); return; }
                if (string.IsNullOrWhiteSpace(txtCodigo.Text)) { btnPaintBorder_Click(txtCodigo); return; }
                if (string.IsNullOrWhiteSpace(txtQtMinima.Text)) { btnPaintBorder_Click(txtQtMinima); return; }

                if (!apenasNumero.IsMatch(txtQtMinima.Text)) { btnPaintBorder_Click(txtQtMinima); return; }
                this.Refresh();

                
                if (Program.SQL.CRUDCommand("UPDATE MateriaPrima SET Codigo = @Codigo, Descricao = @Descricao, Tolerancia_erro = @Tolerancia_erro, quantidade_minima = @quantidade_minima, bit_status = @bit_status, dateinsert = @dateinsert WHERE Id = @Id", "MateriaPrima", 
                    new Dictionary<string, object>()
                    {
                        {"@Id", idMateriaEdit},
                        {"@Codigo", txtCodigo.Text },
                        {"@Descricao", txtDescricao.Text },
                        {"@Tolerancia_erro", pctTolerancia },
                        {"@quantidade_minima", Convert.ToInt32(txtQtMinima.Text) },
                        {"@bit_status", disponivelCk },
                        {"@dateinsert", DateTime.Now}
                    }))
                {
                    var insertHistorico = Program.SQL.CRUDCommand("INSERT INTO Historico_Acoes (Id_usuario, Nome_usuario, Acao, Descricao, dateinsert) VALUES (@Id_usuario, @Nome_usuario, @Acao, @Descricao, @dateinsert)", "Historico_Acoes",
                    new Dictionary<string, object>()
                    {
                        {"@Id_usuario", idUsuario},
                        {"@Nome_usuario", nomeUsuario},
                        {"@Acao", "Edição de Matéria-prima"},
                        {"@Descricao", "Matéria-prima: " + txtCodigo.Text + " - " + txtDescricao.Text },
                        {"@dateinsert", DateTime.Now}
                    });

                    btnNovoUser_Click(sender, e);
                    btnEditarUser.Visible = false;
                    btnSalvarUser.Visible = true;
                    btnNovoUser.Visible = false;
                    LoadProductDB();
                }
                else
                {
                    MessageBox.Show("Erro ao adicionar item");
                }
            }
            catch (Exception)
            {
            }
        }


        private void txt_search_TextChanged(object sender, EventArgs e)
        {
            try
            {
                var ds = new DataSet();
                DataTable dt;
                dt = (DataTable)dgv_dados.DataSource;
                dt.DefaultView.RowFilter = string.Format("[{0}] LIKE '%" + txt_search.Text + "%'", "Descrição");
            }
            catch (Exception)
            {
            }
        }

        private void dgv_dados_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 0)
                {
                    MateriaPrimaClass materiaPrima = (MateriaPrimaClass)Program.SQL.SelectObject("SELECT Id, Codigo, Descricao, Tolerancia_erro, quantidade_minima, bit_status, dateinsert from MateriaPrima WHERE Id = @Id", "MateriaPrima", new Dictionary<string, object>()
                    {
                        {"@Id", dgv_dados.CurrentRow.Cells["Id"].Value}
                    });

                    btnEditarUser.Visible = true;
                    btnSalvarUser.Visible = false;
                    btnNovoUser.Visible = true;

                    idMateriaEdit = materiaPrima.Id;
                    txtDescricao.Text = materiaPrima.Descricao;
                    txtCodigo.Text = materiaPrima.Codigo;
                    txtTolerancia.Text = materiaPrima.Tolerancia_erro.ToString() + "%";
                    pctTolerancia = materiaPrima.Tolerancia_erro;
                    txtQtMinima.Text = materiaPrima.Quantidade_minima.ToString();
                    tkbr_Tolerancia.Value = Convert.ToInt32(materiaPrima.Tolerancia_erro);
                    disponivelCheck.Checked = materiaPrima.Status;
                    LoadProductDB();

                }

                if (e.ColumnIndex == 1)
                {
                    YesOrNo question = new YesOrNo("Você tem certeza que deseja remover o produto selecionado?");
                    question.ShowDialog();

                    if (question.RESPOSTA && Program._permissaoUsuario.Produto_remove)
                    {
                        if (Program.SQL.CRUDCommand("DELETE FROM MateriaPrima WHERE Id = @Id", "MateriaPrima", new Dictionary<string, object>() { { "@Id", dgv_dados.CurrentRow.Cells["Id"].Value } }))
                        {
                            var insertHistorico = Program.SQL.CRUDCommand("INSERT INTO Historico_Acoes (Id_usuario, Nome_usuario, Acao, Descricao, dateinsert) VALUES (@Id_usuario, @Nome_usuario, @Acao, @Descricao, @dateinsert)", "Historico_Acoes",
                            new Dictionary<string, object>()
                            {
                                {"@Id_usuario", idUsuario},
                                {"@Nome_usuario", nomeUsuario},
                                {"@Acao", "Exclusão de Matéria-prima"},
                                {"@Descricao", "Matéria-prima: " + txtCodigo.Text + " - " + txtDescricao.Text },
                                {"@dateinsert", DateTime.Now}
                            });

                            btnNovoUser_Click(new object(), new EventArgs());
                            LoadProductDB();
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

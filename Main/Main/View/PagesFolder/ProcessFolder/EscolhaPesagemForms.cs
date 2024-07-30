using Main.Model;
using Main.Service;
using Main.View.MainFolder;
using Main.View.PopupFolder;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Main.View.PagesFolder.ProcessFolder
{
    public partial class EscolhaPesagemForms : Form
    {
        int idUsuario = 0;
        string nomeUsuario = "";

        string CodDesc = "";
        int codLength = 0;
        string CodSubs = "";

        List<object> listaMateriaPrima = new List<object>();

        Regex validadigito = new Regex("^[0-9]+$");

        public EscolhaPesagemForms(int id_Usuario, string nome_Usuario)
        {
            InitializeComponent();
            idUsuario = id_Usuario;
            nomeUsuario = nome_Usuario;
            this.Select();
        }

        private void EscolhaPesagemForms_Load(object sender, EventArgs e)
        {
            //listaMateriaPrima = Program.SQL.SelectList("SELECT * FROM MateriaPrima", "MateriaPrima", null,
            //new Dictionary<string, object>());

            //foreach (MateriaPrimaClass materia in listaMateriaPrima)
            //{
            //    CodDesc = materia.Codigo + " - " + materia.Descricao;
            //    codLength = materia.Codigo.Length;
            //    cb_MateriaPrima.Items.Add(CodDesc);
            //}

            //LoadComboBox(cb_MateriaPrima, "SELECT * FROM MateriaPrima", "MateriaPrima", new Dictionary<string, object>(), "Descricao");

            //cb_MateriaPrima.SelectedIndex = 0;
        }

        public void LoadComboBox(ComboBox cb, string consulta, string tabela, Dictionary<string, object> parameter, string parameter_name)
        {
            try
            {
                List<object> items = Program.SQL.SelectList(consulta, tabela, null, parameter);

                cb.DataSource = items;
                cb.DisplayMember = parameter_name;
                cb.SelectedItem = null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private void X_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_Confirmar_Click(object sender, EventArgs e)
        {

            #region =Descartado por Enquanto=

            //if (cb_MateriaPrima.SelectedItem == null)
            //{
            //    return;
            //}

            //if (cb_MateriaPrima.Text == "")
            //{
            //    InfoPopup info = new InfoPopup("Erro", "Selecione a Matéria-prima para pesagem!", Properties.Resources.errorIcon);
            //    info.ShowDialog();
            //}
            //else
            //{
            //    YesOrNo question = new YesOrNo("Tem certeza que deseja iniciar esse processo?");
            //    question.ShowDialog();

            //    if(question.RESPOSTA)
            //    {
            //        MateriaPrimaClass materia = (MateriaPrimaClass)cb_MateriaPrima.SelectedItem;

            //        int idMateria = 0;
            //        int qtMinima = 0;

            //        var listaMateriaPrima = Program.SQL.SelectList("SELECT * FROM MateriaPrima WHERE Id = @Id", "MateriaPrima", null,
            //        new Dictionary<string, object>()
            //        {
            //            {"@Id", materia.Id }
            //        });


            //        idMateria = materia.Id;
            //        qtMinima = materia.Quantidade_minima;

            //        DateTime dataInsertBanco = DateTime.Now;


            //        var insertBanco = Program.SQL.CRUDCommand("INSERT INTO Processos (Id_produto, Id_usuario, Descricao, Status_processo, dateinsert) VALUES (@Id_produto, @Id_usuario, @Descricao, @Status_processo, @dateinsert)", "Processos",
            //        new Dictionary<string, object>()
            //        {
            //            {"@Id_produto", idMateria },
            //            {"@Id_usuario", idUsuario },
            //            {"@Descricao", txt_Descricao.Text },
            //            {"@Status_processo", 0 },
            //            {"@dateinsert", dataInsertBanco}
            //        });

            //        int idInserido = 0;

            //        await Task.Delay(500);

            //        var selectID = Program.SQL.SelectList("SELECT * FROM Processos WHERE dateinsert = @dateinsert", "Processos", "Id",
            //        new Dictionary<string, object>()
            //        {
            //            {"@dateinsert", dataInsertBanco }
            //        });

            //        idInserido = (int)selectID.First();

            //        PesoProcessForms proc = new PesoProcessForms(idUsuario, nomeUsuario, idMateria, qtMinima, txt_Descricao.Text, idInserido);

            //        foreach (Form openForm in Application.OpenForms)
            //        {
            //            if (openForm is MainForms)
            //            {
            //                MainForms mainForm = (MainForms)openForm;
            //                mainForm.OpenPage(proc);
            //                this.Close();
            //                return;
            //            }
            //        }
            //    }
            //}
            #endregion


            string observacao = "";

            Boolean validaquant = validadigito.IsMatch(txt_qtfolhas.Text);

            try
            {
                if (txt_Descricao.Text == "")
                {
                    observacao = "Processo sem observação.";
                }
                else
                {
                    observacao = txt_Descricao.Text;
                }

                if (txt_cliente.Text == "" || txt_tipo.Text == "" || txt_qtfolhas.Text == "" || txt_formato.Text == "" || txt_op.Text == "")
                {
                    InfoPopup info = new InfoPopup("Erro", "Preencha todos os campos com '*' para prosseguir!", Properties.Resources.errorIcon);
                    info.ShowDialog();
                }
                else
                {
                    if (validaquant == false)
                    {
                        InfoPopup info = new InfoPopup("Erro", "Os campos de Número e Quantidade devem ser preenchidos apenas com Digítos!", Properties.Resources.errorIcon);
                        info.ShowDialog();
                    }
                    else
                    {

                        YesOrNo question = new YesOrNo("Tem certeza que deseja iniciar esse processo?");
                        question.ShowDialog();

                        if (question.RESPOSTA)
                        {

                            DateTime dataInsertBanco = DateTime.Now;

                            if (Convert.ToInt32(txt_qtfolhas.Text) >= 5)
                            {

                                var insertBanco = Program.SQL.InsertAndSelectLasRow("INSERT INTO Processos (Id_produto, Id_usuario, Descricao, Status_processo, dateinsert, Cliente, Numero, OP, Tipo, Papel, Formato, Quantidade,Gramatura,GramaturaDigitado) VALUES (@Id_produto, @Id_usuario, @Descricao, @Status_processo, @dateinsert, @Cliente, @Numero, @OP, @Tipo, @Papel, @Formato, @Quantidade,@Gramatura, @GramaturaDigitado) SELECT SCOPE_IDENTITY() AS Last_Id;", "Processos",
                                new Dictionary<string, object>()
                                {
                                    {"@Id_produto", 0 },
                                    {"@Id_usuario", idUsuario },
                                    {"@Descricao", observacao },
                                    {"@Status_processo", 0 },
                                    {"@dateinsert", dataInsertBanco},
                                    {"@Cliente", txt_cliente.Text},
                                    {"@Numero", "0"},
                                    {"@OP", txt_op.Text},
                                    {"@Tipo", txt_tipo.Text},
                                    {"@Papel", "0"},
                                    {"@Formato", txt_formato.Text},
                                    {"@Quantidade", txt_qtfolhas.Text},
                                    {"@Gramatura", 0},
                                    {"@GramaturaDigitado", Convert.ToDouble(txt_gramatura.Text)}
                                });

                                if (insertBanco > 0)
                                {
                                    if (SerialCommunicationService.SERIALPORT1.IsOpen == true)
                                    {
                                        PesoProcessForms proc = new PesoProcessForms(idUsuario, nomeUsuario, Convert.ToInt32(txt_qtfolhas.Text), insertBanco);

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
                                    else
                                    {
                                        this.Close();

                                        InfoPopup info = new InfoPopup("Erro!", "O processo " + txt_op.Text + " - " + txt_cliente.Text +  " foi criado, porém, você não está conectado a nenhuma balança. Pressiona a tecla F1 e efetue a configuração antes de iniciar o processo!", Properties.Resources.errorIcon);
                                        info.ShowDialog();
                                    }
                                }
                            }
                            else
                            {
                                InfoPopup info = new InfoPopup("Erro", "Quantidade não pode ser menor que 10!", Properties.Resources.errorIcon);
                                info.ShowDialog();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

    }
}

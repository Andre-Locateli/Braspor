using Main.Model;
using Main.View.MainFolder;
using Main.View.PopupFolder;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
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

            LoadComboBox(cb_MateriaPrima, "SELECT * FROM MateriaPrima", "MateriaPrima", new Dictionary<string, object>(), "Descricao");

            cb_MateriaPrima.SelectedIndex = 0;
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
            catch (Exception)
            {
            }
        }

        private void X_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void btn_Confirmar_Click(object sender, EventArgs e)
        {
            if (cb_MateriaPrima.SelectedItem == null)
            {
                return;
            }

            if (cb_MateriaPrima.Text == "")
            {
                InfoPopup info = new InfoPopup("Erro", "Selecione a Matéria-prima para pesagem!", Properties.Resources.errorIcon);
                info.ShowDialog();
            }
            else
            {
                YesOrNo question = new YesOrNo("Tem certeza que deseja iniciar esse processo?");
                question.ShowDialog();

                if(question.RESPOSTA)
                {
                    MateriaPrimaClass materia = (MateriaPrimaClass)cb_MateriaPrima.SelectedItem;

                    int idMateria = 0;
                    int qtMinima = 0;

                    var listaMateriaPrima = Program.SQL.SelectList("SELECT * FROM MateriaPrima WHERE Id = @Id", "MateriaPrima", null,
                    new Dictionary<string, object>()
                    {
                        {"@Id", materia.Id }
                    });


                    idMateria = materia.Id;
                    qtMinima = materia.Quantidade_minima;

                    DateTime dataInsertBanco = DateTime.Now;


                    var insertBanco = Program.SQL.CRUDCommand("INSERT INTO Processos (Id_produto, Id_usuario, Descricao, Status_processo, dateinsert) VALUES (@Id_produto, @Id_usuario, @Descricao, @Status_processo, @dateinsert)", "Processos",
                    new Dictionary<string, object>()
                    {
                        {"@Id_produto", idMateria },
                        {"@Id_usuario", idUsuario },
                        {"@Descricao", txt_Descricao.Text },
                        {"@Status_processo", 0 },
                        {"@dateinsert", dataInsertBanco}
                    });

                    int idInserido = 0;

                    await Task.Delay(500);

                    var selectID = Program.SQL.SelectList("SELECT * FROM Processos WHERE dateinsert = @dateinsert", "Processos", "Id",
                    new Dictionary<string, object>()
                    {
                        {"@dateinsert", dataInsertBanco }
                    });

                    idInserido = (int)selectID.First();

                    PesoProcessForms proc = new PesoProcessForms(idUsuario, nomeUsuario, idMateria, qtMinima, txt_Descricao.Text, idInserido);

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

        private void cb_MateriaPrima_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = true;
            e.SuppressKeyPress = true;
        }

        private void cb_MateriaPrima_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cb_MateriaPrima.SelectedItem == null)
            {
                return;
            }

            var listaMateriaPrima = Program.SQL.SelectList("SELECT * FROM MateriaPrima WHERE Codigo = @Codigo", "MateriaPrima", null,
            new Dictionary<string, object>()
            {
                {"@Codigo", CodSubs }
            });

            MateriaPrimaClass materia = (MateriaPrimaClass)cb_MateriaPrima.SelectedItem;

            //foreach (MateriaPrimaClass materia in listaMateriaPrima)
            //{

            lbl_QtMateriaPrima.Text = "Quantidade mínima para referência: " + materia.Quantidade_minima + " un.";

            //}

            lbl_QtMateriaPrima.Visible = true;
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}

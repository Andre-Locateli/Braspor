﻿using Main.Model;
using Main.View.MainFolder;
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

        public EscolhaPesagemForms(int id_Usuario, string nome_Usuario)
        {
            InitializeComponent();
            idUsuario = id_Usuario;
            nomeUsuario = nome_Usuario;
            this.Select();
        }

        private void EscolhaPesagemForms_Load(object sender, EventArgs e)
        {
            var listaMateriaPrima = Program.SQL.SelectList("SELECT * FROM MateriaPrima", "MateriaPrima", null,
            new Dictionary<string, object>());

            foreach (MateriaPrimaClass materia in listaMateriaPrima)
            {
                CodDesc = materia.Codigo + " - " + materia.Descricao;
                codLength = materia.Codigo.Length;
                cb_MateriaPrima.Items.Add(CodDesc);
            }

            cb_MateriaPrima.SelectedIndex = 0;
        }

        private void X_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_Confirmar_Click(object sender, EventArgs e)
        {
            if (cb_MateriaPrima.Text == "")
            {
                MessageBox.Show("Selecione a Matéria-prima para pesagem!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                CodSubs = cb_MateriaPrima.Text.Substring(0, codLength);

                int idMateria = 0;
                int qtMinima = 0;

                var listaMateriaPrima = Program.SQL.SelectList("SELECT * FROM MateriaPrima WHERE Codigo = @Codigo", "MateriaPrima", null,
                new Dictionary<string, object>()
                {
                    {"@Codigo", CodSubs }
                });

                foreach (MateriaPrimaClass materia in listaMateriaPrima)
                {
                    idMateria = materia.Id;
                    qtMinima = materia.Quantidade_minima;
                }

                ProcessForms proc = new ProcessForms(idUsuario, idMateria, qtMinima, txt_Descricao.Text);

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

        private void cb_MateriaPrima_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = true;
            e.SuppressKeyPress = true;
        }

        private void cb_MateriaPrima_SelectedIndexChanged(object sender, EventArgs e)
        {
            CodSubs = cb_MateriaPrima.Text.Substring(0, codLength);

            var listaMateriaPrima = Program.SQL.SelectList("SELECT * FROM MateriaPrima WHERE Codigo = @Codigo", "MateriaPrima", null,
            new Dictionary<string, object>()
            {
                {"@Codigo", CodSubs }
            });

            foreach (MateriaPrimaClass materia in listaMateriaPrima)
            {
                lbl_QtMateriaPrima.Text = "Quantidade mínima para referência: " + materia.Quantidade_minima.ToString() + " un.";
            }

            lbl_QtMateriaPrima.Visible = true;
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}

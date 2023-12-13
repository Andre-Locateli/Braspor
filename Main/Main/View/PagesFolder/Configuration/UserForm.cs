using Main.Helper;
using Main.Model;
using Main.Properties;
using Main.Service;
using Main.View.CustomLayout;
using Main.View.PopupFolder;
using Microsoft.CSharp;
using Microsoft.SqlServer.Server;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media.TextFormatting;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Main.View.PagesFolder.Configuration
{
    public partial class UserForm : Form
    {
        private UsuarioClass _user_selecionado;
        private PermissaoClass _permissao_selecionada;
        public UserForm()
        {
            InitializeComponent();
            LoadUserDB();

            lbl_time.Text = $"{DateTime.Now.Day.ToString("D2")}/{DateTime.Now.Month.ToString("D2")}/{DateTime.Now.Year}";
            lblUsuario.Text = Program._usuarioLogado.Nome;
            lblAcesso.Text = Program._usuarioLogado.Acesso;

            ChangeButtonStyle(btnUser, Resources.userEnable, true, Color.FromArgb(0, 93, 131), Color.White);
            ChangeButtonStyle(btnPermissao, Resources.lockDisable, true, Color.White, Color.FromArgb(178, 178, 178));

            btnSalvarUser.Enabled = Program._permissaoUsuario.Usuario_add;
            btnEditarUser.Enabled = Program._permissaoUsuario.Usuario_edit;
            btnSalvar.Enabled = Program._permissaoUsuario.Usuario_add;
        }

        public void LoadUserDB()
        {
            try
            {
                dgv_users.DataSource = Program.SQL.SelectDataGrid("SELECT Id, Nome AS [Nome do Usuário], Login, senha AS [Senha], acesso AS [Acesso] FROM Usuario", "Usuario");
                dgv_users.Columns[2].Visible = false;
            }
            catch (Exception)
            {
            }
        }

        private void btnUser_Click(object sender, EventArgs e)
        {
            try
            {
                if (btnUser.BackColor == Color.White)
                {
                    ChangeButtonStyle((System.Windows.Forms.Button)sender, Resources.userEnable, true, Color.FromArgb(0, 93, 131), Color.White);
                    ChangeButtonStyle(btnPermissao, Resources.lockDisable, true, Color.White, Color.FromArgb(178, 178, 178));
                }
                else
                {
                    ChangeButtonStyle((System.Windows.Forms.Button)sender, Resources.userDisable, true, Color.White, Color.FromArgb(178, 178, 178));
                }
                pn_user.Visible = true;
                pn_permission.Visible = false;
            }
            catch (Exception)
            {
            }
        }

        private void btnPermissao_Click(object sender, EventArgs e)
        {
            try
            {
                if (btnPermissao.BackColor == Color.White)
                {
                    ChangeButtonStyle((System.Windows.Forms.Button)sender, Resources.lockEnable, true, Color.FromArgb(0, 93, 131), Color.White);
                    ChangeButtonStyle(btnUser, Resources.userDisable, true, Color.White, Color.FromArgb(178, 178, 178));
                }
                else
                {
                    ChangeButtonStyle((System.Windows.Forms.Button)sender, Resources.lockDisable, true, Color.White, Color.FromArgb(178, 178, 178));
                }

                pn_user.Visible = false;
                pn_permission.Visible = true;
            }
            catch (Exception)
            {
            }
        }


        private void ChangeButtonStyle(System.Windows.Forms.Button btn, Image img, bool flag_status, Color clr, Color txtColor)
        {
            try
            {
                btn.BackColor = clr;
                btn.Image = img;
                btn.ForeColor = txtColor;
            }
            catch (Exception)
            {
            }
        }



        private void btnSalvarUser_Click(object sender, EventArgs e)
        {
            try
            {
                resetLayout();
                if (string.IsNullOrWhiteSpace(txtNomeUser.Text))
                {
                    lblNome.Text += "*";
                    lblNome.ForeColor = Color.Red;
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtLoginUser.Text))
                {
                    lblLogin.Text += "*";
                    lblLogin.ForeColor = Color.Red;
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtSenhaUser.Text))
                {
                    lblSenha.Text += "*";
                    lblSenha.ForeColor = Color.Red;
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtAcessoUser.Text))
                {
                    lblAcesso_.Text += "*";
                    lblAcesso_.ForeColor = Color.Red;
                    return;
                }

                if (Program.SQL.CRUDCommand("INSERT INTO Usuario (Nome, login, senha, acesso, dateinsert) VALUES (@Nome, @login, @senha, @acesso, @dateinsert)", "Usuario", new Dictionary<string, object>()
                { {"@Nome", txtNomeUser.Text }, {"@login", txtLoginUser.Text }, {"@senha", SecurityContext.ReturnHashMD5(txtSenhaUser.Text) }, {"@acesso", txtAcessoUser.Text }, {"@dateinsert", DateTime.Now}}))
                {
                    resetLayout();
                    LoadUserDB();
                    txtNomeUser.Text = "";
                    txtLoginUser.Text = "";
                    txtSenhaUser.Text = "";
                    txtAcessoUser.Text = "";
                }

            }
            catch (Exception)
            {
            }
        }

        private void resetLayout()
        {
            try
            {
                lblNome.ForeColor = Color.Black;
                lblNome.Text = "Nome";
                lblLogin.ForeColor = Color.Black;
                lblLogin.Text = "Login";
                lblSenha.ForeColor = Color.Black;
                lblSenha.Text = "Senha";
                lblAcesso.ForeColor = Color.Black;
                lblAcesso.Text = "Acesso";
            }
            catch (Exception)
            {
            }
        }

        private void resetCheckbox() 
        {
            try
            {
                ch_view_pesagem.Checked = false;
                ch_add_pesagem.Checked = false;
                ch_edit_pesagem.Checked = false;
                ch_remove_pesagem.Checked = false;
                ch_search_pesagem.Checked = false;

                ch_view_relatorio.Checked = false;
                ch_add_relatorio.Checked = false;
                ch_edit_relatorio.Checked = false;
                ch_remove_relatorio.Checked = false;
                ch_search_relatorio.Checked = false;

                ch_view_rede.Checked = false;
                ch_add_rede.Checked = false;
                ch_edit_rede.Checked = false;
                ch_remove_rede.Checked = false;
                ch_search_rede.Checked = false;

                ch_view_sistema.Checked = false;
                ch_add_sistema.Checked = false;
                ch_edit_sistema.Checked = false;
                ch_remove_sistema.Checked = false;
                ch_search_sistema.Checked = false;

                ch_view_usuarios.Checked = false;
                ch_add_usuarios.Checked = false;
                ch_edit_usuarios.Checked = false;
                ch_remove_usuarios.Checked = false;
                ch_search_consulta.Checked = false;

                ch_view_receita.Checked = false;
                ch_add_receita.Checked = false;
                ch_edit_receita.Checked = false;
                ch_remove_receita.Checked = false;
                ch_search_receita.Checked = false;

                ch_view_Tiporeceita.Checked = false;
                ch_add_TipoReceita.Checked = false;
                ch_edit_Tiporeceita.Checked = false;
                ch_remove_Tiporeceita.Checked = false;
                ch_search_Tiporeceita.Checked = false;

                ch_view_Recipiente.Checked = false;
                ch_add_Recipiente.Checked = false;
                ch_edit_Recipiente.Checked = false;
                ch_remove_recipiente.Checked = false;
                ch_search_Recipiente.Checked = false;

                ch_view_Bandeja.Checked = false;
                ch_add_Bandeja.Checked = false;
                ch_edit_Bandeja.Checked = false;
                ch_remove_bandeja.Checked = false;
                ch_search_bandeja.Checked = false;

                ch_view_Bandeja.Checked = false;
                ch_add_Bandeja.Checked = false;
                ch_edit_Bandeja.Checked = false;
                ch_remove_bandeja.Checked = false;
                ch_search_bandeja.Checked = false;

                ch_view_produto.Checked = false;
                ch_add_produto.Checked = false;
                ch_edit_produto.Checked = false;
                ch_remove_produto.Checked = false;
                ch_search_produto.Checked = false;

            }
            catch (Exception)
            {
            }
        }

        private void dgv_users_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 0) 
                {
                    UsuarioClass user = (UsuarioClass)Program.SQL.SelectObject("SELECT * FROM Usuario WHERE Id = @Id", "Usuario", new Dictionary<string, object>() { { "@Id", dgv_users.CurrentRow.Cells["Id"].Value } });

                    if (user != null) 
                    {
                        PermissaoClass permissao = (PermissaoClass)Program.SQL.SelectObject("SELECT * FROM Acessos WHERE id_usuario = @id_usuario", "Acessos",
                            new Dictionary<string, object>()
                            {
                                {"@id_usuario", user.Id}
                            });

                        if (permissao != null) 
                        {
                            ch_view_pesagem.Checked = permissao.Pesagem_View;
                            ch_add_pesagem.Checked = permissao.Pesagem_add;
                            ch_edit_pesagem.Checked = permissao.Pesagem_edit;
                            ch_remove_pesagem.Checked = permissao.Pesagem_remove;
                            ch_search_pesagem.Checked = permissao.Pesagem_search;

                            ch_view_relatorio.Checked = permissao.Relatorio_View;
                            ch_add_relatorio.Checked = permissao.Relatorio_add;
                            ch_edit_relatorio.Checked = permissao.Relatorio_edit;
                            ch_remove_relatorio.Checked = permissao.Relatorio_remove;
                            ch_search_relatorio.Checked = permissao.Relatorio_search;

                            ch_view_rede.Checked = permissao.Rede_View;
                            ch_add_rede.Checked = permissao.Rede_add;
                            ch_edit_rede.Checked = permissao.Rede_edit;
                            ch_remove_rede.Checked = permissao.Rede_remove;
                            ch_search_rede.Checked = permissao.Rede_search;

                            ch_view_sistema.Checked = permissao.Sistema_View;
                            ch_add_sistema.Checked = permissao.Sistema_add;
                            ch_edit_sistema.Checked = permissao.Sistema_edit;
                            ch_remove_sistema.Checked = permissao.Sistema_remove;
                            ch_search_sistema.Checked = permissao.Sistema_search;

                            ch_view_usuarios.Checked = permissao.Usuario_View;
                            ch_add_usuarios.Checked = permissao.Usuario_add;
                            ch_edit_usuarios.Checked = permissao.Usuario_edit;
                            ch_remove_usuarios.Checked = permissao.Usuario_remove;
                            ch_search_consulta.Checked = permissao.Usuario_search;

                            ch_view_receita.Checked = permissao.receita_view;
                            ch_add_receita.Checked = permissao.receita_add;
                            ch_edit_receita.Checked = permissao.receita_edit;
                            ch_remove_receita.Checked = permissao.receita_remove;
                            ch_search_receita.Checked = permissao.receita_search;

                            ch_view_Tiporeceita.Checked = permissao.tipoReceita_view;
                            ch_add_TipoReceita.Checked = permissao.tipoReceita_add;
                            ch_edit_Tiporeceita.Checked = permissao.tipoReceita_edit;
                            ch_remove_Tiporeceita.Checked = permissao.tipoReceita_remove;
                            ch_search_Tiporeceita.Checked = permissao.tipoReceita_search;

                            ch_view_Recipiente.Checked = permissao.Recipiente_view;
                            ch_add_Recipiente.Checked = permissao.Recipiente_add;
                            ch_edit_Recipiente.Checked = permissao.Recipiente_edit;
                            ch_remove_recipiente.Checked = permissao.Recipiente_remove;
                            ch_search_Recipiente.Checked = permissao.Recipiente_search;

                            ch_view_Bandeja.Checked = permissao.Bandeja_view;
                            ch_add_Bandeja.Checked = permissao.Bandeja_add;
                            ch_edit_Bandeja.Checked = permissao.Bandeja_edit;
                            ch_remove_bandeja.Checked = permissao.Bandeja_remove;
                            ch_search_bandeja.Checked = permissao.Bandeja_search;

                            ch_view_produto.Checked = permissao.Produto_view;
                            ch_add_produto.Checked = permissao.Produto_add;
                            ch_edit_produto.Checked = permissao.Produto_edit;
                            ch_remove_produto.Checked = permissao.Produto_remove;
                            ch_search_produto.Checked = permissao.Produto_search;

                        }
                        
                        btnPermissao.Enabled = true;
                        btnPermissao.Enabled = true;
                        _user_selecionado = user;
                        txtNomeUser.Text = user.Nome;
                        txtLoginUser.Text = user.Login;
                        txtSenhaUser.Text = user.Senha;
                        txtAcessoUser.Text = user.Acesso;
                        btnSalvarUser.Visible = false;
                        btnEditarUser.Visible = true;
                        btnNovoUser.Visible = true;
                    }
                }

                if (e.ColumnIndex == 1) 
                {
                    YesOrNo question = new YesOrNo("Você tem certeza que deseja remover o usuário selecionado ?");
                    question.ShowDialog();

                    if (question.RESPOSTA && Program._permissaoUsuario.Usuario_remove)
                    {
                        if (Program.SQL.CRUDCommand("DELETE FROM Usuario WHERE Id = @Id", "Usuario", new Dictionary<string, object>() { { "@Id", dgv_users.CurrentRow.Cells["Id"].Value } }))
                        {
                            resetLayout();
                            LoadUserDB();
                            txtNomeUser.Text = "";
                            txtLoginUser.Text = "";
                            txtSenhaUser.Text = "";
                            txtAcessoUser.Text = "";
                        }
                    }
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
                btnSalvarUser.Visible = true;
                btnEditarUser.Visible = false;
                btnNovoUser.Visible = false;
                _user_selecionado = new UsuarioClass();
                _permissao_selecionada = new PermissaoClass();

                resetLayout();

                txtNomeUser.Text = "";
                txtLoginUser.Text = "";
                txtSenhaUser.Text = "";
                txtAcessoUser.Text = "";

                btnPermissao.Enabled = false;
            }
            catch (Exception)
            {
            }
        }

        private void btnEditarUser_Click(object sender, EventArgs e)
        {
            try
            {
                resetLayout();
                if (string.IsNullOrWhiteSpace(txtNomeUser.Text))
                {
                    lblNome.Text += "*";
                    lblNome.ForeColor = Color.Red;
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtLoginUser.Text))
                {
                    lblLogin.Text += "*";
                    lblLogin.ForeColor = Color.Red;
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtSenhaUser.Text))
                {
                    lblSenha.Text += "*";
                    lblSenha.ForeColor = Color.Red;
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtAcessoUser.Text))
                {
                    lblAcesso_.Text += "*";
                    lblAcesso_.ForeColor = Color.Red;
                    return;
                }

                if (Program.SQL.CRUDCommand("UPDATE Usuario SET Nome = @Nome, login = @login, senha = @senha, acesso = @acesso, dateupdate = @dateupdate WHERE Id = @Id", "Usuario", new Dictionary<string, object>()
                { {"@Nome", txtNomeUser.Text }, {"@login", txtLoginUser.Text }, {"@senha", SecurityContext.ReturnHashMD5(txtSenhaUser.Text) }, {"@acesso", txtAcessoUser.Text }, {"@dateupdate", DateTime.Now}, {"@Id", dgv_users.CurrentRow.Cells["Id"].Value } }))
                {
                    resetLayout();
                    LoadUserDB();
                    txtNomeUser.Text = "";
                    txtLoginUser.Text = "";
                    txtSenhaUser.Text = "";
                    txtAcessoUser.Text = "";
                }
            }
            catch (Exception)
            {
            }
        }
        
        private void txtLoginUser_Leave(object sender, EventArgs e)
        {
            try
            {
                UsuarioClass user = (UsuarioClass)Program.SQL.SelectObject("SELECT * FROM Usuario WHERE login = @login", "Usuario", new Dictionary<string, object>() { { "@login", txtLoginUser.Text } });

                if (user != null) 
                {
                    txtLoginUser.Text = "";
                }    
            }
            catch (Exception)
            {
            }

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            StyleSheet.panel_Paint(sender, e);
        }

        private void txt_search_TextChanged_1(object sender, EventArgs e)
        {
            try
            {
                var ds = new DataSet();
                DataTable dt;
                dt = (DataTable)dgv_users.DataSource;
                dt.DefaultView.RowFilter = string.Format("[{0}] LIKE '%" + txt_search.Text + "%'", "Nome do Usuário");
            }
            catch (Exception)
            {
            }
        }

        private void pn_permission_VisibleChanged(object sender, EventArgs e)
        {

            try
            {
                resetCheckbox();
                if (pn_permission.Visible)
                {
                    PermissaoClass permissao = (PermissaoClass)Program.SQL.SelectObject("SELECT * FROM Acessos WHERE id_usuario = @id_usuario", "Acessos",
                        new Dictionary<string, object>()
                        {
                            {"@id_usuario", _user_selecionado.Id}
                        });

                    if (permissao != null)
                    {
                        _permissao_selecionada = permissao;
                        txt_permission.Text = permissao.Acesso;
                        ch_view_pesagem.Checked = permissao.Pesagem_View;
                        ch_add_pesagem.Checked = permissao.Pesagem_add;
                        ch_edit_pesagem.Checked = permissao.Pesagem_edit;
                        ch_remove_pesagem.Checked = permissao.Pesagem_remove;
                        ch_search_pesagem.Checked = permissao.Pesagem_search;

                        ch_view_relatorio.Checked = permissao.Relatorio_View;
                        ch_add_relatorio.Checked = permissao.Relatorio_add;
                        ch_edit_relatorio.Checked = permissao.Relatorio_edit;
                        ch_remove_relatorio.Checked = permissao.Relatorio_remove;
                        ch_search_relatorio.Checked = permissao.Relatorio_search;

                        ch_view_rede.Checked = permissao.Rede_View;
                        ch_add_rede.Checked = permissao.Rede_add;
                        ch_edit_rede.Checked = permissao.Rede_edit;
                        ch_remove_rede.Checked = permissao.Rede_remove;
                        ch_search_rede.Checked = permissao.Rede_search;

                        ch_view_sistema.Checked = permissao.Sistema_View;
                        ch_add_sistema.Checked = permissao.Sistema_add;
                        ch_edit_sistema.Checked = permissao.Sistema_edit;
                        ch_remove_sistema.Checked = permissao.Sistema_remove;
                        ch_search_sistema.Checked = permissao.Sistema_search;

                        ch_view_usuarios.Checked = permissao.Usuario_View;
                        ch_add_usuarios.Checked = permissao.Usuario_add;
                        ch_edit_usuarios.Checked = permissao.Usuario_edit;
                        ch_remove_usuarios.Checked = permissao.Usuario_remove;
                        ch_search_consulta.Checked = permissao.Usuario_search;

                        ch_view_receita.Checked = permissao.receita_view;
                        ch_add_receita.Checked = permissao.receita_add;
                        ch_edit_receita.Checked = permissao.receita_edit;
                        ch_remove_receita.Checked = permissao.receita_remove;
                        ch_search_receita.Checked = permissao.receita_search;

                        ch_view_Tiporeceita.Checked = permissao.tipoReceita_view;
                        ch_add_TipoReceita.Checked = permissao.tipoReceita_add;
                        ch_edit_Tiporeceita.Checked = permissao.tipoReceita_edit;
                        ch_remove_Tiporeceita.Checked = permissao.tipoReceita_remove;
                        ch_search_Tiporeceita.Checked = permissao.tipoReceita_search;

                        ch_view_Recipiente.Checked = permissao.Recipiente_view;
                        ch_add_Recipiente.Checked = permissao.Recipiente_add;
                        ch_edit_Recipiente.Checked = permissao.Recipiente_edit;
                        ch_remove_recipiente.Checked = permissao.Recipiente_remove;
                        ch_search_Recipiente.Checked = permissao.Recipiente_search;

                        ch_view_Bandeja.Checked = permissao.Bandeja_view;
                        ch_add_Bandeja.Checked = permissao.Bandeja_add;
                        ch_edit_Bandeja.Checked = permissao.Bandeja_edit;
                        ch_remove_bandeja.Checked = permissao.Bandeja_remove;
                        ch_search_bandeja.Checked = permissao.Bandeja_search;

                        ch_view_produto.Checked = permissao.Produto_view;
                        ch_add_produto.Checked = permissao.Produto_add;
                        ch_edit_produto.Checked = permissao.Produto_edit;
                        ch_remove_produto.Checked = permissao.Produto_remove;
                        ch_search_produto.Checked = permissao.Produto_search;
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                if (_permissao_selecionada == null)
                {
                    var permission = Program.SQL.InsertAndSelectLasRow("INSERT INTO Acessos " +
                                "(Acesso, " +
                                "pesagem_view, " +
                                "pesagem_add, " +
                                "pesagem_edit, " +
                                "pesagem_remove, " +
                                "pesagem_search, " +
                                "relatorio_view, " +
                                "relatorio_add, " +
                                "relatorio_edit, " +
                                "relatorio_remove, " +
                                "relatorio_search, " +
                                "rede_view, " +
                                "rede_add, " +
                                "rede_edit, " +
                                "rede_remove, " +
                                "rede_search, " +
                                "sistema_view, " +
                                "sistema_add, " +
                                "sistema_edit, " +
                                "sistema_remove, " +
                                "sistema_search, " +
                                "usuario_view, " +
                                "usuario_add, " +
                                "usuario_edit, " +
                                "usuario_remove, " +
                                "usuario_search, " +
                                "receita_view, " +
                                "receita_add, " +
                                "receita_edit, " +
                                "receita_remove, " +
                                "receita_search, " +
                                "tipoReceita_view, " +
                                "tipoReceita_add, " +
                                "tipoReceita_edit, " +
                                "tipoReceita_remove, " +
                                "tipoReceita_search, " +
                                "Recipiente_view, " +
                                "Recipiente_add, " +
                                "Recipiente_edit, " +
                                "Recipiente_remove, " +
                                "Recipiente_search, " +
                                "Bandeja_view, " +
                                "Bandeja_add, " +
                                "Bandeja_edit, " +
                                "Bandeja_remove, " +
                                "Bandeja_search, " +
                                "Produto_view, " +
                                "Produto_add, " +
                                "Produto_edit, " +
                                "Produto_remove, " +
                                "Produto_search, " +
                                "id_usuario" +
                                ") " +
                                "VALUES " +
                                "(@Acesso," +
                                "@pesagem_view, " +
                                "@pesagem_add, " +
                                "@pesagem_edit, " +
                                "@pesagem_remove, " +
                                "@pesagem_search, " +
                                "@relatorio_view, " +
                                "@relatorio_add, " +
                                "@relatorio_edit, " +
                                "@relatorio_remove, " +
                                "@relatorio_search, " +
                                "@rede_view, " +
                                "@rede_add, " +
                                "@rede_edit, " +
                                "@rede_remove, " +
                                "@rede_search, " +
                                "@sistema_view, " +
                                "@sistema_add, " +
                                "@sistema_edit, " +
                                "@sistema_remove, " +
                                "@sistema_search, " +
                                "@usuario_view, " +
                                "@usuario_add, " +
                                "@usuario_edit, " +
                                "@usuario_remove, " +
                                "@usuario_search, " +
                                "@receita_view, " +
                                "@receita_add, " +
                                "@receita_edit, " +
                                "@receita_remove, " +
                                "@receita_search, " +
                                "@tipoReceita_view, " +
                                "@tipoReceita_add, " +
                                "@tipoReceita_edit, " +
                                "@tipoReceita_remove, " +
                                "@tipoReceita_search, " +
                                "@Recipiente_view, " +
                                "@Recipiente_add, " +
                                "@Recipiente_edit, " +
                                "@Recipiente_remove, " +
                                "@Recipiente_search, " +
                                "@Bandeja_view, " +
                                "@Bandeja_add, " +
                                "@Bandeja_edit, " +
                                "@Bandeja_remove, " +
                                "@Bandeja_search, " +
                                "@Produto_view, " +
                                "@Produto_add, " +
                                "@Produto_edit, " +
                                "@Produto_remove, " +
                                "@Produto_search, " +
                                " @id_usuario);SELECT SCOPE_IDENTITY() AS Last_Id;", "Usuario", new Dictionary<string, object>()
                                {
                                    {"@Acesso", $"Permissao {_user_selecionado.Nome}"},

                                    {"@pesagem_view", ch_view_pesagem.Checked},
                                    {"@pesagem_add", ch_add_pesagem.Checked},
                                    {"@pesagem_edit", ch_edit_pesagem.Checked},
                                    {"@pesagem_remove", ch_remove_pesagem.Checked},
                                    {"@pesagem_search", ch_search_pesagem.Checked},
                                    {"@relatorio_view", ch_view_relatorio.Checked},
                                    {"@relatorio_add", ch_add_relatorio.Checked},
                                    {"@relatorio_edit", ch_edit_relatorio.Checked},
                                    {"@relatorio_remove", ch_remove_relatorio.Checked},
                                    {"@relatorio_search", ch_search_relatorio.Checked},
                                    {"@rede_view", ch_view_rede.Checked},
                                    {"@rede_add", ch_add_rede.Checked},
                                    {"@rede_edit", ch_edit_rede.Checked},
                                    {"@rede_remove", ch_remove_rede.Checked},
                                    {"@rede_search", ch_search_rede.Checked},
                                    {"@sistema_view", ch_view_sistema.Checked},
                                    {"@sistema_add", ch_add_sistema.Checked},
                                    {"@sistema_edit", ch_edit_sistema.Checked},
                                    {"@sistema_remove", ch_remove_sistema.Checked},
                                    {"@sistema_search", ch_search_sistema.Checked},
                                    {"@usuario_view", ch_view_usuarios.Checked},
                                    {"@usuario_add", ch_add_usuarios.Checked},
                                    {"@usuario_edit", ch_edit_usuarios.Checked},
                                    {"@usuario_remove", ch_remove_usuarios.Checked},
                                    {"@usuario_search", ch_search_consulta.Checked},
                                    {"@receita_view", ch_view_receita.Checked},
                                    {"@receita_add", ch_add_receita.Checked},

                                    {"@receita_edit", ch_edit_receita.Checked},
                                    {"@receita_remove", ch_remove_receita.Checked},
                                    {"@receita_search", ch_search_receita.Checked},
                                    {"@tipoReceita_view", ch_view_Tiporeceita.Checked},
                                    {"@tipoReceita_add", ch_add_TipoReceita.Checked},
                                    {"@tipoReceita_edit", ch_edit_Tiporeceita.Checked},
                                    {"@tipoReceita_remove", ch_remove_Tiporeceita.Checked},
                                    {"@tipoReceita_search", ch_search_Tiporeceita.Checked},

                                    {"@Recipiente_view", ch_view_Recipiente.Checked},
                                    {"@Recipiente_add", ch_add_Recipiente.Checked},
                                    {"@Recipiente_edit", ch_edit_Recipiente.Checked},
                                    {"@Recipiente_remove", ch_remove_recipiente.Checked},

                                    {"@Recipiente_search", ch_search_Recipiente.Checked},

                                    {"@Bandeja_view", ch_view_Bandeja.Checked},
                                    {"@Bandeja_add", ch_add_Bandeja.Checked},
                                    {"@Bandeja_edit", ch_edit_Bandeja.Checked},
                                    {"@Bandeja_remove", ch_remove_bandeja.Checked},
                                    {"@Bandeja_search", ch_search_bandeja.Checked},
                                    {"@Produto_view", ch_view_produto.Checked},
                                    {"@Produto_add", ch_add_produto.Checked},
                                    {"@Produto_edit", ch_edit_produto.Checked},
                                    {"@Produto_remove", ch_remove_produto.Checked},
                                    {"@Produto_search", ch_search_produto.Checked},

                                    {"@id_usuario", _user_selecionado.Id},
                                });

                    if (permission > 0)
                    {
                        txt_permission.Text = $"Permissao_{_user_selecionado.Nome}";

                        PermissaoClass permissao = (PermissaoClass)Program.SQL.SelectObject("SELECT * FROM Acessos WHERE id_usuario = @id_usuario", "Acessos",
                            new Dictionary<string, object>()
                            {
                                {"@id_usuario", permission}
                            });
                        _permissao_selecionada = permissao;
                    }
                }
                else 
                {
                    //Comando de update no sql

                    var permission = Program.SQL.CRUDCommand("UPDATE Acessos SET " +
                        "Acesso = @Acesso, " +
                        "pesagem_view = @pesagem_view, " +
                        "pesagem_add = @pesagem_add, " +
                        "pesagem_edit = @pesagem_edit, " +
                        "pesagem_remove = @pesagem_remove, " +
                        "pesagem_search = @pesagem_search, " +
                        "relatorio_view = @relatorio_view, " +
                        "relatorio_add = @relatorio_add, " +
                        "relatorio_edit = @relatorio_edit, " +
                        "relatorio_remove = @relatorio_remove, " +
                        "relatorio_search = @relatorio_search, " +
                        "rede_view  = @rede_view, " +
                        "rede_add = @rede_add, " +
                        "rede_edit = @rede_edit, " +
                        "rede_remove = @rede_remove, " +
                        "rede_search = @rede_search, " +
                        "sistema_view = @sistema_view, " +
                        "sistema_add = @sistema_add, " +
                        "sistema_edit = @sistema_edit, " +
                        "sistema_remove = @sistema_remove, " +
                        "sistema_search = @sistema_search, " +
                        "usuario_view = @usuario_view, " +
                        "usuario_add = @usuario_add, " +
                        "usuario_edit = @usuario_edit, " +
                        "usuario_remove = @usuario_remove, " +
                        "usuario_search = @usuario_search, " +
                        "receita_view = @receita_view, " +
                        "receita_add = @receita_add, " +
                        "receita_edit = @receita_edit, " +
                        "receita_remove = @receita_remove, " +
                        "receita_search = @receita_search, " +
                        "tipoReceita_view = @tipoReceita_view, " +
                        "tipoReceita_add = @tipoReceita_add, " +
                        "tipoReceita_edit = @tipoReceita_edit, " +
                        "tipoReceita_remove = @tipoReceita_remove, " +
                        "tipoReceita_search = @tipoReceita_search, " +
                        "Recipiente_view = @Recipiente_view, " +
                        "Recipiente_add = @Recipiente_add, " +
                        "Recipiente_edit = @Recipiente_edit, " +
                        "Recipiente_remove = @Recipiente_remove, " +
                        "Recipiente_search = @Recipiente_search, " +
                        "Bandeja_view = @Bandeja_view, " +
                        "Bandeja_add = @Bandeja_add, " +
                        "Bandeja_edit = @Bandeja_edit, " +
                        "Bandeja_remove = @Bandeja_remove, " +
                        "Bandeja_search = @Bandeja_search, " +
                        "Produto_view = @Produto_view, " +
                        "Produto_add  = @Produto_add, " +
                        "Produto_edit = @Produto_edit, " +
                        "Produto_remove = @Produto_remove, " +
                        "Produto_search = @Produto_search " +
                        "WHERE id = @id", "Usuario", new Dictionary<string, object>()
                        {
                            {"@Acesso", $"Permissao {_user_selecionado.Nome}"},
                            {"@pesagem_view", ch_view_pesagem.Checked},
                            {"@pesagem_add", ch_add_pesagem.Checked},
                            {"@pesagem_edit", ch_edit_pesagem.Checked},
                            {"@pesagem_remove", ch_remove_pesagem.Checked},
                            {"@pesagem_search", ch_search_pesagem.Checked},
                            {"@relatorio_view", ch_view_relatorio.Checked},
                            {"@relatorio_add", ch_add_relatorio.Checked},
                            {"@relatorio_edit", ch_edit_relatorio.Checked},
                            {"@relatorio_remove", ch_remove_relatorio.Checked},
                            {"@relatorio_search", ch_search_relatorio.Checked},
                            {"@rede_view", ch_view_rede.Checked},
                            {"@rede_add", ch_add_rede.Checked},
                            {"@rede_edit", ch_edit_rede.Checked},
                            {"@rede_remove", ch_remove_rede.Checked},
                            {"@rede_search", ch_search_rede.Checked},
                            {"@sistema_view", ch_view_sistema.Checked},
                            {"@sistema_add", ch_add_sistema.Checked},
                            {"@sistema_edit", ch_edit_sistema.Checked},
                            {"@sistema_remove", ch_remove_sistema.Checked},
                            {"@sistema_search", ch_search_sistema.Checked},
                            {"@usuario_view", ch_view_usuarios.Checked},
                            {"@usuario_add", ch_add_usuarios.Checked},
                            {"@usuario_edit", ch_edit_usuarios.Checked},
                            {"@usuario_remove", ch_remove_usuarios.Checked},
                            {"@usuario_search", ch_search_consulta.Checked},
                            {"@receita_view", ch_view_receita.Checked},
                            {"@receita_add", ch_add_receita.Checked},
                            {"@receita_edit", ch_edit_receita.Checked},
                            {"@receita_remove", ch_remove_receita.Checked},
                            {"@receita_search", ch_search_receita.Checked},
                            {"@tipoReceita_view", ch_view_Tiporeceita.Checked},
                            {"@tipoReceita_add", ch_add_TipoReceita.Checked},
                            {"@tipoReceita_edit", ch_edit_Tiporeceita.Checked},
                            {"@tipoReceita_remove", ch_remove_Tiporeceita.Checked},
                            {"@tipoReceita_search", ch_search_Tiporeceita.Checked},
                            {"@Recipiente_view", ch_view_Recipiente.Checked},
                            {"@Recipiente_add", ch_add_Recipiente.Checked},
                            {"@Recipiente_edit", ch_edit_Recipiente.Checked},
                            {"@Recipiente_remove", ch_remove_recipiente.Checked},
                            {"@Recipiente_search", ch_search_Recipiente.Checked},
                            {"@Bandeja_view", ch_view_Bandeja.Checked},
                            {"@Bandeja_add", ch_add_Bandeja.Checked},
                            {"@Bandeja_edit", ch_edit_Bandeja.Checked},
                            {"@Bandeja_remove", ch_remove_bandeja.Checked},
                            {"@Bandeja_search", ch_search_bandeja.Checked},
                            {"@Produto_view", ch_view_produto.Checked},
                            {"@Produto_add", ch_add_produto.Checked},
                            {"@Produto_edit", ch_edit_produto.Checked},
                            {"@Produto_remove", ch_remove_produto.Checked},
                            {"@Produto_search", ch_search_produto.Checked},
                            {"@id", _permissao_selecionada.Id},
                        });
                }
            }
            catch (Exception)
            {
            }
        }

    }
}

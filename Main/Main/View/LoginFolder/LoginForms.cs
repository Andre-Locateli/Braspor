using Main.Helper;
using Main.Model;
using Main.Service;
using Main.View.CommunicationFolder;
using Main.View.MainFolder;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media.Animation;
using System.Xml;

namespace Main.View.LoginFolder
{
    public partial class LoginForms : Form
    {

        private UIHelper uiHelper;

        public LoginForms()
        {
            InitializeComponent();
            uiHelper = new UIHelper(this, true);

            pictureBox2.Parent = pictureBox1;
            pictureBox3.Parent = pictureBox1;

            txt_version.Text = Program.software_version;

            btnAcessar.Focus();

            try
            {
                if (Program._checkSalvar)
                {
                    txt_user.Text = Program._ultimoLogin;
                    txt_password.Text = Program._ultimaSenha;
                    ch_ultimoAcesso.Checked = Program._checkSalvar;
                }

                if (txt_password.Text != "Senha")
                {
                    txt_password.UseSystemPasswordChar = true;
                }
            }
            catch (Exception)
            {
            }
        }

        private void pcb_close_Click(object sender, EventArgs e)
        {
            try
            {
                Application.Exit();
            }
            catch (Exception)
            {
            }
        }

        private void btnAcessar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txt_user.Text))
                {
                    uiHelper.ShowErrorLabel(lbl_error, "Informe o usuário de acesso", 5000);
                    return;
                }
                if (string.IsNullOrWhiteSpace(txt_password.Text))
                {
                    uiHelper.ShowErrorLabel(lbl_error, "Informe a senha de acesso", 5000);
                    return;
                }

                //tenho que passar o hash criptografado a senha
                UsuarioClass user = (UsuarioClass)Program.SQL.SelectObject("SELECT * FROM Usuario where login = @login and senha = @senha", "Usuario", new Dictionary<string, object>() { { "@login", txt_user.Text }, { "@senha", SecurityContext.ReturnHashMD5(txt_password.Text) } }, 0);

                if (user != null)
                {
                    Program._usuarioLogado = user;
                    //Salvar o ultimo Login

                    PermissaoClass permissao = (PermissaoClass)Program.SQL.SelectObject("SELECT * FROM Acessos WHERE id_usuario = @id_usuario", "Acessos",
                        new Dictionary<string, object>()
                        {
                            {"@id_usuario", user.Id}
                        });

                    if (permissao != null)
                    {

                        Program._permissaoUsuario = permissao;
                        SaveLoginInfo(ch_ultimoAcesso.Checked);

                        this.Hide();
                        SerialCommunicationService.InitWithAutoConnect();
                        MainForms main = new MainForms(user.Id, user.Nome);
                        main.Show();
                    }
                    else 
                    {
                        uiHelper.ShowErrorLabel(lbl_error, "Nenhuma permissão vinculada ao usuário.", 5000);
                    }
                }
                else
                {
                    uiHelper.ShowErrorLabel(lbl_error, "Usuário Inválido.", 5000);
                }
            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// Altera a flag que controla se o usuário está salvando o ultimo acesso
        /// e altera no aquivo xml os valores de login e senha
        /// </summary>
        /// <param name="flag"></param>
        private void SaveLoginInfo(bool flag)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                string caminhoCompleto = Path.Combine(Application.StartupPath, Program.CFG.CaminhoConfig);
                doc.Load(caminhoCompleto);
                XmlElement login = (XmlElement)doc.SelectSingleNode("//ultimoLogin");
                XmlElement senha = (XmlElement)doc.SelectSingleNode("//ultimoSenha");
                XmlElement check = (XmlElement)doc.SelectSingleNode("//checkLembrar");
                if (login != null && senha != null & check != null)
                {
                    if (flag)
                    {
                        login.InnerText = txt_user.Text;
                        senha.InnerText = txt_password.Text;
                        check.InnerText = Convert.ToInt32(ch_ultimoAcesso.Checked).ToString();
                    }
                    else
                    {
                        login.InnerText = "";
                        senha.InnerText = "";
                        check.InnerText = "0";
                    }
                }
                try
                {
                    doc.Save(caminhoCompleto);
                    //MessageBox.Show("Configuração de Estação salva com sucesso!", "VC Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro ao salvar dados do ultimo acesso. {ex}", "VC Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception)
            {
            }
        }

        private void LoginForms_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                uiHelper.MouseUp(sender,e);
            }
            catch (Exception)
            {
            }
        }

        private void LoginForms_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                uiHelper.MouseMove(sender, e);
            }
            catch (Exception)
            {
            }
        }

        private void LoginForms_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                uiHelper.MouseDown(sender, e);
            }
            catch (Exception)
            {
            }
        }


        private void txt_user_Enter(object sender, EventArgs e)
        {
            try
            {
                System.Windows.Forms.TextBox element = (System.Windows.Forms.TextBox)sender;

                if (element.Text == "Nome de usuário" || element.Text == "Senha")
                {
                    element.Text = "";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private void txt_user_Leave(object sender, EventArgs e)
        {
            try
            {
                System.Windows.Forms.TextBox element = (System.Windows.Forms.TextBox)sender;

                if (string.IsNullOrWhiteSpace(element.Text))
                {
                    if (element.Name == "txt_user") { element.Text = "Nome de usuário"; }
                    if (element.Name == "txt_password") { element.Text = "Senha"; }
                    txt_password.UseSystemPasswordChar = false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }


        private void txt_password_Enter(object sender, EventArgs e)
        {
            try
            {
                System.Windows.Forms.TextBox element = (System.Windows.Forms.TextBox)sender;
                txt_password.UseSystemPasswordChar = true;

                if (element.Text == "Nome de usuário" || element.Text == "Senha")
                {
                    element.Text = "";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private void txt_password_Leave(object sender, EventArgs e)
        {
            try
            {
                System.Windows.Forms.TextBox element = (System.Windows.Forms.TextBox)sender;

                if (string.IsNullOrWhiteSpace(element.Text))
                {
                    if (element.Name == "txt_user") { element.Text = "Nome de usuário"; }
                    if (element.Name == "txt_password") { element.Text = "Senha"; }
                    txt_password.UseSystemPasswordChar = false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}

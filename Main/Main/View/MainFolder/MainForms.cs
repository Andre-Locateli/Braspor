using Main.Helper;
using Main.Properties;
using Main.Service;
using Main.View.CadastroFolder;
using Main.View.PagesFolder;
using Main.View.PagesFolder.Configuration;
using Main.View.PagesFolder.PesagemFolder;
using Main.View.PopupFolder;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Dynamic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace Main.View.MainFolder
{
    public partial class MainForms : Form
    {
        private Size _FormSize;
        private Point _FormLocation;
        private Point _mouseOffset;
        private bool _isResizing;
        private bool _isFullScreen = false;

        bool sidebarExpand = true;
        bool layout = true;
        bool AnimationRunning = false;
        bool closeProgram = true;

        private MainInfoForms m_Form = new MainInfoForms();

        public MainForms()
        {
            InitializeComponent();
            lbl_time.Text = $"{DateTime.Now.Day.ToString("D2")}/{DateTime.Now.Month.ToString("D2")}/{DateTime.Now.Year} {DateTime.Now.Hour.ToString("D2")}:{DateTime.Now.Minute.ToString("D2")}";
            lblUsuario.Text = Program._usuarioLogado.Nome;
            lblAcesso.Text = Program._usuarioLogado.Acesso;

            btnPesagem.Enabled = Program._permissaoUsuario.Pesagem_View;
            button5.Enabled = Program._permissaoUsuario.Relatorio_View;

            btnRede.Enabled = Program._permissaoUsuario.Rede_View;
            btnSistema.Enabled = Program._permissaoUsuario.Sistema_View;
            btnUsuario.Enabled = Program._permissaoUsuario.Usuario_View;
            btnProduto.Enabled = Program._permissaoUsuario.Produto_view;

            m_Form.QuickButtonEventClick += M_Form_QuickButtonEventClick;
            //OpenPage(m_Form);

            bool modo_operacao = Convert.ToBoolean(Program.CFG._modoOperacao);

            if (modo_operacao)
            {
                PesagemList pList = new PesagemList();
                //pList.ItemEditadoTrigger += PList_ItemEditadoTrigger;
                OpenPage(pList);
            }
            else
            {
                OpenPage(m_Form);
            }

            UpdateStatusSerial();
        }

        private void M_Form_QuickButtonEventClick(object sender, EventArgs e)
        {
            try
            {
                MainInfoForms form = (MainInfoForms)sender;
                //form.QuickButtonEventClick += M_Form_QuickButtonEventClick;
                OpenPage(form.QuickButtonPress);
            }
            catch (Exception)
            {
            }
        }


        private void MainForms_Load(object sender, EventArgs e)
        {
            this.Width = Program.CFG._width;
            this.Height = Program.CFG._height;
            //_FormSize = new Size(Width, Height);
            this.Location = new Point(Program.CFG._x, Program.CFG._y);
            _FormLocation = Location;
            _isFullScreen = Program.CFG._full_screen;
        }
        private void AnimationMenu() 
        {
            try
            {
                Task.Run(async () =>
                {
                    Thread.CurrentThread.Name = "menuAnimation";
                    var thread = Thread.CurrentThread;

                    while (AnimationRunning)
                    {
                        sidebar.Invoke((MethodInvoker)delegate
                        {
                            AnimateSidebar();
                        });
                        await Task.Delay(1);
                    }
                });
            }
            catch (Exception)
            {
            }
        }

        bool b_grow = false;
        private void AnimateSidebar()
        {
            int pixelcycle = 10;
            if (sidebarExpand)
            {
                sidebar.Width -= pixelcycle;
                pcb_logo.Width -= pixelcycle;
                panel2.Width -= pixelcycle;
                //pnDownSize.Width += pixelcycle;
                //pn_container.Width += pixelcycle;
                //pnDownSize.Location = new Point(pnDownSize.Location.X - pixelcycle, pnDownSize.Location.Y);
                //pn_container.Location = new Point(pn_container.Location.X - pixelcycle, pn_container.Location.Y);
                pnSubMenu.Location = new Point(pnSubMenu.Location.X - pixelcycle, pnSubMenu.Location.Y);
            }
            else
            {
                sidebar.Width += pixelcycle;
                pcb_logo.Width += pixelcycle;
                panel2.Width += pixelcycle;
                //pnDownSize.Width -= pixelcycle;
                //pn_container.Width -= pixelcycle;
                //pnDownSize.Location = new Point(pnDownSize.Location.X + pixelcycle, pnDownSize.Location.Y);
                //pn_container.Location = new Point(pn_container.Location.X + pixelcycle, pn_container.Location.Y);
                pnSubMenu.Location = new Point(pnSubMenu.Location.X + pixelcycle, pnSubMenu.Location.Y);
                if(b_grow == false)
                {
                    pnDownSize.Width -= 160;
                    pn_container.Width -= 160;
                    pnDownSize.Location = new Point(pnDownSize.Location.X + 160, pnDownSize.Location.Y);
                    pn_container.Location = new Point(pn_container.Location.X + 160, pn_container.Location.Y);
                    b_grow = true;
                }
            }

            if (sidebar.Width == 50 || sidebar.Width == 210)
            {
                if(sidebar.Width == 50)
                {
                    MenuExpand.Location = new Point(12, 0);
                    MenuExpand.Image = Resources.menuIcon;
                    pnDownSize.Width += 160;
                    pn_container.Width += 160;
                    pnDownSize.Location = new Point(pnDownSize.Location.X - 160, pnDownSize.Location.Y);
                    pn_container.Location = new Point(pn_container.Location.X - 160, pn_container.Location.Y);
                    foreach (Control control in sidebar.Controls)
                    {
                        if (control is Button)
                        {
                            control.Margin = new Padding(0, 3, 0, 3);
                        }
                    }
                    tbSerial.Visible = false;
                    //pcb_logo.Visible = false;
                }
                else if (sidebar.Width == 210)
                {
                    MenuExpand.Location = new Point(179, 0);
                    MenuExpand.Image = Resources.Menu_Over;
                    foreach (Control control in sidebar.Controls)
                    {
                        if (control is Button)
                        {
                            control.Margin = new Padding(10, 3, 10, 3);
                        }
                    }
                    tbSerial.Visible = true;
                    //pnDownSize.Width -= 160;
                    //pn_container.Width -= 160;
                    //pnDownSize.Location = new Point(pnDownSize.Location.X + 160, pnDownSize.Location.Y);
                    //pn_container.Location = new Point(pn_container.Location.X + 160, pn_container.Location.Y);
                    //pcb_logo.Visible = true;
                }
                b_grow = false;
                sidebarExpand = !sidebarExpand;
                //Stop
                AnimationRunning = false;
            }
            //this.Refresh();
            //this.Update();
        }

        private void MainForms_FormClosed(object sender, FormClosedEventArgs e)
        {
            closeProgram = false;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            try
            {
                pnSubMenu.Visible = false;
                AnimationRunning = true;
                AnimationMenu();
            }
            catch (Exception)
            {
            }
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            try
            {
                OpenPage(new InitialForms());
            }
            catch (Exception)
            {
            }
        }

        public void OpenPage(Form page) 
        {
            try
            {
                foreach (Form frm in pn_container.Controls)
                {
                    frm.Close();
                }

                pn_container.Controls.Clear();

                page.TopLevel = false;
                page.FormBorderStyle = FormBorderStyle.None;
                page.Dock = DockStyle.Fill;
                pn_container.Controls.Add(page);
                page.Show();
                pnSubMenu.Visible = false;
                pn_container.Focus();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void panel7_MouseDown(object sender, MouseEventArgs e)
        {
            _mouseOffset = e.Location;
        }

        private void panel7_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && _isFullScreen == false)
            {
                Point mousePosition = Control.MousePosition;
                mousePosition = PointToClient(mousePosition);
                int deltaX = mousePosition.X - _mouseOffset.X;
                int deltaY = mousePosition.Y - _mouseOffset.Y;
                //Location = new Point(Location.X + deltaX - 210, Location.Y + deltaY);
                Location = new Point(Location.X + deltaX , Location.Y + deltaY);
            }
        }

        private void panel7_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if(_isFullScreen == false){
                panel1.Cursor = Cursors.Default;
                _isFullScreen = true;
                _FormSize = new Size(Width, Height);
                _FormLocation = this.Location;
                // Obtém a tela em que o formulário está atualmente exibido
                Screen currentScreen = Screen.FromControl(this);
                // Define o tamanho do formulário para o tamanho da tela atual
                this.Size = new Size(currentScreen.WorkingArea.Width, currentScreen.WorkingArea.Height);
                // Define a posição do formulário para a posição superior esquerda da tela atual
                this.Location = new Point(currentScreen.WorkingArea.Left, currentScreen.WorkingArea.Top);
            }
            else
            {
                panel1.Cursor = Cursors.SizeAll;
                _isFullScreen = false;
                this.Size = _FormSize;
                this.Location = _FormLocation;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            TestSend teste = new TestSend();
            teste.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            OpenPage(new RelatorioForms());
        }

        private void btnClose_Click_1(object sender, EventArgs e)
        {
            try
            {
                YesOrNo question = new YesOrNo("Tem certeza que deseja fechar o programa ?");
                question.ShowDialog();
                if (question.RESPOSTA)
                {
                    Application.ExitThread();
                    Application.Exit();
                }
            }
            catch (Exception)
            {
            }
        }


        private void pnRightSize_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if(_isFullScreen == false) _isResizing = true;
            }
        }

        private void pnRightSize_MouseMove(object sender, MouseEventArgs e)
        {
            if (_isResizing)
            {
                int newWidth = Width +e.X ;
                if (newWidth > MinimumSize.Width)
                {
                    Width = Width + e.X;
                    this.Refresh();
                }
            }
        }

        private void pnRightSize_MouseUp(object sender, MouseEventArgs e)
        {
            _isResizing = false;
        }

        private void pnDownSize_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (_isFullScreen == false) _isResizing = true;
            }
        }

        private void pnDownSize_MouseMove(object sender, MouseEventArgs e)
        {
            if (_isResizing)
            {
                int newHeight = Height + e.Y;
                if (newHeight > MinimumSize.Height)
                {
                    Height = Height + e.Y;
                    this.Refresh();
                }
            }
        }

        private void pnDownSize_MouseUp(object sender, MouseEventArgs e)
        {
            _isResizing = false;
        }

        private void pnBothSize_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (_isFullScreen == false) _isResizing = true;
            }
        }

        private void pnBothSize_MouseMove(object sender, MouseEventArgs e)
        {
            if (_isResizing)
            {
                int newHeight = Height + e.Y;
                int newWidth = Width + e.X;
                if (newHeight > MinimumSize.Height)
                {
                    Height = Height + e.Y;
                }
                if (newWidth > MinimumSize.Width)
                {
                    Width = Width + e.X;
                }
                this.Refresh();
            }
        }

        private void pnBothSize_MouseUp(object sender, MouseEventArgs e)
        {
            _isResizing = false;
        }

        private void vEnableMenu(string sMenuSelected)
        {
            foreach (Control item in pnSubMenu.Controls)
            {
                if(item is Button button)
                {
                    button.Visible = false;
                }
            }
            switch (sMenuSelected)
            {
                case "Cadastro":
                    this.Invoke(new MethodInvoker(delegate 
                    {
                        btnProduto.Visible = true;
                    }));
                    break;

                case "Configurações":
                    btnRede.Visible = true;
                    btnSistema.Visible = true;
                    btnSistema.Visible = true;
                    btnUsuario.Visible = true;
                    break;
                default:
                    break;
            }

        }
        private void btnConfiguração_Click(object sender, EventArgs e)
        {
            if (pnSubMenu.Visible) pnSubMenu.Visible = false;
            else pnSubMenu.Visible = true;
            lbHeader.Text = btnConfiguração.Text.Trim();
            vEnableMenu(lbHeader.Text);
            //ProductProtheusPopup produto = new ProductProtheusPopup();
            //produto.ShowDialog(); 
        }

        private void btnRede_Click(object sender, EventArgs e)
        {
            OpenPage(new RedeConfigForm());
        }

        private void btnPesagem_Click(object sender, EventArgs e)
        {
            //PesagemList pList = new PesagemList();
            //pList.ItemEditadoTrigger += PList_ItemEditadoTrigger;
            //OpenPage(pList);
        }

        private void PList_ItemEditadoTrigger(object sender, EventArgs e)
        {
            try
            {
                //PesagemList item = (PesagemList)sender;
                //if (item.ItemEditado != null) 
                //{
                //    PesagemProcess pes = new PesagemProcess(item.LogReceita);
                //    pes.ItemRepesarTrigger += Pes_ItemRepesarTrigger;
                //    pes.ListarNovamenteTrigger += Pes_ListarNovamenteTrigger;
                //    OpenPage(pes);
                //    return;
                //}
            }
            catch (Exception)
            {
            }
        }

        private void Pes_ListarNovamenteTrigger(object sender, EventArgs e)
        {
            try
            {
                //PesagemProcess item = (PesagemProcess)sender;
                //if (item.ListarNovamente) 
                //{
                //    PesagemList pList = new PesagemList();
                //    pList.ItemEditadoTrigger += PList_ItemEditadoTrigger;
                //    OpenPage(pList);
                //}
            }
            catch (Exception)
            {
            }
        }

        private void Pes_ItemRepesarTrigger(object sender, EventArgs e)
        {
            try
            {
                //PesagemProcess item = (PesagemProcess)sender;
                //if (item.ItemRepesar != null) 
                //{
                //    PesagemProcess pes = new PesagemProcess(item.ItemRepesar);
                //    pes.ItemRepesarTrigger += Pes_ItemRepesarTrigger;
                //    pes.ListarNovamenteTrigger += Pes_ListarNovamenteTrigger;
                //    OpenPage(pes);
               // }
            }
            catch (Exception)
            {
            }
        }

        private void btnSistema_Click(object sender, EventArgs e)
        {
            OpenPage(new SystemForm());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenPage(new UserForm());
        }

        private void MainForms_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    string barcode = e.KeyData.ToString().Replace("\r", "");
                    // if (IsValidBarcode(barcode))
                    //  {
                    // código de barras válido, faça alguma coisa com ele
                    //      Console.WriteLine("Código de barras lido: " + barcode);
                    //  }
                }
            }
            catch (Exception)
            {
            }
        }

        private void MainForms_SizeChanged(object sender, EventArgs e)
        {
            //this.SuspendLayout();
            //this.DoubleBuffered = true;
            //this.Invalidate();
            //this.Refresh();
            //this.ResumeLayout();
        }

        private void btnHome_Click_1(object sender, EventArgs e)
        {
            MainInfoForms info = new MainInfoForms();
            info.QuickButtonEventClick += M_Form_QuickButtonEventClick;
            OpenPage(info);
        }

        private void sidebar_Resize(object sender, EventArgs e)
        {
            //Invalidate();
        }
        public void UpdateStatusSerial()
        {
            if (Program.SERIALPORT1.IsOpen) // Verifica se a porta serial está aberta
            {
                lbSerialStatus01.Text = "OPEN";
                lbSerialStatus01.ForeColor = Color.FromArgb(100, 0, 161, 155);
            }
            else
            {
                lbSerialStatus01.Text = "CLOSE";
                lbSerialStatus01.ForeColor = Color.FromArgb(100, 127, 47, 50);
            }

            if (Program.SERIALPORT2.IsOpen) // Verifica se a porta serial está aberta
            {
                lbSerialStatus02.Text = "OPEN";
                lbSerialStatus02.ForeColor = Color.FromArgb(100, 0, 161, 155);
            }
            else
            {
                lbSerialStatus02.Text = "CLOSE";
                lbSerialStatus02.ForeColor = Color.FromArgb(100, 127, 47, 50);
            }


            if (Program.IMPRESSORAPORT.IsOpen) // Verifica se a porta serial IMP está aberta
            {
                lbSerialStatus2.Text = "OPEN";
                lbSerialStatus2.ForeColor = Color.FromArgb(100, 0, 161, 155);
            }
            else
            {
                lbSerialStatus2.Text = "CLOSE";
                lbSerialStatus2.ForeColor = Color.FromArgb(100, 127, 47, 50);
            }
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            StyleSheet.v_SaveFormConfig(this, _isFullScreen);
        }

        private void pcb_logo_MouseUp(object sender, MouseEventArgs e)
        {
            StyleSheet.v_SaveFormConfig(this, _isFullScreen);
        }

        private void btnCadastro_Click(object sender, EventArgs e)
        {
            try
            {
                if (pnSubMenu.Visible) pnSubMenu.Visible = false;
                else pnSubMenu.Visible = true;
                lbHeader.Text = btnCadastro.Text.Trim();
                vEnableMenu(lbHeader.Text);
                //ProductProtheusPopup produto = new ProductProtheusPopup();
                //produto.ShowDialog(); 
            }
            catch (Exception)
            {
            }
        }

        private void btnProduto_Click(object sender, EventArgs e)
        {
            try
            {
                OpenPage(new CadastroMateriaPrimaForms());
            }
            catch (Exception)
            {
            }
        }

        private void btnBandeja_Click(object sender, EventArgs e)
        {
            try
            {
                //OpenPage(new CadastroBandejaForms());
            }
            catch (Exception)
            {
            }
        }

        private void btnRecipiente_Click(object sender, EventArgs e)
        {
            try
            {
                //OpenPage(new CadastroRecipienteForms());
            }
            catch (Exception)
            {
            }
        }

        private void btnReceita_Click(object sender, EventArgs e)
        {
            try 
            {
                //OpenPage(new CadastroNovaReceita());
                //CadastroReceitaForms cad_receita = new CadastroReceitaForms();
                //cad_receita.ItemSelecionadoTrigger += Cad_receita_ItemSelecionadoTrigger;
                //cad_receita.ItemEditadoTrigger += Cad_receita_ItemEditadoTrigger;
                //OpenPage(cad_receita);
            }
            catch (Exception)
            {
            }
        }

        private void Cad_receita_ItemEditadoTrigger(object sender, EventArgs e)
        {
            try
            {
                //CadastroReceitaForms selecionado = (CadastroReceitaForms)sender;
                //CadastroNovaReceita recNova = new CadastroNovaReceita(selecionado.ItemEditado);
                //recNova.ItemSelecionadoTrigger += RecNova_ItemSelecionadoTrigger;
                //OpenPage(recNova);
            }
            catch (Exception)
            {
            }
        }

        private void Cad_receita_ItemSelecionadoTrigger(object sender, EventArgs e)
        {
            try
            {
                //CadastroNovaReceita recNova = new CadastroNovaReceita();
                //recNova.ItemSelecionadoTrigger += RecNova_ItemSelecionadoTrigger;
                //OpenPage(recNova);
            }
            catch (Exception)
            {
            }
        }

        private void RecNova_ItemSelecionadoTrigger(object sender, EventArgs e)
        {
            try
            {
                //OpenPage(new CadastroNovaReceita());
                //CadastroReceitaForms cad_receita = new CadastroReceitaForms();
                //cad_receita.ItemSelecionadoTrigger += Cad_receita_ItemSelecionadoTrigger;
                //cad_receita.ItemEditadoTrigger += Cad_receita_ItemEditadoTrigger;
                //OpenPage(cad_receita);
            }
            catch (Exception)
            {
            }
        }

        private void btnSerialConfig_Click(object sender, EventArgs e)
        {
            SerialForm serialForm = new SerialForm();
            serialForm.ShowDialog();
        }

        private void btnTipoReceita_Click(object sender, EventArgs e)
        {
            try
            {
                //OpenPage(new CadastroTipoReceita());
            }
            catch (Exception)
            {
            }
        }

        private void btnManual_Click(object sender, EventArgs e)
        {
            try
            {
                var path = AppDomain.CurrentDomain.BaseDirectory + "Modelo.pdf";

                string chromePath = @"C:\Program Files\Google\Chrome\Application\chrome.exe";

                if (System.IO.File.Exists(chromePath))
                {
                    Process.Start(new ProcessStartInfo(chromePath, path));
                    return;
                }

                string internetExplorer = @"C:\Program Files\Internet Explorer\iexplore.exe";
                if (System.IO.File.Exists(internetExplorer)) 
                {
                    Process.Start(new ProcessStartInfo(internetExplorer, path));
                    return;
                }

                InfoPopup info = new InfoPopup("Não foi possível abrir o documento",
                    "Não foi possível abrir o manual, verifique se o Navegador Google ou Internet Explorer está instalado na maquina.");
            }
            catch (Exception)
            {
            }
        }

        private void panel8_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                Panel pn = (Panel)sender;
                System.Drawing.Color COLOR_BORDER = System.Drawing.Color.FromArgb(237, 237, 237);
                int borderSize = 2;
                ControlPaint.DrawBorder(e.Graphics, pn.ClientRectangle, COLOR_BORDER, borderSize, ButtonBorderStyle.Solid, COLOR_BORDER, borderSize, ButtonBorderStyle.Solid, COLOR_BORDER, borderSize, ButtonBorderStyle.Solid, COLOR_BORDER, borderSize, ButtonBorderStyle.Solid);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void MainForms_Resize(object sender, EventArgs e)
        {
            try
            {
                panel8.Refresh();
            }
            catch (Exception ex)
            {
            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lbSerialStatus2_Click(object sender, EventArgs e)
        {

        }
    }
}

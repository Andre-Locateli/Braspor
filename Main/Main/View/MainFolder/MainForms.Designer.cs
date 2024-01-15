namespace Main.View.MainFolder
{
    partial class MainForms
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForms));
            this.sidebar = new System.Windows.Forms.FlowLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pcb_logo = new System.Windows.Forms.PictureBox();
            this.MenuExpand = new System.Windows.Forms.PictureBox();
            this.btnHome = new System.Windows.Forms.Button();
            this.btnCadastro = new System.Windows.Forms.Button();
            this.btnPesagem = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.btnConfiguração = new System.Windows.Forms.Button();
            this.btnManual = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.pn_container = new System.Windows.Forms.Panel();
            this.pnSubMenu = new System.Windows.Forms.FlowLayoutPanel();
            this.lbHeader = new System.Windows.Forms.Label();
            this.btnRede = new System.Windows.Forms.Button();
            this.btnSistema = new System.Windows.Forms.Button();
            this.btnUsuario = new System.Windows.Forms.Button();
            this.btnProduto = new System.Windows.Forms.Button();
            this.pnRightSize = new System.Windows.Forms.Panel();
            this.pnDownSize = new System.Windows.Forms.Panel();
            this.pnBothSize = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel9 = new System.Windows.Forms.Panel();
            this.tbSerial = new System.Windows.Forms.TableLayoutPanel();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.btnSerialConfig = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.lbSerialStatus01 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lbSerialStatus2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.panel8 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.lbl_time = new System.Windows.Forms.Label();
            this.lblAcesso = new System.Windows.Forms.Label();
            this.lblUsuario = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.sidebar.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pcb_logo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MenuExpand)).BeginInit();
            this.pnSubMenu.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tbSerial.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel6.SuspendLayout();
            this.panel8.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // sidebar
            // 
            this.sidebar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(46)))), ((int)(((byte)(84)))));
            this.sidebar.Controls.Add(this.panel1);
            this.sidebar.Controls.Add(this.btnHome);
            this.sidebar.Controls.Add(this.btnCadastro);
            this.sidebar.Controls.Add(this.btnPesagem);
            this.sidebar.Controls.Add(this.button5);
            this.sidebar.Controls.Add(this.btnConfiguração);
            this.sidebar.Controls.Add(this.btnManual);
            this.sidebar.Controls.Add(this.button1);
            this.sidebar.Dock = System.Windows.Forms.DockStyle.Left;
            this.sidebar.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.sidebar.Location = new System.Drawing.Point(0, 0);
            this.sidebar.Margin = new System.Windows.Forms.Padding(0);
            this.sidebar.Name = "sidebar";
            this.sidebar.Size = new System.Drawing.Size(210, 690);
            this.sidebar.TabIndex = 0;
            this.sidebar.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.panel7_MouseDoubleClick);
            this.sidebar.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel7_MouseDown);
            this.sidebar.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel7_MouseMove);
            this.sidebar.Resize += new System.EventHandler(this.sidebar_Resize);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.pcb_logo);
            this.panel1.Controls.Add(this.MenuExpand);
            this.panel1.Cursor = System.Windows.Forms.Cursors.SizeAll;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(209, 174);
            this.panel1.TabIndex = 0;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            this.panel1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.panel7_MouseDoubleClick);
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel7_MouseDown);
            this.panel1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel7_MouseMove);
            this.panel1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseUp);
            // 
            // pcb_logo
            // 
            this.pcb_logo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pcb_logo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(46)))), ((int)(((byte)(84)))));
            this.pcb_logo.Image = ((System.Drawing.Image)(resources.GetObject("pcb_logo.Image")));
            this.pcb_logo.Location = new System.Drawing.Point(3, 34);
            this.pcb_logo.Name = "pcb_logo";
            this.pcb_logo.Size = new System.Drawing.Size(198, 95);
            this.pcb_logo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pcb_logo.TabIndex = 5;
            this.pcb_logo.TabStop = false;
            this.pcb_logo.Click += new System.EventHandler(this.pcb_logo_Click);
            this.pcb_logo.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.panel7_MouseDoubleClick);
            this.pcb_logo.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel7_MouseDown);
            this.pcb_logo.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel7_MouseMove);
            this.pcb_logo.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pcb_logo_MouseUp);
            // 
            // MenuExpand
            // 
            this.MenuExpand.BackColor = System.Drawing.Color.Transparent;
            this.MenuExpand.Cursor = System.Windows.Forms.Cursors.Hand;
            this.MenuExpand.Image = ((System.Drawing.Image)(resources.GetObject("MenuExpand.Image")));
            this.MenuExpand.Location = new System.Drawing.Point(179, 0);
            this.MenuExpand.Margin = new System.Windows.Forms.Padding(0);
            this.MenuExpand.Name = "MenuExpand";
            this.MenuExpand.Size = new System.Drawing.Size(20, 27);
            this.MenuExpand.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.MenuExpand.TabIndex = 1;
            this.MenuExpand.TabStop = false;
            this.MenuExpand.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // btnHome
            // 
            this.btnHome.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnHome.FlatAppearance.BorderSize = 0;
            this.btnHome.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(80)))), ((int)(((byte)(158)))));
            this.btnHome.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(80)))), ((int)(((byte)(158)))));
            this.btnHome.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHome.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHome.ForeColor = System.Drawing.Color.White;
            this.btnHome.Image = global::Main.Properties.Resources.principalIcon;
            this.btnHome.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnHome.Location = new System.Drawing.Point(10, 183);
            this.btnHome.Margin = new System.Windows.Forms.Padding(10, 3, 10, 3);
            this.btnHome.Name = "btnHome";
            this.btnHome.Padding = new System.Windows.Forms.Padding(4, 0, 0, 0);
            this.btnHome.Size = new System.Drawing.Size(187, 42);
            this.btnHome.TabIndex = 2;
            this.btnHome.Text = "           Principal";
            this.btnHome.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnHome.UseVisualStyleBackColor = true;
            this.btnHome.Click += new System.EventHandler(this.btnHome_Click_1);
            // 
            // btnCadastro
            // 
            this.btnCadastro.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCadastro.FlatAppearance.BorderSize = 0;
            this.btnCadastro.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(80)))), ((int)(((byte)(158)))));
            this.btnCadastro.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(80)))), ((int)(((byte)(158)))));
            this.btnCadastro.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCadastro.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCadastro.ForeColor = System.Drawing.Color.White;
            this.btnCadastro.Image = global::Main.Properties.Resources.Plus_27px1;
            this.btnCadastro.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCadastro.Location = new System.Drawing.Point(10, 231);
            this.btnCadastro.Margin = new System.Windows.Forms.Padding(10, 3, 10, 3);
            this.btnCadastro.Name = "btnCadastro";
            this.btnCadastro.Padding = new System.Windows.Forms.Padding(4, 0, 0, 0);
            this.btnCadastro.Size = new System.Drawing.Size(187, 42);
            this.btnCadastro.TabIndex = 5;
            this.btnCadastro.Text = "           Cadastro";
            this.btnCadastro.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCadastro.UseVisualStyleBackColor = true;
            this.btnCadastro.Click += new System.EventHandler(this.btnCadastro_Click);
            // 
            // btnPesagem
            // 
            this.btnPesagem.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPesagem.FlatAppearance.BorderSize = 0;
            this.btnPesagem.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(80)))), ((int)(((byte)(158)))));
            this.btnPesagem.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(80)))), ((int)(((byte)(158)))));
            this.btnPesagem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPesagem.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPesagem.ForeColor = System.Drawing.Color.White;
            this.btnPesagem.Image = ((System.Drawing.Image)(resources.GetObject("btnPesagem.Image")));
            this.btnPesagem.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPesagem.Location = new System.Drawing.Point(10, 279);
            this.btnPesagem.Margin = new System.Windows.Forms.Padding(10, 3, 10, 3);
            this.btnPesagem.Name = "btnPesagem";
            this.btnPesagem.Padding = new System.Windows.Forms.Padding(4, 0, 0, 0);
            this.btnPesagem.Size = new System.Drawing.Size(187, 42);
            this.btnPesagem.TabIndex = 1;
            this.btnPesagem.Text = "           Pesagem";
            this.btnPesagem.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPesagem.UseVisualStyleBackColor = true;
            this.btnPesagem.Click += new System.EventHandler(this.btnPesagem_Click);
            // 
            // button5
            // 
            this.button5.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button5.FlatAppearance.BorderSize = 0;
            this.button5.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(80)))), ((int)(((byte)(158)))));
            this.button5.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(80)))), ((int)(((byte)(158)))));
            this.button5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button5.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button5.ForeColor = System.Drawing.Color.White;
            this.button5.Image = ((System.Drawing.Image)(resources.GetObject("button5.Image")));
            this.button5.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button5.Location = new System.Drawing.Point(10, 327);
            this.button5.Margin = new System.Windows.Forms.Padding(10, 3, 10, 3);
            this.button5.Name = "button5";
            this.button5.Padding = new System.Windows.Forms.Padding(4, 0, 0, 0);
            this.button5.Size = new System.Drawing.Size(187, 42);
            this.button5.TabIndex = 1;
            this.button5.Text = "           Relatório";
            this.button5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // btnConfiguração
            // 
            this.btnConfiguração.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnConfiguração.FlatAppearance.BorderSize = 0;
            this.btnConfiguração.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(80)))), ((int)(((byte)(158)))));
            this.btnConfiguração.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(80)))), ((int)(((byte)(158)))));
            this.btnConfiguração.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConfiguração.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConfiguração.ForeColor = System.Drawing.Color.White;
            this.btnConfiguração.Image = ((System.Drawing.Image)(resources.GetObject("btnConfiguração.Image")));
            this.btnConfiguração.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnConfiguração.Location = new System.Drawing.Point(10, 375);
            this.btnConfiguração.Margin = new System.Windows.Forms.Padding(10, 3, 10, 3);
            this.btnConfiguração.Name = "btnConfiguração";
            this.btnConfiguração.Padding = new System.Windows.Forms.Padding(4, 0, 0, 0);
            this.btnConfiguração.Size = new System.Drawing.Size(187, 42);
            this.btnConfiguração.TabIndex = 1;
            this.btnConfiguração.Text = "           Configurações";
            this.btnConfiguração.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnConfiguração.UseVisualStyleBackColor = true;
            this.btnConfiguração.Click += new System.EventHandler(this.btnConfiguração_Click);
            // 
            // btnManual
            // 
            this.btnManual.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnManual.FlatAppearance.BorderSize = 0;
            this.btnManual.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(80)))), ((int)(((byte)(158)))));
            this.btnManual.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(80)))), ((int)(((byte)(158)))));
            this.btnManual.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnManual.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnManual.ForeColor = System.Drawing.Color.White;
            this.btnManual.Image = global::Main.Properties.Resources.document_27px;
            this.btnManual.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnManual.Location = new System.Drawing.Point(10, 423);
            this.btnManual.Margin = new System.Windows.Forms.Padding(10, 3, 10, 3);
            this.btnManual.Name = "btnManual";
            this.btnManual.Padding = new System.Windows.Forms.Padding(4, 0, 0, 0);
            this.btnManual.Size = new System.Drawing.Size(187, 42);
            this.btnManual.TabIndex = 6;
            this.btnManual.Text = "           Manual";
            this.btnManual.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnManual.UseVisualStyleBackColor = true;
            this.btnManual.Click += new System.EventHandler(this.btnManual_Click);
            // 
            // button1
            // 
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(80)))), ((int)(((byte)(158)))));
            this.button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(80)))), ((int)(((byte)(158)))));
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(10, 471);
            this.button1.Margin = new System.Windows.Forms.Padding(10, 3, 10, 3);
            this.button1.Name = "button1";
            this.button1.Padding = new System.Windows.Forms.Padding(4, 0, 0, 0);
            this.button1.Size = new System.Drawing.Size(187, 42);
            this.button1.TabIndex = 4;
            this.button1.Text = "           Sair";
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.btnClose_Click_1);
            // 
            // pn_container
            // 
            this.pn_container.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pn_container.BackColor = System.Drawing.Color.White;
            this.pn_container.Location = new System.Drawing.Point(210, 95);
            this.pn_container.Name = "pn_container";
            this.pn_container.Size = new System.Drawing.Size(931, 595);
            this.pn_container.TabIndex = 1;
            this.pn_container.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.panel7_MouseDoubleClick);
            this.pn_container.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel7_MouseDown);
            this.pn_container.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel7_MouseMove);
            // 
            // pnSubMenu
            // 
            this.pnSubMenu.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.pnSubMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(74)))), ((int)(((byte)(126)))));
            this.pnSubMenu.Controls.Add(this.lbHeader);
            this.pnSubMenu.Controls.Add(this.btnRede);
            this.pnSubMenu.Controls.Add(this.btnSistema);
            this.pnSubMenu.Controls.Add(this.btnUsuario);
            this.pnSubMenu.Controls.Add(this.btnProduto);
            this.pnSubMenu.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.pnSubMenu.Location = new System.Drawing.Point(210, 0);
            this.pnSubMenu.Margin = new System.Windows.Forms.Padding(0);
            this.pnSubMenu.Name = "pnSubMenu";
            this.pnSubMenu.Size = new System.Drawing.Size(210, 690);
            this.pnSubMenu.TabIndex = 0;
            this.pnSubMenu.Visible = false;
            // 
            // lbHeader
            // 
            this.lbHeader.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbHeader.ForeColor = System.Drawing.Color.White;
            this.lbHeader.Location = new System.Drawing.Point(10, 0);
            this.lbHeader.Margin = new System.Windows.Forms.Padding(10, 0, 3, 0);
            this.lbHeader.Name = "lbHeader";
            this.lbHeader.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.lbHeader.Size = new System.Drawing.Size(184, 68);
            this.lbHeader.TabIndex = 1;
            this.lbHeader.Text = "Configurações";
            this.lbHeader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnRede
            // 
            this.btnRede.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(74)))), ((int)(((byte)(126)))));
            this.btnRede.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRede.FlatAppearance.BorderSize = 0;
            this.btnRede.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(80)))), ((int)(((byte)(158)))));
            this.btnRede.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(80)))), ((int)(((byte)(158)))));
            this.btnRede.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRede.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRede.ForeColor = System.Drawing.Color.White;
            this.btnRede.Image = ((System.Drawing.Image)(resources.GetObject("btnRede.Image")));
            this.btnRede.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRede.Location = new System.Drawing.Point(10, 71);
            this.btnRede.Margin = new System.Windows.Forms.Padding(10, 3, 10, 3);
            this.btnRede.Name = "btnRede";
            this.btnRede.Padding = new System.Windows.Forms.Padding(4, 0, 0, 0);
            this.btnRede.Size = new System.Drawing.Size(191, 42);
            this.btnRede.TabIndex = 0;
            this.btnRede.Text = "           Rede";
            this.btnRede.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRede.UseVisualStyleBackColor = false;
            this.btnRede.Click += new System.EventHandler(this.btnRede_Click);
            // 
            // btnSistema
            // 
            this.btnSistema.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(74)))), ((int)(((byte)(126)))));
            this.btnSistema.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSistema.FlatAppearance.BorderSize = 0;
            this.btnSistema.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(80)))), ((int)(((byte)(158)))));
            this.btnSistema.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(80)))), ((int)(((byte)(158)))));
            this.btnSistema.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSistema.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSistema.ForeColor = System.Drawing.Color.White;
            this.btnSistema.Image = ((System.Drawing.Image)(resources.GetObject("btnSistema.Image")));
            this.btnSistema.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSistema.Location = new System.Drawing.Point(10, 119);
            this.btnSistema.Margin = new System.Windows.Forms.Padding(10, 3, 10, 3);
            this.btnSistema.Name = "btnSistema";
            this.btnSistema.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.btnSistema.Size = new System.Drawing.Size(191, 42);
            this.btnSistema.TabIndex = 2;
            this.btnSistema.Text = "           Sistema";
            this.btnSistema.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSistema.UseVisualStyleBackColor = false;
            this.btnSistema.Click += new System.EventHandler(this.btnSistema_Click);
            // 
            // btnUsuario
            // 
            this.btnUsuario.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(74)))), ((int)(((byte)(126)))));
            this.btnUsuario.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnUsuario.FlatAppearance.BorderSize = 0;
            this.btnUsuario.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(80)))), ((int)(((byte)(158)))));
            this.btnUsuario.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(80)))), ((int)(((byte)(158)))));
            this.btnUsuario.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUsuario.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUsuario.ForeColor = System.Drawing.Color.White;
            this.btnUsuario.Image = ((System.Drawing.Image)(resources.GetObject("btnUsuario.Image")));
            this.btnUsuario.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnUsuario.Location = new System.Drawing.Point(10, 167);
            this.btnUsuario.Margin = new System.Windows.Forms.Padding(10, 3, 10, 3);
            this.btnUsuario.Name = "btnUsuario";
            this.btnUsuario.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.btnUsuario.Size = new System.Drawing.Size(191, 42);
            this.btnUsuario.TabIndex = 3;
            this.btnUsuario.Text = "           Usuários";
            this.btnUsuario.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnUsuario.UseVisualStyleBackColor = false;
            this.btnUsuario.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnProduto
            // 
            this.btnProduto.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(74)))), ((int)(((byte)(126)))));
            this.btnProduto.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnProduto.FlatAppearance.BorderSize = 0;
            this.btnProduto.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(80)))), ((int)(((byte)(158)))));
            this.btnProduto.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(80)))), ((int)(((byte)(158)))));
            this.btnProduto.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnProduto.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnProduto.ForeColor = System.Drawing.Color.White;
            this.btnProduto.Image = ((System.Drawing.Image)(resources.GetObject("btnProduto.Image")));
            this.btnProduto.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnProduto.Location = new System.Drawing.Point(10, 215);
            this.btnProduto.Margin = new System.Windows.Forms.Padding(10, 3, 10, 3);
            this.btnProduto.Name = "btnProduto";
            this.btnProduto.Padding = new System.Windows.Forms.Padding(4, 0, 0, 0);
            this.btnProduto.Size = new System.Drawing.Size(191, 42);
            this.btnProduto.TabIndex = 0;
            this.btnProduto.Text = "           Matéria-prima";
            this.btnProduto.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnProduto.UseVisualStyleBackColor = false;
            this.btnProduto.Click += new System.EventHandler(this.btnProduto_Click);
            // 
            // pnRightSize
            // 
            this.pnRightSize.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnRightSize.BackColor = System.Drawing.Color.White;
            this.pnRightSize.Cursor = System.Windows.Forms.Cursors.SizeWE;
            this.pnRightSize.Location = new System.Drawing.Point(1141, 0);
            this.pnRightSize.Name = "pnRightSize";
            this.pnRightSize.Size = new System.Drawing.Size(2, 689);
            this.pnRightSize.TabIndex = 2;
            this.pnRightSize.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pnRightSize_MouseDown);
            this.pnRightSize.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pnRightSize_MouseMove);
            this.pnRightSize.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pnRightSize_MouseUp);
            // 
            // pnDownSize
            // 
            this.pnDownSize.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnDownSize.BackColor = System.Drawing.Color.White;
            this.pnDownSize.Cursor = System.Windows.Forms.Cursors.SizeNS;
            this.pnDownSize.Location = new System.Drawing.Point(210, 688);
            this.pnDownSize.Name = "pnDownSize";
            this.pnDownSize.Size = new System.Drawing.Size(933, 2);
            this.pnDownSize.TabIndex = 3;
            this.pnDownSize.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pnDownSize_MouseDown);
            this.pnDownSize.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pnDownSize_MouseMove);
            this.pnDownSize.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pnDownSize_MouseUp);
            // 
            // pnBothSize
            // 
            this.pnBothSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pnBothSize.BackColor = System.Drawing.Color.White;
            this.pnBothSize.Cursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.pnBothSize.Location = new System.Drawing.Point(1141, 688);
            this.pnBothSize.Name = "pnBothSize";
            this.pnBothSize.Size = new System.Drawing.Size(2, 2);
            this.pnBothSize.TabIndex = 3;
            this.pnBothSize.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pnBothSize_MouseDown);
            this.pnBothSize.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pnBothSize_MouseMove);
            this.pnBothSize.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pnBothSize_MouseUp);
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(46)))), ((int)(((byte)(84)))));
            this.panel2.Controls.Add(this.panel9);
            this.panel2.Controls.Add(this.tbSerial);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Location = new System.Drawing.Point(0, 550);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(5);
            this.panel2.Size = new System.Drawing.Size(210, 140);
            this.panel2.TabIndex = 4;
            this.panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.panel2_Paint);
            // 
            // panel9
            // 
            this.panel9.BackColor = System.Drawing.Color.White;
            this.panel9.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel9.Location = new System.Drawing.Point(5, 27);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(200, 2);
            this.panel9.TabIndex = 7;
            // 
            // tbSerial
            // 
            this.tbSerial.ColumnCount = 2;
            this.tbSerial.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tbSerial.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tbSerial.ContextMenuStrip = this.contextMenuStrip1;
            this.tbSerial.Controls.Add(this.label1, 0, 0);
            this.tbSerial.Controls.Add(this.lbSerialStatus01, 1, 0);
            this.tbSerial.Controls.Add(this.label2, 0, 1);
            this.tbSerial.Controls.Add(this.lbSerialStatus2, 1, 1);
            this.tbSerial.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbSerial.Location = new System.Drawing.Point(5, 27);
            this.tbSerial.Name = "tbSerial";
            this.tbSerial.RowCount = 2;
            this.tbSerial.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tbSerial.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tbSerial.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tbSerial.Size = new System.Drawing.Size(200, 108);
            this.tbSerial.TabIndex = 0;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnSerialConfig});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(163, 26);
            // 
            // btnSerialConfig
            // 
            this.btnSerialConfig.Name = "btnSerialConfig";
            this.btnSerialConfig.Size = new System.Drawing.Size(162, 22);
            this.btnSerialConfig.Text = "Configurar Serial";
            this.btnSerialConfig.Click += new System.EventHandler(this.btnSerialConfig_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 54);
            this.label1.TabIndex = 0;
            this.label1.Text = "Serial Indicadores";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbSerialStatus01
            // 
            this.lbSerialStatus01.AutoSize = true;
            this.lbSerialStatus01.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbSerialStatus01.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lbSerialStatus01.ForeColor = System.Drawing.Color.White;
            this.lbSerialStatus01.Location = new System.Drawing.Point(103, 0);
            this.lbSerialStatus01.Name = "lbSerialStatus01";
            this.lbSerialStatus01.Size = new System.Drawing.Size(94, 54);
            this.lbSerialStatus01.TabIndex = 1;
            this.lbSerialStatus01.Text = "-";
            this.lbSerialStatus01.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(3, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 54);
            this.label2.TabIndex = 2;
            this.label2.Text = "Serial Impressora";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbSerialStatus2
            // 
            this.lbSerialStatus2.AutoSize = true;
            this.lbSerialStatus2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbSerialStatus2.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lbSerialStatus2.ForeColor = System.Drawing.Color.White;
            this.lbSerialStatus2.Location = new System.Drawing.Point(103, 54);
            this.lbSerialStatus2.Name = "lbSerialStatus2";
            this.lbSerialStatus2.Size = new System.Drawing.Size(94, 54);
            this.lbSerialStatus2.TabIndex = 3;
            this.lbSerialStatus2.Text = "-";
            this.lbSerialStatus2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbSerialStatus2.Click += new System.EventHandler(this.lbSerialStatus2_Click);
            // 
            // label5
            // 
            this.label5.Dock = System.Windows.Forms.DockStyle.Top;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(5, 5);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(200, 22);
            this.label5.TabIndex = 6;
            this.label5.Text = "Informações de comunicação:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.tableLayoutPanel1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(210, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(933, 97);
            this.panel3.TabIndex = 5;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tableLayoutPanel1.Controls.Add(this.panel5, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel4, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel6, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(933, 97);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(247)))), ((int)(((byte)(248)))));
            this.panel5.Controls.Add(this.panel7);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(733, 0);
            this.panel5.Margin = new System.Windows.Forms.Padding(0);
            this.panel5.Name = "panel5";
            this.panel5.Padding = new System.Windows.Forms.Padding(10);
            this.panel5.Size = new System.Drawing.Size(200, 97);
            this.panel5.TabIndex = 2;
            // 
            // panel7
            // 
            this.panel7.BackColor = System.Drawing.Color.White;
            this.panel7.Controls.Add(this.pictureBox1);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel7.Location = new System.Drawing.Point(10, 10);
            this.panel7.Name = "panel7";
            this.panel7.Padding = new System.Windows.Forms.Padding(2);
            this.panel7.Size = new System.Drawing.Size(180, 77);
            this.panel7.TabIndex = 3;
            this.panel7.Paint += new System.Windows.Forms.PaintEventHandler(this.panel8_Paint);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.pictureBox1.Image = global::Main.Properties.Resources.braspor;
            this.pictureBox1.Location = new System.Drawing.Point(2, 2);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(176, 73);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(247)))), ((int)(((byte)(248)))));
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(728, 0);
            this.panel4.Margin = new System.Windows.Forms.Padding(0);
            this.panel4.Name = "panel4";
            this.panel4.Padding = new System.Windows.Forms.Padding(20);
            this.panel4.Size = new System.Drawing.Size(5, 97);
            this.panel4.TabIndex = 1;
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(247)))), ((int)(((byte)(248)))));
            this.panel6.Controls.Add(this.panel8);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel6.Location = new System.Drawing.Point(0, 0);
            this.panel6.Margin = new System.Windows.Forms.Padding(0);
            this.panel6.Name = "panel6";
            this.panel6.Padding = new System.Windows.Forms.Padding(10);
            this.panel6.Size = new System.Drawing.Size(728, 97);
            this.panel6.TabIndex = 0;
            // 
            // panel8
            // 
            this.panel8.BackColor = System.Drawing.Color.White;
            this.panel8.Controls.Add(this.tableLayoutPanel2);
            this.panel8.Controls.Add(this.lbl_time);
            this.panel8.Controls.Add(this.lblAcesso);
            this.panel8.Controls.Add(this.lblUsuario);
            this.panel8.Controls.Add(this.pictureBox2);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel8.Location = new System.Drawing.Point(10, 10);
            this.panel8.Margin = new System.Windows.Forms.Padding(0);
            this.panel8.Name = "panel8";
            this.panel8.Padding = new System.Windows.Forms.Padding(5);
            this.panel8.Size = new System.Drawing.Size(708, 77);
            this.panel8.TabIndex = 3;
            this.panel8.Paint += new System.Windows.Forms.PaintEventHandler(this.panel8_Paint);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.pictureBox5, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.pictureBox4, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(664, 5);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(39, 67);
            this.tableLayoutPanel2.TabIndex = 4;
            // 
            // pictureBox5
            // 
            this.pictureBox5.BackColor = System.Drawing.Color.White;
            this.pictureBox5.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox5.Image = global::Main.Properties.Resources._8664917_window_minimize_icon;
            this.pictureBox5.Location = new System.Drawing.Point(3, 36);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(33, 28);
            this.pictureBox5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox5.TabIndex = 2;
            this.pictureBox5.TabStop = false;
            this.pictureBox5.Tag = "Minimizar o programa";
            this.pictureBox5.Click += new System.EventHandler(this.pictureBox5_Click);
            this.pictureBox5.Paint += new System.Windows.Forms.PaintEventHandler(this.eventPaintPic);
            this.pictureBox5.MouseHover += new System.EventHandler(this.pictureBox4_MouseHover);
            // 
            // pictureBox4
            // 
            this.pictureBox4.BackColor = System.Drawing.Color.White;
            this.pictureBox4.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox4.Image = global::Main.Properties.Resources._352270_close_icon;
            this.pictureBox4.Location = new System.Drawing.Point(3, 3);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(33, 27);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox4.TabIndex = 1;
            this.pictureBox4.TabStop = false;
            this.pictureBox4.Tag = "Fechar o programa";
            this.pictureBox4.Click += new System.EventHandler(this.pictureBox4_Click);
            this.pictureBox4.Paint += new System.Windows.Forms.PaintEventHandler(this.eventPaintPic);
            this.pictureBox4.MouseHover += new System.EventHandler(this.pictureBox4_MouseHover);
            // 
            // lbl_time
            // 
            this.lbl_time.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_time.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lbl_time.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(114)))), ((int)(((byte)(113)))));
            this.lbl_time.Location = new System.Drawing.Point(484, 42);
            this.lbl_time.Name = "lbl_time";
            this.lbl_time.Size = new System.Drawing.Size(169, 25);
            this.lbl_time.TabIndex = 3;
            this.lbl_time.Text = "label1";
            this.lbl_time.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblAcesso
            // 
            this.lblAcesso.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAcesso.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(114)))), ((int)(((byte)(113)))));
            this.lblAcesso.Location = new System.Drawing.Point(76, 41);
            this.lblAcesso.Name = "lblAcesso";
            this.lblAcesso.Size = new System.Drawing.Size(345, 20);
            this.lblAcesso.TabIndex = 2;
            this.lblAcesso.Text = "label1";
            // 
            // lblUsuario
            // 
            this.lblUsuario.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUsuario.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(114)))), ((int)(((byte)(113)))));
            this.lblUsuario.Location = new System.Drawing.Point(76, 16);
            this.lblUsuario.Name = "lblUsuario";
            this.lblUsuario.Size = new System.Drawing.Size(345, 25);
            this.lblUsuario.TabIndex = 1;
            this.lblUsuario.Text = "label1";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::Main.Properties.Resources.userMain;
            this.pictureBox2.Location = new System.Drawing.Point(19, 16);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(44, 45);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 0;
            this.pictureBox2.TabStop = false;
            // 
            // toolTip1
            // 
            this.toolTip1.Popup += new System.Windows.Forms.PopupEventHandler(this.toolTip1_Popup);
            // 
            // MainForms
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1143, 690);
            this.Controls.Add(this.pnSubMenu);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.pnBothSize);
            this.Controls.Add(this.pnDownSize);
            this.Controls.Add(this.pnRightSize);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.sidebar);
            this.Controls.Add(this.pn_container);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MinimumSize = new System.Drawing.Size(1143, 690);
            this.Name = "MainForms";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MainForms";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForms_FormClosed);
            this.Load += new System.EventHandler(this.MainForms_Load);
            this.SizeChanged += new System.EventHandler(this.MainForms_SizeChanged);
            this.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.MainForms_PreviewKeyDown);
            this.Resize += new System.EventHandler(this.MainForms_Resize);
            this.sidebar.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pcb_logo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MenuExpand)).EndInit();
            this.pnSubMenu.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.tbSerial.ResumeLayout(false);
            this.tbSerial.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel6.ResumeLayout(false);
            this.panel8.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel sidebar;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnConfiguração;
        private System.Windows.Forms.PictureBox MenuExpand;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Panel pn_container;
        private System.Windows.Forms.Button btnHome;
        private System.Windows.Forms.Button btnRede;
        private System.Windows.Forms.Panel pnRightSize;
        private System.Windows.Forms.Panel pnDownSize;
        private System.Windows.Forms.Panel pnBothSize;
        private System.Windows.Forms.FlowLayoutPanel pnSubMenu;
        private System.Windows.Forms.Label lbHeader;
        private System.Windows.Forms.Button btnSistema;
        private System.Windows.Forms.Button btnUsuario;
        private System.Windows.Forms.PictureBox pcb_logo;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbSerialStatus01;
        private System.Windows.Forms.TableLayoutPanel tbSerial;
        private System.Windows.Forms.Button btnPesagem;
        private System.Windows.Forms.Button btnCadastro;
        private System.Windows.Forms.Button btnProduto;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem btnSerialConfig;
        private System.Windows.Forms.Button btnManual;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Label lbl_time;
        private System.Windows.Forms.Label lblAcesso;
        private System.Windows.Forms.Label lblUsuario;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lbSerialStatus2;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.PictureBox pictureBox5;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}
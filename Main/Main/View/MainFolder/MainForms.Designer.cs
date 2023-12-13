﻿namespace Main.View.MainFolder
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
            this.btnBandeja = new System.Windows.Forms.Button();
            this.btnRecipiente = new System.Windows.Forms.Button();
            this.btnTipoReceita = new System.Windows.Forms.Button();
            this.btnReceita = new System.Windows.Forms.Button();
            this.pnRightSize = new System.Windows.Forms.Panel();
            this.pnDownSize = new System.Windows.Forms.Panel();
            this.pnBothSize = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tbSerial = new System.Windows.Forms.TableLayoutPanel();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.btnSerialConfig = new System.Windows.Forms.ToolStripMenuItem();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lbSerialStatus = new System.Windows.Forms.Label();
            this.lbSerialStatus2 = new System.Windows.Forms.Label();
            this.sidebar.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pcb_logo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MenuExpand)).BeginInit();
            this.pnSubMenu.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tbSerial.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // sidebar
            // 
            this.sidebar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.sidebar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(46)))), ((int)(((byte)(84)))));
            this.sidebar.Controls.Add(this.panel1);
            this.sidebar.Controls.Add(this.btnHome);
            this.sidebar.Controls.Add(this.btnCadastro);
            this.sidebar.Controls.Add(this.btnPesagem);
            this.sidebar.Controls.Add(this.button5);
            this.sidebar.Controls.Add(this.btnConfiguração);
            this.sidebar.Controls.Add(this.btnManual);
            this.sidebar.Controls.Add(this.button1);
            this.sidebar.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.sidebar.Location = new System.Drawing.Point(0, 0);
            this.sidebar.Margin = new System.Windows.Forms.Padding(0);
            this.sidebar.Name = "sidebar";
            this.sidebar.Size = new System.Drawing.Size(210, 588);
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
            this.button1.Click += new System.EventHandler(this.btnClose_Click_1);
            // 
            // pn_container
            // 
            this.pn_container.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pn_container.BackColor = System.Drawing.Color.White;
            this.pn_container.Location = new System.Drawing.Point(210, 0);
            this.pn_container.Name = "pn_container";
            this.pn_container.Size = new System.Drawing.Size(931, 688);
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
            this.pnSubMenu.Controls.Add(this.btnBandeja);
            this.pnSubMenu.Controls.Add(this.btnRecipiente);
            this.pnSubMenu.Controls.Add(this.btnTipoReceita);
            this.pnSubMenu.Controls.Add(this.btnReceita);
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
            this.btnUsuario.Text = "           Usuarios";
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
            this.btnProduto.Text = "           Produto";
            this.btnProduto.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnProduto.UseVisualStyleBackColor = false;
            this.btnProduto.Click += new System.EventHandler(this.btnProduto_Click);
            // 
            // btnBandeja
            // 
            this.btnBandeja.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(74)))), ((int)(((byte)(126)))));
            this.btnBandeja.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBandeja.FlatAppearance.BorderSize = 0;
            this.btnBandeja.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(80)))), ((int)(((byte)(158)))));
            this.btnBandeja.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(80)))), ((int)(((byte)(158)))));
            this.btnBandeja.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBandeja.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBandeja.ForeColor = System.Drawing.Color.White;
            this.btnBandeja.Image = ((System.Drawing.Image)(resources.GetObject("btnBandeja.Image")));
            this.btnBandeja.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBandeja.Location = new System.Drawing.Point(10, 263);
            this.btnBandeja.Margin = new System.Windows.Forms.Padding(10, 3, 10, 3);
            this.btnBandeja.Name = "btnBandeja";
            this.btnBandeja.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.btnBandeja.Size = new System.Drawing.Size(191, 42);
            this.btnBandeja.TabIndex = 2;
            this.btnBandeja.Text = "           Bandeja";
            this.btnBandeja.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBandeja.UseVisualStyleBackColor = false;
            this.btnBandeja.Click += new System.EventHandler(this.btnBandeja_Click);
            // 
            // btnRecipiente
            // 
            this.btnRecipiente.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(74)))), ((int)(((byte)(126)))));
            this.btnRecipiente.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRecipiente.FlatAppearance.BorderSize = 0;
            this.btnRecipiente.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(80)))), ((int)(((byte)(158)))));
            this.btnRecipiente.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(80)))), ((int)(((byte)(158)))));
            this.btnRecipiente.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRecipiente.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRecipiente.ForeColor = System.Drawing.Color.White;
            this.btnRecipiente.Image = ((System.Drawing.Image)(resources.GetObject("btnRecipiente.Image")));
            this.btnRecipiente.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRecipiente.Location = new System.Drawing.Point(10, 311);
            this.btnRecipiente.Margin = new System.Windows.Forms.Padding(10, 3, 10, 3);
            this.btnRecipiente.Name = "btnRecipiente";
            this.btnRecipiente.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.btnRecipiente.Size = new System.Drawing.Size(191, 42);
            this.btnRecipiente.TabIndex = 3;
            this.btnRecipiente.Text = "           Recipiente";
            this.btnRecipiente.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRecipiente.UseVisualStyleBackColor = false;
            this.btnRecipiente.Click += new System.EventHandler(this.btnRecipiente_Click);
            // 
            // btnTipoReceita
            // 
            this.btnTipoReceita.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(74)))), ((int)(((byte)(126)))));
            this.btnTipoReceita.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTipoReceita.FlatAppearance.BorderSize = 0;
            this.btnTipoReceita.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(80)))), ((int)(((byte)(158)))));
            this.btnTipoReceita.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(80)))), ((int)(((byte)(158)))));
            this.btnTipoReceita.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTipoReceita.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTipoReceita.ForeColor = System.Drawing.Color.White;
            this.btnTipoReceita.Image = ((System.Drawing.Image)(resources.GetObject("btnTipoReceita.Image")));
            this.btnTipoReceita.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnTipoReceita.Location = new System.Drawing.Point(10, 359);
            this.btnTipoReceita.Margin = new System.Windows.Forms.Padding(10, 3, 10, 3);
            this.btnTipoReceita.Name = "btnTipoReceita";
            this.btnTipoReceita.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.btnTipoReceita.Size = new System.Drawing.Size(191, 42);
            this.btnTipoReceita.TabIndex = 5;
            this.btnTipoReceita.Text = "           Tipo de Receita";
            this.btnTipoReceita.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnTipoReceita.UseVisualStyleBackColor = false;
            this.btnTipoReceita.Click += new System.EventHandler(this.btnTipoReceita_Click);
            // 
            // btnReceita
            // 
            this.btnReceita.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(74)))), ((int)(((byte)(126)))));
            this.btnReceita.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnReceita.FlatAppearance.BorderSize = 0;
            this.btnReceita.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(80)))), ((int)(((byte)(158)))));
            this.btnReceita.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(80)))), ((int)(((byte)(158)))));
            this.btnReceita.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReceita.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReceita.ForeColor = System.Drawing.Color.White;
            this.btnReceita.Image = ((System.Drawing.Image)(resources.GetObject("btnReceita.Image")));
            this.btnReceita.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnReceita.Location = new System.Drawing.Point(10, 407);
            this.btnReceita.Margin = new System.Windows.Forms.Padding(10, 3, 10, 3);
            this.btnReceita.Name = "btnReceita";
            this.btnReceita.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.btnReceita.Size = new System.Drawing.Size(191, 42);
            this.btnReceita.TabIndex = 4;
            this.btnReceita.Text = "           Receita";
            this.btnReceita.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnReceita.UseVisualStyleBackColor = false;
            this.btnReceita.Click += new System.EventHandler(this.btnReceita_Click);
            // 
            // pnRightSize
            // 
            this.pnRightSize.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnRightSize.BackColor = System.Drawing.Color.Black;
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
            this.pnDownSize.BackColor = System.Drawing.Color.Black;
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
            this.pnBothSize.BackColor = System.Drawing.Color.Black;
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
            this.panel2.Controls.Add(this.tbSerial);
            this.panel2.Location = new System.Drawing.Point(0, 581);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(210, 109);
            this.panel2.TabIndex = 4;
            // 
            // tbSerial
            // 
            this.tbSerial.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.tbSerial.ColumnCount = 2;
            this.tbSerial.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tbSerial.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tbSerial.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 56.62651F));
            this.tbSerial.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 43.37349F));
            this.tbSerial.ContextMenuStrip = this.contextMenuStrip1;
            this.tbSerial.Controls.Add(this.label2, 0, 1);
            this.tbSerial.Controls.Add(this.label1, 0, 0);
            this.tbSerial.Controls.Add(this.lbSerialStatus, 1, 0);
            this.tbSerial.Controls.Add(this.lbSerialStatus2, 1, 1);
            this.tbSerial.Location = new System.Drawing.Point(31, 24);
            this.tbSerial.Name = "tbSerial";
            this.tbSerial.RowCount = 2;
            this.tbSerial.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tbSerial.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tbSerial.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tbSerial.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tbSerial.Size = new System.Drawing.Size(166, 69);
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
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(3, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 35);
            this.label2.TabIndex = 2;
            this.label2.Text = "Serial Imp:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Serial Ind:";
            // 
            // lbSerialStatus
            // 
            this.lbSerialStatus.AutoSize = true;
            this.lbSerialStatus.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lbSerialStatus.ForeColor = System.Drawing.Color.White;
            this.lbSerialStatus.Location = new System.Drawing.Point(86, 0);
            this.lbSerialStatus.Name = "lbSerialStatus";
            this.lbSerialStatus.Size = new System.Drawing.Size(15, 20);
            this.lbSerialStatus.TabIndex = 1;
            this.lbSerialStatus.Text = "-";
            // 
            // lbSerialStatus2
            // 
            this.lbSerialStatus2.AutoSize = true;
            this.lbSerialStatus2.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lbSerialStatus2.ForeColor = System.Drawing.Color.White;
            this.lbSerialStatus2.Location = new System.Drawing.Point(86, 34);
            this.lbSerialStatus2.Name = "lbSerialStatus2";
            this.lbSerialStatus2.Size = new System.Drawing.Size(15, 20);
            this.lbSerialStatus2.TabIndex = 3;
            this.lbSerialStatus2.Text = "-";
            // 
            // MainForms
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1143, 690);
            this.Controls.Add(this.pnBothSize);
            this.Controls.Add(this.pnDownSize);
            this.Controls.Add(this.pnRightSize);
            this.Controls.Add(this.sidebar);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.pnSubMenu);
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
            this.sidebar.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pcb_logo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MenuExpand)).EndInit();
            this.pnSubMenu.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.tbSerial.ResumeLayout(false);
            this.tbSerial.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
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
        private System.Windows.Forms.Label lbSerialStatus;
        private System.Windows.Forms.TableLayoutPanel tbSerial;
        private System.Windows.Forms.Button btnPesagem;
        private System.Windows.Forms.Button btnCadastro;
        private System.Windows.Forms.Button btnProduto;
        private System.Windows.Forms.Button btnBandeja;
        private System.Windows.Forms.Button btnRecipiente;
        private System.Windows.Forms.Button btnReceita;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lbSerialStatus2;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem btnSerialConfig;
        private System.Windows.Forms.Button btnTipoReceita;
        private System.Windows.Forms.Button btnManual;
    }
}
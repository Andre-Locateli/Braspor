namespace Main.View.PagesFolder.Configuration
{
    partial class RedeConfigForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RedeConfigForm));
            this.tvRede = new System.Windows.Forms.TreeView();
            this.MenuItem = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.btnNovaBalança = new System.Windows.Forms.ToolStripMenuItem();
            this.btnNovaImpressora = new System.Windows.Forms.ToolStripMenuItem();
            this.btnEditar = new System.Windows.Forms.ToolStripMenuItem();
            this.btnExcluir = new System.Windows.Forms.ToolStripMenuItem();
            this.pnBalança = new System.Windows.Forms.GroupBox();
            this.txtIP4 = new System.Windows.Forms.TextBox();
            this.txtIP3 = new System.Windows.Forms.TextBox();
            this.txtIP2 = new System.Windows.Forms.TextBox();
            this.txtIP1 = new System.Windows.Forms.TextBox();
            this.btnSalvar = new System.Windows.Forms.Button();
            this.txtPorta = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtNome = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbProtocolo = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cbModelo = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbFabricante = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.ListaIcones = new System.Windows.Forms.ImageList(this.components);
            this.pnImpressora = new System.Windows.Forms.GroupBox();
            this.label20 = new System.Windows.Forms.Label();
            this.pEthernet = new System.Windows.Forms.Panel();
            this.txtIPIMP1 = new System.Windows.Forms.TextBox();
            this.txtIPIMP4 = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtIPIMP3 = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtIPIMP2 = new System.Windows.Forms.TextBox();
            this.txtPortaImp = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.cbTipoImp = new System.Windows.Forms.ComboBox();
            this.btnSalvarImp = new System.Windows.Forms.Button();
            this.txtNomeImp = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.pInstalled = new System.Windows.Forms.Panel();
            this.chkSimples = new System.Windows.Forms.CheckBox();
            this.label21 = new System.Windows.Forms.Label();
            this.cbImpressoras = new System.Windows.Forms.ComboBox();
            this.pProtocolo = new System.Windows.Forms.Panel();
            this.cbFabricanteImp = new System.Windows.Forms.ComboBox();
            this.label15 = new System.Windows.Forms.Label();
            this.cbModeloImp = new System.Windows.Forms.ComboBox();
            this.label14 = new System.Windows.Forms.Label();
            this.cbProtocoloImp = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtEndereco = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.MenuItem.SuspendLayout();
            this.pnBalança.SuspendLayout();
            this.pnImpressora.SuspendLayout();
            this.pEthernet.SuspendLayout();
            this.pInstalled.SuspendLayout();
            this.pProtocolo.SuspendLayout();
            this.SuspendLayout();
            // 
            // tvRede
            // 
            this.tvRede.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.tvRede.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.tvRede.Location = new System.Drawing.Point(47, 58);
            this.tvRede.Name = "tvRede";
            this.tvRede.Size = new System.Drawing.Size(288, 320);
            this.tvRede.TabIndex = 0;
            this.tvRede.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.tvRede_MouseDoubleClick);
            this.tvRede.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tvRede_MouseDown);
            // 
            // MenuItem
            // 
            this.MenuItem.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNovaBalança,
            this.btnNovaImpressora,
            this.btnEditar,
            this.btnExcluir});
            this.MenuItem.Name = "MenuItem";
            this.MenuItem.Size = new System.Drawing.Size(164, 92);
            this.MenuItem.Opening += new System.ComponentModel.CancelEventHandler(this.MenuItem_Opening);
            // 
            // btnNovaBalança
            // 
            this.btnNovaBalança.Name = "btnNovaBalança";
            this.btnNovaBalança.Size = new System.Drawing.Size(163, 22);
            this.btnNovaBalança.Text = "Nova Balança";
            this.btnNovaBalança.Click += new System.EventHandler(this.btnNovaBalança_Click);
            // 
            // btnNovaImpressora
            // 
            this.btnNovaImpressora.Name = "btnNovaImpressora";
            this.btnNovaImpressora.Size = new System.Drawing.Size(163, 22);
            this.btnNovaImpressora.Text = "Nova Impressora";
            this.btnNovaImpressora.Click += new System.EventHandler(this.btnNovaImpressora_Click);
            // 
            // btnEditar
            // 
            this.btnEditar.Name = "btnEditar";
            this.btnEditar.Size = new System.Drawing.Size(163, 22);
            this.btnEditar.Text = "Editar";
            this.btnEditar.Click += new System.EventHandler(this.btnEditar_Click);
            // 
            // btnExcluir
            // 
            this.btnExcluir.Name = "btnExcluir";
            this.btnExcluir.Size = new System.Drawing.Size(163, 22);
            this.btnExcluir.Text = "Excluir";
            this.btnExcluir.Click += new System.EventHandler(this.btnExcluir_Click);
            // 
            // pnBalança
            // 
            this.pnBalança.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnBalança.Controls.Add(this.txtEndereco);
            this.pnBalança.Controls.Add(this.label22);
            this.pnBalança.Controls.Add(this.txtIP4);
            this.pnBalança.Controls.Add(this.txtIP3);
            this.pnBalança.Controls.Add(this.txtIP2);
            this.pnBalança.Controls.Add(this.txtIP1);
            this.pnBalança.Controls.Add(this.btnSalvar);
            this.pnBalança.Controls.Add(this.txtPorta);
            this.pnBalança.Controls.Add(this.label6);
            this.pnBalança.Controls.Add(this.label5);
            this.pnBalança.Controls.Add(this.txtNome);
            this.pnBalança.Controls.Add(this.label4);
            this.pnBalança.Controls.Add(this.cbProtocolo);
            this.pnBalança.Controls.Add(this.label3);
            this.pnBalança.Controls.Add(this.label2);
            this.pnBalança.Controls.Add(this.cbModelo);
            this.pnBalança.Controls.Add(this.label1);
            this.pnBalança.Controls.Add(this.cbFabricante);
            this.pnBalança.Controls.Add(this.label7);
            this.pnBalança.Controls.Add(this.label9);
            this.pnBalança.Controls.Add(this.label8);
            this.pnBalança.Location = new System.Drawing.Point(379, 58);
            this.pnBalança.Name = "pnBalança";
            this.pnBalança.Size = new System.Drawing.Size(376, 320);
            this.pnBalança.TabIndex = 1;
            this.pnBalança.TabStop = false;
            this.pnBalança.Text = "Informações Balança";
            // 
            // txtIP4
            // 
            this.txtIP4.Location = new System.Drawing.Point(114, 200);
            this.txtIP4.Name = "txtIP4";
            this.txtIP4.Size = new System.Drawing.Size(26, 20);
            this.txtIP4.TabIndex = 7;
            this.txtIP4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtIP4.Visible = false;
            // 
            // txtIP3
            // 
            this.txtIP3.Location = new System.Drawing.Point(84, 200);
            this.txtIP3.Name = "txtIP3";
            this.txtIP3.Size = new System.Drawing.Size(26, 20);
            this.txtIP3.TabIndex = 6;
            this.txtIP3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtIP3.Visible = false;
            // 
            // txtIP2
            // 
            this.txtIP2.Location = new System.Drawing.Point(54, 200);
            this.txtIP2.Name = "txtIP2";
            this.txtIP2.Size = new System.Drawing.Size(26, 20);
            this.txtIP2.TabIndex = 5;
            this.txtIP2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtIP2.Visible = false;
            // 
            // txtIP1
            // 
            this.txtIP1.Location = new System.Drawing.Point(24, 200);
            this.txtIP1.Name = "txtIP1";
            this.txtIP1.Size = new System.Drawing.Size(26, 20);
            this.txtIP1.TabIndex = 4;
            this.txtIP1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtIP1.Visible = false;
            // 
            // btnSalvar
            // 
            this.btnSalvar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSalvar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnSalvar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSalvar.Location = new System.Drawing.Point(15, 270);
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.Size = new System.Drawing.Size(266, 33);
            this.btnSalvar.TabIndex = 9;
            this.btnSalvar.Text = "Salvar";
            this.btnSalvar.UseVisualStyleBackColor = true;
            this.btnSalvar.Click += new System.EventHandler(this.btnSalvar_Click);
            // 
            // txtPorta
            // 
            this.txtPorta.Location = new System.Drawing.Point(169, 200);
            this.txtPorta.Name = "txtPorta";
            this.txtPorta.Size = new System.Drawing.Size(49, 20);
            this.txtPorta.TabIndex = 8;
            this.txtPorta.Visible = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(166, 184);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(32, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "Porta";
            this.label6.Visible = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(21, 184);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(17, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "IP";
            this.label5.Visible = false;
            // 
            // txtNome
            // 
            this.txtNome.Location = new System.Drawing.Point(169, 95);
            this.txtNome.Name = "txtNome";
            this.txtNome.Size = new System.Drawing.Size(121, 20);
            this.txtNome.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(169, 79);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Nome Balança";
            // 
            // cbProtocolo
            // 
            this.cbProtocolo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbProtocolo.FormattingEnabled = true;
            this.cbProtocolo.Location = new System.Drawing.Point(21, 95);
            this.cbProtocolo.Name = "cbProtocolo";
            this.cbProtocolo.Size = new System.Drawing.Size(133, 21);
            this.cbProtocolo.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(21, 79);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Protocolo";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(169, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Modelo";
            // 
            // cbModelo
            // 
            this.cbModelo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbModelo.FormattingEnabled = true;
            this.cbModelo.Location = new System.Drawing.Point(169, 46);
            this.cbModelo.Name = "cbModelo";
            this.cbModelo.Size = new System.Drawing.Size(121, 21);
            this.cbModelo.TabIndex = 1;
            this.cbModelo.SelectedIndexChanged += new System.EventHandler(this.cbModelo_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Fabricante";
            // 
            // cbFabricante
            // 
            this.cbFabricante.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFabricante.FormattingEnabled = true;
            this.cbFabricante.Items.AddRange(new object[] {
            "AEPH do Brasil"});
            this.cbFabricante.Location = new System.Drawing.Point(21, 46);
            this.cbFabricante.Name = "cbFabricante";
            this.cbFabricante.Size = new System.Drawing.Size(133, 21);
            this.cbFabricante.TabIndex = 0;
            this.cbFabricante.SelectedIndexChanged += new System.EventHandler(this.cbFabricante_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(47, 207);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(10, 13);
            this.label7.TabIndex = 17;
            this.label7.Text = ".";
            this.label7.Visible = false;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(107, 208);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(10, 13);
            this.label9.TabIndex = 19;
            this.label9.Text = ".";
            this.label9.Visible = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(77, 208);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(10, 13);
            this.label8.TabIndex = 18;
            this.label8.Text = ".";
            this.label8.Visible = false;
            // 
            // ListaIcones
            // 
            this.ListaIcones.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ListaIcones.ImageStream")));
            this.ListaIcones.TransparentColor = System.Drawing.Color.Transparent;
            this.ListaIcones.Images.SetKeyName(0, "workstation_50px_2.png");
            this.ListaIcones.Images.SetKeyName(1, "Industrial Scales_50px_2.png");
            this.ListaIcones.Images.SetKeyName(2, "print_50px.png");
            // 
            // pnImpressora
            // 
            this.pnImpressora.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnImpressora.Controls.Add(this.label20);
            this.pnImpressora.Controls.Add(this.pEthernet);
            this.pnImpressora.Controls.Add(this.cbTipoImp);
            this.pnImpressora.Controls.Add(this.btnSalvarImp);
            this.pnImpressora.Controls.Add(this.txtNomeImp);
            this.pnImpressora.Controls.Add(this.label12);
            this.pnImpressora.Controls.Add(this.pInstalled);
            this.pnImpressora.Controls.Add(this.pProtocolo);
            this.pnImpressora.Location = new System.Drawing.Point(379, 58);
            this.pnImpressora.Name = "pnImpressora";
            this.pnImpressora.Size = new System.Drawing.Size(376, 320);
            this.pnImpressora.TabIndex = 20;
            this.pnImpressora.TabStop = false;
            this.pnImpressora.Text = "Informações Impressora";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(12, 30);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(79, 13);
            this.label20.TabIndex = 21;
            this.label20.Text = "Tipo Impressão";
            // 
            // pEthernet
            // 
            this.pEthernet.Controls.Add(this.txtIPIMP1);
            this.pEthernet.Controls.Add(this.txtIPIMP4);
            this.pEthernet.Controls.Add(this.label11);
            this.pEthernet.Controls.Add(this.txtIPIMP3);
            this.pEthernet.Controls.Add(this.label10);
            this.pEthernet.Controls.Add(this.txtIPIMP2);
            this.pEthernet.Controls.Add(this.txtPortaImp);
            this.pEthernet.Controls.Add(this.label18);
            this.pEthernet.Controls.Add(this.label17);
            this.pEthernet.Controls.Add(this.label16);
            this.pEthernet.Location = new System.Drawing.Point(15, 209);
            this.pEthernet.Name = "pEthernet";
            this.pEthernet.Size = new System.Drawing.Size(266, 42);
            this.pEthernet.TabIndex = 22;
            this.pEthernet.Visible = false;
            // 
            // txtIPIMP1
            // 
            this.txtIPIMP1.Location = new System.Drawing.Point(0, 20);
            this.txtIPIMP1.Name = "txtIPIMP1";
            this.txtIPIMP1.Size = new System.Drawing.Size(26, 20);
            this.txtIPIMP1.TabIndex = 4;
            this.txtIPIMP1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtIPIMP4
            // 
            this.txtIPIMP4.Location = new System.Drawing.Point(90, 20);
            this.txtIPIMP4.Name = "txtIPIMP4";
            this.txtIPIMP4.Size = new System.Drawing.Size(26, 20);
            this.txtIPIMP4.TabIndex = 7;
            this.txtIPIMP4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(-3, 4);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(17, 13);
            this.label11.TabIndex = 8;
            this.label11.Text = "IP";
            // 
            // txtIPIMP3
            // 
            this.txtIPIMP3.Location = new System.Drawing.Point(60, 20);
            this.txtIPIMP3.Name = "txtIPIMP3";
            this.txtIPIMP3.Size = new System.Drawing.Size(26, 20);
            this.txtIPIMP3.TabIndex = 6;
            this.txtIPIMP3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(142, 4);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(32, 13);
            this.label10.TabIndex = 10;
            this.label10.Text = "Porta";
            // 
            // txtIPIMP2
            // 
            this.txtIPIMP2.Location = new System.Drawing.Point(30, 20);
            this.txtIPIMP2.Name = "txtIPIMP2";
            this.txtIPIMP2.Size = new System.Drawing.Size(26, 20);
            this.txtIPIMP2.TabIndex = 5;
            this.txtIPIMP2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtPortaImp
            // 
            this.txtPortaImp.Location = new System.Drawing.Point(145, 20);
            this.txtPortaImp.Name = "txtPortaImp";
            this.txtPortaImp.Size = new System.Drawing.Size(49, 20);
            this.txtPortaImp.TabIndex = 8;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(53, 28);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(10, 13);
            this.label18.TabIndex = 18;
            this.label18.Text = ".";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(83, 28);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(10, 13);
            this.label17.TabIndex = 19;
            this.label17.Text = ".";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(23, 27);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(10, 13);
            this.label16.TabIndex = 17;
            this.label16.Text = ".";
            // 
            // cbTipoImp
            // 
            this.cbTipoImp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTipoImp.FormattingEnabled = true;
            this.cbTipoImp.Items.AddRange(new object[] {
            "Via drivers do Windows",
            "Via porta Serial",
            "Via porta TCP/IP"});
            this.cbTipoImp.Location = new System.Drawing.Point(15, 46);
            this.cbTipoImp.Name = "cbTipoImp";
            this.cbTipoImp.Size = new System.Drawing.Size(266, 21);
            this.cbTipoImp.TabIndex = 20;
            this.cbTipoImp.SelectedIndexChanged += new System.EventHandler(this.cbTipoImp_SelectedIndexChanged);
            // 
            // btnSalvarImp
            // 
            this.btnSalvarImp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSalvarImp.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSalvarImp.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnSalvarImp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSalvarImp.Location = new System.Drawing.Point(15, 270);
            this.btnSalvarImp.Name = "btnSalvarImp";
            this.btnSalvarImp.Size = new System.Drawing.Size(266, 33);
            this.btnSalvarImp.TabIndex = 9;
            this.btnSalvarImp.Text = "Salvar";
            this.btnSalvarImp.UseVisualStyleBackColor = true;
            this.btnSalvarImp.Click += new System.EventHandler(this.btnSalvarImp_Click);
            // 
            // txtNomeImp
            // 
            this.txtNomeImp.Location = new System.Drawing.Point(15, 91);
            this.txtNomeImp.Name = "txtNomeImp";
            this.txtNomeImp.Size = new System.Drawing.Size(266, 20);
            this.txtNomeImp.TabIndex = 3;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(12, 75);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(89, 13);
            this.label12.TabIndex = 6;
            this.label12.Text = "Nome Impressora";
            // 
            // pInstalled
            // 
            this.pInstalled.Controls.Add(this.chkSimples);
            this.pInstalled.Controls.Add(this.label21);
            this.pInstalled.Controls.Add(this.cbImpressoras);
            this.pInstalled.Location = new System.Drawing.Point(15, 115);
            this.pInstalled.Name = "pInstalled";
            this.pInstalled.Size = new System.Drawing.Size(266, 78);
            this.pInstalled.TabIndex = 23;
            this.pInstalled.Visible = false;
            // 
            // chkSimples
            // 
            this.chkSimples.AutoSize = true;
            this.chkSimples.Location = new System.Drawing.Point(0, 52);
            this.chkSimples.Name = "chkSimples";
            this.chkSimples.Size = new System.Drawing.Size(152, 17);
            this.chkSimples.TabIndex = 24;
            this.chkSimples.Text = "Enviar dados Simplificados";
            this.chkSimples.UseVisualStyleBackColor = true;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(-3, 4);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(114, 13);
            this.label21.TabIndex = 23;
            this.label21.Text = "Impressoras Instaladas";
            // 
            // cbImpressoras
            // 
            this.cbImpressoras.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbImpressoras.FormattingEnabled = true;
            this.cbImpressoras.Location = new System.Drawing.Point(0, 20);
            this.cbImpressoras.Name = "cbImpressoras";
            this.cbImpressoras.Size = new System.Drawing.Size(266, 21);
            this.cbImpressoras.TabIndex = 22;
            // 
            // pProtocolo
            // 
            this.pProtocolo.Controls.Add(this.cbFabricanteImp);
            this.pProtocolo.Controls.Add(this.label15);
            this.pProtocolo.Controls.Add(this.cbModeloImp);
            this.pProtocolo.Controls.Add(this.label14);
            this.pProtocolo.Controls.Add(this.cbProtocoloImp);
            this.pProtocolo.Controls.Add(this.label13);
            this.pProtocolo.Location = new System.Drawing.Point(15, 115);
            this.pProtocolo.Name = "pProtocolo";
            this.pProtocolo.Size = new System.Drawing.Size(266, 90);
            this.pProtocolo.TabIndex = 21;
            this.pProtocolo.Visible = false;
            // 
            // cbFabricanteImp
            // 
            this.cbFabricanteImp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFabricanteImp.FormattingEnabled = true;
            this.cbFabricanteImp.Items.AddRange(new object[] {
            "Zebra"});
            this.cbFabricanteImp.Location = new System.Drawing.Point(0, 22);
            this.cbFabricanteImp.Name = "cbFabricanteImp";
            this.cbFabricanteImp.Size = new System.Drawing.Size(121, 21);
            this.cbFabricanteImp.TabIndex = 0;
            this.cbFabricanteImp.SelectedIndexChanged += new System.EventHandler(this.cbFabrincateImp_SelectedIndexChanged);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(-3, 6);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(57, 13);
            this.label15.TabIndex = 1;
            this.label15.Text = "Fabricante";
            // 
            // cbModeloImp
            // 
            this.cbModeloImp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbModeloImp.FormattingEnabled = true;
            this.cbModeloImp.Location = new System.Drawing.Point(145, 22);
            this.cbModeloImp.Name = "cbModeloImp";
            this.cbModeloImp.Size = new System.Drawing.Size(121, 21);
            this.cbModeloImp.TabIndex = 1;
            this.cbModeloImp.SelectedIndexChanged += new System.EventHandler(this.cbModeloImp_SelectedIndexChanged);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(142, 6);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(42, 13);
            this.label14.TabIndex = 3;
            this.label14.Text = "Modelo";
            // 
            // cbProtocoloImp
            // 
            this.cbProtocoloImp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbProtocoloImp.FormattingEnabled = true;
            this.cbProtocoloImp.Location = new System.Drawing.Point(0, 67);
            this.cbProtocoloImp.Name = "cbProtocoloImp";
            this.cbProtocoloImp.Size = new System.Drawing.Size(121, 21);
            this.cbProtocoloImp.TabIndex = 2;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(-3, 51);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(52, 13);
            this.label13.TabIndex = 4;
            this.label13.Text = "Protocolo";
            // 
            // txtEndereco
            // 
            this.txtEndereco.Location = new System.Drawing.Point(21, 149);
            this.txtEndereco.Name = "txtEndereco";
            this.txtEndereco.Size = new System.Drawing.Size(121, 20);
            this.txtEndereco.TabIndex = 22;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(21, 133);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(53, 13);
            this.label22.TabIndex = 23;
            this.label22.Text = "Endereço";
            // 
            // RedeConfigForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tvRede);
            this.Controls.Add(this.pnBalança);
            this.Controls.Add(this.pnImpressora);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "RedeConfigForm";
            this.Text = "BalancaConfigForm";
            this.VisibleChanged += new System.EventHandler(this.RedeConfigForm_VisibleChanged);
            this.MenuItem.ResumeLayout(false);
            this.pnBalança.ResumeLayout(false);
            this.pnBalança.PerformLayout();
            this.pnImpressora.ResumeLayout(false);
            this.pnImpressora.PerformLayout();
            this.pEthernet.ResumeLayout(false);
            this.pEthernet.PerformLayout();
            this.pInstalled.ResumeLayout(false);
            this.pInstalled.PerformLayout();
            this.pProtocolo.ResumeLayout(false);
            this.pProtocolo.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView tvRede;
        private System.Windows.Forms.ContextMenuStrip MenuItem;
        private System.Windows.Forms.ToolStripMenuItem btnNovaBalança;
        private System.Windows.Forms.ToolStripMenuItem btnExcluir;
        private System.Windows.Forms.GroupBox pnBalança;
        private System.Windows.Forms.ComboBox cbFabricante;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbModelo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbProtocolo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtNome;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnSalvar;
        private System.Windows.Forms.TextBox txtPorta;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ImageList ListaIcones;
        private System.Windows.Forms.ToolStripMenuItem btnEditar;
        private System.Windows.Forms.TextBox txtIP4;
        private System.Windows.Forms.TextBox txtIP3;
        private System.Windows.Forms.TextBox txtIP2;
        private System.Windows.Forms.TextBox txtIP1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox pnImpressora;
        private System.Windows.Forms.TextBox txtIPIMP4;
        private System.Windows.Forms.TextBox txtIPIMP3;
        private System.Windows.Forms.TextBox txtIPIMP2;
        private System.Windows.Forms.TextBox txtIPIMP1;
        private System.Windows.Forms.Button btnSalvarImp;
        private System.Windows.Forms.TextBox txtPortaImp;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtNomeImp;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox cbProtocoloImp;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ComboBox cbModeloImp;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.ComboBox cbFabricanteImp;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.ToolStripMenuItem btnNovaImpressora;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.ComboBox cbTipoImp;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.ComboBox cbImpressoras;
        private System.Windows.Forms.Panel pProtocolo;
        private System.Windows.Forms.Panel pEthernet;
        private System.Windows.Forms.Panel pInstalled;
        private System.Windows.Forms.CheckBox chkSimples;
        private System.Windows.Forms.TextBox txtEndereco;
        private System.Windows.Forms.Label label22;
    }
}
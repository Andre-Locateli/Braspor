namespace Main.View.PagesFolder.ProcessFolder
{
    partial class EscolhaPesagemForms
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
            this.panel3 = new System.Windows.Forms.Panel();
            this.lbl_QtMateriaPrima = new System.Windows.Forms.Label();
            this.X = new System.Windows.Forms.Label();
            this.btn_Confirmar = new System.Windows.Forms.Button();
            this.cb_MateriaPrima = new System.Windows.Forms.ComboBox();
            this.lbl = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label17 = new System.Windows.Forms.Label();
            this.txt_Descricao = new System.Windows.Forms.TextBox();
            this.panel17 = new System.Windows.Forms.Panel();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.White;
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.lbl_QtMateriaPrima);
            this.panel3.Controls.Add(this.X);
            this.panel3.Controls.Add(this.btn_Confirmar);
            this.panel3.Controls.Add(this.cb_MateriaPrima);
            this.panel3.Controls.Add(this.lbl);
            this.panel3.Controls.Add(this.panel4);
            this.panel3.Controls.Add(this.label17);
            this.panel3.Controls.Add(this.txt_Descricao);
            this.panel3.Controls.Add(this.panel17);
            this.panel3.Location = new System.Drawing.Point(12, 12);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(419, 335);
            this.panel3.TabIndex = 124;
            this.panel3.Paint += new System.Windows.Forms.PaintEventHandler(this.panel3_Paint);
            // 
            // lbl_QtMateriaPrima
            // 
            this.lbl_QtMateriaPrima.Font = new System.Drawing.Font("Segoe UI Black", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_QtMateriaPrima.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lbl_QtMateriaPrima.Location = new System.Drawing.Point(39, 88);
            this.lbl_QtMateriaPrima.Name = "lbl_QtMateriaPrima";
            this.lbl_QtMateriaPrima.Size = new System.Drawing.Size(347, 45);
            this.lbl_QtMateriaPrima.TabIndex = 149;
            this.lbl_QtMateriaPrima.Text = "Quantidade mínima para referência: 1000";
            this.lbl_QtMateriaPrima.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lbl_QtMateriaPrima.Visible = false;
            // 
            // X
            // 
            this.X.AutoSize = true;
            this.X.BackColor = System.Drawing.Color.Transparent;
            this.X.Cursor = System.Windows.Forms.Cursors.Hand;
            this.X.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.X.Font = new System.Drawing.Font("Microsoft Yi Baiti", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.X.ForeColor = System.Drawing.Color.Gray;
            this.X.Location = new System.Drawing.Point(388, -5);
            this.X.Name = "X";
            this.X.Size = new System.Drawing.Size(25, 29);
            this.X.TabIndex = 148;
            this.X.Text = "x";
            this.X.Click += new System.EventHandler(this.X_Click);
            // 
            // btn_Confirmar
            // 
            this.btn_Confirmar.BackColor = System.Drawing.Color.White;
            this.btn_Confirmar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_Confirmar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Confirmar.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Confirmar.ForeColor = System.Drawing.Color.Green;
            this.btn_Confirmar.Location = new System.Drawing.Point(139, 285);
            this.btn_Confirmar.Name = "btn_Confirmar";
            this.btn_Confirmar.Size = new System.Drawing.Size(139, 37);
            this.btn_Confirmar.TabIndex = 147;
            this.btn_Confirmar.Text = "CONFIRMAR";
            this.btn_Confirmar.UseVisualStyleBackColor = false;
            this.btn_Confirmar.Click += new System.EventHandler(this.btn_Confirmar_Click);
            // 
            // cb_MateriaPrima
            // 
            this.cb_MateriaPrima.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cb_MateriaPrima.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cb_MateriaPrima.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_MateriaPrima.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.cb_MateriaPrima.FormattingEnabled = true;
            this.cb_MateriaPrima.Location = new System.Drawing.Point(39, 54);
            this.cb_MateriaPrima.Name = "cb_MateriaPrima";
            this.cb_MateriaPrima.Size = new System.Drawing.Size(347, 29);
            this.cb_MateriaPrima.TabIndex = 146;
            this.cb_MateriaPrima.Tag = "";
            this.cb_MateriaPrima.SelectedIndexChanged += new System.EventHandler(this.cb_MateriaPrima_SelectedIndexChanged);
            this.cb_MateriaPrima.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cb_MateriaPrima_KeyDown);
            // 
            // lbl
            // 
            this.lbl.AutoSize = true;
            this.lbl.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold);
            this.lbl.Location = new System.Drawing.Point(117, 31);
            this.lbl.Name = "lbl";
            this.lbl.Size = new System.Drawing.Size(200, 20);
            this.lbl.TabIndex = 123;
            this.lbl.Text = "Selecione a Matéria-prima *";
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.Silver;
            this.panel4.Location = new System.Drawing.Point(39, 83);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(347, 2);
            this.panel4.TabIndex = 125;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold);
            this.label17.Location = new System.Drawing.Point(141, 133);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(148, 20);
            this.label17.TabIndex = 122;
            this.label17.Text = "Descrição (opcional)";
            // 
            // txt_Descricao
            // 
            this.txt_Descricao.BackColor = System.Drawing.Color.White;
            this.txt_Descricao.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_Descricao.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.txt_Descricao.Location = new System.Drawing.Point(37, 156);
            this.txt_Descricao.Multiline = true;
            this.txt_Descricao.Name = "txt_Descricao";
            this.txt_Descricao.Size = new System.Drawing.Size(347, 102);
            this.txt_Descricao.TabIndex = 105;
            // 
            // panel17
            // 
            this.panel17.BackColor = System.Drawing.Color.Silver;
            this.panel17.Location = new System.Drawing.Point(37, 258);
            this.panel17.Name = "panel17";
            this.panel17.Size = new System.Drawing.Size(347, 2);
            this.panel17.TabIndex = 106;
            // 
            // EscolhaPesagemForms
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.ClientSize = new System.Drawing.Size(443, 359);
            this.ControlBox = false;
            this.Controls.Add(this.panel3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "EscolhaPesagemForms";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Load += new System.EventHandler(this.EscolhaPesagemForms_Load);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.ComboBox cb_MateriaPrima;
        private System.Windows.Forms.Label lbl;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox txt_Descricao;
        private System.Windows.Forms.Panel panel17;
        private System.Windows.Forms.Label X;
        private System.Windows.Forms.Button btn_Confirmar;
        private System.Windows.Forms.Label lbl_QtMateriaPrima;
    }
}
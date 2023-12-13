namespace Main.View.CustomLayout
{
    partial class CardBalanca
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lbl_name_balanca = new System.Windows.Forms.Label();
            this.lbl_peso_indicador = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cartesianChart1 = new LiveCharts.WinForms.CartesianChart();
            this.lblInfo = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // lbl_name_balanca
            // 
            this.lbl_name_balanca.BackColor = System.Drawing.Color.Transparent;
            this.lbl_name_balanca.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_name_balanca.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_name_balanca.ForeColor = System.Drawing.Color.White;
            this.lbl_name_balanca.Location = new System.Drawing.Point(0, 0);
            this.lbl_name_balanca.Name = "lbl_name_balanca";
            this.lbl_name_balanca.Size = new System.Drawing.Size(238, 66);
            this.lbl_name_balanca.TabIndex = 0;
            this.lbl_name_balanca.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_peso_indicador
            // 
            this.lbl_peso_indicador.BackColor = System.Drawing.Color.Transparent;
            this.lbl_peso_indicador.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_peso_indicador.Font = new System.Drawing.Font("Verdana", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_peso_indicador.ForeColor = System.Drawing.Color.White;
            this.lbl_peso_indicador.Location = new System.Drawing.Point(0, 66);
            this.lbl_peso_indicador.Name = "lbl_peso_indicador";
            this.lbl_peso_indicador.Size = new System.Drawing.Size(238, 84);
            this.lbl_peso_indicador.TabIndex = 1;
            this.lbl_peso_indicador.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 763);
            this.panel1.Margin = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.panel1.Size = new System.Drawing.Size(238, 9);
            this.panel1.TabIndex = 5;
            // 
            // cartesianChart1
            // 
            this.cartesianChart1.BackColor = System.Drawing.Color.Transparent;
            this.cartesianChart1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.cartesianChart1.Location = new System.Drawing.Point(0, 531);
            this.cartesianChart1.Margin = new System.Windows.Forms.Padding(0);
            this.cartesianChart1.Name = "cartesianChart1";
            this.cartesianChart1.Size = new System.Drawing.Size(238, 213);
            this.cartesianChart1.TabIndex = 24;
            this.cartesianChart1.Text = "-";
            // 
            // lblInfo
            // 
            this.lblInfo.BackColor = System.Drawing.Color.Transparent;
            this.lblInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblInfo.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInfo.ForeColor = System.Drawing.Color.White;
            this.lblInfo.Location = new System.Drawing.Point(0, 150);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(238, 231);
            this.lblInfo.TabIndex = 3;
            this.lblInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblStatus
            // 
            this.lblStatus.BackColor = System.Drawing.Color.Transparent;
            this.lblStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblStatus.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.ForeColor = System.Drawing.Color.White;
            this.lblStatus.Location = new System.Drawing.Point(0, 744);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(238, 19);
            this.lblStatus.TabIndex = 25;
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // comboBox1
            // 
            this.comboBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(17, 406);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(199, 21);
            this.comboBox1.TabIndex = 26;
            this.comboBox1.Visible = false;
            // 
            // CardBalanca
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(74)))), ((int)(((byte)(136)))));
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.cartesianChart1);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.lbl_peso_indicador);
            this.Controls.Add(this.lbl_name_balanca);
            this.DoubleBuffered = true;
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "CardBalanca";
            this.Size = new System.Drawing.Size(238, 772);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lbl_name_balanca;
        private System.Windows.Forms.Label lbl_peso_indicador;
        private System.Windows.Forms.Panel panel1;
        private LiveCharts.WinForms.CartesianChart cartesianChart1;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.ComboBox comboBox1;
    }
}

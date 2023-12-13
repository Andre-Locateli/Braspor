namespace Main.View.CustomLayout
{
    partial class PesagemRowInfo
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.f_l_box = new System.Windows.Forms.FlowLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.lblQtdReal = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblAlvo = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.f_l_box);
            this.panel1.Location = new System.Drawing.Point(9, 24);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(6);
            this.panel1.Size = new System.Drawing.Size(289, 32);
            this.panel1.TabIndex = 0;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // f_l_box
            // 
            this.f_l_box.Dock = System.Windows.Forms.DockStyle.Fill;
            this.f_l_box.Location = new System.Drawing.Point(6, 6);
            this.f_l_box.Name = "f_l_box";
            this.f_l_box.Size = new System.Drawing.Size(277, 20);
            this.f_l_box.TabIndex = 0;
            this.f_l_box.WrapContents = false;
            this.f_l_box.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(314, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 23);
            this.label2.TabIndex = 45;
            this.label2.Text = "Real";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel5
            // 
            this.panel5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(157)))), ((int)(((byte)(162)))), ((int)(((byte)(200)))));
            this.panel5.Controls.Add(this.lblQtdReal);
            this.panel5.Location = new System.Drawing.Point(316, 24);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(64, 31);
            this.panel5.TabIndex = 44;
            // 
            // lblQtdReal
            // 
            this.lblQtdReal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblQtdReal.Font = new System.Drawing.Font("Segoe UI Semibold", 14F, System.Drawing.FontStyle.Bold);
            this.lblQtdReal.ForeColor = System.Drawing.Color.White;
            this.lblQtdReal.Location = new System.Drawing.Point(0, 0);
            this.lblQtdReal.Name = "lblQtdReal";
            this.lblQtdReal.Size = new System.Drawing.Size(64, 31);
            this.lblQtdReal.TabIndex = 44;
            this.lblQtdReal.Text = "0";
            this.lblQtdReal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(393, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 23);
            this.label1.TabIndex = 47;
            this.label1.Text = "Alvo";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.Color.Black;
            this.panel2.Controls.Add(this.lblAlvo);
            this.panel2.Location = new System.Drawing.Point(395, 24);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(1);
            this.panel2.Size = new System.Drawing.Size(64, 31);
            this.panel2.TabIndex = 46;
            // 
            // lblAlvo
            // 
            this.lblAlvo.BackColor = System.Drawing.Color.White;
            this.lblAlvo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblAlvo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblAlvo.Font = new System.Drawing.Font("Segoe UI Semibold", 14F, System.Drawing.FontStyle.Bold);
            this.lblAlvo.ForeColor = System.Drawing.Color.Black;
            this.lblAlvo.Location = new System.Drawing.Point(1, 1);
            this.lblAlvo.Name = "lblAlvo";
            this.lblAlvo.Size = new System.Drawing.Size(62, 29);
            this.lblAlvo.TabIndex = 44;
            this.lblAlvo.Text = "10";
            this.lblAlvo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PesagemRowInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "PesagemRowInfo";
            this.Size = new System.Drawing.Size(473, 60);
            this.Resize += new System.EventHandler(this.PesagemRowInfo_Resize);
            this.panel1.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label lblQtdReal;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblAlvo;
        private System.Windows.Forms.FlowLayoutPanel f_l_box;
    }
}

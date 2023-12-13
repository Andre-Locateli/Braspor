namespace Main.View.CustomLayout
{
    partial class RowCardCustom
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
            this.lblObjeto = new System.Windows.Forms.Label();
            this.lblInfo = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblObjeto
            // 
            this.lblObjeto.AllowDrop = true;
            this.lblObjeto.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(228)))), ((int)(((byte)(255)))));
            this.lblObjeto.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblObjeto.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblObjeto.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(80)))), ((int)(((byte)(158)))));
            this.lblObjeto.Image = global::Main.Properties.Resources.arrowDown;
            this.lblObjeto.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblObjeto.Location = new System.Drawing.Point(0, 0);
            this.lblObjeto.Name = "lblObjeto";
            this.lblObjeto.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.lblObjeto.Size = new System.Drawing.Size(239, 32);
            this.lblObjeto.TabIndex = 0;
            this.lblObjeto.Text = "     Produto 01";
            this.lblObjeto.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblObjeto.Click += new System.EventHandler(this.lblObjeto_Click);
            this.lblObjeto.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblObjeto_MouseDown);
            // 
            // lblInfo
            // 
            this.lblInfo.AllowDrop = true;
            this.lblInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(228)))), ((int)(((byte)(255)))));
            this.lblInfo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblInfo.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(80)))), ((int)(((byte)(158)))));
            this.lblInfo.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblInfo.Location = new System.Drawing.Point(0, 32);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.lblInfo.Size = new System.Drawing.Size(239, 54);
            this.lblInfo.TabIndex = 1;
            this.lblInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // RowCardCustom
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(216)))), ((int)(((byte)(255)))));
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.lblObjeto);
            this.Name = "RowCardCustom";
            this.Size = new System.Drawing.Size(239, 32);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblObjeto;
        private System.Windows.Forms.Label lblInfo;
    }
}

﻿namespace Main.View.PagesFolder.Configuration
{
    partial class SerialForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SerialForm));
            this.label12 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSalvar = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.cbStopBit = new Main.View.CustomLayout.CTComboBox();
            this.cbBaudRate = new Main.View.CustomLayout.CTComboBox();
            this.cbPortaSerial = new Main.View.CustomLayout.CTComboBox();
            this.chkAutoconnect = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.cbPortaSerial2 = new Main.View.CustomLayout.CTComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbBaudRate2 = new Main.View.CustomLayout.CTComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cbStopBit2 = new Main.View.CustomLayout.CTComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cbParidade2 = new Main.View.CustomLayout.CTComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.chkAutoconnect2 = new System.Windows.Forms.CheckBox();
            this.btnConectar02 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.label12.ForeColor = System.Drawing.Color.Gray;
            this.label12.Location = new System.Drawing.Point(42, 82);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(102, 21);
            this.label12.TabIndex = 6;
            this.label12.Text = "Porta Serial:";
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(46)))), ((int)(((byte)(84)))));
            this.label5.Location = new System.Drawing.Point(104, 16);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(215, 61);
            this.label5.TabIndex = 12;
            this.label5.Text = "CONFIGURAÇÕES SERIAL INDICADOR";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.Gray;
            this.label1.Location = new System.Drawing.Point(42, 151);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 21);
            this.label1.TabIndex = 14;
            this.label1.Text = "BaudRate:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.Gray;
            this.label2.Location = new System.Drawing.Point(43, 221);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 21);
            this.label2.TabIndex = 16;
            this.label2.Text = "StopBit";
            // 
            // btnSalvar
            // 
            this.btnSalvar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSalvar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(190)))), ((int)(((byte)(187)))));
            this.btnSalvar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSalvar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSalvar.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnSalvar.ForeColor = System.Drawing.Color.White;
            this.btnSalvar.Location = new System.Drawing.Point(46, 413);
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.Size = new System.Drawing.Size(331, 37);
            this.btnSalvar.TabIndex = 18;
            this.btnSalvar.Text = "Conectar";
            this.btnSalvar.UseVisualStyleBackColor = false;
            this.btnSalvar.Click += new System.EventHandler(this.btnSalvarEtq_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(214)))), ((int)(((byte)(250)))));
            this.btnClose.FlatAppearance.BorderSize = 2;
            this.btnClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(214)))), ((int)(((byte)(250)))));
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.btnClose.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(97)))));
            this.btnClose.Location = new System.Drawing.Point(806, 2);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(25, 25);
            this.btnClose.TabIndex = 19;
            this.btnClose.Text = "X";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // cbStopBit
            // 
            this.cbStopBit.BackColor = System.Drawing.Color.White;
            this.cbStopBit.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.cbStopBit.BorderSize = 1;
            this.cbStopBit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbStopBit.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.cbStopBit.ForeColor = System.Drawing.Color.Black;
            this.cbStopBit.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(97)))));
            this.cbStopBit.ListBackColor = System.Drawing.Color.White;
            this.cbStopBit.ListTextColor = System.Drawing.Color.DimGray;
            this.cbStopBit.Location = new System.Drawing.Point(46, 245);
            this.cbStopBit.MinimumSize = new System.Drawing.Size(200, 30);
            this.cbStopBit.Name = "cbStopBit";
            this.cbStopBit.Padding = new System.Windows.Forms.Padding(1);
            this.cbStopBit.Size = new System.Drawing.Size(331, 30);
            this.cbStopBit.TabIndex = 15;
            this.cbStopBit.Texts = "";
            // 
            // cbBaudRate
            // 
            this.cbBaudRate.BackColor = System.Drawing.Color.White;
            this.cbBaudRate.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.cbBaudRate.BorderSize = 1;
            this.cbBaudRate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBaudRate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.cbBaudRate.ForeColor = System.Drawing.Color.Black;
            this.cbBaudRate.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(97)))));
            this.cbBaudRate.ListBackColor = System.Drawing.Color.White;
            this.cbBaudRate.ListTextColor = System.Drawing.Color.DimGray;
            this.cbBaudRate.Location = new System.Drawing.Point(46, 175);
            this.cbBaudRate.MinimumSize = new System.Drawing.Size(200, 30);
            this.cbBaudRate.Name = "cbBaudRate";
            this.cbBaudRate.Padding = new System.Windows.Forms.Padding(1);
            this.cbBaudRate.Size = new System.Drawing.Size(331, 30);
            this.cbBaudRate.TabIndex = 13;
            this.cbBaudRate.Texts = "";
            // 
            // cbPortaSerial
            // 
            this.cbPortaSerial.BackColor = System.Drawing.Color.White;
            this.cbPortaSerial.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.cbPortaSerial.BorderSize = 1;
            this.cbPortaSerial.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPortaSerial.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.cbPortaSerial.ForeColor = System.Drawing.Color.Black;
            this.cbPortaSerial.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(97)))));
            this.cbPortaSerial.ListBackColor = System.Drawing.Color.White;
            this.cbPortaSerial.ListTextColor = System.Drawing.Color.DimGray;
            this.cbPortaSerial.Location = new System.Drawing.Point(46, 106);
            this.cbPortaSerial.MinimumSize = new System.Drawing.Size(200, 30);
            this.cbPortaSerial.Name = "cbPortaSerial";
            this.cbPortaSerial.Padding = new System.Windows.Forms.Padding(1);
            this.cbPortaSerial.Size = new System.Drawing.Size(331, 30);
            this.cbPortaSerial.TabIndex = 5;
            this.cbPortaSerial.Texts = "";
            // 
            // chkAutoconnect
            // 
            this.chkAutoconnect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkAutoconnect.AutoSize = true;
            this.chkAutoconnect.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.chkAutoconnect.Location = new System.Drawing.Point(46, 369);
            this.chkAutoconnect.Name = "chkAutoconnect";
            this.chkAutoconnect.Size = new System.Drawing.Size(216, 25);
            this.chkAutoconnect.TabIndex = 20;
            this.chkAutoconnect.Text = "Conectar automáticamente";
            this.chkAutoconnect.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Silver;
            this.panel1.Location = new System.Drawing.Point(418, 22);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(2, 430);
            this.panel1.TabIndex = 21;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(46)))), ((int)(((byte)(84)))));
            this.label3.Location = new System.Drawing.Point(483, 16);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(289, 54);
            this.label3.TabIndex = 22;
            this.label3.Text = "CONFIGURAÇÕES SERIAL IMPRESSORA\r\n";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cbPortaSerial2
            // 
            this.cbPortaSerial2.BackColor = System.Drawing.Color.White;
            this.cbPortaSerial2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.cbPortaSerial2.BorderSize = 1;
            this.cbPortaSerial2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPortaSerial2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.cbPortaSerial2.ForeColor = System.Drawing.Color.Black;
            this.cbPortaSerial2.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(97)))));
            this.cbPortaSerial2.ListBackColor = System.Drawing.Color.White;
            this.cbPortaSerial2.ListTextColor = System.Drawing.Color.DimGray;
            this.cbPortaSerial2.Location = new System.Drawing.Point(462, 106);
            this.cbPortaSerial2.MinimumSize = new System.Drawing.Size(200, 30);
            this.cbPortaSerial2.Name = "cbPortaSerial2";
            this.cbPortaSerial2.Padding = new System.Windows.Forms.Padding(1);
            this.cbPortaSerial2.Size = new System.Drawing.Size(331, 30);
            this.cbPortaSerial2.TabIndex = 23;
            this.cbPortaSerial2.Texts = "";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.label4.ForeColor = System.Drawing.Color.Gray;
            this.label4.Location = new System.Drawing.Point(458, 82);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(102, 21);
            this.label4.TabIndex = 24;
            this.label4.Text = "Porta Serial:";
            // 
            // cbBaudRate2
            // 
            this.cbBaudRate2.BackColor = System.Drawing.Color.White;
            this.cbBaudRate2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.cbBaudRate2.BorderSize = 1;
            this.cbBaudRate2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBaudRate2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.cbBaudRate2.ForeColor = System.Drawing.Color.Black;
            this.cbBaudRate2.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(97)))));
            this.cbBaudRate2.ListBackColor = System.Drawing.Color.White;
            this.cbBaudRate2.ListTextColor = System.Drawing.Color.DimGray;
            this.cbBaudRate2.Location = new System.Drawing.Point(462, 175);
            this.cbBaudRate2.MinimumSize = new System.Drawing.Size(200, 30);
            this.cbBaudRate2.Name = "cbBaudRate2";
            this.cbBaudRate2.Padding = new System.Windows.Forms.Padding(1);
            this.cbBaudRate2.Size = new System.Drawing.Size(331, 30);
            this.cbBaudRate2.TabIndex = 25;
            this.cbBaudRate2.Texts = "";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.label6.ForeColor = System.Drawing.Color.Gray;
            this.label6.Location = new System.Drawing.Point(458, 151);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(87, 21);
            this.label6.TabIndex = 26;
            this.label6.Text = "BaudRate:";
            // 
            // cbStopBit2
            // 
            this.cbStopBit2.BackColor = System.Drawing.Color.White;
            this.cbStopBit2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.cbStopBit2.BorderSize = 1;
            this.cbStopBit2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbStopBit2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.cbStopBit2.ForeColor = System.Drawing.Color.Black;
            this.cbStopBit2.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(97)))));
            this.cbStopBit2.ListBackColor = System.Drawing.Color.White;
            this.cbStopBit2.ListTextColor = System.Drawing.Color.DimGray;
            this.cbStopBit2.Location = new System.Drawing.Point(462, 245);
            this.cbStopBit2.MinimumSize = new System.Drawing.Size(200, 30);
            this.cbStopBit2.Name = "cbStopBit2";
            this.cbStopBit2.Padding = new System.Windows.Forms.Padding(1);
            this.cbStopBit2.Size = new System.Drawing.Size(331, 30);
            this.cbStopBit2.TabIndex = 27;
            this.cbStopBit2.Texts = "";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.label7.ForeColor = System.Drawing.Color.Gray;
            this.label7.Location = new System.Drawing.Point(459, 221);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(66, 21);
            this.label7.TabIndex = 28;
            this.label7.Text = "StopBit";
            // 
            // cbParidade2
            // 
            this.cbParidade2.BackColor = System.Drawing.Color.White;
            this.cbParidade2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.cbParidade2.BorderSize = 1;
            this.cbParidade2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbParidade2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.cbParidade2.ForeColor = System.Drawing.Color.Black;
            this.cbParidade2.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(97)))));
            this.cbParidade2.ListBackColor = System.Drawing.Color.White;
            this.cbParidade2.ListTextColor = System.Drawing.Color.DimGray;
            this.cbParidade2.Location = new System.Drawing.Point(462, 317);
            this.cbParidade2.MinimumSize = new System.Drawing.Size(200, 30);
            this.cbParidade2.Name = "cbParidade2";
            this.cbParidade2.Padding = new System.Windows.Forms.Padding(1);
            this.cbParidade2.Size = new System.Drawing.Size(331, 30);
            this.cbParidade2.TabIndex = 29;
            this.cbParidade2.Texts = "";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.label8.ForeColor = System.Drawing.Color.Gray;
            this.label8.Location = new System.Drawing.Point(459, 293);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(78, 21);
            this.label8.TabIndex = 30;
            this.label8.Text = "Paridade";
            // 
            // chkAutoconnect2
            // 
            this.chkAutoconnect2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkAutoconnect2.AutoSize = true;
            this.chkAutoconnect2.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.chkAutoconnect2.Location = new System.Drawing.Point(462, 364);
            this.chkAutoconnect2.Name = "chkAutoconnect2";
            this.chkAutoconnect2.Size = new System.Drawing.Size(216, 25);
            this.chkAutoconnect2.TabIndex = 32;
            this.chkAutoconnect2.Text = "Conectar automáticamente";
            this.chkAutoconnect2.UseVisualStyleBackColor = true;
            // 
            // btnConectar02
            // 
            this.btnConectar02.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnConectar02.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(190)))), ((int)(((byte)(187)))));
            this.btnConectar02.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnConectar02.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConectar02.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnConectar02.ForeColor = System.Drawing.Color.White;
            this.btnConectar02.Location = new System.Drawing.Point(462, 409);
            this.btnConectar02.Name = "btnConectar02";
            this.btnConectar02.Size = new System.Drawing.Size(331, 37);
            this.btnConectar02.TabIndex = 31;
            this.btnConectar02.Text = "Conectar";
            this.btnConectar02.UseVisualStyleBackColor = false;
            this.btnConectar02.Click += new System.EventHandler(this.btnConectar02_Click);
            // 
            // SerialForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(247)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(833, 484);
            this.Controls.Add(this.chkAutoconnect2);
            this.Controls.Add(this.btnConectar02);
            this.Controls.Add(this.cbParidade2);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.cbStopBit2);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.cbBaudRate2);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cbPortaSerial2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.chkAutoconnect);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnSalvar);
            this.Controls.Add(this.cbStopBit);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbBaudRate);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbPortaSerial);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label5);
            this.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "SerialForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SerialForm";
            this.Load += new System.EventHandler(this.SerialForm_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.SerialForm_Paint);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label12;
        private CustomLayout.CTComboBox cbPortaSerial;
        private System.Windows.Forms.Label label5;
        private CustomLayout.CTComboBox cbBaudRate;
        private System.Windows.Forms.Label label1;
        private CustomLayout.CTComboBox cbStopBit;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSalvar;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.CheckBox chkAutoconnect;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label3;
        private CustomLayout.CTComboBox cbPortaSerial2;
        private System.Windows.Forms.Label label4;
        private CustomLayout.CTComboBox cbBaudRate2;
        private System.Windows.Forms.Label label6;
        private CustomLayout.CTComboBox cbStopBit2;
        private System.Windows.Forms.Label label7;
        private CustomLayout.CTComboBox cbParidade2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckBox chkAutoconnect2;
        private System.Windows.Forms.Button btnConectar02;
    }
}
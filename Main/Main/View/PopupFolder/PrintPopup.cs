﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Main.View.PopupFolder
{
    public partial class PrintPopup : Form
    {
        public PrintPopup(Bitmap bmp)
        {
            InitializeComponent();
            this.Size = bmp.Size;
            pictureBox1.Image = bmp;
        }

        private async void PrintPopup_Load(object sender, EventArgs e)
        {
            try
            {
                await Task.Delay(3000);
                this.Close();
            }
            catch (Exception)
            {
            }
        }
    }
}

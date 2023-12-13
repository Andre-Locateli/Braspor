using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Main.View.PagesFolder
{
    public partial class TestSend : Form
    {
        public TestSend()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Program.TCP.WriteClient("12313");
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

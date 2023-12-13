using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Main.Service
{
    public  class CornerElement
    {
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        public static extern IntPtr CreateRoundRectRgn
    (
    int nLeftRect,
    int nTopRect,
    int nRightRect,
    int nBottomRect,
    int nWidthEllipse,
    int nHeightEllipse
    );


        public CornerElement(Button btn, int corner_1, int corner_2)
        {
            btn.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, btn.Width, btn.Height, corner_1, corner_2));
        }

        public CornerElement(Panel panel, int corner_1, int corner_2)
        {
            panel.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, panel.Width, panel.Height, corner_1, corner_2));
        }
    }
}

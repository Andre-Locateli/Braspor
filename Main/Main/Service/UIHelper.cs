using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace Main.Service
{
    public class UIHelper
    {
        private Form form;
        private bool MoveFoorm = false;
        private Point MoveForm_MousePosition= Point.Empty;
        private Panel pn;

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        public  static extern IntPtr CreateRoundRectRgn
            (
            int nLeftRect,
            int nTopRect,
            int nRightRect,
            int nBottomRect,
            int nWidthEllipse,
            int nHeightEllipse
            );

        public UIHelper(Form frm, bool flag_corner)
        {
            form = frm;
            if (flag_corner) 
            {
                form.Region =  System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, form.Width, form.Height, 20, 20));
            }
        }

        public UIHelper(Panel _pn)
        {
            pn = _pn;
        }

        public void MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Left)
                {
                    MoveFoorm = false;
                    if (form != null)
                        form.Cursor = Cursors.Default;

                    if (pn != null)
                        pn.Cursor = Cursors.Default;
                }
            }
            catch (Exception) { }
        }

        public void MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                if (MoveFoorm)

                    if (form != null)
                        form.Location = new Point(e.X + form.Left - MoveForm_MousePosition.X,
                        e.Y + form.Top - MoveForm_MousePosition.Y);

                    if (pn != null)
                        pn.Location = new Point(e.X + pn.Left - MoveForm_MousePosition.X,
                        e.Y + pn.Top - MoveForm_MousePosition.Y);
            }
            catch (Exception) { }
        }

        public void MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Left)
                {
                    MoveFoorm = true;
                    if (form != null) 
                        form.Cursor = Cursors.Default;

                    if(pn !=null)
                        pn.Cursor = Cursors.Default;

                    MoveForm_MousePosition = e.Location;
                }
            }
            catch (Exception) { }
        }

        public async void ShowErrorLabel(Label lbl, string message, int time)
        {
            try
            {
                lbl.Text = message;
                await Task.Delay(time);
                lbl.Text = "";
            }
            catch (Exception)
            {
            }
        }

    }
}

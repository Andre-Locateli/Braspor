using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace Main.Helper
{
    public static class StyleSheet
    {
        public static void panel_Paint(object sender, PaintEventArgs e)
        {
            Panel pn = (Panel)sender;
            System.Drawing.Color COLOR_BORDER = System.Drawing.Color.FromArgb(237, 237, 237);
            int borderSize = 2;
            ControlPaint.DrawBorder(e.Graphics, pn.ClientRectangle, COLOR_BORDER, borderSize, ButtonBorderStyle.Solid, COLOR_BORDER, borderSize, ButtonBorderStyle.Solid, COLOR_BORDER, borderSize, ButtonBorderStyle.Solid, COLOR_BORDER, borderSize, ButtonBorderStyle.Solid);
        }

        public static void RedrawAll(Form frm)
        {
            try
            {
                foreach (Control item in frm.Controls)
                {
                    item.Invalidate();
                }
            }
            catch (Exception)
            {
            }
            frm.Update();
            frm.Refresh();
        }

        public static void v_SaveFormConfig(Form form, bool full_screen)
        {
            XmlDocument doc = new XmlDocument();
            string caminhoCompleto = Path.Combine(Application.StartupPath, Program.CFG.CaminhoConfig);
            doc.Load(caminhoCompleto);
            XmlElement xml_x = (XmlElement)doc.SelectSingleNode("//x");
            XmlElement xml_y = (XmlElement)doc.SelectSingleNode("//y");
            XmlElement xml_width = (XmlElement)doc.SelectSingleNode("//width");
            XmlElement xml_height = (XmlElement)doc.SelectSingleNode("//height");
            XmlElement xml_full_screen = (XmlElement)doc.SelectSingleNode("//full_screen");
            if (xml_x != null && xml_y != null && xml_width != null && xml_height != null && xml_full_screen != null)
            {
                xml_x.InnerText = form.Location.X.ToString();
                xml_y.InnerText = form.Location.Y.ToString();
                xml_width.InnerText = form.Width.ToString();
                xml_height.InnerText = form.Height.ToString();
                if (full_screen == true) xml_full_screen.InnerText = "1";
                else xml_full_screen.InnerText = "0";
            }
            try
            {
                doc.Save(caminhoCompleto);
                //MessageBox.Show("String de Conexão SQL salva com sucesso!", "VC Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao salvar dados de dimensionamento Form. {ex.ToString()}");
                //MessageBox.Show($"Erro ao salvar String de Conexão SQL da Estação! {ex}", "VC Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}

using Main.View.PagesFolder.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Main.Helper
{
    public class TecladoFilter : IMessageFilter
    {
        private const int WM_CHAR = 0x0102;
        private const int WM_KEYDOWN = 0x0100;
        private StringBuilder barcodeBuilder = new StringBuilder();

        public bool PreFilterMessage(ref Message m)
        {
            if (m.Msg == WM_CHAR)
            {
                Program._usbValue = "";
                char c = (char)m.WParam.ToInt32();
                if (c == '\r' || c == '\n')
                {
                    string barcode = barcodeBuilder.ToString();
                    barcodeBuilder.Clear();
                    Program._usbValue = barcode;
                    Console.WriteLine(barcode);
                }
                else
                {
                    barcodeBuilder.Append(c);
                }
            }
            else if (m.Msg == WM_KEYDOWN && (Keys)m.WParam.ToInt32() == Keys.F1)
            {
                if (!GeralClass.IsFormOpen(typeof(SerialForm)))
                {
                    //Console.WriteLine("F1");
                    SerialForm serialForm = new SerialForm();
                    serialForm.ShowDialog();
                }
            }
            return false;
        }
    }
}

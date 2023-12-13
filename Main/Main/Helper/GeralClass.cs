using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Main.Helper
{
    static public class GeralClass
    {
        public static StopBits v_StopBit(string Value)
        {
            StopBits _StopBit = StopBits.Two;
            switch (Value)
            {
                case "0":
                    _StopBit = StopBits.None;
                    break;
                case "1":
                    _StopBit = StopBits.One;
                    break;
                case "1.5":
                    _StopBit = StopBits.OnePointFive;
                    break;
                case "2":
                    _StopBit = StopBits.Two;
                    break;
            }
            return _StopBit;
        }

        public static string v_StopBit(StopBits Value)
        {
            string _StopBit = "2";
            switch (Value)
            {
                case StopBits.None:
                    _StopBit = "0";
                    break;
                case StopBits.One:
                    _StopBit = "1";
                    break;
                case StopBits.OnePointFive:
                    _StopBit = "1.5";
                    break;
                case StopBits.Two:
                    _StopBit = "2";
                    break;
            }
            return _StopBit;
        }

        public static bool IsFormOpen(Type formType)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == formType)
                {
                    return true;
                }
            }
            return false;
        }

    }
}

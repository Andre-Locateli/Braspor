using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Main.Helper
{
    public static class RandomsHelper
    {

        public static byte[] ImageToDB(Image box) 
        {
			try
			{
                byte[] imageBytes;
                using (MemoryStream ms = new MemoryStream())
                {
                    box.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                    imageBytes = ms.ToArray();
                }
                return imageBytes;
            }
			catch (Exception)
			{
                return new byte[1];
			}
        }

    }
}

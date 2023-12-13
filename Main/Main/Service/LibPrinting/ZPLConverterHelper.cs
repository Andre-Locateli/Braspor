using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsultaContatos
{
    public class ZPLConverterHelper
    {
        private int blackLimit = 380;
        private int total;
        private int widthBytes;
        private bool compressHex = false;
        private static Dictionary<int, string> mapCode = new Dictionary<int, string>()
        {
            { 1, "G" },
            { 2, "H" },
            { 3, "I" },
            { 4, "J" },
            { 5, "K" },
            { 6, "L" },
            { 7, "M" },
            { 8, "N" },
            { 9, "O" },
            { 10, "P" },
            { 11, "Q" },
            { 12, "R" },
            { 13, "S" },
            { 14, "T" },
            { 15, "U" },
            { 16, "V" },
            { 17, "W" },
            { 18, "X" },
            { 19, "Y" },
            { 20, "g" },
            { 40, "h" },
            { 60, "i" },
            { 80, "j" },
            { 100, "k" },
            { 120, "l" },
            { 140, "m" },
            { 160, "n" },
            { 180, "o" },
            { 200, "p" },
            { 220, "q" },
            { 240, "r" },
            { 260, "s" },
            { 280, "t" },
            { 300, "u" },
            { 320, "v" },
            { 340, "w" },
            { 360, "x" },
            { 380, "y" },
            { 400, "z" }
        };

        public string ConvertFromImage(Bitmap image)
        {
            string cuerpo = CreateBody(image);
            if (compressHex)
                cuerpo = EncodeHexAscii(cuerpo);
            return HeadDoc() + cuerpo + FootDoc();
        }

        private string CreateBody(Bitmap originalImage)
        {
            var sb = new System.Text.StringBuilder();
            using (var graphics = Graphics.FromImage(originalImage))
            {
                graphics.DrawImage(originalImage, 0, 0);
                int height = originalImage.Height;
                int width = originalImage.Width;
                int rgb, red, green, blue, index = 0;
                char[] auxBinaryChar = { '0', '0', '0', '0', '0', '0', '0', '0' };
                widthBytes = width / 8;
                if (width % 8 > 0)
                {
                    widthBytes = (((int)(width / 8)) + 1);
                }
                else
                {
                    widthBytes = width / 8;
                }
                total = widthBytes * height;
                for (int h = 0; h < height; h++)
                {
                    for (int w = 0; w < width; w++)
                    {
                        rgb = originalImage.GetPixel(w, h).ToArgb();
                        red = (rgb >> 16) & 0x000000FF;
                        green = (rgb >> 8) & 0x000000FF;
                        blue = (rgb) & 0x000000FF;
                        char auxChar = '1';
                        int totalColor = red + green + blue;
                        if (totalColor > blackLimit)
                        {
                            auxChar = '0';
                        }
                        auxBinaryChar[index] = auxChar;
                        index++;
                        if (index == 8 || w == (width - 1))
                        {
                            sb.Append(FourByteBinary(new string(auxBinaryChar)));
                            auxBinaryChar = new char[] { '0', '0', '0', '0', '0', '0', '0', '0' };
                            index = 0;
                        }
                    }
                    sb.Append("\n");
                }
            }
            return sb.ToString();
        }

        private string FourByteBinary(string binaryStr)
        {
            int decimalValue = Convert.ToInt32(binaryStr, 2);
            return decimalValue > 15 ? decimalValue.ToString("X").ToUpper() : "0" + decimalValue.ToString("X").ToUpper();
        }

        private string EncodeHexAscii(string code)
        {
            int maxLineLength = widthBytes * 2;
            var sbCode = new System.Text.StringBuilder();
            var sbLine = new System.Text.StringBuilder();
            string previousLine = null;
            int counter = 1;
            char aux = code[0];
            bool firstChar = false;
            for (int i = 1; i < code.Length; i++)
            {
                if (firstChar)
                {
                    aux = code[i];
                    firstChar = false;
                    continue;
                }
                if (code[i] == '\n')
                {
                    if (counter >= maxLineLength && aux == '0')
                    {
                        sbLine.Append(",");
                    }
                    else if (counter >= maxLineLength && aux == 'F')
                    {
                        sbLine.Append("!");
                    }
                    else if (counter > 20)
                    {
                        int multi20 = (counter / 20) * 20;
                        int resto20 = (counter % 20);
                        sbLine.Append(mapCode[multi20]);
                        if (resto20 != 0)
                        {
                            sbLine.Append(mapCode[resto20] + aux);
                        }
                        else
                        {
                            sbLine.Append(aux);
                        }
                    }
                    else
                    {
                        sbLine.Append(mapCode[counter] + aux);
                        if (!mapCode.ContainsKey(counter))
                        {
                        }
                    }
                    counter = 1;
                    firstChar = true;
                    if (sbLine.ToString() == previousLine)
                    {
                        sbCode.Append(":");
                    }
                    else
                    {
                        sbCode.Append(sbLine.ToString());
                    }
                    previousLine = sbLine.ToString();
                    sbLine.Clear();
                    continue;
                }
                if (aux == code[i])
                {
                    counter++;
                }
                else
                {
                    if (counter > 20)
                    {
                        int multi20 = (counter / 20) * 20;
                        int resto20 = (counter % 20);
                        sbLine.Append(mapCode[multi20]);
                        if (resto20 != 0)
                        {
                            sbLine.Append(mapCode[resto20] + aux);
                        }
                        else
                        {
                            sbLine.Append(aux);
                        }
                    }
                    else
                    {
                        sbLine.Append(mapCode[counter] + aux);
                    }
                    counter = 1;
                    aux = code[i];
                }
            }
            return sbCode.ToString();
        }

        private string HeadDoc()
        {
            return "^XA " +
                $"^FO0,0^GFA,{total},{total},{widthBytes}, ";
        }

        private string FootDoc()
        {
            return "^FS" +
                "^XZ";
        }

        public void SetCompressHex(bool compressHex)
        {
            this.compressHex = compressHex;
        }

        public void SetBlacknessLimitPercentage(int percentage)
        {
            blackLimit = (percentage * 768 / 100);
        }

        //public static void Main(string[] args)
        //{
        //    Bitmap originalImage = new Bitmap("/tmp/logo.jpg");
        //    ZPLConverter zp = new ZPLConverter();
        //    zp.SetCompressHex(true);
        //    zp.SetBlacknessLimitPercentage(50);
        //    Console.WriteLine(zp.ConvertFromImage(originalImage));
        //}
    }
}

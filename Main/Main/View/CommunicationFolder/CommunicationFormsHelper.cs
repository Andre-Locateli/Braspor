
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.View.CommunicationFolder
{
    public static class CommunicationFormsHelper
    {

        public static byte[] CRC(byte[] data)
        {
            ushort CRCFull = 0xFFFF;
            byte CRCHigh = 0x0F, CRCLow = 0xFF;
            char CRCLSB;
            byte[] result = new byte[2];

            for (int i = 0; i < data.Length - 2; i++)
            {
                CRCFull ^= (ushort)data[i];

                for (int j = 0; j < 8; j++)
                {
                    CRCLSB = (char)(CRCFull & 0x0001);
                    CRCFull = (ushort)((CRCFull >> 1) & 0x7FFF);

                    if (Convert.ToInt32(CRCLSB) == 1)
                    {
                        CRCFull ^= 0xA001;
                    }
                }
            }

            CRCHigh = (byte)((CRCFull >> 8) & 0xFF);
            CRCLow = (byte)(CRCFull & 0xFF);

            result[0] = CRCLow;
            result[1] = CRCHigh;

            return result;
        }

        public static string PesoConverted(byte sts, byte peso_a, byte peso_b, byte peso_c)
        {
            double Multiplicador;
            int Decimal = 0;

            try
            {
                switch (sts & 0b00000111)
                {
                    case 0:
                        Multiplicador = 1.0;
                        Decimal = 0;
                        break;

                    case 1:
                        Multiplicador = 0.1;
                        Decimal = 1;
                        break;

                    case 2:
                        Multiplicador = 0.01;
                        Decimal = 2;
                        break;

                    case 3:
                        Multiplicador = 0.001;
                        Decimal = 3;
                        break;

                    case 4:
                        Multiplicador = 0.0001;
                        Decimal = 4;
                        break;

                    default:
                        Multiplicador = 1.0;
                        Decimal = 0;
                        break;
                }

                double peso = 0;

                if ((sts & 0b00001000) == 0x08)
                {
                    peso = (((peso_a * 65536) + (peso_b * 256) + (peso_c)) * Multiplicador * (-1.0));
                }

                if ((sts & 0b00001000) == 0)
                {
                    peso = (((peso_a * 65536) + (peso_b * 256) + (peso_c)) * Multiplicador * (1.0));
                }

                if (!peso.ToString().Contains(","))
                {
                    switch (Decimal)
                    {
                        case 0:
                            return Convert.ToString(string.Format("{0}", peso));
                        case 1:
                            return Convert.ToString(string.Format("{0},0", peso));
                        case 2:
                            return Convert.ToString(string.Format("{0},00", peso));
                        case 3:
                            return Convert.ToString(string.Format("{0},000", peso));
                        case 4:
                            return Convert.ToString(string.Format("{0},0000", peso));
                    }
                }

                return Convert.ToString(peso);
            }
            catch (Exception)
            {
                return "0";
            }

        }

        public static int get_HIGH_byte(int valor)
        {
            int HIGH = (int)Math.Floor((double)valor / 256);
            return HIGH;
        }

        public static int get_LOW_byte(int valor)
        {
            int LOW = valor % 256;
            return LOW;
        }

    }
}

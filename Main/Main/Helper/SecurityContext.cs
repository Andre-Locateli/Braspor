using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Main.Helper
{
    public static class SecurityContext
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns>Retorna o valor enviado como parametro criptografado em HASH MD5</returns>
        public static string ReturnHashMD5(string value) 
        {
			try
			{
                MD5 md5 = MD5.Create();

                byte[] inputBytes = Encoding.UTF8.GetBytes(value);

                byte[] hashBytes = md5.ComputeHash(inputBytes);

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("x2"));
                }
                string hash = sb.ToString();

                //Console.WriteLine("Texto original: " + value);
                //Console.WriteLine("Hash MD5: " + hash);
                return hash;
            }
			catch (Exception)
			{
                return "";
			}
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace WebTech.Common
{
    public static class WTRandom
    {
        public static string getRandomPlan3()
        {
            string strK = string.Empty;
            strK = GenerateRandomNumber().ToString();
            strK = Math.Abs(decimal.Parse(strK)).ToString();
            strK = decimal.Parse(strK).ToString("0000");
            strK = RandomString(3) + strK.Substring(0, 4);

            return strK;
        }

        public static string getNumber()
        {
            string strK = string.Empty;
            strK = GenerateRandomNumber().ToString();
            strK = Math.Abs(decimal.Parse(strK)).ToString();
            strK = decimal.Parse(strK).ToString("0000000000");
            strK = RandomString(3) + strK;

            return strK;
        }

        public static string getRandom()
        {
            string strK = string.Empty;
            strK = GenerateRandomNumber().ToString();
            strK = Math.Abs(decimal.Parse(strK)).ToString();
            strK = decimal.Parse(strK).ToString("0000000000");
            strK = RandomString(10) + strK;

            return strK;
        }

        public static string getOrderNo()
        {
            string strK = string.Empty;
            strK = GenerateRandomNumber1().ToString();
            strK = Math.Abs(decimal.Parse(strK)).ToString();
            strK = decimal.Parse(strK).ToString("00000000");

            return strK;
        }

        #region Random Number & Characters

        private static int GenerateRandomNumber()
        {
            byte[] byt = new byte[4];
            RNGCryptoServiceProvider rngCrypto =
                new RNGCryptoServiceProvider();
            rngCrypto.GetBytes(byt);
            int randomNumber = BitConverter.ToInt32(byt, 0);
            return randomNumber;
        }

        private static int GenerateRandomNumber1()
        {
            byte[] byt = new byte[3];
            byte[] byt1 = new byte[4];
            RNGCryptoServiceProvider rngCrypto =
                new RNGCryptoServiceProvider();
            rngCrypto.GetBytes(byt);

            byt1[0] = byt[0];
            byt1[1] = byt[1];
            byt1[2] = byt[2];


            int randomNumber = BitConverter.ToInt32(byt1, 0);
            return randomNumber;
        }

        private static string RandomString(int size)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }

            return builder.ToString();
        }

        #endregion Random Number & Characters
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace WebTech.Common
{
    public class clsWTEncryption
    {
        //returns HASH of 128 bit 
        public static string MD5Hash(string text)
        {
            MD5 md5 = new MD5CryptoServiceProvider();

            //compute hash from the bytes of text
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(text));

            //get hash result after compute it
            byte[] result = md5.Hash;

            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                //change it into 2 hexadecimal digits
                //for each byte
                strBuilder.Append(result[i].ToString("x2"));
            }

            return strBuilder.ToString();
        }

        public static string MD5Hash(string password, string Pwdkey)
        {
            password = password.Substring(0, Pwdkey.Length / 2) + password + password.Substring(password.Length / 2);
            return MD5Hash(password);
        }

        public static string MD5Hash(string userid, string password, string Pwdkey)
        {
            password = userid + Pwdkey.Substring(0, Pwdkey.Length / 2) + password + Pwdkey.Substring(Pwdkey.Length / 2);
            return MD5Hash(password);
        }
    }
}

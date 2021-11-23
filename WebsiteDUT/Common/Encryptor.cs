using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebsiteDUT.Common
{
    public class Encryptor
    {
        ///<summary>
        ///Mã hoá chuỗi thành chuỗi MD5
        ///Input: sToEncrypt (Chuỗi cần mã hoá)
        ///Output: Chuỗi sau khi mã hoá
        ///</summary>
        public static string EncryptorMD5(string sToEncrypt)
        {
            System.Text.UTF8Encoding ue = new System.Text.UTF8Encoding();
            byte[] bytes = ue.GetBytes(sToEncrypt);

            //encrypt bytes
            System.Security.Cryptography.MD5CryptoServiceProvider md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] hashBytes = md5.ComputeHash(bytes);

            //Convert the encrypted bytes bacj to a string (bsae 16)
            string hashString = "";

            for (int i = 0; i < hashBytes.Length; i++)
            {
                hashString += Convert.ToString(hashBytes[i], 16).PadLeft(2, '0');
            }
            return hashString.PadLeft(32, '0');
        }
    }
}
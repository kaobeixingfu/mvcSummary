using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Standard.Common
{
    public class SecurityUtil
    {
        /// <summary>
        /// 加密字符串
        /// </summary>
        /// <param name="plainText">明码</param>
        /// <param name="encryptKey">密钥</param>
        /// <returns>密文</returns>
        public static string DESEncode(string plainText, string encryptKey)
        {
            byte[] iv = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
            try
            {
                var newEncryptKey = encryptKey.Length > 8 ? encryptKey.Substring(0, 8) : "";
                byte[] key = System.Text.Encoding.UTF8.GetBytes(newEncryptKey);
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                byte[] inputByteArray = Encoding.UTF8.GetBytes(plainText);

                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(key, iv), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();

                return Convert.ToBase64String(ms.ToArray());
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 解密字符串
        /// </summary>
        /// <param name="encryptText">密文</param>
        /// <param name="encryptKey">密钥</param>
        /// <returns>明文</returns>
        public static string DESDecode(string encryptText, string encryptKey)
        {
            byte[] iv = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
            byte[] inputByteArray = new Byte[encryptText.Length];

            try
            {
                var newEncryptKey = encryptKey.Length > 8 ? encryptKey.Substring(0, 8) : "";
                byte[] key = System.Text.Encoding.UTF8.GetBytes(newEncryptKey);
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                inputByteArray = Convert.FromBase64String(encryptText);

                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(key, iv), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();

                System.Text.Encoding encoding = new System.Text.UTF8Encoding();
                return encoding.GetString(ms.ToArray());
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
    }
}

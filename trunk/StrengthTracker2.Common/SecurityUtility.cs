using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace StrengthTracker2.Common
{
    public static class SecurityUtility
    {
        private static Object securityLockObject = new Object();

        /// <summary>
        /// Encrypts a given text.
        /// </summary>
        /// <param name="textToEncrypt"></param>
        /// <returns>Encrypted text</returns>
        public static string EncryptText(string textToEncrypt)
        {
            string encryptedString = string.Empty;
            lock (securityLockObject)
            {
                const string encryptionkey = "MAKV2SPBNI99212";

                byte[] clearBytes = Encoding.Unicode.GetBytes(textToEncrypt);

                using (Aes aesEncryptor = Aes.Create())
                {
                    using (var derivedBytes = new Rfc2898DeriveBytes(encryptionkey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 }))
                    {
                        aesEncryptor.Key = derivedBytes.GetBytes(32);
                        aesEncryptor.IV = derivedBytes.GetBytes(16);
                    }

                    using (var memoryStream = new MemoryStream())
                    {
                        using (var cryptoStream = new CryptoStream(memoryStream, aesEncryptor.CreateEncryptor(), CryptoStreamMode.Write))
                        {
                            cryptoStream.Write(clearBytes, 0, clearBytes.Length);
                            cryptoStream.Close();
                        }
                        encryptedString = Convert.ToBase64String(memoryStream.ToArray());
                    }
                }
            }
            return encryptedString;
        }

        /// <summary>
        /// Decrypt the Cipher text
        /// </summary>
        /// <param name="cipherTextToDecrypt"></param>
        /// <returns>Decrypted text</returns>
        public static string DecryptText(string cipherTextToDecrypt)
        {
            string cipherTextDecrypted = "";
            lock (securityLockObject)
            {
                const string encryptionkey = "MAKV2SPBNI99212";

                byte[] cipherBytes = Convert.FromBase64String(cipherTextToDecrypt);

                using (Aes aesDecryptor = Aes.Create())
                {
                    using (var derivedBytes = new Rfc2898DeriveBytes(encryptionkey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 }))
                    {
                        aesDecryptor.Key = derivedBytes.GetBytes(32);
                        aesDecryptor.IV = derivedBytes.GetBytes(16);
                    }

                    using (var memoryStream = new MemoryStream())
                    {
                        using (var cryptoStream = new CryptoStream(memoryStream, aesDecryptor.CreateDecryptor(), CryptoStreamMode.Write))
                        {
                            cryptoStream.Write(cipherBytes, 0, cipherBytes.Length);
                            cryptoStream.Close();
                        }
                        cipherTextDecrypted = Encoding.Unicode.GetString(memoryStream.ToArray());
                    }
                }
            }
            return cipherTextDecrypted;
        }

        /// <summary>
        /// Encrypts the value with the key passed. Uses HMACMD5 encryption
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string HMAC_MD5(string key, string value)
        {
            string hMACMD5String = "";
            // The first two lines take the input values and convert them from strings to Byte arrays
            byte[] HMACkey = (new System.Text.ASCIIEncoding()).GetBytes(key);
            byte[] HMACdata = (new System.Text.ASCIIEncoding()).GetBytes(value);

            // create a HMACMD5 object with the key set
            using (HMACMD5 myhmacMD5 = new HMACMD5(HMACkey))
            {
                //calculate the hash (returns a byte array)
                byte[] HMAChash = myhmacMD5.ComputeHash(HMACdata);

                //loop through the byte array and add append each piece to a string to obtain a hash string
                hMACMD5String = HMAChash.Aggregate("", (current, t) => current + t.ToString("x").PadLeft(2, '0'));
            }

            return hMACMD5String;
        }
    }
}

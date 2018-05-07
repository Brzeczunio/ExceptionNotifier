using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Queris.ExceptionNotifier.Common.Enums;

namespace Queris.ExceptionNotifier.Common.Helpers
{
    public static class CryptoHelper
    {
        internal static readonly string Key = "hashthepaaswordifucan";
        internal static readonly byte[] SaltBytes = { 1, 2, 3, 4, 5, 6, 7, 8 };

        public static string Encrypt(string str)
        {
            var bytesToBeEncrypted = Encoding.UTF8.GetBytes(str);
            var passwordBytes = Encoding.UTF8.GetBytes(Key);
            passwordBytes = SHA256.Create().ComputeHash(passwordBytes);
            return Convert.ToBase64String(Crypt(bytesToBeEncrypted, passwordBytes, CryptoMethod.Encrypt));
        }

        public static string Decrypt(string str)
        {
            var bytesToBeDecrypted = Convert.FromBase64String(str);
            var passwordBytes = Encoding.UTF8.GetBytes(Key);
            passwordBytes = SHA256.Create().ComputeHash(passwordBytes);
            return Encoding.UTF8.GetString(Crypt(bytesToBeDecrypted, passwordBytes, CryptoMethod.Decrypt));
        }

        private static byte[] Crypt(byte[] bytesToBeCryptography, byte[] passwordBytes, CryptoMethod method)
        {
            using (var ms = new MemoryStream())
            {
                using (var aes = new RijndaelManaged())
                {
                    var key = new Rfc2898DeriveBytes(passwordBytes, SaltBytes, 1000);

                    aes.KeySize = 256;
                    aes.BlockSize = 128;
                    aes.Key = key.GetBytes(aes.KeySize / 8);
                    aes.IV = key.GetBytes(aes.BlockSize / 8);
                    aes.Mode = CipherMode.CBC;

                    using (var cs = new CryptoStream(ms, method == CryptoMethod.Encrypt ? aes.CreateEncryptor() : aes.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(bytesToBeCryptography, 0, bytesToBeCryptography.Length);
                        cs.Close();
                    }

                    return ms.ToArray();
                }
            }
        }
    }
}

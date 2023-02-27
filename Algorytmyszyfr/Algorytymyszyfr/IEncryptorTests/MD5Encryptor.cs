using System;
using System.Text;
using System.Security.Cryptography;

namespace Algorytmyszyfr
{
    class MD5Encryptor : IEncryptor
    {
        public string Name()
        {
            return "MD5";
        }

        public string Encrypt(string password)
        {
            using (var md5 = new MD5CryptoServiceProvider())
            {
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
                byte[] encryptedBytes = md5.ComputeHash(passwordBytes);
                return BitConverter.ToString(encryptedBytes).Replace("-", "");
            }
        }
    }
}
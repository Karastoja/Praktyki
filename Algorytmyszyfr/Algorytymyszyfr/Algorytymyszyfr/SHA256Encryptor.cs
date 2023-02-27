using System;
using System.Text;
using System.Security.Cryptography;

namespace Algorytmyszyfr
{
    class SHA256Encryptor : IEncryptor
    {
        public string Name()
        {
            return "SHA256";
        }

        public string Encrypt(string password)
        {
            using (var sha256 = new SHA256CryptoServiceProvider())
            {
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
                byte[] encryptedBytes = sha256.ComputeHash(passwordBytes);
                return BitConverter.ToString(encryptedBytes).Replace("-", "");
            }
        }
    }
}
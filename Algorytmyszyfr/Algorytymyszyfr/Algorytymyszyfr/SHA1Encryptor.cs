using System;
using System.Text;
using System.Security.Cryptography;

namespace Algorytmyszyfr
{
    class SHA1Encryptor : IEncryptor
    {
        public string Name()
        {
            return "SHA1";
        }

        public string Encrypt(string password)
        {
            using (var sha1 = new SHA1CryptoServiceProvider())
            {
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
                byte[] encryptedBytes = sha1.ComputeHash(passwordBytes);
                return BitConverter.ToString(encryptedBytes).Replace("-", "");
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using BCrypt.Net;

namespace PasswordEncryption
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Podaj hasło do zaszyfrowania: ");
            string password = Console.ReadLine();

            Console.WriteLine("Wybierz z którego algorytmu chcesz skorzystać:");
            Console.WriteLine("1 - MD5");
            Console.WriteLine("2 - SHA1");
            Console.WriteLine("3 - SHA256");
            Console.WriteLine("4 - bcrypt");

            int algorithmChoice = Convert.ToInt32(Console.ReadLine());

            IEncryptor encryptor;
            switch (algorithmChoice)
            {
                case 1:
                    encryptor = new MD5Encryptor();
                    break;
                case 2:
                    encryptor = new SHA1Encryptor();
                    break;
                case 3:
                    encryptor = new SHA256Encryptor();
                    break;
                case 4:
                    encryptor = new BcryptEncryptor();
                    break;
                default:
                    Console.WriteLine("Zły wybóre wybierz liczbe od 1 do 4.");
                    return;
            }

            string encryptedPassword = encryptor.Encrypt(password);
            Console.WriteLine(encryptor.Name() + " \nzaszyforwane hasło: " + encryptedPassword);

            Console.ReadKey();
        }
    }

    interface IEncryptor
    {
        string Name();
        string Encrypt(string password);
    }

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

    class BcryptEncryptor : IEncryptor
    {
        public string Name()
        {
            return "bcrypt";
        }

        public string Encrypt(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using BCrypt.Net;

namespace Algorytmyszyfr
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
}
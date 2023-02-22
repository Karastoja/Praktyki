using System;
using System.IO;
using System.Security.Cryptography;
using System.Data.SQLite;
namespace PasswordEncryption
{
    class Program
    {
        static void Main(string[] args)
        {
            // utworzenie bazy danych lub otwarcie istniejącej
            string connectionString = @"Data Source=C:\Users\Nauczyciel\Desktop\projects\szyfr\szyfr\baza.db";
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                // utworzenie tabeli w bazie danych, jeśli nie istnieje
                string createTableQuery = "CREATE TABLE IF NOT EXISTS passwords (id INTEGER PRIMARY KEY, username TEXT, password BLOB, file BLOB, key BLOB, iv BLOB)";
                using (var command = new SQLiteCommand(createTableQuery, connection))
                {
                    command.ExecuteNonQuery();
                }
                Console.Write("Podaj ID rekordu, który chcesz usunąć: ");
                string id_to_delete = Console.ReadLine();

                // usuwanie rekordu z bazy danych
                var deleteCommand = new SQLiteCommand("DELETE FROM passwords WHERE id=@id", connection);
                deleteCommand.Parameters.AddWithValue("@id", id_to_delete);
                deleteCommand.ExecuteNonQuery();

                // wyświetlenie informacji o usuniętym rekordzie
                Console.WriteLine("Usunięto rekord o ID: " + id_to_delete);

                // pobranie hasła od użytkownika i zaszyfrowanie go
                Console.Write("Podaj hasło: ");
                string password = Console.ReadLine();
                byte[] key, iv, encryptedPassword;
                EncryptStringToBytes_Aes(password, out key, out iv, out encryptedPassword);

                // pobranie pliku od użytkownika i zaszyfrowanie go
                Console.Write("Podaj nazwę pliku: ");
                string filename = Console.ReadLine();
                byte[] fileBytes = File.ReadAllBytes(filename);
                byte[] encryptedFile = EncryptBytes_Aes(fileBytes, key, iv);

                // zapisanie danych do bazy danych
                string insertQuery = @"INSERT INTO passwords (username, password, file, key, iv) VALUES (@username, @password, @file, @key, @iv)";
                using (var command = new SQLiteCommand(insertQuery, connection))
                {
                    command.Parameters.AddWithValue("@username", "example_username");
                    command.Parameters.AddWithValue("@password", encryptedPassword);
                    command.Parameters.AddWithValue("@file", encryptedFile);
                    command.Parameters.AddWithValue("@key", key);
                    command.Parameters.AddWithValue("@iv", iv);
                    command.ExecuteNonQuery();
                }

                Console.WriteLine("Hasło i plik zostały zaszyfrowane i zapisane w bazie danych.");
            }
        }


        static byte[] EncryptStringToBytes_Aes(string plainText, out byte[] key, out byte[] iv, out byte[] encrypted)
        {
            // wygenerowanie losowych klucza i wektora inicjującego
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.KeySize = 256;
                aesAlg.GenerateKey();
                aesAlg.GenerateIV();
                key = aesAlg.Key;
                iv = aesAlg.IV;

                // utworzenie obiektu AES i zaszyfrowanie danych
                using (Aes aesAlg2 = Aes.Create())
                {
                    aesAlg2.Key = key;
                    aesAlg2.IV = iv;

                    ICryptoTransform encryptor = aesAlg2.CreateEncryptor(aesAlg2.Key, aesAlg2.IV);

                    using (MemoryStream msEncrypt = new MemoryStream())
                    {
                        using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                        {
                            using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                            {
                                swEncrypt.Write(plainText);
                            }
                            encrypted = msEncrypt.ToArray();
                        }
                    }
                }
            }

            return encrypted;
        }

        static byte[] EncryptBytes_Aes(byte[] plainBytes, byte[] key, byte[] iv)
        {
            byte[] encrypted;

            // utworzenie obiektu AES i zaszyfrowanie danych
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = key;
                aesAlg.IV = iv;

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        csEncrypt.Write(plainBytes, 0, plainBytes.Length);
                        csEncrypt.FlushFinalBlock();
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }

            return encrypted;
        }

        static byte[] DecryptBytes_Aes(byte[] cipherBytes, byte[] key, byte[] iv)
        {
            byte[] decrypted;

            // utworzenie obiektu AES i odszyfrowanie danych
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = key;
                aesAlg.IV = iv;

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msDecrypt = new MemoryStream())
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Write))
                    {
                        csDecrypt.Write(cipherBytes, 0, cipherBytes.Length);
                        csDecrypt.FlushFinalBlock();
                        decrypted = msDecrypt.ToArray();
                    }
                }
            }

            return decrypted;
        }
    }
}


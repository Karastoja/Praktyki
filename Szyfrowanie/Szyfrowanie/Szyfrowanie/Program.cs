using System;
using System.Data.SqlClient;
using System.IO;
using System.Security.Cryptography;
using System.Text;

class Program
{
    static void Main()
    {
        // Wygeneruj klucz główny
        byte[] key = GenerateKey();

        // Wprowadź hasło użytkownika do szyfrowania
        Console.Write("Wprowadź hasło: ");
        string password = Console.ReadLine();

        // Szyfruj hasło za pomocą klucza głównego
        byte[] encryptedPassword = Encrypt(Encoding.UTF8.GetBytes(password), key);

        // Zapisz szyfrowane hasło do bazy danych
        SaveToDatabase("password", encryptedPassword, key);

        // Wprowadź nazwę pliku do szyfrowania
        Console.Write("Wprowadź nazwę pliku: ");
        string fileName = Console.ReadLine();

        // Szyfruj plik za pomocą klucza głównego
        byte[] encryptedFile = Encrypt(File.ReadAllBytes(fileName), key);

        // Zapisz szyfrowany plik do bazy danych
        SaveToDatabase(fileName, encryptedFile, key);

        Console.WriteLine("Szyfrowanie zakończone.");
    }

    static byte[] GenerateKey()
    {
        // Utwórz generator liczb losowych
        using (var rng = new RNGCryptoServiceProvider())
        {
            // Wygeneruj klucz o długości 32 bajtów
            byte[] key = new byte[32];
            rng.GetBytes(key);
            return key;
        }
    }

    static byte[] Encrypt(byte[] data, byte[] key)
    {
        // Utwórz obiekt AES z kluczem
        using (var aes = new AesManaged())
        {
            aes.Key = key;
            aes.Mode = CipherMode.CBC;

            // Wygeneruj losowy wektor inicjalizacji
            aes.GenerateIV();

            // Utwórz strumień do szyfrowania danych
            using (var encryptor = aes.CreateEncryptor())
            using (var ms = new MemoryStream())
            {
                // Zapisz wektor inicjalizacji do strumienia
                ms.Write(aes.IV, 0, aes.IV.Length);

                // Szyfruj dane i zapisz je do strumienia
                using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                {
                    cs.Write(data, 0, data.Length);
                }

                return ms.ToArray();
            }
        }
    }

    static void SaveToDatabase(string name, byte[] data, byte[] key)
    {
        // Utwórz połączenie z bazą danych
        string connectionString = "Data Source = 127.0.0.1,443;Initial Catalog=szyfrowanie;Integrated Security=True";
        using (var connection = new SqlConnection(connectionString))
        {
            connection.Open();

            // Sprawdź, czy istnieje tabela "Files"
            using (var command = new SqlCommand("SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Files'", connection))
            {
                int tableExists = (int)command.ExecuteScalar();

                if (tableExists == 0)
                {
                    // Utwórz tabelę "Files", jeśli nie istnieje
                    using (var createTableCommand = new SqlCommand("CREATE TABLE Files (Name VARCHAR(255cd, Data VARBINARY(MAX), Key VARBINARY(32))", connection))
                    {
                        createTableCommand.ExecuteNonQuery();
                    }
                }
            }
            // Zapisz dane do bazy danych
            using (var command = new SqlCommand("INSERT INTO Files (Name, Data, Key) VALUES (@name, @data, @key)", connection))
            {
                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@data", data);
                command.Parameters.AddWithValue("@key", key);
                command.ExecuteNonQuery();
            }
        }
    }

    static void DeleteFromDatabase(string name)
    {
        // Utwórz połączenie z bazą danych
        string connectionString = "Data Source = 127.0.0.1,443;Initial Catalog=szyfrowanie;Integrated Security=True";
        using (var connection = new SqlConnection(connectionString))
        {
            connection.Open();

            // Usuń rekord z bazy danych
            using (var command = new SqlCommand("DELETE FROM Files WHERE Name = @name", connection))
            {
                command.Parameters.AddWithValue("@name", name);
                command.ExecuteNonQuery();
            }
        }
    }

}


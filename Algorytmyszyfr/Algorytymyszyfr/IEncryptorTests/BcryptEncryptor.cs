namespace Algorytmyszyfr
{
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
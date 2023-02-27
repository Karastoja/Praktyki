using Algorytmyszyfr;
using BCrypt.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PasswordEncryption.Tests
{
    [TestClass()]
    public class IEncryptorTests
    {
        private readonly string password = "password123";

        [TestMethod()]
        public void MD5EncryptionTest()
        {
            IEncryptor encryptor = new MD5Encryptor();
            string encryptedPassword = encryptor.Encrypt(password);

            Assert.AreEqual("482c811da5d5b4bc6d497ffa98491e38", encryptedPassword.ToLower());
        }

        [TestMethod()]
        public void SHA1EncryptionTest()
        {
            IEncryptor encryptor = new SHA1Encryptor();
            string encryptedPassword = encryptor.Encrypt(password);

            Assert.AreEqual("cbfdac6008f9cab4083784cbd1874f76618d2a97", encryptedPassword.ToLower());
        }

        [TestMethod()]
        public void SHA256EncryptionTest()
        {
            IEncryptor encryptor = new SHA256Encryptor();
            string encryptedPassword = encryptor.Encrypt(password);

            Assert.AreEqual("ef92b778bafe771e89245b89ecbc08a44a4e166c06659911881f383d4473e94f", encryptedPassword.ToLower());
        }

        [TestMethod()]
        public void BcryptEncryptionTest()
        {
            IEncryptor encryptor = new BcryptEncryptor();
            string encryptedPassword = encryptor.Encrypt(password);

            Assert.IsTrue(BCrypt.Net.BCrypt.Verify(password, encryptedPassword));
        }
    }
}

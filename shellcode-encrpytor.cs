using System;
using System.Text;
using System.IO;
using System.Security.Cryptography;

namespace ShellcodeEncryptor
{
    class Program
    {
        // Hardcoding encryption values is less than ideal in the real world, but for OSEP it will be fine
        // IV needs to be 16 characters in length if using the keysize of 256
        public static string Encrypt(byte[] PlainTextBytes, string Password = "8687ggvuytfdYVGVVYRTFYTFytgiyut6",
              string Salt = "igfiIFYT756", string HashAlgorithm = "SHA1",
              int PasswordIterations = 420, string InitialVector = "OFRna73m*aze01xY",
              int KeySize = 256)
        {
            byte[] InitialVectorBytes = Encoding.ASCII.GetBytes(InitialVector);
            byte[] SaltValueBytes = Encoding.ASCII.GetBytes(Salt);
            PasswordDeriveBytes DerivedPassword = new PasswordDeriveBytes(Password, SaltValueBytes, HashAlgorithm, PasswordIterations);
            byte[] KeyBytes = DerivedPassword.GetBytes(KeySize / 8);
            RijndaelManaged SymmetricKey = new RijndaelManaged();
            SymmetricKey.Mode = CipherMode.CBC;
            byte[] CipherTextBytes = null;
            using (ICryptoTransform Encryptor = SymmetricKey.CreateEncryptor(KeyBytes, InitialVectorBytes))
            {
                using (MemoryStream MemStream = new MemoryStream())
                {
                    using (CryptoStream CryptoStream = new CryptoStream(MemStream, Encryptor, CryptoStreamMode.Write))
                    {
                        CryptoStream.Write(PlainTextBytes, 0, PlainTextBytes.Length);
                        CryptoStream.FlushFinalBlock();
                        CipherTextBytes = MemStream.ToArray();
                        MemStream.Close();
                        CryptoStream.Close();
                    }
                }
            }
            SymmetricKey.Clear();
            return Convert.ToBase64String(CipherTextBytes);
        }


        static void Main(string[] args)
        {
            // Supply shellcode here
            byte[] sc = new byte[1] { 0x00 };
            string sc64 = Encrypt(sc);
            Console.WriteLine(sc64);
            Console.WriteLine("\n\nHere is the B64 Encoded Encrypted Shellcode. Copy the output above then press enter to exit...");
            Console.ReadLine();
        }
    }
}

using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Common
{
    /// <summary>
    /// Clase que permite la encriptación de desencriptación de una cadena de texto o arreglo de bytes.
    /// </summary>
    public static class CipherSecurity
    {
        /// <summary>
        /// Encriptar una cadena de texto.
        /// </summary>
        /// <param name="plainText"></param>
        /// <param name="password"></param>
        /// <returns>Resultado de encriptación.</returns>
        public static string Encrypt(string plainText, string password)
        {
            if (plainText == null)
            {
                return null;
            }

            if (password == null)
            {
                password = String.Empty;
            }

            var bytesToBeEncrypted = Encoding.UTF8.GetBytes(plainText);
            var passwordBytes = Encoding.UTF8.GetBytes(password);
            passwordBytes = SHA256.Create().ComputeHash(passwordBytes);
            var bytesEncrypted = CipherSecurity.Encrypt(bytesToBeEncrypted, passwordBytes);
            return Convert.ToBase64String(bytesEncrypted);
        }

        /// <summary>
        /// Desencripta una cadena de texto.
        /// </summary>
        /// <param name="encryptedText"></param>
        /// <param name="password"></param>
        /// <exception cref="FormatException"></exception>
        /// <returns>Resultado de desencriptación.</returns>
        public static string Decrypt(string encryptedText, string password)
        {
            if (encryptedText == null)
            {
                return null;
            }

            if (password == null)
            {
                password = String.Empty;
            }

            var bytesToBeDecrypted = Convert.FromBase64String(encryptedText);
            var passwordBytes = Encoding.UTF8.GetBytes(password);
            passwordBytes = SHA256.Create().ComputeHash(passwordBytes);
            var bytesDecrypted = CipherSecurity.Decrypt(bytesToBeDecrypted, passwordBytes);

            return Encoding.UTF8.GetString(bytesDecrypted);
        }

        /// <summary>
        /// Encripta un arreglo de bytes.
        /// </summary>
        /// <param name="bytesToBeEncrypted"></param>
        /// <param name="passwordBytes"></param>
        /// <returns>Resultado de encriptación.</returns>
        private static byte[] Encrypt(byte[] bytesToBeEncrypted, byte[] passwordBytes)
        {
            byte[] encryptedBytes = null;

            // Set your salt here, change it to meet your flavor:
            // The salt bytes must be at least 8 bytes.
            var saltBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };

            using (MemoryStream ms = new MemoryStream())
            {
                using (RijndaelManaged AES = new RijndaelManaged())
                {
                    var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);

                    AES.KeySize = 256;
                    AES.BlockSize = 128;
                    AES.Key = key.GetBytes(AES.KeySize / 8);
                    AES.IV = key.GetBytes(AES.BlockSize / 8);

                    AES.Mode = CipherMode.CBC;

                    using (var cs = new CryptoStream(ms, AES.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(bytesToBeEncrypted, 0, bytesToBeEncrypted.Length);
                        cs.Close();
                    }

                    encryptedBytes = ms.ToArray();
                }
            }

            return encryptedBytes;
        }

        /// <summary>
        /// Desencripta un arreglo de bytes.
        /// </summary>
        /// <param name="bytesToBeDecrypted"></param>
        /// <param name="passwordBytes"></param>
        /// <returns>Resultado de desencriptación.</returns>
        private static byte[] Decrypt(byte[] bytesToBeDecrypted, byte[] passwordBytes)
        {
            byte[] decryptedBytes = null;

            // Set your salt here, change it to meet your flavor:
            // The salt bytes must be at least 8 bytes.
            var saltBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };

            using (MemoryStream ms = new MemoryStream())
            {
                using (RijndaelManaged AES = new RijndaelManaged())
                {
                    var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);

                    AES.KeySize = 256;
                    AES.BlockSize = 128;
                    AES.Key = key.GetBytes(AES.KeySize / 8);
                    AES.IV = key.GetBytes(AES.BlockSize / 8);
                    AES.Mode = CipherMode.CBC;

                    using (var cs = new CryptoStream(ms, AES.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(bytesToBeDecrypted, 0, bytesToBeDecrypted.Length);
                        cs.Close();
                    }

                    decryptedBytes = ms.ToArray();
                }
            }

            return decryptedBytes;
        }
    }
}

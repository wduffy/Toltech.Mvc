using System.IO;
using System.Security.Cryptography;
using System.Text;
using System;

namespace Toltech.Mvc.Tools
{
    public static class EncryptionTools
    {

        /// <summary>
        /// Hashes the string using SHA256. SHA allows only one way encrpytion.
        /// </summary>
        /// <param name="input">Text string to encrypt</param>
        /// <param name="salt">Salt to shake on the text being encrypted</param>
        /// <returns>Encrypted text as a string</returns>
        public static string ShaHash(string input, string salt)
        {
            var bytes = new UnicodeEncoding().GetBytes(salt + input + salt);            
            var hash = new SHA256Managed().ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }

        /// <summary>
        /// Encrypts input string using Rijndael (AES) algorithm with CBC blocking and PKCS7 padding. AES allows both encryption and decryption.
        /// </summary>
        /// <param name="input">text string to encrypt </param>
        /// <returns>Encrypted text in Byte array</returns>
        /// <remarks>The key and IV are the same, and use encryptionKey.</remarks>
        public static byte[] AesEncrypt(string input, string encryptionKey)
        {
            RijndaelManaged aes = new RijndaelManaged();
            byte[] outputBytes;

            // Set the mode, padding and block size for the key
            aes.Padding = PaddingMode.PKCS7;
            aes.Mode = CipherMode.CBC;
            aes.KeySize = 128; // 16 bytes
            aes.BlockSize = 128; // 16 bytes

            // Convert key and plain text input into byte arrays
            byte[] keyBytes = Encoding.UTF8.GetBytes(encryptionKey);
            byte[] inputBytes = Encoding.UTF8.GetBytes(input);

            // Create streams and encryptor object
            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(memoryStream, aes.CreateEncryptor(keyBytes, keyBytes), CryptoStreamMode.Write);

            // Perform encryption
            cryptoStream.Write(inputBytes, 0, inputBytes.Length);
            cryptoStream.FlushFinalBlock();

            // Get encrypted stream into byte array
            outputBytes = memoryStream.ToArray();
            
            // Close streams
            memoryStream.Close();
            cryptoStream.Close();
            aes.Clear();
            
            return outputBytes;
        }

        /// <summary>
        /// Decrypts input string from Rijndael (AES) algorithm with CBC blocking and PKCS7 padding. AES allows both encryption and decryption.
        /// </summary>
        /// <param name="input">Encrypted binary array to decrypt</param>
        /// <returns>string of Decrypted data</returns>
        /// <remarks>The key and IV are the same, and use encryptionKey.</remarks>
        public static string AesDecrypt(byte[] input, string encryptionKey)
        {
            RijndaelManaged aes = new RijndaelManaged();
            byte[] keyBytes = Encoding.UTF8.GetBytes(encryptionKey);
            byte[] outputBytes = new byte[input.Length];

            // Set the mode, padding and block size
            aes.Padding = PaddingMode.PKCS7;
            aes.Mode = CipherMode.CBC;
            aes.KeySize = 128; // 16 bytes
            aes.BlockSize = 128; // 16 bytes

            // Create streams and decryptor object
            MemoryStream memoryStream = new MemoryStream(input);
            CryptoStream cryptoStream = new CryptoStream(memoryStream, aes.CreateDecryptor(keyBytes, keyBytes), CryptoStreamMode.Read);

            // Perform decryption
            int decryptedByteCount = cryptoStream.Read(outputBytes, 0, outputBytes.Length);

            // Close streams
            memoryStream.Close();
            cryptoStream.Close();
            aes.Clear();

            // Convert decrypted data into string, assuming original text was UTF-8 encoded
            return Encoding.UTF8.GetString(outputBytes, 0, decryptedByteCount);
        }

    }
}

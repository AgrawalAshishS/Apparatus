// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EncryptionUtility.cs" company="Toshal Infotech">
//   http://www.ToshalInfotech.com
//   Copyright (c) 2022-23
//   by Toshal Infotech
//   Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated 
//   documentation files (the "Software"), to deal in the Software without restriction, including without limitation 
//   the rights to use, copy, modify, merge, publish, distribute, sub-license, and/or sell copies of the Software, and 
//   to permit persons to whom the Software is furnished to do so, subject to the following conditions:
//   The above copyright notice and this permission notice shall be included in all copies or substantial portions 
//   of the Software.
//   THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED 
//   TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL 
//   THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF 
//   CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
//   DEALINGS IN THE SOFTWARE.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Security.Cryptography;
using System.Text;

namespace Apparatus
{
    /// <summary>
    /// Simple string encryption and decryption using a passphrase.
    /// </summary>
    /// <remarks>
    /// <para>
    /// <b>Not for sensitive data.</b> This class uses Triple DES in ECB mode with a key made from an MD5 hash of the passphrase.
    /// These are old algorithms and modes. The same text always gives the same result, and there is no protection against tampering.
    /// Use it only to lightly hide values (for example query string values). For passwords, personal data or anything that needs
    /// real security, use AES-GCM from <c>System.Security.Cryptography</c>.
    /// </para>
    /// <para>
    /// Only the first 48 characters of the passphrase are used. A shorter passphrase is padded with the letter X.
    /// </para>
    /// </remarks>
    public static class EncryptionUtility
    {
        /// <summary>
        /// Decrypts text that was encrypted by <see cref="EncryptString"/> with the same passphrase.
        /// </summary>
        /// <param name="source">The Base64 encrypted text.</param>
        /// <param name="passphrase">The same passphrase that was used to encrypt.</param>
        /// <returns>The original text.</returns>
        /// <exception cref="FormatException"><paramref name="source"/> is not valid Base64.</exception>
        /// <exception cref="System.Security.Cryptography.CryptographicException">The passphrase is wrong or the data is damaged.</exception>
        /// <example>
        /// <code>
        /// string plain = encrypted.DecryptString("my secret phrase");
        /// </code>
        /// </example>
        public static string DecryptString([NotNull] this string source, [NotNull] string passphrase)
        {
            byte[] results;
            var utf8 = new UTF8Encoding();

            int len = 48;

            //convert key to 16 characters for simplicity
            if (passphrase.Length < len)
            {
                passphrase = passphrase + "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX".Substring(0, len - passphrase.Length);
            }
            else
            {
                passphrase = passphrase.Substring(0, len);
            }

            //hash the passphrase using MD5 to create 128bit byte array

            using (var hashProvider = MD5.Create())
            {
                var hash = GetMd5Hash(hashProvider, passphrase);
                if (hash.Length > 24) hash = hash.Substring(0, 24);
                byte[] tdesKey = Encoding.UTF8.GetBytes(hash);

                using (var tdesAlgorithm = TripleDES.Create())
                {
                    tdesAlgorithm.Key = tdesKey;
                    tdesAlgorithm.Mode = CipherMode.ECB;
                    tdesAlgorithm.Padding = PaddingMode.PKCS7;


                    byte[] dataToDecrypt = Convert.FromBase64String(source);
                    ICryptoTransform decryptor = tdesAlgorithm.CreateDecryptor();
                    results = decryptor.TransformFinalBlock(dataToDecrypt, 0, dataToDecrypt.Length);
                }
            }

            return utf8.GetString(results);
        }

        /// <summary>
        /// Encrypts text with a passphrase and returns it as a Base64 string.
        /// </summary>
        /// <param name="source">The text to encrypt.</param>
        /// <param name="passphrase">The secret phrase. Keep it private; you need the same one to decrypt.</param>
        /// <returns>The encrypted text in Base64 format.</returns>
        /// <example>
        /// <code>
        /// string encrypted = "hello".EncryptString("my secret phrase");
        /// string plain = encrypted.DecryptString("my secret phrase"); // "hello"
        /// </code>
        /// </example>
        public static string EncryptString([NotNull] this string source, [NotNull] string passphrase)
        {
            byte[] results;
            var utf8 = new UTF8Encoding();

            int len = 48;

            //convert key to 16 characters for simplicity
            if (passphrase.Length < len)
            {
                passphrase = passphrase + "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX".Substring(0, len - passphrase.Length);
            }
            else
            {
                passphrase = passphrase.Substring(0, len);
            }

            //hash the passphrase using MD5 to create 128bit byte array
            using (var hashProvider = MD5.Create())
            {
                var hash = GetMd5Hash(hashProvider, passphrase);
                if (hash.Length > 24) hash = hash.Substring(0, 24);
                byte[] tdesKey = Encoding.UTF8.GetBytes(hash);

                using (var tdesAlgorithm = TripleDES.Create())
                {
                    tdesAlgorithm.Key = tdesKey;
                    tdesAlgorithm.Mode = CipherMode.ECB;
                    tdesAlgorithm.Padding = PaddingMode.PKCS7;

                    byte[] dataToEncrypt = utf8.GetBytes(source);

                    ICryptoTransform encryptor = tdesAlgorithm.CreateEncryptor();
                    results = encryptor.TransformFinalBlock(dataToEncrypt, 0, dataToEncrypt.Length);
                }
            }

            //Return the encrypted string as a base64 encoded string 
            return Convert.ToBase64String(results);
        }

        static string GetMd5Hash(MD5 md5Hash, string input)
        {
            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new StringBuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }
    }
}

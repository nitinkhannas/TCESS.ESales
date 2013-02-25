#region Using directives

using System;
using System.Security.Cryptography;
using System.Text;

#endregion

namespace TCESS.ESales.CommonLayer.CommonLibrary
{
    public class EncryptDecrypt
    {
        #region Static Member Variable

        /// <summary>
        /// Secret key to encrypt data.
        /// </summary>
        static string desKey = "m/ti5TXBWPOigPCSqBy0Kg==";

        /// <summary>
        /// Object for SymmetricAlgorithm.
        /// </summary>
        protected static SymmetricAlgorithm DES = null;

        #endregion

        #region Public Methods

        /// <summary>
        ///  Encrypts the offline transaction file using the TripleDES cryptography.
        /// </summary>
        /// <param name="Password"></param>
        /// <returns></returns>
        public static string EncryptPassword(string Password)
        {
            string testPwd = string.Empty;

            DES = new TripleDESCryptoServiceProvider();
            byte[] plaintext = Encoding.ASCII.GetBytes(Password);
            DES.Key = ParseKey(desKey);
            DES.IV = GetIV();
            //string decPwd = DecryptPassword("Lw5AEvoSG+7VlrMK+XgmGw==");
            byte[] encrypted = DES.CreateEncryptor().TransformFinalBlock(plaintext, 0, plaintext.Length);
            return Convert.ToBase64String(encrypted);
        }

        /// <summary>
        ///  Decrypts the offline transaction file using the TripleDES cryptography.
        /// </summary>
        /// <param name="Password"> Password to be decrypted</param>
        /// <returns></returns>
        public static string DecryptPassword(string Password)
        {
            DES = new TripleDESCryptoServiceProvider();
            byte[] encryptedBytes = Convert.FromBase64String(Password);
            byte[] decryptedBytes = DES.CreateDecryptor().TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length);
            return Encoding.UTF8.GetString(decryptedBytes);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Parse the key in bytes.
        /// </summary>
        /// <param name="data">Key</param>
        /// <returns>Array of bytes</returns>
        private static byte[] ParseKey(string data)
        {
            // Convert a Base64 encoded key into a byte array
            // as required by the encryption classes
            byte[] key = Convert.FromBase64String(data);
            return key;
        }

        // Generate an initialisation vector, the encryptor and
        // decryptor must share the same value. This vector is
        // used to obscure the first block of data encrypted
        // to make it more difficult for an attacker to figure
        // out the encryption key by looking for known patterns
        // in the data.
        // 
        // The length of the initialisation vector should be the
        // same size as the blocks the symmetric encryption algorithm
        // processes at a time. Notice however, that the BlockSize
        // is measured in bits and there are 8bits to a byte, so we
        // must divide the value by 8.

        // In this sample we simply use an initialisation vector
        // made up of 0's. This is not very secure, ideally we
        // should use something a bit more "random".
        private static byte[] GetIV()
        {

            byte[] iv = new byte[DES.BlockSize / 8];
            return iv;
        }

        #endregion
    }
}
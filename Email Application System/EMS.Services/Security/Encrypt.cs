using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace EMS.Services.Security
{
    public static class Encrypt
    {  
        // This size of the IV (in bytes) must = (keysize / 8).  Default keysize is 256, so the IV must be
        // 32 bytes long.  Using a 16 character string here gives us 32 bytes when converted to a byte array.
        private const string initVector = "pemgail9uzpgzl88";
        // This constant is used to determine the keysize of the encryption algorithm
        private const int keysize = 256;
        private const string passPhrase = "password";

        //Encrypt
        public static string EncryptData(string plainText)
        {
            byte[] initVectorBytes = Encoding.UTF8.GetBytes(initVector);
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            PasswordDeriveBytes password = new PasswordDeriveBytes(passPhrase, null);
            byte[] keyBytes = password.GetBytes(keysize / 8);
            RijndaelManaged symmetricKey = new RijndaelManaged();
            symmetricKey.Mode = CipherMode.CBC;
            ICryptoTransform encryptor = symmetricKey.CreateEncryptor(keyBytes, initVectorBytes);
            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
            cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
            cryptoStream.FlushFinalBlock();
            byte[] cipherTextBytes = memoryStream.ToArray();
            memoryStream.Close();
            cryptoStream.Close();
            return Convert.ToBase64String(cipherTextBytes);
        }
        //Decrypt
        public static string DecryptData(string cipherText)
        {
            byte[] initVectorBytes = Encoding.UTF8.GetBytes(initVector);
            byte[] cipherTextBytes = Convert.FromBase64String(cipherText);
            PasswordDeriveBytes password = new PasswordDeriveBytes(passPhrase, null);
            byte[] keyBytes = password.GetBytes(keysize / 8);
            RijndaelManaged symmetricKey = new RijndaelManaged();
            symmetricKey.Mode = CipherMode.CBC;
            ICryptoTransform decryptor = symmetricKey.CreateDecryptor(keyBytes, initVectorBytes);
            MemoryStream memoryStream = new MemoryStream(cipherTextBytes);
            CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
            byte[] plainTextBytes = new byte[cipherTextBytes.Length];
            int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
            memoryStream.Close();
            cryptoStream.Close();
            return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
        }
    




    //public static byte[] EncryptData(string data)
    //{
    //    try
    //    {
    //        // Create a new instance of the RijndaelManaged
    //        // class.  This generates a new key and initialization 
    //        // vector (IV).
    //        using (RijndaelManaged myRijndael = new RijndaelManaged())
    //        {

    //            myRijndael.GenerateKey();
    //            myRijndael.GenerateIV();
    //            // Encrypt the string to an array of bytes.
    //            byte[] encrypted = EncryptStringToBytes(data, myRijndael.Key, myRijndael.IV);

    //            return encrypted;
    //        }

    //    }
    //    catch (Exception e)
    //    {
    //        throw new ArgumentException(e.Message);
    //    }
    //}

    //public static string DecryptData(byte[] encryptedData)
    //{
    //    try
    //    {
    //        // Create a new instance of the RijndaelManaged
    //        // class.  This generates a new key and initialization 
    //        // vector (IV).
    //        using (RijndaelManaged myRijndael = new RijndaelManaged())
    //        {
    //            myRijndael.GenerateKey();
    //            myRijndael.GenerateIV();

    //            // Decrypt the bytes to a string.
    //            string decryptedData = DecryptStringFromBytes(encryptedData, myRijndael.Key, myRijndael.IV);

    //            return decryptedData;
    //        }

    //    }
    //    catch (Exception e)
    //    {
    //        throw new ArgumentException(e.Message);
    //    }
    //}

    //private static byte[] EncryptStringToBytes(string plainText, byte[] Key, byte[] IV)
    //{
    //    // Check arguments.
    //    if (plainText == null || plainText.Length <= 0)
    //        throw new ArgumentNullException("plainText");
    //    if (Key == null || Key.Length <= 0)
    //        throw new ArgumentNullException("Key");
    //    if (IV == null || IV.Length <= 0)
    //        throw new ArgumentNullException("IV");
    //    byte[] encrypted;
    //    // Create an RijndaelManaged object
    //    // with the specified key and IV.
    //    using (RijndaelManaged rijAlg = new RijndaelManaged())
    //    {
    //        rijAlg.Key = Key;
    //        rijAlg.IV = IV;

    //        // Create an encryptor to perform the stream transform.
    //        ICryptoTransform encryptor = rijAlg.CreateEncryptor(rijAlg.Key, rijAlg.IV);

    //        // Create the streams used for encryption.
    //        using (MemoryStream msEncrypt = new MemoryStream())
    //        {
    //            using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
    //            {
    //                using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
    //                {
    //                    //Write all data to the stream.
    //                    swEncrypt.Write(plainText);
    //                }
    //                encrypted = msEncrypt.ToArray();
    //            }
    //        }
    //    }

    //    // Return the encrypted bytes from the memory stream.
    //    return encrypted;
    //}

    //private static string DecryptStringFromBytes(byte[] cipherText, byte[] Key, byte[] IV)
    //{
    //    // Check arguments.
    //    if (cipherText == null || cipherText.Length <= 0)
    //        throw new ArgumentNullException("cipherText");
    //    if (Key == null || Key.Length <= 0)
    //        throw new ArgumentNullException("Key");
    //    if (IV == null || IV.Length <= 0)
    //        throw new ArgumentNullException("IV");

    //    // Declare the string used to hold
    //    // the decrypted text.
    //    string plaintext = null;

    //    // Create an RijndaelManaged object
    //    // with the specified key and IV.
    //    using (RijndaelManaged rijAlg = new RijndaelManaged())
    //    {
    //        rijAlg.Key = Key;
    //        rijAlg.IV = IV;

    //        // Create a decryptor to perform the stream transform.
    //        ICryptoTransform decryptor = rijAlg.CreateDecryptor(rijAlg.Key, rijAlg.IV);

    //        // Create the streams used for decryption.
    //        using (MemoryStream msDecrypt = new MemoryStream(cipherText))
    //        {
    //            using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
    //            {
    //                using (StreamReader srDecrypt = new StreamReader(csDecrypt))
    //                {
    //                    // Read the decrypted bytes from the decrypting stream
    //                    // and place them in a string.
    //                    plaintext = srDecrypt.ReadToEnd();
    //                }
    //            }
    //        }
    //    }

    //    return plaintext;
    //}
}

}

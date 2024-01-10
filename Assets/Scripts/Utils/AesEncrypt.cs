using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

public class AesEncrypt
{
    // Encrypt string using AES-256 CBC mode with a new IV each time
    public static string EncryptString(string plainText, string key, out string iv)
    {
        using (AesManaged aesAlg = new AesManaged())
        {
            aesAlg.Key = System.Text.Encoding.UTF8.GetBytes(key);
            aesAlg.Mode = CipherMode.CBC;
            aesAlg.Padding = PaddingMode.PKCS7;

            // Generate a new IV for each encryption
            aesAlg.GenerateIV();
            iv = Convert.ToBase64String(aesAlg.IV.ToArray());

            using (ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV))
            using (MemoryStream msEncrypt = new MemoryStream())
            using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
            {
                // Write the actual data to the CryptoStream
                byte[] dataBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
                csEncrypt.Write(dataBytes, 0, dataBytes.Length);
                csEncrypt.FlushFinalBlock();

                // Convert the encrypted data to Base64 for string representation
                return Convert.ToBase64String(msEncrypt.ToArray());
            }
        }
    }

    // Decrypt string using AES-256 CBC mode
    public static string DecryptString(string encryptedText, string key, string iv)
    {
        // Convert Base64 string to byte array
        byte[] encryptedData = Convert.FromBase64String(encryptedText);

        using (AesManaged aesAlg = new AesManaged())
        {
            aesAlg.Key = System.Text.Encoding.UTF8.GetBytes(key);
            aesAlg.Mode = CipherMode.CBC;
            aesAlg.Padding = PaddingMode.PKCS7;
            aesAlg.IV = Convert.FromBase64String(iv);

            using (ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV))
            using (MemoryStream msDecrypt = new MemoryStream(encryptedData))
            using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
            using (StreamReader sr = new StreamReader(csDecrypt))
            {
                return sr.ReadToEnd();
            }
        }
    }

    public static string GenerateRandomAESKey()
    {
        using (var rngCryptoServiceProvider = new RNGCryptoServiceProvider())
        {
            byte[] keyBytes = new byte[16];
            rngCryptoServiceProvider.GetBytes(keyBytes);

            // Convert the byte array to a hexadecimal string
            StringBuilder keyBuilder = new StringBuilder(keyBytes.Length * 2);
            foreach (byte b in keyBytes)
            {
                keyBuilder.AppendFormat("{0:x2}", b);
            }

            return keyBuilder.ToString();
        }
    }
}

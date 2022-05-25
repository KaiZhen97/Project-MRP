using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace WebFrameWorkLib.BusinessLogic
{
    public class DataEncryption
    {
        public static string ComputeHash(string plainText,
                                            string hashAlgorithm,
                                            byte[] saltBytes)
        {
            if (saltBytes == null)
            {
                int minSaltSize = 4;
                int maxSaltSize = 8;

                Random random = new Random();
                int saltSize = random.Next(minSaltSize, maxSaltSize);

                saltBytes = new byte[saltSize];

                RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();

                rng.GetNonZeroBytes(saltBytes);
            }

            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);

            byte[] plainTextWithSaltBytes =
                    new byte[plainTextBytes.Length + saltBytes.Length];

            for (int i = 0; i < plainTextBytes.Length; i++)
                plainTextWithSaltBytes[i] = plainTextBytes[i];

            for (int i = 0; i < saltBytes.Length; i++)
                plainTextWithSaltBytes[plainTextBytes.Length + i] = saltBytes[i];

            HashAlgorithm hash;

            if (hashAlgorithm == null)
                hashAlgorithm = "";

            switch (hashAlgorithm.ToUpper())
            {
                case "SHA1":
                    hash = new SHA1Managed();
                    break;

                case "SHA256":
                    hash = new SHA256Managed();
                    break;

                case "SHA384":
                    hash = new SHA384Managed();
                    break;

                case "SHA512":
                    hash = new SHA512Managed();
                    break;

                default:
                    hash = new MD5CryptoServiceProvider();
                    break;
            }

            byte[] hashBytes = hash.ComputeHash(plainTextWithSaltBytes);

            byte[] hashWithSaltBytes = new byte[hashBytes.Length +
                                                saltBytes.Length];

            for (int i = 0; i < hashBytes.Length; i++)
                hashWithSaltBytes[i] = hashBytes[i];

            for (int i = 0; i < saltBytes.Length; i++)
                hashWithSaltBytes[hashBytes.Length + i] = saltBytes[i];

            string hashValue = Convert.ToBase64String(hashWithSaltBytes);

            return hashValue;
        }

        public static string ComputeHash(string plainText,
                                 //string hashAlgorithm,
                                 byte[] saltBytes)
        {
            string hashAlgorithm = "SHA1";

            if (saltBytes == null)
            {
                int minSaltSize = 4;
                int maxSaltSize = 8;

                Random random = new Random();
                int saltSize = random.Next(minSaltSize, maxSaltSize);

                saltBytes = new byte[saltSize];

                RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();

                rng.GetNonZeroBytes(saltBytes);
            }

            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);

            byte[] plainTextWithSaltBytes =
                    new byte[plainTextBytes.Length + saltBytes.Length];

            for (int i = 0; i < plainTextBytes.Length; i++)
                plainTextWithSaltBytes[i] = plainTextBytes[i];

            for (int i = 0; i < saltBytes.Length; i++)
                plainTextWithSaltBytes[plainTextBytes.Length + i] = saltBytes[i];

            HashAlgorithm hash;

            if (hashAlgorithm == null)
                hashAlgorithm = "";

            switch (hashAlgorithm.ToUpper())
            {
                case "SHA1":
                    hash = new SHA1Managed();
                    break;

                case "SHA256":
                    hash = new SHA256Managed();
                    break;

                case "SHA384":
                    hash = new SHA384Managed();
                    break;

                case "SHA512":
                    hash = new SHA512Managed();
                    break;

                default:
                    hash = new MD5CryptoServiceProvider();
                    break;
            }

            byte[] hashBytes = hash.ComputeHash(plainTextWithSaltBytes);

            byte[] hashWithSaltBytes = new byte[hashBytes.Length +
                                                saltBytes.Length];

            for (int i = 0; i < hashBytes.Length; i++)
                hashWithSaltBytes[i] = hashBytes[i];

            for (int i = 0; i < saltBytes.Length; i++)
                hashWithSaltBytes[hashBytes.Length + i] = saltBytes[i];

            string hashValue = Convert.ToBase64String(hashWithSaltBytes);

            return hashValue;
        }

        public static bool VerifyHash(string plainText,
                                      string hashAlgorithm,
                                      string hashValue)
        {
            byte[] hashWithSaltBytes = Convert.FromBase64String(hashValue);

            int hashSizeInBits, hashSizeInBytes;

            if (hashAlgorithm == null)
                hashAlgorithm = "";

            switch (hashAlgorithm.ToUpper())
            {
                case "SHA1":
                    hashSizeInBits = 160;
                    break;

                case "SHA256":
                    hashSizeInBits = 256;
                    break;

                case "SHA384":
                    hashSizeInBits = 384;
                    break;

                case "SHA512":
                    hashSizeInBits = 512;
                    break;

                default: // Must be MD5
                    hashSizeInBits = 128;
                    break;
            }

            hashSizeInBytes = hashSizeInBits / 8;

            if (hashWithSaltBytes.Length < hashSizeInBytes)
                return false;

            byte[] saltBytes = new byte[hashWithSaltBytes.Length -
                                        hashSizeInBytes];

            for (int i = 0; i < saltBytes.Length; i++)
                saltBytes[i] = hashWithSaltBytes[hashSizeInBytes + i];

            string expectedHashString =
                        ComputeHash(plainText, hashAlgorithm, saltBytes);

            return (hashValue == expectedHashString);
        }

        public static bool VerifyHash(string plainText,
                              string hashValue)
        {
            string hashAlgorithm = "SHA1";

            byte[] hashWithSaltBytes = Convert.FromBase64String(hashValue);

            int hashSizeInBits, hashSizeInBytes;

            if (hashAlgorithm == null)
                hashAlgorithm = "";

            switch (hashAlgorithm.ToUpper())
            {
                case "SHA1":
                    hashSizeInBits = 160;
                    break;

                case "SHA256":
                    hashSizeInBits = 256;
                    break;

                case "SHA384":
                    hashSizeInBits = 384;
                    break;

                case "SHA512":
                    hashSizeInBits = 512;
                    break;

                default: // Must be MD5
                    hashSizeInBits = 128;
                    break;
            }

            hashSizeInBytes = hashSizeInBits / 8;

            if (hashWithSaltBytes.Length < hashSizeInBytes)
                return false;

            byte[] saltBytes = new byte[hashWithSaltBytes.Length -
                                        hashSizeInBytes];

            for (int i = 0; i < saltBytes.Length; i++)
                saltBytes[i] = hashWithSaltBytes[hashSizeInBytes + i];

            string expectedHashString =
                        ComputeHash(plainText, hashAlgorithm, saltBytes);

            return (hashValue == expectedHashString);
        }


        public static string SHA1HashStringForUTF8String(string s)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(s);

            var sha1 = SHA1.Create();
            byte[] hashBytes = sha1.ComputeHash(bytes);

            return HexStringFromBytes(hashBytes);
        }

        /// <summary>
        /// Convert an array of bytes to a string of hex digits
        /// </summary>
        /// <param name="bytes">array of bytes</param>
        /// <returns>String of hex digits</returns>
        public static string HexStringFromBytes(byte[] bytes)
        {
            var sb = new StringBuilder();
            foreach (byte b in bytes)
            {
                var hex = b.ToString("x2");
                sb.Append(hex);
            }
            return sb.ToString();
        }

        public static string HashDataToSha(string dataStr, string shakey)
        {
            //ConfigurationManager.AppSettings["ShaSecretKey"].ToString()--bakup
            string hashedStr = SHA1HashStringForUTF8String(dataStr + shakey);
            return hashedStr;
        }

        public static string GetEncrypt(string input)
        {
            // Create a new Stringbuilder to collect the bytes and create a string.
            StringBuilder sBuilder = new StringBuilder();
            using (MD5 md5Hash = MD5.Create())
            {
                // Convert the input string to a byte array and compute the hash. 
                byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

                // Loop through each byte of the hashed data and format each one as a hexadecimal string. 
                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }
            }

            // Return the hexadecimal string. 
            return sBuilder.ToString();
        }
    }
}
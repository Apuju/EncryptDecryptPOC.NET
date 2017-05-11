using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;

namespace EncryptDecryptHelper
{
    public class AESCryptography
    {
        public byte[] EncryptData(string data, ByteSerializer.StringEncodingType type)
        {
            byte[] cipherText;
            // Create a byte array to hold the random value. 
            byte[] password = new byte[256];
            byte[] salt = new byte[8];
            int iterations = 1000;
            try
            {
                
                using (RNGCryptoServiceProvider rngCsp = new RNGCryptoServiceProvider())
                {
                    // Fill the array with a random value.
                    rngCsp.GetBytes(password);
                    rngCsp.GetBytes(salt);
                }
                Rfc2898DeriveBytes keyGen = new Rfc2898DeriveBytes(password, salt, iterations);
                byte[] key = keyGen.GetBytes(32);
                byte[] iv = keyGen.GetBytes(16);
                RijndaelManaged cipher = new RijndaelManaged { Key = key, KeySize = 256, IV = iv, BlockSize = 128 };
                
                using (ICryptoTransform encryptor = cipher.CreateEncryptor()) 
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                        {
                            ByteSerializer serializer = new ByteSerializer();
                            cs.Write(serializer.ToBytes(data, type), 0, data.Length);
                            cs.FlushFinalBlock();
                            cipherText = ms.ToArray();
                        }

                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return cipherText;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace EncryptDecryptHelper
{
    public class EncryptHelper
    {
        public enum CryptographyAlgorithm
        {
            AES
        }

        public enum HashByteAlgorithm
        {
            Base32,
            Base64
        }

        public string EnCryptData(string data, CryptographyAlgorithm ca, HashByteAlgorithm ha)
        {
            string encryptText = string.Empty;
            if (!string.IsNullOrEmpty(data))
            {
                byte[] chiperText = null;
                try
                {
                    switch (ca)
                    {
                        case CryptographyAlgorithm.AES:
                            {
                                AESCryptography aes = new AESCryptography();
                                chiperText = aes.EncryptData(data, ByteSerializer.StringEncodingType.UTF8);
                                break;
                            }
                    }
                }
                catch (Exception)
                {
                    throw;
                }
                if (chiperText != null)
                {
                    try
                    {
                        switch (ha)
                        {
                            case HashByteAlgorithm.Base32:
                                {
                                    Base32Encoding base32 = new Base32Encoding();
                                    encryptText = base32.EncodeData(chiperText);
                                    break;
                                }
                            case HashByteAlgorithm.Base64:
                                {
                                    Base64Encoding base64 = new Base64Encoding();
                                    encryptText = base64.EncodeData(chiperText);
                                    break;
                                }
                        }
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
            }
            return encryptText;
        }
    }
}

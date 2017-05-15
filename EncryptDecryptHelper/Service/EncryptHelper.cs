using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using EncryptDecryptHelper.Bussiness;
using EncryptDecryptHelper.SharedTypes;

namespace EncryptDecryptHelper.Service
{
    public class EncryptHelper
    {
        private string m_Password = string.Empty;
        public string Password
        {
            get
            {
                return m_Password;
            }
        }

        private string m_Salt = string.Empty;
        public string Salt
        {
            get
            {
                return m_Salt;
            }
        }

        public string EncryptData(string data, Algorithm.StringTransformationFormat inputStringEncode, Algorithm.Cryptography cryptographyAlgorithm, Algorithm.StringEncodeFormat outputStringEncode)
        {
            string encryptText = string.Empty;
            if (!string.IsNullOrEmpty(data))
            {
                byte[] ciperText = null;
                try
                {
                    BinaryEncodeHelper encodeHelper = new BinaryEncodeHelper(Algorithm.StringTransformationFormat.UTF8);
                    byte[] dataBinary = encodeHelper.Decode(data);

                    switch (outputStringEncode)
                    {
                        case Algorithm.StringEncodeFormat.Base32:
                            {
                                encodeHelper.SetEncodeFormat(Algorithm.StringTransformationFormat.Base32);
                                break;
                            }
                        case Algorithm.StringEncodeFormat.Base64:
                            {
                                encodeHelper.SetEncodeFormat(Algorithm.StringTransformationFormat.Base64);
                                break;
                            }
                    }
                    switch (cryptographyAlgorithm)
                    {
                        case Algorithm.Cryptography.AES:
                            {
                                AESCryptography aes = new AESCryptography();
                                ciperText = aes.EncryptData(dataBinary);
                                m_Password = encodeHelper.Encode(aes.Password);
                                m_Salt = encodeHelper.Encode(aes.Salt);
                                break;
                            }
                    }

                    if (ciperText != null)
                    {
                        encryptText = encodeHelper.Encode(ciperText);
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return encryptText;
        }
    }
}

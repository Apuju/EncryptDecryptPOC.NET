using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EncryptDecryptHelper.SharedTypes;
using EncryptDecryptHelper.Bussiness;

namespace EncryptDecryptHelper.Service
{
    public class DecryptHelper
    {
        private string m_Password = string.Empty;
        private string m_Salt = string.Empty;

        public DecryptHelper(string password = null, string salt = null)
        {
            m_Password = password;
            m_Salt = salt;
        }

        public string DecryptData(string encryptText, Algorithm.StringEncodeFormat inputStringEncode, Algorithm.Cryptography cryptographyAlgorithm, Algorithm.StringTransformationFormat outputStringEncode)
        {
            string data = string.Empty;
            if (!string.IsNullOrEmpty(encryptText))
            {
                byte[] cipherText = null;
                byte[] password = null;
                byte[] salt = null;
                try
                {
                    BinaryEncodeHelper encodeHelper = new BinaryEncodeHelper(Algorithm.StringTransformationFormat.Base64);
                    switch (inputStringEncode)
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
                    cipherText = encodeHelper.Decode(encryptText);
                    if (!string.IsNullOrEmpty(m_Password))
                    {
                        password = encodeHelper.Decode(m_Password);
                    }
                    if (!string.IsNullOrEmpty(m_Salt))
                    {
                        salt = encodeHelper.Decode(m_Salt);
                    }

                    byte[] text = null;
                    switch (cryptographyAlgorithm)
                    {
                        case Algorithm.Cryptography.AES:
                            {
                                if (cipherText != null && password != null && salt != null)
                                {
                                    AESCryptography aes = new AESCryptography(password, salt);
                                    text = aes.DecryptData(cipherText);
                                }
                                break;
                            }
                        case Algorithm.Cryptography.RSA:
                            {
                                RSACryptography rsa = new RSACryptography();
                                text = rsa.DecryptData(cipherText);
                                break;
                            }
                    }
                    if (text != null)
                    {
                        encodeHelper.SetEncodeFormat(outputStringEncode);
                        data = encodeHelper.Encode(text);
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return data;
        }
    }
}

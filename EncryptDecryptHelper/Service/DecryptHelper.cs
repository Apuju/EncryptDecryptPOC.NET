﻿using System;
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

        public DecryptHelper(string password, string salt)
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
                    password = encodeHelper.Decode(m_Password);
                    salt = encodeHelper.Decode(m_Salt);

                    if (cipherText != null && password != null && salt != null)
                    {
                        AESCryptography aes = new AESCryptography(password, salt);
                        byte[] text = aes.DecryptData(cipherText);
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

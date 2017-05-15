using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;

namespace EncryptDecryptHelper.Bussiness
{
    public class AESCryptography
    {
        private const int m_Iterations = 1000;
        private const int m_AESKeySize = 256;
        private const int m_AESBlockSize = 128;
        private byte[] m_Key = null;
        private byte[] m_IV = null;

        private byte[] m_Password = new byte[256];
        public byte[] Password
        {
            get
            {
                return m_Password;
            }
            set
            {
                m_Password = value;
            }
        }

        private byte[] m_Salt = new byte[8];
        public byte[] Salt
        {
            get
            {
                return m_Salt;
            }
            set
            {
                m_Salt = value;
            }
        }

        public AESCryptography(byte[] password = null, byte[] salt = null)
        {
            //Generates Password and Salt
            RandomWrapper random = new RandomWrapper();
            if (password == null)
                m_Password = random.GenerateData(256);
            else
                m_Password = password;
            if (salt == null)
                m_Salt = random.GenerateData(8);
            else
                m_Salt = salt;
            //Generates Key and IV for AES
            HashWrapper hash = new HashWrapper();
            m_Key = hash.HashRFC2898(m_Password, m_Salt, m_Iterations, m_AESKeySize / 8);
            m_IV = hash.HashRFC2898(m_Password, m_Salt, m_Iterations, m_AESBlockSize / 8);
        }

        private byte[] PerformCryptography(ICryptoTransform transformer, byte[] data)
        {
            byte[] trasformedData = null;
            try
            {
                using(MemoryStream contentStream = new MemoryStream())
                using (CryptoStream cs = new CryptoStream(contentStream, transformer, CryptoStreamMode.Write))
                {
                    cs.Write(data, 0, data.Length);
                    cs.FlushFinalBlock();
                    trasformedData = contentStream.ToArray();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return trasformedData;
        }

        private byte[] EncryptData(byte[] data, byte[] key, byte[] iv)
        {
            byte[] cipherText = null;
            try
            {
                using (RijndaelManaged cipher = new RijndaelManaged() { KeySize = m_AESKeySize, BlockSize = m_AESBlockSize })
                {
                    using (ICryptoTransform encryptor = cipher.CreateEncryptor(m_Key, m_IV))
                    {
                        cipherText = PerformCryptography(encryptor, data);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return cipherText;
        }

        public byte[] EncryptData(byte[] data)
        {
            byte[] cipherText = null;
            try
            {
                cipherText = EncryptData(data, m_Key, m_IV);
            }
            catch (Exception)
            {
                throw;
            }
            return cipherText;
        }

        private byte[] DecryptData(byte[] data, byte[] key, byte[] iv)
        {
            byte[] text = null;
            try
            {
                using (RijndaelManaged cipher = new RijndaelManaged())
                using (ICryptoTransform decryptor = cipher.CreateDecryptor(m_Key, m_IV))
                {
                    text = PerformCryptography(decryptor, data);
                }
                
            }
            catch (Exception)
            {
                throw;
            }
            return text;
        }

        public byte[] DecryptData(byte[] data)
        {
            byte[] text = null;
            try
            {
                text = DecryptData(data, m_Key, m_IV);
            }
            catch (Exception)
            {
                throw;
            }
            return text;
        }
    }
}

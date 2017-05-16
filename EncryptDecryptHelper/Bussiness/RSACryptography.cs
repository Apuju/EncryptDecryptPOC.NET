using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace EncryptDecryptHelper.Bussiness
{
    public class RSACryptography
    {
        private const string m_RSAKeyContainerName = "RSA_Container";

        private bool m_DoOAEPPadding = false;
        private CspParameters m_KeyContainer = new CspParameters();

        private string m_PublicKey = string.Empty;
        public string PublicKey
        {
            get
            {
                return m_PublicKey;
            }
        }

        public RSACryptography()
        {
            m_KeyContainer.KeyContainerName = m_RSAKeyContainerName;
            m_KeyContainer.Flags = CspProviderFlags.UseDefaultKeyContainer;
        }

        private byte[] EncryptData(byte[] data, RSAParameters rsaKeyInfo, bool doOAEPPadding)
        {
            byte[] encryptedText = null;
            try
            {
                using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
                {
                    //Import the RSA Key information. This only needs to include the public key information.
                    rsa.ImportParameters(rsaKeyInfo);
                    //Encrypt the passed byte array and specify OAEP padding.  
                    //OAEP padding is only available on Microsoft Windows XP or later.  
                    encryptedText = rsa.Encrypt(data, doOAEPPadding);
                }
            }
            catch (CryptographicException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
            return encryptedText;
        }

        public byte[] EncryptData(byte[] data, string publicKey = null)
        {
            byte[] encryptedText = null;
            try
            {
                using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(m_KeyContainer))
                {
                    string a = rsa.ToXmlString(true);
                    if (!string.IsNullOrEmpty(publicKey))
                    {
                        rsa.FromXmlString(publicKey);
                    }
                    m_PublicKey = rsa.ToXmlString(false);
                    encryptedText = EncryptData(data, rsa.ExportParameters(false), m_DoOAEPPadding);

                }
            }
            catch (Exception)
            {
                throw;
            }
            return encryptedText;
        }

        private byte[] DecryptData(byte[] encryptedData, RSAParameters rsaKeyInfo, bool doOAEPPadding)
        {
            byte[] text = null;
            try
            {                
                using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
                {
                    //Import the RSA Key information. This needs to include the private key information.
                    rsa.ImportParameters(rsaKeyInfo);
                    //Decrypt the passed byte array and specify OAEP padding.  
                    //OAEP padding is only available on Microsoft Windows XP or later.  
                    text = rsa.Decrypt(encryptedData, doOAEPPadding);
                }
            }
            catch (CryptographicException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
            return text;
        }

        public byte[] DecryptData(byte[] encryptedData)
        {
            byte[] text = null;
            try
            {
                using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(m_KeyContainer))
                {
                    text = DecryptData(encryptedData, rsa.ExportParameters(true), m_DoOAEPPadding);
                }
            }
            catch (CryptographicException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
            return text;
        }

        public void ClearKeyContainer()
        {
            try
            {
                using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(m_KeyContainer))
                {
                    rsa.PersistKeyInCsp = false;
                    rsa.Clear();
                }
            }
            catch (CryptographicException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

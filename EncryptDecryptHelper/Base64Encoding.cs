using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncryptDecryptHelper
{
    public class Base64Encoding
    {
        public string EncodeData(byte[] data)
        {
            string encryptText;
            try
            {
                encryptText = Convert.ToBase64String(data);
            }
            catch (Exception)
            {
                throw;
            }
            return encryptText;
        }
    }
}

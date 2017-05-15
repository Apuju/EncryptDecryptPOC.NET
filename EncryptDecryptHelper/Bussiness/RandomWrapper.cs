using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace EncryptDecryptHelper.Bussiness
{
    public class RandomWrapper
    {
        public byte[] GenerateData(int byteLength)
        {
            byte[] randomText = null;
            bool go = true;
            if (byteLength <= 0)
            {
                go = false;
            }
            else
            {
                randomText = new byte[byteLength];
            }
            if (go)
            {
                try
                {
                    using (RNGCryptoServiceProvider rngCsp = new RNGCryptoServiceProvider())
                    {
                        rngCsp.GetBytes(randomText);
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return randomText;
        }
    }
}

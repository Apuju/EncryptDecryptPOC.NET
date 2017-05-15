using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace EncryptDecryptHelper.Bussiness
{
    public class HashWrapper
    {
        public byte[] HashSHA256(byte[] data)
        {
            byte[] hashText = null;
            try
            {
                using (SHA256 hasher = SHA256Managed.Create())
                {
                    hashText = hasher.ComputeHash(data);
                }

            }
            catch (Exception)
            {
                throw;
            }
            return hashText;
        }

        public byte[] HashRFC2898(byte[] data, byte[] salt, int iteration, int hashByteLength)
        {
            byte[] hashText = null;
            try
            {
                using (Rfc2898DeriveBytes hasher = new Rfc2898DeriveBytes(data, salt, iteration))
                {
                    hashText = hasher.GetBytes(hashByteLength);
                }

            }
            catch (Exception)
            {
                throw;
            }
            return hashText;
        }
    }
}

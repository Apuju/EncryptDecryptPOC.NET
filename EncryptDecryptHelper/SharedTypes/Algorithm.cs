using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncryptDecryptHelper.SharedTypes
{
    public class Algorithm
    {
        public enum Cryptography
        {
            AES,
            RSA
        }

        public enum StringTransformationFormat
        {
            Base32,
            Base64,
            UTF8
        }

        public enum StringEncodeFormat
        {
            Base32,
            Base64
        }
    }
}

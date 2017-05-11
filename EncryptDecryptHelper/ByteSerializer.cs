using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncryptDecryptHelper
{
    public class ByteSerializer
    {
        public enum StringEncodingType
        {
            UTF8
        }

        public byte[] ToBytes(string data, StringEncodingType type)
        {
            byte[] bytes = null;
            switch (type)
            {
                case StringEncodingType.UTF8:
                    {
                        bytes =  ToUTF8Bytes(data);
                        break;
                    }
            }
            return bytes;
        }

        public byte[] ToUTF8Bytes(string data)
        {
            return Encoding.UTF8.GetBytes(data);
        }
    }
}

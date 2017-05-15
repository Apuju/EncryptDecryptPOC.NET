using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EncryptDecryptHelper.SharedTypes;
using EncryptDecryptHelper.Bussiness;

namespace EncryptDecryptHelper.Service
{
    public class BinaryEncodeHelper
    {
        private Algorithm.StringTransformationFormat m_EncodeForamt;

        public BinaryEncodeHelper(Algorithm.StringTransformationFormat encodeFormat)
        {
            m_EncodeForamt = encodeFormat;
        }

        public void SetEncodeFormat(Algorithm.StringTransformationFormat encodeFormat)
        {
            m_EncodeForamt = encodeFormat;
        }

        public string Encode(byte[] binary)
        {
            string data = string.Empty;
            BinaryEncoder encoder = new BinaryEncoder();
            try
            {
                switch (m_EncodeForamt)
                {
                    case Algorithm.StringTransformationFormat.Base64:
                        {
                            data = encoder.EncodeBinaryToBase64(binary);
                            break;
                        }
                    case Algorithm.StringTransformationFormat.UTF8:
                        {
                            data = encoder.EncodeBinaryToUTF8(binary);
                            break;
                        }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return data;
        }

        public byte[] Decode(string data)
        {
            byte[] binary = null;
            BinaryEncoder encoder = new BinaryEncoder();
            try
            {
                switch (m_EncodeForamt)
                {
                    case Algorithm.StringTransformationFormat.Base64:
                        {
                            binary = encoder.DecodeDataFromBase64(data);
                            break;
                        }
                    case Algorithm.StringTransformationFormat.UTF8:
                        {
                            binary = encoder.DecodeDataFromUTF8(data);
                            break;
                        }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return binary;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncryptDecryptHelper.Bussiness
{
    public class BinaryEncoder
    {
        #region Base32
        public string EncodeBinaryToBase32(byte[] binary)
        {
            int inByteSize = 8;
            int outByteSize = 5;
            char[] alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ234567".ToCharArray();

            int i = 0, index = 0, digit = 0;
            int current_byte, next_byte;
            StringBuilder data = new StringBuilder((binary.Length + 7) * inByteSize / outByteSize);

            while (i < binary.Length)
            {
                current_byte = (binary[i] >= 0) ? binary[i] : (binary[i] + 256); // Unsign

                /* Is the current digit going to span a byte boundary? */
                if (index > (inByteSize - outByteSize))
                {
                    if ((i + 1) < binary.Length)
                        next_byte = (binary[i + 1] >= 0) ? binary[i + 1] : (binary[i + 1] + 256);
                    else
                        next_byte = 0;

                    digit = current_byte & (0xFF >> index);
                    index = (index + outByteSize) % inByteSize;
                    digit <<= index;
                    digit |= next_byte >> (inByteSize - index);
                    i++;
                }
                else
                {
                    digit = (current_byte >> (inByteSize - (index + outByteSize))) & 0x1F;
                    index = (index + outByteSize) % inByteSize;
                    if (index == 0)
                        i++;
                }
                data.Append(alphabet[digit]);
            }

            return data.ToString();
        }
        #endregion

        #region Base 64
        public string EncodeBinaryToBase64(byte[] binary)
        {
            string data;
            try
            {
                data = Convert.ToBase64String(binary);
            }
            catch (Exception)
            {
                throw;
            }
            return data;
        }

        public byte[] DecodeDataFromBase64(string data)
        {
            byte[] binary = null;
            try
            {
                binary = Convert.FromBase64String(data);
            }
            catch (Exception)
            {
                throw;
            }
            return binary;
        }
        #endregion

        #region UTF-8
        public string EncodeBinaryToUTF8(byte[] binary)
        {
            string data;
            try
            {
                data = Encoding.UTF8.GetString(binary);
            }
            catch (Exception)
            {
                throw;
            }
            return data;
        }

        public byte[] DecodeDataFromUTF8(string data)
        {
            byte[] binary = null;
            try
            {
                binary = Encoding.UTF8.GetBytes(data);
            }
            catch (Exception)
            {
                throw;
            }
            return binary;
        }
        #endregion
    }
}

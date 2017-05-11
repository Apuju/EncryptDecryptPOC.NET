using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EncryptDecryptHelper;

namespace ClientA
{
    class Program
    {
        static void Main(string[] args)
        {
            bool pass = false;
            string ans = "ABC123";
            do
            {
                Console.WriteLine(string.Format("Please type \"{0}\"", ans));
                string input = Console.ReadLine();
                EncryptHelper helper = new EncryptHelper();
                string encryptString = helper.EnCryptData(input, EncryptHelper.CryptographyAlgorithm.AES, EncryptHelper.HashByteAlgorithm.Base64);
                Console.WriteLine(encryptString);
                if (input == ans)
                {
                    pass = true;
                }
            }
            while (!pass);
        }
    }
}

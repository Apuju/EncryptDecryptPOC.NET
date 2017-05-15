using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EncryptDecryptHelper.SharedTypes;
using EncryptDecryptHelper.Service;

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

                EncryptHelper encryptor = new EncryptHelper();
                string encryptString = encryptor.EncryptData(input, Algorithm.StringTransformationFormat.UTF8, Algorithm.Cryptography.AES, Algorithm.StringEncodeFormat.Base64);
                Console.WriteLine(string.Format("Encrypted String: {0}", encryptString));
                Console.WriteLine(string.Format("Encrypted Password & Salt is {0} & {1}", encryptor.Password, encryptor.Salt));

                DecryptHelper decryptor = new DecryptHelper(encryptor.Password, encryptor.Salt);
                string originalInput = decryptor.DecryptData(encryptString, Algorithm.StringEncodeFormat.Base64, Algorithm.Cryptography.AES, Algorithm.StringTransformationFormat.UTF8);
                Console.WriteLine(string.Format("Decrypted String: {0}", originalInput));
                
                if (input == ans)
                {
                    pass = true;
                }
            }
            while (!pass);
        }
    }
}

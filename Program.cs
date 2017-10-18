using System;
using System.Security.Cryptography;
using System.Text;

namespace RandomKeyGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                int keySize = 40;
                if(args.Length > 0)
                    Int32.TryParse(args[0], out keySize);

                int keyNumber = 1;
                if(args.Length > 1)
                    Int32.TryParse(args[1], out keyNumber);

                for(int i=0; i<keyNumber; i++)
                {
                    string key = GetUniqueKey(keySize);
                    Console.WriteLine(key);
                }                
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static string GetUniqueKey(int maxSize)
        {
            char[] chars = new char[62];
            chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();
            byte[] data = new byte[1];
            using (RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider())
            {
                crypto.GetNonZeroBytes(data);
                data = new byte[maxSize];
                crypto.GetNonZeroBytes(data);
            }
            StringBuilder result = new StringBuilder(maxSize);
            foreach (byte b in data)
            {
                result.Append(chars[b % (chars.Length)]);
            }
            return result.ToString();
        }
    }
}
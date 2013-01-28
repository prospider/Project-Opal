using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace Project_Opal
{
    class Secure
    {
        private static SHA512CryptoServiceProvider cryptoProvider = new SHA512CryptoServiceProvider();

        public static string Hash(string unsecure)
        {
            // MD5 nor SHA1 is secure; SHA512 is reasonably secure apparently
            //works for me. 
            

            byte[] data = Encoding.ASCII.GetBytes(unsecure);
            data = cryptoProvider.ComputeHash(data);

            string secure = BitConverter.ToString(data);

            return secure;
        }
    }
}

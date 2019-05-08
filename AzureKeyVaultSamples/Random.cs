using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace ASPNET4YOU.AzureKeyVault
{
    public class Random
    {
        public static byte[] GenerateRandomNumber(int length)
        {
            using (var randomNumberGenerator = new RNGCryptoServiceProvider())
            {
                var randomNumber = new byte[length];
                randomNumberGenerator.GetBytes(randomNumber);

                return randomNumber;
            }
        }
    }
}

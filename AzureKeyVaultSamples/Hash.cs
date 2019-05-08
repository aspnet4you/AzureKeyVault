using System;
using System.Security.Cryptography;

namespace ASPNET4YOU.AzureKeyVault
{
    public class Hash
    {
        public static byte[] Sha256(byte[] toBeHashed)
        {
            using (var sha256 = SHA256.Create())
            {
                return sha256.ComputeHash(toBeHashed);
            }
        }
    }
}
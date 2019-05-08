using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ASPNET4YOU.AzureKeyVault
{
    public class SampleKeyWrapping
    {
        private readonly SampleKeyVault vault;
        public SampleKeyWrapping(SampleKeyVault sampleKeyVault)
        {
            vault = sampleKeyVault;
        }

        public async Task TestKeyWrapping()
        {
            byte[] localKey = Random.GenerateRandomNumber(32);

            // Encrypt our local key with Key Vault and Store it in the database
            byte[] encryptedKey = await vault.EncryptAsync(vault.CustomerMasterKeyId, localKey);


            // Get our encrypted key from the database and decrypt it with the Key Vault.
            byte[] decryptedKey = await vault.DecryptAsync(vault.CustomerMasterKeyId, encryptedKey);

            // Now we have recovered the key with the Key Vault we can encrypt with AES locally.
            byte[] iv = Random.GenerateRandomNumber(16);
            byte[] encryptedData = AesEncryption.Encrypt(Encoding.UTF8.GetBytes("MEGA TOP SECRET STUFF"), decryptedKey, iv);
            byte[] decryptedMessage = AesEncryption.Decrypt(encryptedData, decryptedKey, iv);

            var encryptedText = Convert.ToBase64String(encryptedData);
            var decryptedData = Encoding.UTF8.GetString(decryptedMessage);
        }
    }
}

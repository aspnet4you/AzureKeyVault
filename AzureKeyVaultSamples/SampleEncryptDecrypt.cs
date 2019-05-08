using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ASPNET4YOU.AzureKeyVault
{
    public class SampleEncryptDecrypt
    {
        private readonly SampleKeyVault vault;
        public SampleEncryptDecrypt(SampleKeyVault sampleKeyVault)
        {
            vault = sampleKeyVault;
        }

        public async Task TestEncryptDecrypt()
        {
            // Test encryption and decryption.
            string dataToEncrypt = "Hello World!!";

            byte[] encrypted = await vault.EncryptAsync(vault.CustomerMasterKeyId, Encoding.UTF8.GetBytes(dataToEncrypt));
            byte[] decrypted = await vault.DecryptAsync(vault.CustomerMasterKeyId, encrypted);

            var encryptedText = Convert.ToBase64String(encrypted);
            var decryptedData = Encoding.UTF8.GetString(decrypted);
        }
    }
}

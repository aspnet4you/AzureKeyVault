using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ASPNET4YOU.AzureKeyVault
{
    public class SampleEncryptedSecret
    {
        private readonly SampleKeyVault vault;
        public SampleEncryptedSecret(SampleKeyVault sampleKeyVault)
        {
            vault = sampleKeyVault;
        }

        public async Task TestSampleEncryptedSecret()
        {
            string secName = "ASPNET4YOUEncSecret";
            string clearSecret = "Top Secret!!";
            byte[] encrypted = await vault.EncryptAsync(vault.CustomerMasterKeyId, Encoding.UTF8.GetBytes(clearSecret));
            var encryptedText = Convert.ToBase64String(encrypted);
            var secretIdentifier = await vault.SetSecretAsync(secName, encryptedText);

            // Retrive the encrypted secret from vault and decrypt it to cleartext
            var encryptedSecret = await vault.GetSecretAsync(secName);
            encrypted = Convert.FromBase64String(encryptedSecret);
            byte[] decrypted = await vault.DecryptAsync(vault.CustomerMasterKeyId, encrypted);
            var decryptedData = Encoding.UTF8.GetString(decrypted);
        }
    }
}

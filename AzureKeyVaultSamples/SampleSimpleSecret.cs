using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ASPNET4YOU.AzureKeyVault
{
    public class SampleSimpleSecret
    {
        private readonly SampleKeyVault vault;
        public SampleSimpleSecret(SampleKeyVault sampleKeyVault)
        {
            vault = sampleKeyVault;
        }

        public async Task TestSampleSimpleSecret()
        {
            const string secName = "ASPNET4YOUSecret";

            var secretId = await vault.SetSecretAsync(secName, "Mary had a little lamb.");

            string secret = await vault.GetSecretAsync(secName);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ASPNET4YOU.AzureKeyVault
{
    public class SampleDigitalSignature
    {
        private readonly SampleKeyVault vault;
        public SampleDigitalSignature(SampleKeyVault sampleKeyVault)
        {
            vault = sampleKeyVault;
        }

        public async Task TestDigitalSignature()
        {
            string importantDocument = "This is a really important document that I need to digitally sign.";
            byte[] documentDigest = Hash.Sha256(Encoding.UTF8.GetBytes(importantDocument));

            // Positive Signature Verification Test
            byte[] signature = await vault.Sign(vault.CustomerMasterKeyId, documentDigest);
            bool verified = await vault.Verify(vault.CustomerMasterKeyId, documentDigest, signature);

            // Negative Signature Verification Test
            importantDocument = "@This is a really important document that I need to digitally sign.";
            documentDigest = Hash.Sha256(Encoding.UTF8.GetBytes(importantDocument));
            verified = await vault.Verify(vault.CustomerMasterKeyId, documentDigest, signature);
        }
    }
}

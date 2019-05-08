using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ASPNET4YOU.AzureKeyVault
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var builder = new ConfigurationBuilder()
                                .SetBasePath(Directory.GetCurrentDirectory())
                                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

                IConfigurationRoot configuration = builder.Build();

                string clientId = configuration.GetSection("clientId").Value;
                string customerMasterKeyId = configuration.GetSection("customerMasterKeyId").Value;
                string vaultAddress = configuration.GetSection("vaultAddress").Value;
                string myCertFile = configuration.GetSection("myCertFile").Value;

                // In real-world application, you must set a password to the cert! This cert was created
                // in Azure Key Vault, exported with the private key and deleted from there. Export does not
                // require password!! Okay to use this cert for authentication to Key Vault for this tests!
                X509Certificate2 myCert = new X509Certificate2(myCertFile, "", X509KeyStorageFlags.PersistKeySet);

                SampleKeyVault vault = new SampleKeyVault(customerMasterKeyId, clientId, vaultAddress, myCert);

                SampleDigitalSignature digitalSignature = new SampleDigitalSignature(vault);
                digitalSignature.TestDigitalSignature().GetAwaiter().GetResult();

                SampleEncryptDecrypt sampleEncryptDecrypt = new SampleEncryptDecrypt(vault);
                sampleEncryptDecrypt.TestEncryptDecrypt().GetAwaiter().GetResult();

                SampleKeyWrapping keyWrapping = new SampleKeyWrapping(vault);
                keyWrapping.TestKeyWrapping().GetAwaiter().GetResult();

                SampleSimpleSecret sampleSimpleSecret = new SampleSimpleSecret(vault);
                sampleSimpleSecret.TestSampleSimpleSecret().GetAwaiter().GetResult();

                SampleEncryptedSecret sampleEncryptedSecret = new SampleEncryptedSecret(vault);
                sampleEncryptedSecret.TestSampleEncryptedSecret().GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}

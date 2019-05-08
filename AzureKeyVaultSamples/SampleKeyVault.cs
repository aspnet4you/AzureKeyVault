using Microsoft.Azure.KeyVault;
using Microsoft.Azure.KeyVault.Models;
using Microsoft.Azure.KeyVault.WebKey;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ASPNET4YOU.AzureKeyVault
{
    public class SampleKeyVault
    {
        private readonly string xCustomerMasterKeyId;
        private readonly string xClientId;
        private readonly X509Certificate2 x509Cert;
        private readonly string xVaultAddress;
        private KeyVaultClient KeyVaultClient;

        public SampleKeyVault(string customerMasterKeyId,string clientId, string vaultAddress, X509Certificate2 x509Certificate)
        {
            xCustomerMasterKeyId = customerMasterKeyId;
            xClientId = clientId;
            xVaultAddress = vaultAddress;
            x509Cert = x509Certificate;
            KeyVaultClient = new KeyVaultClient(GetAccessTokenAsync, GetHttpClient());
        }

        public string CustomerMasterKeyId { get { return xCustomerMasterKeyId; } }

        public async Task<byte[]> Sign(string keyId, byte[] hash)
        {
            var bundle = await KeyVaultClient.SignAsync(keyId, "RS256", hash);
            return bundle.Result;
        }

        public async Task<bool> Verify(string keyId, byte[] hash, byte[] signature)
        {
            var result = await KeyVaultClient.VerifyAsync(keyId, "RS256", hash, signature);
            return result;
        }

        public async Task<byte[]> EncryptAsync(string keyId, byte[] dataToEncrypt)
        {
            var operationResult = await KeyVaultClient.EncryptAsync(keyId, JsonWebKeyEncryptionAlgorithm.RSAOAEP, dataToEncrypt);

            return operationResult.Result;
        }

        public async Task<byte[]> DecryptAsync(string keyId, byte[] dataToDecrypt)
        {
            var operationResult = await KeyVaultClient.DecryptAsync(keyId, JsonWebKeyEncryptionAlgorithm.RSAOAEP, dataToDecrypt);

            return operationResult.Result;
        }

        public async Task<string> SetSecretAsync(string secretName, string secretValue)
        {
            var bundle = await KeyVaultClient.SetSecretAsync(xVaultAddress, secretName, secretValue, null, "secrettext");
            return bundle.Id;
        }

        public async Task<KeyBundle> GetKeyAsync(string keyId)
        {
            var bundle = await KeyVaultClient.GetKeyAsync(keyId);
            return bundle;
        }

        public async Task<string> GetSecretAsync(string secretName)
        {
            try
            {
                var bundle = await KeyVaultClient.GetSecretAsync(xVaultAddress, secretName);
                return bundle.Value;
            }
            catch (KeyVaultErrorException)
            {
                return string.Empty;
            }
        }

        protected async Task<string> GetAccessTokenAsync(string authority, string resource, string scope)
        {
            var context = new AuthenticationContext(authority, TokenCache.DefaultShared);
            var clientAssertionCertificate = new ClientAssertionCertificate(xClientId, x509Cert);
            var result = await context.AcquireTokenAsync(resource, clientAssertionCertificate);
            Console.WriteLine(scope);
            return result.AccessToken;
        }
        
        protected HttpClient GetHttpClient()
        {
            return new HttpClient();
        }
    }
}

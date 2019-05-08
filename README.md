# AzureKeyVault
Sample code for the Azure Key Vault to demonestrate digital signature, encrypt/decrypt, key wrapping, secret management, encrypted secret management and more. Thanks to @stephenhaunts for sharring the code. This repo is cloned and modified for simplicity. Client authentication is changed from client secret to client certificate. Original code can be found at https://github.com/stephenhaunts/AzureKeyVault.

Update appsettings.json file with clientId, customerMasterKeyId (key id of a key in keyvault), vaultAddress and myCertFile (pfx, self-sign is okay).

using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Cribbage.ConsoleApp;

internal class Program
{
    private static string DrawMenu()
    {
        Console.WriteLine("Which operation do you wish to perform?");
        Console.WriteLine("Connect to a channel (c)");
        Console.WriteLine("Get the secret (g)");
        Console.WriteLine("Send a message to the channel (s)");
        Console.WriteLine("Exit (x)");

        string operation = Console.ReadLine();
        return operation;
    }

    private async static void GetSecret()
    {

        const string secretName = "WebAPIKey";
        var keyVaultName = "kv-300089145";
        var kvUri = $"https://{keyVaultName}.vault.azure.net";

        var client = new SecretClient(new Uri(kvUri), new DefaultAzureCredential());

        var secretValue = "webapikey";

        Console.Write($"Creating a secret in {keyVaultName} called '{secretName}' with the value '{secretValue}' ...");
        await client.SetSecretAsync(secretName, secretValue);
        Console.WriteLine(" done.");

        Console.WriteLine("Forgetting your secret.");
        secretValue = string.Empty;
        Console.WriteLine($"Your secret is '{secretValue}'.");

        Console.WriteLine($"Retrieving your secret from {keyVaultName}.");
        var secret = await client.GetSecretAsync(secretName);
        Console.WriteLine($"Your secret is '{secret.Value.Value}'.");

        Console.Write($"Deleting your secret from {keyVaultName} ...");
        //DeleteSecretOperation operation = await client.StartDeleteSecretAsync(secretName);
        // You only need to wait for completion if you want to purge or recover the secret.
        //await operation.WaitForCompletionAsync();
        //Console.WriteLine(" done.");

        //Console.Write($"Purging your secret from {keyVaultName} ...");
        //await client.PurgeDeletedSecretAsync(secretName);
        //Console.WriteLine(" done.");
    }

    private static void Main(string[] args)
    {
        string user = "CribbageUser";
        //string hubAddress = "https://fvtcdp.azurewebsites.net/GameHub";
        string hubAddress = "https://bigprojectapi-300089145.azurewebsites.net/CribbageHub";
        //string hubAddress = "https://localhost:7186/CribbageHub";

        string operation = DrawMenu();

        var signalRConnection = new SignalRConnection(hubAddress);

        while (operation != "x")
        {
            switch (operation)
            {
                case "c":
                    signalRConnection.ConnectToChannel(user);
                    break;
                case "s":
                    break;
                case "x":
                    break;
            }

            operation = DrawMenu();
        }
    }
}
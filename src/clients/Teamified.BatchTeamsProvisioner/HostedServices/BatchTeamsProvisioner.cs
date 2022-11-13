using Azure.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Kiota.Authentication.Azure;
using Microsoft.Kiota.Http.HttpClientLibrary;
using Teamified.BatchTeamsProvisioner.Models;
using Teamified.Sdk;

namespace Teamified.BatchTeamsProvisioner.HostedServices;

internal sealed class BatchTeamsProvisioner : IHostedService
{
    private readonly IConfiguration _configuration;

    public BatchTeamsProvisioner(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        var teamifiedServiceClient = GetTeamifiedApiClient();

        var teams = await teamifiedServiceClient.Teams.GetAsync(
            cancellationToken: cancellationToken);

        var bulkTeamsToProvision = GenerateBulkData();

        foreach (var teamToProvision in bulkTeamsToProvision)
        {
            var alreadyExists = 
                teams.FirstOrDefault(t => t.DisplayName.Equals(
                    teamToProvision.DisplayName, 
                    StringComparison.OrdinalIgnoreCase)) != default;

            Console.Write($"Provisioning Team: {teamToProvision.DisplayName}...");
            if (!alreadyExists)
            {
                await teamifiedServiceClient.Teams.PostAsync(
                    new Sdk.Models.ProvisionTeamCommand
                    {
                        DisplayName = teamToProvision.DisplayName,
                        Description = teamToProvision.Description,
                    }, 
                    cancellationToken: cancellationToken);

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Done!");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("Already existed.");
                Console.ResetColor();
            }
        }
    }

    private TeamifiedApiClient GetTeamifiedApiClient()
    {
        var clientId = _configuration.GetValue<string>("ClientId");
        var tenantId = _configuration.GetValue<string>("TenantId");
        var apiClientId = _configuration.GetValue<string>("ApiClientId");

        string[] allowedHosts = { "localhost" };
        string[] scopes = { $"api://{apiClientId}/Teams.Manage" };

        var options = new InteractiveBrowserCredentialOptions
        {
            ClientId = clientId,
            TenantId = tenantId,
            RedirectUri = new Uri("http://localhost")
        };

        var credentials = new InteractiveBrowserCredential(options);

        var authProvider = new AzureIdentityAuthenticationProvider(credentials, allowedHosts, null, scopes);
        var requestAdapter = new HttpClientRequestAdapter(authProvider);

        var teamifiedServiceClient = new TeamifiedApiClient(requestAdapter);
        return teamifiedServiceClient;
    }

    private static IEnumerable<TeamProvisionItem> GenerateBulkData()
    {
        var bulkData = new List<TeamProvisionItem> 
        { 
            new TeamProvisionItem("TeamifiedBulk 1", "Testing teamified SDK"),
            new TeamProvisionItem("Gotham", "Testing teamified SDK"),
            new TeamProvisionItem("TeamifiedBulk 3", "Testing teamified SDK")
        };

        return bulkData;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}

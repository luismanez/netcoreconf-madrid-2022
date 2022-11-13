using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Teamified.BatchTeamsProvisioner.HostedServices;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureHostConfiguration(configHost =>
    {
        configHost.SetBasePath(Directory.GetCurrentDirectory());
        configHost.AddJsonFile("hostsettings.json", optional: true);
        configHost.AddCommandLine(args);
    })
    .ConfigureServices((hostContext, services) =>
    {
        services.AddHostedService<BatchTeamsProvisioner>();
    })
    .Build();

host.Run();
using Cephalopod.Contracts.Accounts;
using Microsoft.Extensions.DependencyInjection;
using Octopus.Client.Accounts;
using System.Text.Json;

namespace Octopus.Client;

public static class ServiceCollectionExtensions
{
    public static void AddOctopusServices(
        this IServiceCollection services)
    {
     services.AddSingleton<IAccountManager, OctopusAccountManager>();

        services.AddHttpClient(OctopusConstants.DefaultClient, client =>
        {
            client.BaseAddress = new("http://localhost:8080");
            client.DefaultRequestHeaders.TryAddWithoutValidation(
                "Content-type", "application/json; charset=utf-8");
        });
    }
}
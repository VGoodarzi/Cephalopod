using Cephalopod.Contracts.Accounts;
using Cephalopod.Contracts.Brands;
using Cephalopod.Contracts.Images;
using Microsoft.Extensions.DependencyInjection;
using Octopus.Client.Accounts;
using Octopus.Client.Brands;
using Octopus.Client.Images;

namespace Octopus.Client;

public static class ServiceCollectionExtensions
{
    public static void AddOctopusServices(
        this IServiceCollection services)
    {
        services.AddSingleton<IAccountManager, OctopusAccountManager>();
        services.Decorate<IAccountManager, AccountManagerDecorator>();

        services.AddSingleton<IBrandService, OctopusBrandService>();

        services.AddSingleton<IImageService, OctopusImageService>();

        services.AddHttpClient(OctopusConstants.DefaultClient, client =>
        {
            client.BaseAddress = new("http://localhost:8080");
            client.DefaultRequestHeaders.TryAddWithoutValidation(
                "Content-type", "application/json; charset=utf-8");
        }).AddHttpMessageHandler<AuthenticationDelegatingHandler>();

        services.AddSingleton(sp =>
        {
            var factory = sp.GetRequiredService<IHttpClientFactory>();
            return factory.CreateClient(OctopusConstants.DefaultClient);
        });

        services.AddSingleton<AuthenticationDelegatingHandler>();
    }
}
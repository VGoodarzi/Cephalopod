using Cephalopod.Client.Accounts;
using Cephalopod.Client.Authentication;
using Cephalopod.Client.Storage;
using Cephalopod.Contracts.Accounts;
using Cephalopod.Contracts.Utilities;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor;
using MudBlazor.Services;
using Octopus.Client;
using Serilog;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

Log.Logger = new LoggerConfiguration()
    .WriteTo.BrowserConsole()
    .CreateLogger();

builder.Services.AddMudServices(config =>
{
    config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomCenter;
    config.SnackbarConfiguration.PreventDuplicates = false;
    config.SnackbarConfiguration.NewestOnTop = false;
    config.SnackbarConfiguration.ShowCloseIcon = true;
    config.SnackbarConfiguration.VisibleStateDuration = 10000;
    config.SnackbarConfiguration.HideTransitionDuration = 500;
    config.SnackbarConfiguration.ShowTransitionDuration = 500;
    config.SnackbarConfiguration.SnackbarVariant = Variant.Filled;
});

builder.Services.AddSingleton<AccessTokenAuthenticationStateProvider>();
builder.Services.AddSingleton<AuthenticationStateProvider>(sp 
    => sp.GetRequiredService<AccessTokenAuthenticationStateProvider>());
builder.Services.AddSingleton<IAuthenticationNotify>(sp 
    => sp.GetRequiredService<AccessTokenAuthenticationStateProvider>());
builder.Services.AddSingleton<ICacheService, LocalStorageCacheService>();
builder.Services.AddSingleton<ITranslator, FakeTranslator>();
builder.Services.AddSingleton<IAuthenticationService, AuthenticationService>();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddAuthorizationCore();
builder.Services.AddOctopusServices();


await builder.Build().RunAsync();


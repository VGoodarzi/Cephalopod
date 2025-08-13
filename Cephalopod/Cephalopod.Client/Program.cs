using Cephalopod.Client;
using Cephalopod.Client.Accounts;
using Cephalopod.Client.Contracts;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

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

builder.Services.AddSingleton<AuthenticationStateProvider, AccessTokenAuthenticationStateProvider>();
builder.Services.AddSingleton<ICacheService, LocalStorageCacheService>();
builder.Services.AddSingleton<IUserService, FakeUserService>();
builder.Services.AddSingleton<ITranslator, FakeTranslator>();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddAuthorizationCore();


await builder.Build().RunAsync();


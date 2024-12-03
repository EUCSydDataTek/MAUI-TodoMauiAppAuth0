using Auth0.OidcClient;
using Microsoft.Extensions.Logging;
using Syncfusion.Maui.Toolkit.Hosting;
using TodoMauiAppAuth0.Services.AuthServices;
using TodoMauiAppAuth0.Services.Data;
using TodoMauiAppAuth0.ViewModels;
using TodoMauiAppAuth0.Views;

namespace TodoMauiAppAuth0
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureSyncfusionToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
            builder.Logging.AddDebug();

#endif

            builder.Services.AddSingleton<AuthService>();

            builder.Services.AddTransient<LoginPage>();
            builder.Services.AddTransient<LoginViewModel>();

            builder.Services.AddSingleton<ItemsPage>();
            builder.Services.AddSingleton<ItemsViewModel>();
            builder.Services.AddTransient<ItemDetailViewModel>();
            builder.Services.AddTransient<DetailsPage>();
            builder.Services.AddTransient<NewEditItemViewModel>();
            builder.Services.AddTransient<NewEditItemPage>();

            builder.Services.AddSingleton<ITodoItemService, TodoItemService>();
            
            builder.Services.AddSingleton(Connectivity.Current);

            builder.Services.AddTransient<TokenHandler>();

            IHttpClientBuilder httpClientBuilder = builder.Services.AddHttpClient<GenericRepository>(client => client.BaseAddress = new Uri(Config.BaseAddress))
                .AddHttpMessageHandler<TokenHandler>();

            // https://learn.microsoft.com/en-us/dotnet/core/resilience/http-resilience?tabs=dotnet-cli#standard-resilience-handler-defaults
            httpClientBuilder.AddStandardResilienceHandler();


            // Register Auth0Client using Auth0Config
            builder.Services.AddSingleton(new Auth0Client(new()
            {
                Domain = Config.Domain,
                ClientId = Config.ClientId,
                RedirectUri = Config.RedirectUri,
                PostLogoutRedirectUri = Config.PostLogoutRedirectUri,
                Scope = Config.Scope
            }));

            return builder.Build();
        }
    }
}

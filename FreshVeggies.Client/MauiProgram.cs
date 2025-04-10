using FreshVeggies.Client.Apis;
using Microsoft.Extensions.Logging;
using Refit;

namespace FreshVeggies.Client;
public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
			});

		builder.Services.AddMauiBlazorWebView();

#if DEBUG
		builder.Services.AddBlazorWebViewDeveloperTools();
		builder.Logging.AddDebug();
#endif
		ConfigureRefit(builder.Services);

        return builder.Build();
	}


	private static void ConfigureRefit(IServiceCollection services)
	{
		const string baseApiUrl = "https://htfkg406-7102.inc1.devtunnels.ms";

		services.AddRefitClient<IProductService>()
				.ConfigureHttpClient(SetHttpClient);

        services.AddRefitClient<IOrderService>(GetRefitSettings)
                .ConfigureHttpClient(SetHttpClient);

        services.AddRefitClient<IAuthService>()
                .ConfigureHttpClient(SetHttpClient);

        services.AddRefitClient<IUserService>(GetRefitSettings)
                .ConfigureHttpClient(SetHttpClient);

		// setup http client
        static void SetHttpClient(HttpClient httpClient)
		{
			httpClient.BaseAddress = new Uri(baseApiUrl);
		}

        // setup refit settings to get authorization token from headers
        static RefitSettings GetRefitSettings(IServiceProvider serviceProvider)
		{
			var settings = new RefitSettings();

			settings.AuthorizationHeaderValueGetter = (_, __) => Task.FromResult("TOKEN");

			return settings;
		}
	}
}

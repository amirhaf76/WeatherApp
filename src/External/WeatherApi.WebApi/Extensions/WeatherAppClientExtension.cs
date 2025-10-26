using Microsoft.Extensions.Options;
using WeatherApp.WebApi.Configurations;

namespace WeatherApp.WebApi.Extensions;

public static class WeatherAppClientExtension
{
    public static IServiceCollection AddWeatherHttpClient(this IServiceCollection sc)
    {
        sc.AddHttpClient(WeatherAppClient.OPEN_WEATHER_API, (sp, httpClient) =>
        {
            var logger = sp.GetService<ILogger>();

            var apiOptions = sp.GetService<IOptions<OpenWeatherApiOptions>>()?.Value;


            if (apiOptions is null)
            {
                logger?.LogError("{0} is null!", nameof(OpenWeatherApiOptions));
                throw new NotFoundOpenWeatherApiException();
            }
            else
            {
                logger?.LogDebug("{0} { Uri: {1} }", nameof(OpenWeatherApiOptions), apiOptions.Uri);
            }


            var uriBuilder = new UriBuilder(apiOptions.Uri)
            {
                Query = $"appid={apiOptions.ApiKey}"
            };

            httpClient.BaseAddress = uriBuilder.Uri;

            logger?.LogDebug("{0} url is {1}", WeatherAppClient.OPEN_WEATHER_API, httpClient.BaseAddress);
        });

        return sc;
    }
}
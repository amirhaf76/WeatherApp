using Microsoft.Extensions.Options;
using WeatherApp.WebApi.Configurations;

namespace WeatherApp.WebApi.Extensions;

public static class WeatherAppClientExtension
{
    public static IServiceCollection AddWeatherHttpClient(this IServiceCollection sc)
    {
        sc.AddHttpClient(WeatherAppClient.OPEN_WEATHER_API, (sp, httpClient) =>
        {
            var logger = sp.GetService<ILogger<HttpClient>>();

            var apiOptions = sp.GetService<IOptions<OpenWeatherApiOptions>>()?.Value;


            if (apiOptions is null)
            {
                logger?.LogError("{options} is null!", nameof(OpenWeatherApiOptions));
                throw new NotFoundOpenWeatherApiException();
            }
            else
            {
                logger?.LogDebug("{options} Uri: {url} ", nameof(OpenWeatherApiOptions), apiOptions.Url);
            }


            var uriBuilder = new UriBuilder(apiOptions.Url)
            {
                Query = $"appid={apiOptions.AppId}"
            };

            httpClient.BaseAddress = uriBuilder.Uri;

            logger?.LogDebug("{client} url is {url}", WeatherAppClient.OPEN_WEATHER_API, httpClient.BaseAddress);
        });

        return sc;
    }
}
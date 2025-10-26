using Microsoft.Extensions.Options;
using WeatherApp.WebApi.Extensions;

namespace WeatherApp.WebApi.Configurations
{
    public class OpenWeatherApiOptionsSetUp : IConfigureOptions<OpenWeatherApiOptions>
    {
        private readonly IConfiguration _configuration;

        public OpenWeatherApiOptionsSetUp(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Configure(OpenWeatherApiOptions options)
        {
            _configuration.GetSection(WeatherAppConfig.OPEN_WEATHER_API).Bind(options);
        }
    }
}

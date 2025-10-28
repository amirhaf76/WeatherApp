using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Moq;
using WeatherApp.Application.Requests;
using WeatherApp.Application.Responses;
using WeatherApp.WebApi.Extensions;
using WeatherApp.WebApi.Implementations;

namespace WeatherApp.Test
{
    public class WeatherAppTest
    {
        private readonly string _appId;

        public WeatherAppTest()
        {
            var config = new ConfigurationBuilder().AddUserSecrets<WeatherService>().Build();

            _appId = config[WeatherAppConfig.OPEN_WEATHER_API+":AppId"] ?? string.Empty;
        }

        [Fact]
        public async Task WeatherService_GeocodingOfTehran_ValidNumber()
        {
            var stubLogger = new StubLogger<WeatherService>();
            var stubHttpClientFactory = new Mock<IHttpClientFactory>();


            using var stubClient = new HttpClient();

            var uriBuilder = new UriBuilder("http://api.openweathermap.org/");

            uriBuilder.Query = $"appid={_appId}";

            stubClient.BaseAddress = uriBuilder.Uri;

            stubHttpClientFactory
                .Setup(x => x.CreateClient(WeatherAppClient.OPEN_WEATHER_API))
                .Returns(stubClient);

            var weatherService = new WeatherService(stubLogger, stubHttpClientFactory.Object);

            var res = await weatherService.GetGeocodingAsync(new GeocodingRequest
            {
                CityName = "Tehran",
                Limit = 1,
            });

            res.First().Should().BeEquivalentTo(new
            {
                Lat = Value.ThatMatches<decimal>(x => x != 0),
                Lon = Value.ThatMatches<decimal>(x => x != 0),
            });
        }


        [Fact]
        public async Task WeatherService_AirPollutionOfTehran_ValidNumber()
        {
            var stubLogger = new StubLogger<WeatherService>();
            var stubHttpClientFactory = new Mock<IHttpClientFactory>();


            using var stubClient = new HttpClient();

            var uriBuilder = new UriBuilder("http://api.openweathermap.org/");

            uriBuilder.Query = $"appid={_appId}";

            stubClient.BaseAddress = uriBuilder.Uri;

            stubHttpClientFactory
                .Setup(x => x.CreateClient(WeatherAppClient.OPEN_WEATHER_API))
                .Returns(stubClient);

            var weatherService = new WeatherService(stubLogger, stubHttpClientFactory.Object);

            var res = await weatherService.GetAirPollutionAsync(new AirPollutionRequest
            {
                Lat = 35.6892523M,
                Lon = 51.3896004M,
            });

            res.List.First().Should().BeEquivalentTo(new
            {
                Main = new
                {
                    AirQualityIndex = Value.ThatMatches<AirQualityIndex>(x => x != AirQualityIndex.Unknown)
                },
                Components = new
                {
                    Co = Value.ThatMatches<decimal>(x => x > 0),
                    No = Value.ThatMatches<decimal>(x => x > 0),
                    No2 = Value.ThatMatches<decimal>(x => x > 0),
                    O3 = Value.ThatMatches<decimal>(x => x > 0),
                    So2 = Value.ThatMatches<decimal>(x => x > 0),
                    Pm2_5 = Value.ThatMatches<decimal>(x => x > 0),
                    Pm10 = Value.ThatMatches<decimal>(x => x > 0),
                    Nh3 = Value.ThatMatches<decimal>(x => x > 0),
                }
            });
        }


        [Fact]
        public async Task WeatherService_CurrentWeatherOfTehran_ValidNumber()
        {
            var stubLogger = new StubLogger<WeatherService>();
            var stubHttpClientFactory = new Mock<IHttpClientFactory>();


            using var stubClient = new HttpClient();

            var uriBuilder = new UriBuilder("http://api.openweathermap.org/");

            uriBuilder.Query = $"appid={_appId}";

            stubClient.BaseAddress = uriBuilder.Uri;

            stubHttpClientFactory
                .Setup(x => x.CreateClient(WeatherAppClient.OPEN_WEATHER_API))
                .Returns(stubClient);

            var weatherService = new WeatherService(stubLogger, stubHttpClientFactory.Object);

            var res = await weatherService.GetCurrentWeatherAsync(new CurrentWeatherRequest
            {
                Lat = 35.6892523M,
                Lon = 51.3896004M,
            });

        }
    }

}
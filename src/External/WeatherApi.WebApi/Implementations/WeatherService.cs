using Microsoft.Extensions.Logging;
using System.Text.Json;
using WeatherApp.Application;
using WeatherApp.Application.Requests;
using WeatherApp.Application.Responses;
using WeatherApp.WebApi.Extensions;

namespace WeatherApp.WebApi.Implementations
{
    public class WeatherService : IWeatherService
    {
        private const string GEOCODING_END_POINT = "geo/1.0/direct";
        private const string CURRENT_WEATHER_END_PONT = "data/2.5/weather";
        private const string AIR_POLLUTION_END_PONT = "data/2.5/air_pollution";


        private readonly ILogger<WeatherService> _logger;
        private readonly IHttpClientFactory _httpClientFactory;


        public WeatherService(ILogger<WeatherService> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }



        public async Task<AirPollutionDto> GetAirPollutionAsync(AirPollutionRequest request)
        {
            HttpClient client = _httpClientFactory.CreateClient(WeatherAppClient.OPEN_WEATHER_API);

            var requestUri = CreateAirPollutionUri(request, client.BaseAddress);

            _logger.LogDebug(requestUri.Query);

            var result = await client.GetFromJsonAsync<AirPollutionDto>(requestUri);

            var responseMessage = await client.GetAsync(requestUri);

            return result;
        }

        public async Task<CurrentWeatherDto> GetCurrentWeatherAsync(CurrentWeatherRequest request)
        {
            HttpClient client = _httpClientFactory.CreateClient(WeatherAppClient.OPEN_WEATHER_API);

            var requestUri = CreateCurrentWeatherUri(request, client.BaseAddress);

            _logger.LogDebug(requestUri.Query);

            var result = await client.GetFromJsonAsync<CurrentWeatherDto>(requestUri);

            return result;
        }

        public async Task<List<GeocodingDto>> GetGeocodingAsync(GeocodingRequest request)
        {
            HttpClient client = _httpClientFactory.CreateClient(WeatherAppClient.OPEN_WEATHER_API);

            var requestUri = CreateGeocodingUri(request, client.BaseAddress);

            _logger.LogDebug(requestUri.Query);

            var result = await client.GetFromJsonAsync<List<GeocodingDto>>(requestUri);

            return result;
        }
        


        private static Uri CreateAirPollutionUri(AirPollutionRequest request, Uri? baseAddress)
        {
            var queryString = $"&lat={request.Lat}&lon={request.Lon}";

            Uri uri = CreateUri(baseAddress, AIR_POLLUTION_END_PONT, queryString);

            return uri;
        }

        private static Uri CreateCurrentWeatherUri(CurrentWeatherRequest request, Uri? baseAddress)
        {
            var queryString = $"&lat={request.Lat}&lon={request.Lon}";

            Uri uri = CreateUri(baseAddress, CURRENT_WEATHER_END_PONT, queryString);

            return uri;
        }

        private static Uri CreateGeocodingUri(GeocodingRequest request, Uri? baseAddress)
        {
            var queryString = $"&q={request.CityName}&limit={request.Limit}&units={request.Unit}";

            Uri uri = CreateUri(baseAddress, GEOCODING_END_POINT, queryString);

            return uri;
        }



        private static Uri CreateUri(Uri? baseAddress, string path, string queryString)
        {
            var uriBuilder = new UriBuilder(baseAddress ?? new Uri(string.Empty));

            uriBuilder.Query += queryString;
            uriBuilder.Path = path;

            return uriBuilder.Uri;
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using WeatherApp.Application;
using WeatherApp.Application.Requests;
using WeatherApp.Application.Responses;
using WeatherApp.WebApi.Controllers.Dtos;
using WeatherApp.WebApi.Extensions;

namespace WeatherApp.WebApi.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class WeatherController : ControllerBase
    {
        private readonly ILogger<WeatherController> _logger;
        private readonly IWeatherService _weatherService;
        private readonly IWeatherValidator _weatherValidator;

        public WeatherController(ILogger<WeatherController> logger, IWeatherService weatherService, IWeatherValidator weatherValidator)
        {
            _weatherService = weatherService;
            _logger = logger;
            _weatherValidator = weatherValidator;
        }

        [HttpGet]
        public async Task<ActionResult<WeatherVM>> GetWeatherAsync([FromQuery] string cityName)
        {
            var queryValidationResult = _weatherValidator.ValidateCityName(cityName);

            _logger.LogDebug("City name validation is {IsValid}", queryValidationResult.IsValid);

            if (queryValidationResult.IsNotValid)
            {
                return BadRequest(new
                {
                    queryValidationResult.Errors
                });
            }

            var theCityGeocodes = await _weatherService.GetGeocodingAsync(new GeocodingRequest
            {
                CityName = cityName,
                Limit = 1,
            });

            if (theCityGeocodes.Count == 0)
            {
                return BadRequest();
            }

            var theCityGeocode = theCityGeocodes.First();

            _logger.LogDebug("the geocode of {city} are lat: {lat}, lon:{lon}", cityName, theCityGeocode.Lat, theCityGeocode.Lon);

            CurrentWeatherDto currentWeather = await _weatherService.GetCurrentWeatherAsync(new CurrentWeatherRequest
            {
                Lat = theCityGeocode.Lat,
                Lon = theCityGeocode.Lon,
            });

            AirPollutionDto airPollution = await _weatherService.GetAirPollutionAsync(new AirPollutionRequest
            {
                Lat = theCityGeocode.Lat,
                Lon = theCityGeocode.Lon,
            });

            WeatherVM result = new()
            {
                Lat = theCityGeocode.Lat,
                Lon = theCityGeocode.Lon,
                Humidity = currentWeather.Main.Humidity,
                Speed = currentWeather.Wind.Speed,
                Temp = currentWeather.Main.Temp,
                Components = airPollution.List.FirstOrDefault()?.Components?.ToVM(),
                AQI = airPollution.List.FirstOrDefault()?.Main?.AirQualityIndex.ToVM() ?? AirQualityIndexVM.Unknown
            };

            return await Task.FromResult(Ok(result));
        }


    }
}

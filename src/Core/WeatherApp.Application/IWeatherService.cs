using WeatherApp.Application.Requests;
using WeatherApp.Application.Responses;

namespace WeatherApp.Application;

public interface IWeatherService
{
    Task<List<GeocodingDto>> GetGeocodingAsync(GeocodingRequest request);

    Task<AirPollutionDto> GetAirPollutionAsync(AirPollutionRequest request);

    Task<CurrentWeatherDto> GetCurrentWeatherAsync(CurrentWeatherRequest request);
}

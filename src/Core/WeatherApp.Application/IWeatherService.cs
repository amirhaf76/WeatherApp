namespace WeatherApp.Application;

public interface IWeatherService
{
    Task<GeocodingDto> GetGeocodingAsync();

    Task<AirPollutionDto> GetAirPollutionAsync();

    Task<CurrentWeatherDto> GetCurrentWeatherAsync();
}

namespace WeatherApp.Application
{
    public interface IWeatherValidator
    {
        ValidationResult ValidateCityName(string cityName);
    }
}
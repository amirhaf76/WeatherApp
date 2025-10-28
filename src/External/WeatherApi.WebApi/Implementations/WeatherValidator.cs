using WeatherApp.Application;

namespace WeatherApp.WebApi.Implementations
{
    public class WeatherValidator : IWeatherValidator
    {
        public ValidationResult ValidateCityName(string cityName)
        {
            if (string.IsNullOrWhiteSpace(cityName))
            {
                return new ValidationResult(false,
                [
                    new() { Message = "City name can not be null or white space" }
                ]);
            }

            return new ValidationResult(true, []);
        }
    }
}
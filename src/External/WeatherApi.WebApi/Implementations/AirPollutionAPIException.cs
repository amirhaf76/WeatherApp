
namespace WeatherApp.WebApi.Implementations
{
    [Serializable]
    internal class AirPollutionAPIException : Exception
    {
        public AirPollutionAPIException()
        {
        }

        public AirPollutionAPIException(string? message) : base(message)
        {
        }

        public AirPollutionAPIException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
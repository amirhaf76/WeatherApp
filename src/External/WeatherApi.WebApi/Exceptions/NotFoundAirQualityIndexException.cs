namespace WeatherApp.WebApi.Exceptions
{
    [Serializable]
    internal class NotFoundAirQualityIndexException : Exception
    {
        public NotFoundAirQualityIndexException()
        {
        }

        public NotFoundAirQualityIndexException(string? message) : base(message)
        {
        }

        public NotFoundAirQualityIndexException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
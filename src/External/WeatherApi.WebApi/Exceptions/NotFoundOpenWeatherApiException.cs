
[Serializable]
internal class NotFoundOpenWeatherApiException : Exception
{
    public NotFoundOpenWeatherApiException()
    {
    }

    public NotFoundOpenWeatherApiException(string? message) : base(message)
    {
    }

    public NotFoundOpenWeatherApiException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
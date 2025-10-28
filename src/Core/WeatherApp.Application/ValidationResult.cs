namespace WeatherApp.Application
{
    public class ValidationResult(bool isValid, IEnumerable<Error> errors)
    {
        public bool IsValid { get; init; } = isValid;

        public bool IsNotValid => !IsValid;

        public IEnumerable<Error> Errors { get; init; } = [.. errors];
    }
}
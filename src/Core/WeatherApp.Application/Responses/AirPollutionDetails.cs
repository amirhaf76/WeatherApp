namespace WeatherApp.Application.Responses;

public class AirPollutionDetails
{
    public MainAirPollutionDetails? Main { get; set; }

    public AirPollutionComponents? Components { get; set; }
}


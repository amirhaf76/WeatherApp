namespace WeatherApp.Application.Responses;

public class CurrentWeatherDto
{
    public MainCurrentWeatherInfo Main { get; set; } = new MainCurrentWeatherInfo();

    public WindDto Wind { get; set; } = new WindDto();
}

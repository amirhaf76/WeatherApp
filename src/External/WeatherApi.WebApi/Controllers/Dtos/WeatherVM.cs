namespace WeatherApp.WebApi.Controllers.Dtos;

public class WeatherVM
{
    public double Speed { get; set; }

    public double Temp { get; set; }

    public double Humidity { get; set; }

    public AirQualityIndexVM AQI { get; set; }

    public AirPollutionComponentsVM? Components { get; set; } = new AirPollutionComponentsVM();

    public decimal Lat { get; set; }

    public decimal Lon { get; set; }
}

namespace WeatherApp.Application.Responses;

public class GeocodingDto
{
    public string Name { get; set; } = string.Empty;
    public decimal Lat { get; set; }
    public decimal Lon { get; set; }
}

namespace WeatherApp.Application.Requests;

public class GeocodingRequest
{
    public string CityName { get; set; } = string.Empty;

    public int Limit { get; set; }

    public Units Unit { get; set; } = Units.Metric;
}

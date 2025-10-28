using System.Text.Json.Serialization;

namespace WeatherApp.Application.Responses;

public class MainAirPollutionDetails
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    [JsonPropertyName("aqi")]
    public AirQualityIndex AirQualityIndex { get; set; } = AirQualityIndex.Unknown;
}


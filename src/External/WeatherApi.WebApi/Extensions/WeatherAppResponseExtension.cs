using WeatherApp.Application.Responses;
using WeatherApp.WebApi.Controllers.Dtos;
using WeatherApp.WebApi.Exceptions;

namespace WeatherApp.WebApi.Extensions;

public static class WeatherAppResponseExtension
{
    public static AirPollutionComponentsVM ToVM(this AirPollutionComponents components)
    {
        return new AirPollutionComponentsVM
        {
            Co = components.Co,
            No = components.No,
            No2 = components.No2,
            O3 = components.O3,
            So2 = components.So2,
            Pm2_5 = components.Pm2_5,
            Pm10 = components.Pm10,
            Nh3 = components.Nh3,
        };
    }

    public static AirQualityIndexVM ToVM(this AirQualityIndex aqi)
    {
        return aqi switch
        {
            AirQualityIndex.Unknown => AirQualityIndexVM.Unknown,
            AirQualityIndex.Good => AirQualityIndexVM.Good,
            AirQualityIndex.Fair => AirQualityIndexVM.Fair,
            AirQualityIndex.Moderate => AirQualityIndexVM.Moderate,
            AirQualityIndex.Poor => AirQualityIndexVM.Poor,
            AirQualityIndex.VeryPoor => AirQualityIndexVM.VeryPoor,
            _ => throw new NotFoundAirQualityIndexException()
        };
    }
}

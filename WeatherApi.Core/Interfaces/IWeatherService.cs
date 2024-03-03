using WeatherApi.Core.Entities;

namespace WeatherApi.Core.Interfaces
{
    public interface IWeatherService
    {
        Task<WeatherApiResponceModel> GetWeatherAsync(string city);
    }
}

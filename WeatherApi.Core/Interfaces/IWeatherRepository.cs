using WeatherApi.Core.Entities;

namespace WeatherApi.Core.Interfaces
{
    public interface IWeatherRepository
    {
        Task<WeatherApiResponceModel> GetWeatherAsync(string city);
    }
}

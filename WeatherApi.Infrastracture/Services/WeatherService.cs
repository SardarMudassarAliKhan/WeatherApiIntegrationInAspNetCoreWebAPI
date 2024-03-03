using WeatherApi.Core.Entities;
using WeatherApi.Core.Interfaces;

namespace WeatherApi.Infrastracture.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly IWeatherRepository _weatherRepository;

        public WeatherService(IWeatherRepository weatherRepository)
        {
            _weatherRepository = weatherRepository;
        }

        public async Task<WeatherApiResponceModel> GetWeatherAsync(string city)
        {
            return await _weatherRepository.GetWeatherAsync(city);
        }
    }
}

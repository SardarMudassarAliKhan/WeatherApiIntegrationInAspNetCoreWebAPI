using Flurl;
using Flurl.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;
using WeatherApi.Core.Entities;
using WeatherApi.Core.Interfaces;

namespace WeatherApi.Infrastracture.Repositories
{
    public class WeatherRepository : IWeatherRepository
    {
        private readonly WeatherApiSettings _settings;

        public WeatherRepository(IOptions<WeatherApiSettings> settings, IConfiguration configuration)
        {
            if (settings == null)
            {
                throw new ArgumentNullException(nameof(settings));
            }

            _settings = settings.Value ?? throw new ArgumentNullException(nameof(settings));

            _settings.ApiKey = configuration["ApiSettings:WeatherApiKey"];
            _settings.BaseUrl = configuration["ApiSettings:WeatherApiBaseUrl"];

            // Handle null scenario for ApiKey
            if (string.IsNullOrEmpty(settings.Value.ApiKey))
            {
                throw new ArgumentNullException("WeatherApiKey is not configured in appsettings.json");
            }

            // Handle null scenario for BaseUrl
            if (string.IsNullOrEmpty(settings.Value.BaseUrl))
            {
                throw new ArgumentNullException("WeatherApiBaseUrl is not configured in appsettings.json");
            }
        }

        public async Task<WeatherApiResponceModel> GetWeatherAsync(string city)
        {
            try
            {
                var response = await _settings.BaseUrl
                    .SetQueryParam("q", city)
                    .SetQueryParam("appid", _settings.ApiKey)
                    .GetJsonAsync<WeatherApiResponceModel>();

                return response;
            }
            catch (FlurlHttpException ex)
            {
                // Log the exception or handle it according to your application's requirements
                throw new Exception("Error in the API", ex);
            }
        }
    }
}

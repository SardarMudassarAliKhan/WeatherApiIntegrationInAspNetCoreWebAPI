using Microsoft.AspNetCore.Mvc;
using WeatherApi.Core.Interfaces;

namespace WeatherApiIntegrationInAspNetCoreWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherAPIController : ControllerBase
    {
        private readonly IWeatherService _weatherService;

        public WeatherAPIController(IWeatherService weatherService)
        {
            _weatherService = weatherService;
        }

        [HttpGet]
        public async Task<IActionResult> GetWeather(string city)
        {
            if (string.IsNullOrEmpty(city))
            {
                return BadRequest("City is required");
            }

            var weather = await _weatherService.GetWeatherAsync(city);

            if (weather == null)
            {
                return NotFound();
            }

            return Ok(weather);
        }
    }
}

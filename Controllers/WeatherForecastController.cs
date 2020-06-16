using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WeatherWalkingSkeleton.Services;

namespace WeatherWalkingSkeleton.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IOpenWeatherService _weatherService;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IOpenWeatherService weatherService)
        {
            _logger = logger;
            _weatherService = weatherService;
        }

        [HttpGet]
        public IActionResult Get(string location, Unit unit = Unit.Metric)
        {
            var forecast = _weatherService.GetFiveDayForecast(location, unit);
            return Ok(forecast);
        }
    }
}

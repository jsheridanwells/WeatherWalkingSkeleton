﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WeatherWalkingSkeleton.Infrastructure;
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
        public async Task<IActionResult> Get(string location, Unit unit = Unit.Metric)
        {
            try
            {
                var forecast = await _weatherService.GetFiveDayForecastAsync(location, unit);
                return Ok(forecast);
            }
            catch (OpenWeatherException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }
    }
}

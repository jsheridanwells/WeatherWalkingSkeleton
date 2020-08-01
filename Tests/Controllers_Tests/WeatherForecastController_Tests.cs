using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using WeatherWalkingSkeleton.Controllers;
using WeatherWalkingSkeleton.Models;
using WeatherWalkingSkeleton.Services;
using WeatherWalkingSkeleton.Tests.Infrastructure;
using Xunit;

namespace WeatherWalkingSkeleton.Tests.Controllers_Tests
{
    public class WeatherForecastController_Tests
    {
        [Fact]
        public async Task Returns_OkResult_With_WeatherForecast()
        {
            var opts = OptionsBuilder.OpenWeatherConfig();
            var clientFactory = ClientBuilder.OpenWeatherClientFactory(OpenWeatherResponses.OkResponse);
            var service = new OpenWeatherService(opts, clientFactory);
            var sut = new WeatherForecastController(new NullLogger<WeatherForecastController>(), service);

            var result = await sut.Get("Chicago") as OkObjectResult;

            Assert.IsType<List<WeatherForecast>>(result.Value);
            Assert.Equal(200, result.StatusCode);
        }
    }
}
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

        [Fact]
        public async Task Returns_500_When_Api_Returns_Error()
        {
            var opts = OptionsBuilder.OpenWeatherConfig();
            var clientFactory = ClientBuilder.OpenWeatherClientFactory(OpenWeatherResponses.UnauthorizedResponse,
                HttpStatusCode.Unauthorized);
            var service = new OpenWeatherService(opts, clientFactory);
            var sut = new WeatherForecastController(new NullLogger<WeatherForecastController>(), service);
            
            var result = await sut.Get("Rio de Janeiro") as ObjectResult;
            
            Assert.Contains("Error response from OpenWeatherApi: Unauthorized", result.Value.ToString());
            Assert.Equal(500, result.StatusCode); 
        }

        [Fact]
        public async Task Returns_BadRequestResult_When_Location_Not_Found()
        {
            var opts = OptionsBuilder.OpenWeatherConfig();
            var clientFactory = ClientBuilder.OpenWeatherClientFactory(OpenWeatherResponses.NotFoundResponse,
                HttpStatusCode.NotFound);
            var service = new OpenWeatherService(opts, clientFactory);
            var sut = new WeatherForecastController(new NullLogger<WeatherForecastController>(), service);
            
            var result = await sut.Get("Westworld") as ObjectResult;
            
            Assert.Contains("not found", result.Value.ToString());
            Assert.Equal(400, result.StatusCode);
        }
    }
}
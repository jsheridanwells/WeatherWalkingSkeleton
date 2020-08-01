using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using WeatherWalkingSkeleton.Infrastructure;
using WeatherWalkingSkeleton.Models;
using WeatherWalkingSkeleton.Tests.Infrastructure;
using Xunit;

namespace WeatherWalkingSkeleton.Services
{
    public class OpenWeatherService_Tests
    {
        [Fact]
        public async Task Returns_A_WeatherForecast()
        {
            var opts = OptionsBuilder.OpenWeatherConfig();
            var clientFactory = ClientBuilder.OpenWeatherClientFactory(OpenWeatherResponses.OkResponse);
            var sut = new OpenWeatherService(opts, clientFactory);

            var result = await sut.GetFiveDayForecastAsync("Chicago");

            Assert.IsType<List<WeatherForecast>>(result);
        }

        [Fact]
        public async Task Returns_Expected_Values_From_the_Api()
        {
            var opts = OptionsBuilder.OpenWeatherConfig();
            var clientFactory = ClientBuilder.OpenWeatherClientFactory(OpenWeatherResponses.OkResponse);
            var sut = new OpenWeatherService(opts, clientFactory);

            var result = await sut.GetFiveDayForecastAsync("Chicago");
            
            Assert.Equal(new DateTime(1594155600), result[0].Date);
            Assert.Equal((decimal)32.93, result[0].Temp);
        }
    }
}
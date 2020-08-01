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

        [Fact]
        public async Task Returns_OpenWeatherException_When_Unauthorized()
        {
            var opts = OptionsBuilder.OpenWeatherConfig();
            var clientFactory = ClientBuilder.OpenWeatherClientFactory(OpenWeatherResponses.UnauthorizedResponse,
                HttpStatusCode.Unauthorized);
            var sut = new OpenWeatherService(opts, clientFactory);

            var result = await Assert.ThrowsAsync<OpenWeatherException>(() => sut.GetFiveDayForecastAsync("Chicago"));
            Assert.Equal(401, (int)result.StatusCode);
        }
        
//        [Fact]
//         public async Task Returns_OpenWeatherException_When_Called_With_Bad_Argument()
//         {
//             var opts = OptionsBuilder.OpenWeatherConfig();
//             var clientFactory = ClientBuilder.OpenWeatherClientFactory(OpenWeatherResponses.NotFoundResponse,
//                 HttpStatusCode.NotFound);
//             var sut = new OpenWeatherService(opts, clientFactory);
// 
//             var result = await Assert.ThrowsAsync<OpenWeatherException>(() => sut.GetFiveDayForecastAsync("Westeros"));
//             Assert.Equal(404, (int)result.StatusCode);
//         }
//
//         [Fact]
//         public async Task Returns_OpenWeatherException_On_OpenWeatherInternalError()
//         {
//             var opts = OptionsBuilder.OpenWeatherConfig();
//             var clientFactory = ClientBuilder.OpenWeatherClientFactory(OpenWeatherResponses.InternalErrorResponse,
//                 HttpStatusCode.InternalServerError);
//             var sut = new OpenWeatherService(opts, clientFactory);
// 
//             var result = await Assert.ThrowsAsync<OpenWeatherException>(() => sut.GetFiveDayForecastAsync("New York"));
//             Assert.Equal(500, (int)result.StatusCode);
//         }
    }
}
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using WeatherWalkingSkeleton.Models;

namespace WeatherWalkingSkeleton.Tests.Infrastructure
{
    public static class OpenWeatherResponses
    {
        public static StringContent OkResponse => BuildOkResponse();
        public static StringContent UnauthorizedResponse => BuildUnauthorizedResponse();
        public static StringContent NotFoundResponse => BuildNotFoundResponse();
        public static StringContent InternalErrorResponse => BuildInternalErrorResponse();

        private static StringContent BuildOkResponse()
        {
            var response = new OpenWeatherResponse
            {
                Forecasts = new List<Forecast>
                {
                    new Forecast{ Dt = 1594155600, Temps = new Temps { Temp = (decimal)32.93 } }
                }
            };
            var json = JsonSerializer.Serialize(response);
            return new StringContent(json);
        }

        private static StringContent BuildUnauthorizedResponse()
        {
            var json = JsonSerializer.Serialize(new { Cod = 401, Message = "Invalid Api key." });
            return new StringContent(json);
        }

        private static StringContent BuildNotFoundResponse()
        {
            var json = JsonSerializer.Serialize(new { Cod = 404, Message = "city not found" });
            return new StringContent(json);
        }

        private static StringContent BuildInternalErrorResponse()
        {
            var json = JsonSerializer.Serialize(new {Cod = 500, Message = "Internal Error."});
            return new StringContent(json);
        }
    }
}
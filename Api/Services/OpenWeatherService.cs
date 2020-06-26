using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using WeatherWalkingSkeleton.Config;
using WeatherWalkingSkeleton.Infrastructure;
using WeatherWalkingSkeleton.Models;

namespace WeatherWalkingSkeleton.Services
{
    public enum Unit
    {
        Metric,
        Imperial,
        Kelvin
    }

    public interface IOpenWeatherService
    {
        Task<List<WeatherForecast>> GetFiveDayForecastAsync(string location, Unit unit = Unit.Metric);
    }
    
    public class OpenWeatherService : IOpenWeatherService
    {
        private readonly OpenWeather _openWeatherConfig;
        private readonly IHttpClientFactory _httpFactory;

        public OpenWeatherService(IOptions<OpenWeather> opts, IHttpClientFactory httpFactory)
        {
            _openWeatherConfig = opts.Value;
            _httpFactory = httpFactory;
        }

        public async Task<List<WeatherForecast>> GetFiveDayForecastAsync(string location, Unit unit = Unit.Metric)
        {
            string url = BuildOpenWeatherUrl("forecast", location, unit);
            var forecasts = new List<WeatherForecast>();
            var client = _httpFactory.CreateClient();
                var response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var openWeatherResponse = JsonSerializer.Deserialize<OpenWeatherResponse>(json);
                    foreach (var forecast in openWeatherResponse.Forecasts)
                    {
                        forecasts.Add(new WeatherForecast
                        {
                            Date = new DateTime(forecast.Dt),
                            Temp = forecast.Temps.Temp,
                            FeelsLike = forecast.Temps.FeelsLike,
                            TempMin = forecast.Temps.TempMin,
                            TempMax = forecast.Temps.TempMax,
                        });
                    }
                }
                else
                {
                    var errorJson = await response.Content.ReadAsStringAsync();
                    var errorResponse = JsonSerializer.Deserialize<OpenWeatherErrorResponse>(errorJson);
                    throw new OpenWeatherException($"OpenWeatherMap error: { errorResponse.Message }");
                }

                return forecasts;
        }

        private string BuildOpenWeatherUrl(string resource, string location, Unit unit)
        {
            return $"https://api.openweathermap.org/data/2.5/{resource}" +
                   $"?q={location}" +
                   $"&appid={_openWeatherConfig.ApiKey}" +
                   $"&units={unit}";
        }
    }
}
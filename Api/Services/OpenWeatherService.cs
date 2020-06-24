using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Options;
using WeatherWalkingSkeleton.Config;
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
        List<WeatherForecast> GetFiveDayForecast(string location, Unit unit = Unit.Metric);
    }
    
    public class OpenWeatherService : IOpenWeatherService
    {
        private OpenWeather _openWeatherConfig;

        public OpenWeatherService(IOptions<OpenWeather> opts)
        {
            _openWeatherConfig = opts.Value;
        }

        public List<WeatherForecast> GetFiveDayForecast(string location, Unit unit = Unit.Metric)
        {
            string url = $"https://api.openweathermap.org/data/2.5/forecast?q={ location }&appid={ _openWeatherConfig.ApiKey }&units={ unit }";
            var forecasts = new List<WeatherForecast>();
            using (HttpClient client = new HttpClient())
            {
                var response = client.GetAsync(url).Result;
                var json = response.Content.ReadAsStringAsync().Result;
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
            
            return forecasts;
        }
    }
}
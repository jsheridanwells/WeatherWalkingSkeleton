using System;

namespace WeatherWalkingSkeleton.Models
{
    public class WeatherForecast
    {
        public DateTime Date { get; set; }
        public decimal Temp { get; set; }
        public decimal FeelsLike { get; set; }
        public decimal TempMin { get; set; }
        public decimal TempMax { get; set; }
    }
}
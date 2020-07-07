using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace WeatherWalkingSkeleton.Models
{
    public class OpenWeatherResponse 
    {
        [JsonPropertyName("list")]
        public List<Forecast> Forecasts { get; set; }
        
    }
    
    public class Forecast
    {
        [JsonPropertyName("dt")]
        public int Dt { get; set; }
        [JsonPropertyName("main")]
        public Temps Temps { get; set; }    
    }

    public class Temps
    {
        [JsonPropertyName("temp")]
        public decimal Temp  { get; set; }
        [JsonPropertyName("feels_like")]
        public decimal FeelsLike { get; set; }
        [JsonPropertyName("temp_min")]
        public decimal TempMin { get; set; }
        [JsonPropertyName("temp_max")]
        public decimal TempMax { get; set; }
    }
}
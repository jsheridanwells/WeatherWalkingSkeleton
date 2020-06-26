using System.Text.Json.Serialization;

namespace WeatherWalkingSkeleton.Models
{
    public class OpenWeatherErrorResponse
    { 
        [JsonPropertyName("message")] 
        public string Message { get; }
    }
}
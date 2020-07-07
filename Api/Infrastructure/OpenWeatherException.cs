using System;
using System.Net;

namespace WeatherWalkingSkeleton.Infrastructure
{
    public class OpenWeatherException : Exception
    {
        public HttpStatusCode StatusCode { get; }
        
        public OpenWeatherException() {  }

        public OpenWeatherException(HttpStatusCode statusCode)
            => StatusCode = statusCode;

        public OpenWeatherException(HttpStatusCode statusCode, string message) : base(message)
            => StatusCode = statusCode;

        public OpenWeatherException(HttpStatusCode statusCode, string message, Exception inner) : base(message, inner)
            => StatusCode = statusCode;
    }
}
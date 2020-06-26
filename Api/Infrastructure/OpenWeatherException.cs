using System;
using System.Net;

namespace WeatherWalkingSkeleton.Infrastructure
{
    public class OpenWeatherException : Exception
    {
        public OpenWeatherException() {  }

        public OpenWeatherException(string message) : base(message)  {  }
        public OpenWeatherException(string message, Exception inner) : base(message, inner) {  }
    }
}
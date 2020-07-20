using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using WeatherWalkingSkeleton.Config;
using WeatherWalkingSkeleton.Services;

namespace WeatherWalkingSkeleton
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            _env = env;
        }

        public IConfiguration Configuration { get; }
        private readonly IWebHostEnvironment _env;

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add OpenWeatherMap API key
            if (_env.IsDevelopment())
            {
                var openWeatherConfig = Configuration.GetSection("OpenWeather");
                services.Configure<OpenWeather>(openWeatherConfig);
            }
            var key = Environment.GetEnvironmentVariable("OpenWeatherApiKey");
            if (!string.IsNullOrEmpty(key))
            {
                services.Configure<OpenWeather>(opts =>
                {
                    opts.ApiKey = key;
                });
            }
            services.AddHttpClient();
            services.AddScoped<IOpenWeatherService, OpenWeatherService>();
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            if (_env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

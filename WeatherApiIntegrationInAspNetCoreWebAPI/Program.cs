using Microsoft.Extensions.Configuration;
using WeatherApi.Core.Entities;
using WeatherApi.Core.Interfaces;
using WeatherApi.Infrastracture.Repositories;
using WeatherApi.Infrastracture.Services;

namespace WeatherApiIntegrationInAspNetCoreWebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var configuration = builder.Configuration;

            // Add services to the container.
            builder.Services.Configure<WeatherApiSettings>(configuration.GetSection("ApiSettings"));
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddScoped<IWeatherService, WeatherService>();
            builder.Services.AddScoped<IWeatherRepository, WeatherRepository>();

            // Add CORS policy
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                               .AllowAnyMethod()
                               .AllowAnyHeader();
                    });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            // Use CORS policy
            app.UseCors("AllowAll");

            app.MapControllers();

            app.Run();
        }
    }
}

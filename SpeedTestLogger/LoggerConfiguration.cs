using System;
using System.Globalization;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace SpeedTestLogger
{
    public class LoggerConfiguration
    {
        public readonly RegionInfo LoggerLocation;
        public readonly string UserId;
        public readonly int LoggerId;
        public readonly Uri ApiUrl;

        public LoggerConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            var configuration = builder.Build();

            var countryCode = configuration["loggerLocationCountryCode"];
            LoggerLocation = new RegionInfo(countryCode);

            LoggerId = Int32.Parse(configuration["loggerId"]);
            UserId = configuration["userId"];

            ApiUrl = new Uri(configuration["speedTestApiUrl"]);

            Console.WriteLine("Logger located in {0}", LoggerLocation.EnglishName);
        }
    }
}
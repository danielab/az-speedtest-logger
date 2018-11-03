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

        public LoggerConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            var configuration = builder.Build();

            var countryCode = configuration["loggerLocationCountryCode"];
            LoggerLocation = new RegionInfo(countryCode);

            var LoggerId = Int32.Parse(configuration["loggerId"]);
            var UserId = configuration["userId"];

            Console.WriteLine("Logger located in {0}", LoggerLocation.EnglishName);
        }
    }
}
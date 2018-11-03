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
        public readonly string QueueConnectionString;
        public readonly string Topic;
        public readonly string SubscriptionName;

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
            
            QueueConnectionString = configuration["Queue:ConnectionString"];
            Topic = configuration["Queue:Topic"];
            SubscriptionName = configuration["Queue:SubscriptionName"];

            ApiUrl = new Uri(configuration["speedTestApiUrl"]);

            Console.WriteLine("Logger located in {0}", LoggerLocation.EnglishName);
        }
    }
}
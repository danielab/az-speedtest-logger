using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using SpeedTest;
using SpeedTestLogger.Models;
using SpeedTestLogger.Services;

namespace SpeedTestLogger
{
    class Program
    {
        private static LoggerConfiguration config;
        private static SpeedTestRunner runner;

        static void Main(string[] args)
        {
            config = new LoggerConfiguration();
            runner = new SpeedTestRunner(config.LoggerLocation);

            var serviceBus = new ServiceBusConnector(config.QueueConnectionString, config.Topic, config.SubscriptionName);
            serviceBus.RegisterOnMessageHandlerAndReceiveMessages();
            
            Console.ReadKey();
            
            serviceBus.Stop();
        }

        public static async Task RunSpeedTestAndPublish() {
            var testData = runner.RunSpeedTest();

            var results = new TestResult
            {
                SessionId = new Guid(),
                User = config.UserId,
                Device = config.LoggerId,
                Timestamp = DateTimeOffset.Now.ToUnixTimeMilliseconds(),
                Data = testData
            };

            var success = false;
            using (var client = new SpeedTestApiClient(config.ApiUrl))
            {
                success = await client.PublishTestResult(results);
            }

            if (success)
            {
                Console.WriteLine("Speedtest complete!");
            }
            else
            {
                Console.WriteLine("Speedtest failed!");
            }
        }
    }
}
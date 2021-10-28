using Grpc.Core;
using Grpc.Net.Client;
using Grpc.Net.Client.Configuration;
using Mars;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MarsExploration.Earth
{
    internal class Program
    {
        private const string Address = "https://localhost:5001";

        private static async Task Main(string[] args)
        {
            Console.Title = "Earth";
            var logger = CreateLogger();

            var token = await Authenticate();
            logger.LogInformation($"Authenticated as {Environment.UserName}");

            var headers = GenerateHeader(token);

            using var channel = CreateChannel();
            var client = new Rovers.RoversClient(channel);

            logger.LogInformation("Sample Exploration Started");
            await Exploration.StartSample(client, headers, logger);
            
            while (true)
            {
                logger.LogInformation("Do you want to add another rover ? (Y/N)");
                var addRoverInput = Console.ReadLine();
                if (!addRoverInput.Equals("Y", StringComparison.InvariantCultureIgnoreCase))
                {
                    break;
                }
                else
                {
                    var coordinate = Console.ReadLine();
                    var instructions = Console.ReadLine();
                    await Exploration.Start(client, headers, logger, coordinate, instructions);
                }
            } 
        } 
       
        private static Metadata? GenerateHeader(string token)
        {
            Metadata? headers = null;
            if (token != null)
            {
                headers = new Metadata
                    {
                        { "Authorization", $"Bearer {token}" }
                    };
            }

            return headers;
        }

        private static ILogger CreateLogger()
        {
            var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder
                    .AddFilter("Microsoft", LogLevel.Warning)
                    .AddFilter("System", LogLevel.Warning)
                    .AddFilter("LoggingConsoleApp.Program", LogLevel.Debug)
                    .AddConsole();
            });
            return loggerFactory.CreateLogger<Program>();
        }

        private static async Task<string> Authenticate()
        {
            using var httpClient = new HttpClient();
            using var request = new HttpRequestMessage
            {
                RequestUri = new Uri($"{Address}/GenerateToken?name={HttpUtility.UrlEncode(Environment.UserName)}"),
                Method = HttpMethod.Get,
                Version = new Version(2, 0)
            };
            using var tokenResponse = await httpClient.SendAsync(request);
            tokenResponse.EnsureSuccessStatusCode();

            var token = await tokenResponse.Content.ReadAsStringAsync();

            return token;
        }

        private static GrpcChannel CreateChannel()
        {
            var methodConfig = new MethodConfig
            {
                Names = { MethodName.Default },
                RetryPolicy = new RetryPolicy
                {
                    MaxAttempts = 3,
                    InitialBackoff = TimeSpan.FromSeconds(0.5),
                    MaxBackoff = TimeSpan.FromSeconds(0.5),
                    BackoffMultiplier = 1,
                    RetryableStatusCodes = { StatusCode.Unavailable }
                }
            };

            return GrpcChannel.ForAddress(Address, new GrpcChannelOptions
            {
                ServiceConfig = new ServiceConfig { MethodConfigs = { methodConfig } }
            });
        }
    }
}
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;

namespace MarsExploration.FuntionalTests.Base
{
    public class TestFixture<TStartup> : IDisposable where TStartup : class
    {
        private readonly TestServer _server;
        private readonly IHost _host;

        public event LogMessage? LoggedMessage;

        public TestFixture() : this(null)
        {
        }

        public TestFixture(Action<IServiceCollection>? initialConfigureServices)
        {
            LoggerFactory = new LoggerFactory();
            LoggerFactory.AddProvider(new LoggerProvider((logLevel, category, eventId, message, exception) =>
            {
                LoggedMessage?.Invoke(logLevel, category, eventId, message, exception);
            }));

            var builder = new HostBuilder()
            .ConfigureServices(services =>
            {
                initialConfigureServices?.Invoke(services);
                services.AddSingleton<ILoggerFactory>(LoggerFactory);
            })
            .ConfigureWebHostDefaults(webHost =>
            {
                webHost
                    .UseTestServer()
                    .UseStartup<TStartup>();
            });

            _host = builder.Start();
            _server = _host.GetTestServer();
            Handler = _server.CreateHandler();
        }

        public LoggerFactory LoggerFactory { get; }

        public HttpMessageHandler Handler { get; }

        public void Dispose()
        {
            Handler.Dispose();
            _host.Dispose();
            _server.Dispose();
        }

        public IDisposable GetTestContext() => new TestContext<TStartup>(this);
    }
}
using Grpc.Net.Client;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using System;

namespace MarsExploration.FuntionalTests.Base
{
    public class ScenarioBase
    {
        private const string _address = "https://localhost";
        private GrpcChannel _channel;
        private IDisposable _testContext;

        protected TestFixture<Rovers.Startup> Fixture { get; private set; } = default!;

        protected ILoggerFactory LoggerFactory => Fixture.LoggerFactory;

        protected GrpcChannel Channel => _channel ??= CreateChannel();

        protected GrpcChannel CreateChannel()
        {
            return GrpcChannel.ForAddress(_address, new GrpcChannelOptions
            {
                LoggerFactory = LoggerFactory,
                HttpHandler = Fixture.Handler
            });
        }

        protected virtual void ConfigureServices(IServiceCollection services) { }

        [OneTimeSetUp]
        public void OneTimeSetUp() => Fixture = new TestFixture<Rovers.Startup>(ConfigureServices);

        [OneTimeTearDown]
        public void OneTimeTearDown() => Fixture.Dispose();

        [SetUp]
        public void SetUp() => _testContext = Fixture.GetTestContext();

        [TearDown]
        public void TearDown()
        {
            _testContext?.Dispose();
            _channel = null;
        }
    }
}
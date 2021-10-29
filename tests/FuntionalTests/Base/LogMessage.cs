using Microsoft.Extensions.Logging;
using System;

namespace MarsExploration.FuntionalTests.Base
{
    public delegate void LogMessage(LogLevel logLevel, string categoryName, EventId eventId, string message, Exception exception);
}
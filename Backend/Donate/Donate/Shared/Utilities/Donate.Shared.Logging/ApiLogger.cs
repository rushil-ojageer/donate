using System;
using Microsoft.Extensions.Logging;

namespace Donate.Shared.Logging
{
    public class ApiLogger<T> : IApiLogger
    {
        private readonly ILogger<T> _logger;

        public ApiLogger(IServiceProvider serviceProvider)
        {
            _logger = serviceProvider.GetService(typeof(ILogger<T>)) as ILogger<T>;

            if (_logger == null)
                throw new Exception("Unable to create instance of logger.");
        }

        public void Info(string log)
        {
            _logger.LogInformation(log);
        }

        public void Error(string log, Exception ex)
        {
            _logger.LogError(ex, log);
        }

        public void Error(string log)
        {
            _logger.LogError(log);
        }

        public void Debug(string log)
        {
            _logger.LogDebug(log);
        }

        public void Critical(string log)
        {
            _logger.LogCritical(log);
        }

        public void Warning(string log)
        {
            _logger.LogWarning(log);
        }

        public void Trace(string log)
        {
            _logger.LogTrace(log);
        }
    }
}
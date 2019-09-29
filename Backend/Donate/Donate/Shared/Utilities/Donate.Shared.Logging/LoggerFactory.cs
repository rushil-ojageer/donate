using System;

namespace Donate.Shared.Logging
{
    public class LoggerFactory : ILoggerFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public LoggerFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IApiLogger GetLogger<T>()
        {
            return new ApiLogger<T>(_serviceProvider);
        }
    }
}
using Donate.Shared.Logging;
using RabbitMQ.Client;

namespace Donate.Shared.Queues
{
    public class QueueChannelManager : IQueueChannelManager
    {
        private readonly IModel _channel;
        private IApiLogger _logger;

        public QueueChannelManager(IQueueConnectionManager queueConnectionManager,
            ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.GetLogger<QueueChannelManager>();
            _channel = queueConnectionManager.GetConnection().CreateModel();
        }

        public void Dispose()
        {
            _channel?.Close();
            _channel?.Dispose();
        }

        public IModel GetChannel()
        {
            return _channel;
        }
    }
}
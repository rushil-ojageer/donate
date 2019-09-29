using System.Collections.Generic;
using Donate.Shared.Logging;

namespace Donate.Shared.Queues
{
    public class QueueFactory : IQueueFactory
    {
        private readonly IQueueChannelManager _queueChannelManager;
        private IApiLogger _logger;

        public QueueFactory(IQueueChannelManager queueChannelManager,
            ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.GetLogger<QueueFactory>();
            _queueChannelManager = queueChannelManager;
        }

        public void SetupQueue(IQueueSettings queueSettings)
        {
            CreateExchange(queueSettings);
            CreateQueue(queueSettings);
            CreateQueueExchangeBinding(queueSettings);

            if (queueSettings.DeadLetterQueueSettings != null)
            {
                CreateDeadLetterQueue(queueSettings);
            }
        }

        public void CreateDeadLetterQueue(IQueueSettings queueSettings)
        {
            _queueChannelManager
                .GetChannel()
                .QueueDeclare(queue: queueSettings.DeadLetterQueueSettings.QueueName,
                    durable: queueSettings.DeadLetterQueueSettings.IsQueueDurable,
                    exclusive: queueSettings.DeadLetterQueueSettings.IsQueueExclusive,
                    autoDelete: queueSettings.DeadLetterQueueSettings.DeleteQueueIfNoConsumers,
                    arguments: queueSettings.DeadLetterQueueSettings.QueueArguments);
        }

        public void CreateExchange(IQueueSettings queueSettings)
        {
            _queueChannelManager
                .GetChannel()
                .ExchangeDeclare(exchange: queueSettings.ExchangeName,
                    type: queueSettings.ExchangeType,
                    durable: queueSettings.IsQueueDurable,
                    autoDelete: queueSettings.DeleteQueueIfNoConsumers,
                    arguments: null);
        }

        public void CreateQueue(IQueueSettings queueSettings)
        {
            _queueChannelManager
                .GetChannel()
                .QueueDeclare(queue: queueSettings.QueueName,
                    durable: queueSettings.IsQueueDurable,
                    exclusive: queueSettings.IsQueueExclusive,
                    autoDelete: queueSettings.DeleteQueueIfNoConsumers,
                    arguments: new Dictionary<string, object>()
                    {
                        {"x-message-ttl", 3600000},
                        {"x-dead-letter-exchange", ""},
                        {"x-dead-letter-routing-key", queueSettings.DeadLetterQueueSettings?.RoutingKey}
                    });
        }

        public void CreateQueueExchangeBinding(IQueueSettings queueSettings)
        {
            _queueChannelManager
                .GetChannel()
                .QueueBind(queueSettings.QueueName,
                    queueSettings.ExchangeName,
                    queueSettings.RoutingKey,
                    null);
        }

    }
}
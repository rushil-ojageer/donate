using System.Collections.Generic;
using System.Text;
using Donate.Shared.IntegrationQueue.Models;
using Donate.Shared.Logging;
using Donate.Shared.Queues;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace Donate.Shared.IntegrationQueue
{
    public class IntegrationEventQueue : IIntegrationEventQueue
    {
        private readonly IQueueChannelManager _responseQueueChannelManager;
        private readonly IQueueSettings _settings;
        private IApiLogger _logger;

        public IntegrationEventQueue(IQueueChannelManager responseQueueChannelManager,
            IQueueSettingsResolver queueSettingsResolver,
            IQueueFactory responseQueueFactory,
            ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.GetLogger<IntegrationEventQueue>();
            _responseQueueChannelManager = responseQueueChannelManager;
            _settings = queueSettingsResolver.GetSettings(nameof(IntegrationEventQueue));
            responseQueueFactory.SetupQueue(_settings);
        }

        public void Post<T>(IntegrationEvent<T> @event) where T : class
        {
            var message = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(@event));
            var messageType = new ServiceEvent(@event.Service, @event.Event);

            var basicProperties = _responseQueueChannelManager.GetChannel().CreateBasicProperties();
            basicProperties.ContentType = "application/json";
            basicProperties.MessageId = JsonConvert.SerializeObject(messageType);
            basicProperties.Headers = new Dictionary<string, object> {{"SeenBy", 0}};

            _responseQueueChannelManager
                .GetChannel()
                .BasicPublish(exchange: _settings.ExchangeName,
                routingKey: _settings.RoutingKey,
                basicProperties: basicProperties,
                body: message);
        }
    }
}
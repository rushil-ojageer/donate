using System.Text;
using Donate.Shared.Logging;
using Donate.Shared.Queues;
using Donate.Shared.ResponseQueue.Models;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace Donate.Shared.ResponseQueue
{
    public class ResponseQueue : IResponseQueue
    {
        private readonly IQueueChannelManager _responseQueueChannelManager;
        private readonly IQueueSettings _responseQueueSettings;
        private IApiLogger _logger;

        public ResponseQueue(IQueueChannelManager responseQueueChannelManager,
            IQueueSettingsResolver queueSettingsResolver,
            IQueueFactory responseQueueFactory,
            ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.GetLogger<ResponseQueue>();
            _responseQueueChannelManager = responseQueueChannelManager;
            _responseQueueSettings = queueSettingsResolver.GetSettings(nameof(ResponseQueue));

            responseQueueFactory.SetupQueue(_responseQueueSettings);
        }

        public void PostResponse<T>(QueueResponse<T> response)
        {
            var message = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(response));
            _responseQueueChannelManager.GetChannel().BasicPublish(exchange: _responseQueueSettings.ExchangeName,
                routingKey: _responseQueueSettings.RoutingKey,
                basicProperties: null,
                body: message);
        }
    }
}

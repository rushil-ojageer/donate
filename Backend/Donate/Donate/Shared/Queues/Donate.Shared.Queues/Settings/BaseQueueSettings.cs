using System.Collections.Generic;

namespace Donate.Shared.Queues.Settings
{
    public abstract class BaseQueueSettings : IQueueSettings
    {
        protected BaseQueueSettings()
        {
            QueueArguments = new Dictionary<string, object>()
            {

            };
        }

        public string QueueName { get; set; }
        public string ExchangeName { get; set; }
        public string ExchangeType { get; set; }
        public string RoutingKey { get; set; }
        public bool IsQueueDurable { get; set; }
        public bool IsQueueExclusive { get; set; }
        public bool DeleteQueueIfNoConsumers { get; set; }
        public IDictionary<string, object> QueueArguments { get; set; }
        public DeadLetterQueueSettings DeadLetterQueueSettings { get; set; }
    }
}
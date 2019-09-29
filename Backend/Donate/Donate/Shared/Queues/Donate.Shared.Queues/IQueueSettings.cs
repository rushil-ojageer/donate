using System.Collections.Generic;
using Donate.Shared.Queues.Settings;

namespace Donate.Shared.Queues
{
    public interface IQueueSettings
    {
        string QueueName { get; set; }
        string ExchangeName { get; set; }
        string ExchangeType { get; set; }
        string RoutingKey { get; set; }
        bool IsQueueDurable { get; set; }
        bool IsQueueExclusive { get; set; }
        bool DeleteQueueIfNoConsumers { get; set; }
        IDictionary<string, object> QueueArguments { get; set; }
        DeadLetterQueueSettings DeadLetterQueueSettings { get; set; }
    }
}

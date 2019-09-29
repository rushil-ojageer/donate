using System;
using Donate.Shared.IntegrationQueue;
using Donate.Shared.Queues;
using Microsoft.Extensions.Options;

namespace Donate.FundService.TransactionFeed.Settings
{
    public class QueueSettingsResolver  : IQueueSettingsResolver
    {
        private readonly IOptions<TransactionListenerSettings> _transactionListenerSettings;

        public QueueSettingsResolver(IOptions<TransactionListenerSettings> transactionListenerSettings)
        {
            _transactionListenerSettings = transactionListenerSettings;
        }


        public IQueueSettings GetSettings(string name)
        {
            switch (name)
            {
                case nameof(IntegrationEventQueue):
                    return _transactionListenerSettings.Value;
                default:
                    throw new Exception($"Unable to resolve settings for '{name}' - {name} is not known");
            }
        }
    }
}

using System;
using Donate.Shared.IntegrationQueue;
using Donate.Shared.IntegrationQueue.Settings;
using Donate.Shared.QueueListener.Services;
using Donate.Shared.Queues;
using Microsoft.Extensions.Options;

namespace Donate.FundService.TransactionProcessor.Settings
{
    public class QueueSettingsResolver  : IQueueSettingsResolver
    {
        private readonly IOptions<TransactionListenerSettings> _transactionListenerSettings;
        private readonly IOptions<IntegrationEventQueueSettings> _integrationEventQueueSettings;

        public QueueSettingsResolver(IOptions<TransactionListenerSettings> transactionListenerSettings,
            IOptions<IntegrationEventQueueSettings> integrationEventQueueSettings)
        {
            _transactionListenerSettings = transactionListenerSettings;
            _integrationEventQueueSettings = integrationEventQueueSettings;
        }

        public IQueueSettings GetSettings(string name)
        {
            switch (name)
            {
                case nameof(QueueListener):
                    return _transactionListenerSettings.Value;
                case nameof(IntegrationEventQueue):
                    return _integrationEventQueueSettings.Value;
                default:
                    throw new Exception($"Unable to resolve settings for '{name}' - {name} is not known");
            }
        }
    }
}

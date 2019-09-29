using System;
using Donate.Shared.IntegrationQueue.Settings;
using Donate.Shared.QueueListener.Services;
using Donate.Shared.Queues;
using Donate.Shared.ResponseQueue.Settings;
using Microsoft.Extensions.Options;

namespace Donate.FundService.IntegrationWorker.Settings
{
    public class QueueSettingsResolver  : IQueueSettingsResolver
    {
        private readonly IOptions<IntegrationEventQueueSettings> _integrationEventQueueSettings;

        public QueueSettingsResolver(IOptions<IntegrationEventQueueSettings> integrationEventQueueSettings)
        {
            _integrationEventQueueSettings = integrationEventQueueSettings;
        }

        public IQueueSettings GetSettings(string name)
        {
            switch (name)
            {
                case nameof(QueueListener):
                    return _integrationEventQueueSettings.Value;
                default:
                    throw new Exception($"Unable to resolve settings for '{name}' - {name} is not known");
            }
        }
    }
}

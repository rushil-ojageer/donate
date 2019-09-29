using System;
using Donate.Shared.IntegrationQueue;
using Donate.Shared.IntegrationQueue.Settings;
using Donate.Shared.Queues;
using Donate.Shared.ResponseQueue;
using Donate.Shared.ResponseQueue.Settings;
using Microsoft.Extensions.Options;

namespace Donate.CharityService.API.Settings
{
    public class QueueSettingsResolver : IQueueSettingsResolver
    {
        private readonly IOptions<IntegrationEventQueueSettings> _integrationEventQueueSettings;
        private readonly IOptions<ResponseQueueSettings> _responseQueueSettings;

        public QueueSettingsResolver(IOptions<IntegrationEventQueueSettings> integrationEventQueueSettings,
            IOptions<ResponseQueueSettings> responseQueueSettings)
        {
            _integrationEventQueueSettings = integrationEventQueueSettings;
            _responseQueueSettings = responseQueueSettings;
        }

        public IQueueSettings GetSettings(string name)
        {
            switch (name)
            {
                case nameof(ResponseQueue):
                    return _responseQueueSettings.Value;
                case nameof(IntegrationEventQueue):
                    return _integrationEventQueueSettings.Value;
                default:
                    throw new Exception($"Unable to resolve settings for '{name}' - {name} is not known");
            }
        }
    }
}

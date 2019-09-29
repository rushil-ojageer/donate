using Donate.Shared.IntegrationQueue.Models;

namespace Donate.Shared.IntegrationQueue
{
    public interface IIntegrationEventQueue
    {
        void Post<T>(IntegrationEvent<T> @event) where T : class;
    }
}

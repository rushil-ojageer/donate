using System.Threading.Tasks;
using Donate.Shared.IntegrationQueue.Models;
using Newtonsoft.Json;

namespace Donate.Shared.QueueListener.EventHandler
{
    public abstract class BaseEventHandler<T> : IEventHandler where T : class
    {
        public abstract string GetService();

        public abstract string GetEvent();
        
        public Task HandleEvent(string content)
        {
            var @event = JsonConvert.DeserializeObject<IntegrationEvent<T>>(content);
            return ProcessEvent(@event.Body);
        }

        protected abstract Task ProcessEvent(T @event);
    }
}
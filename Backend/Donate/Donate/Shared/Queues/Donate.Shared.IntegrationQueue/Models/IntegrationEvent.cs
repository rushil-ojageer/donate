using System;

namespace Donate.Shared.IntegrationQueue.Models
{
    public class IntegrationEvent<T> where T : class
    {
        public string Service { get; set; }
        public DateTime DateTimeUtc { get; set; }
        public string Event { get; set; }
        public T Body { get; set; }

        public IntegrationEvent()
        {

        }

        public IntegrationEvent(string service, string @event, T body)
        {
            Service = service;
            Event = @event;
            Body = body;
            DateTimeUtc = DateTime.UtcNow;
        }
    }
}

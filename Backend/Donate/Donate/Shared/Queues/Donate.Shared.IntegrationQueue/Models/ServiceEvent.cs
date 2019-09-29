using System.Collections.Generic;

namespace Donate.Shared.IntegrationQueue.Models
{
    public class ServiceEvent
    {
        public string Service { get; set; }
        public string Event { get; set; }

        public ServiceEvent()
        {

        }

        public ServiceEvent(string service, string @event)
        {
            Service = service;
            Event = @event;
        }
    }

    public class ServiceEventEqualityComparer : IEqualityComparer<ServiceEvent>
    {
        public bool Equals(ServiceEvent x, ServiceEvent y)
        {
            if (x == null) return false;
            if (y == null) return false;
            if (!x.Service.Equals(y.Service)) return false;
            if (!x.Event.Equals(y.Event)) return false;
            return true;
        }

        public int GetHashCode(ServiceEvent obj)
        {
            return obj.Service.GetHashCode() + obj.Event.GetHashCode();
        }
    }

}
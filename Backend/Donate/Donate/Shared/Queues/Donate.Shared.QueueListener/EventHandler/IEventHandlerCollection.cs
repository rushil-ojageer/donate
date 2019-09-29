using System;
using System.Collections.Generic;

namespace Donate.Shared.QueueListener.EventHandler
{
    public interface IEventHandlerCollection : IDisposable
    {
        IEnumerable<IEventHandler> Get(string service, string @event);
    }
}
using System;
using System.Collections.Generic;
using System.Text;

namespace Donate.Shared.QueueListener.EventHandler
{
    public class EventHandlerRegistry
    {
        private readonly IList<Type> _eventHandlers;

        public EventHandlerRegistry()
        {
            _eventHandlers = new List<Type>();
        }

        public IList<Type> GetEventHandlers()
        {
            return _eventHandlers;
        }

        public void AddEventHandler<T>()
        {
            _eventHandlers.Add(typeof(T));
        }
    }
}

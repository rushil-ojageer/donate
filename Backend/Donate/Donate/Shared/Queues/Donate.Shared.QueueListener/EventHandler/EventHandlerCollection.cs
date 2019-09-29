using System;
using System.Collections.Generic;
using Donate.Shared.IntegrationQueue.Models;
using Donate.Shared.Utilities;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.DependencyInjection;

namespace Donate.Shared.QueueListener.EventHandler
{
    public class EventHandlerCollection : IEventHandlerCollection
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IDictionary<ServiceEvent, IList<IEventHandler>> _eventHandlers;

        public EventHandlerCollection(IServiceProvider serviceProvider, EventHandlerRegistry eventHandlerRegistry)
        {
            _serviceProvider = serviceProvider;
            _eventHandlers = new Dictionary<ServiceEvent, IList<IEventHandler>>(new ServiceEventEqualityComparer());
            foreach (var eventHandler in eventHandlerRegistry.GetEventHandlers())
            {
                AddHandler(eventHandler);
            }
        }

        public IEnumerable<IEventHandler> Get(string service, string @event)
        {
            var serviceEvent = new ServiceEvent(service, @event);
            return _eventHandlers.TryGetValue(serviceEvent, out var handlers) ? 
                handlers : 
                new List<IEventHandler>();
        }

        private void AddHandler(Type type)
        {
            if (!(_serviceProvider.GetService(type) is IEventHandler handler)) return;

            var serviceEvent = new ServiceEvent(handler.GetService(), handler.GetEvent());

            if (_eventHandlers.TryGetValue(serviceEvent, out var handlers))
            {
                handlers.Add(handler);
            }
            else
            {
                _eventHandlers.Add(serviceEvent, new List<IEventHandler> { handler });
            }
        }

        public void Dispose()
        {
        }
    }
}
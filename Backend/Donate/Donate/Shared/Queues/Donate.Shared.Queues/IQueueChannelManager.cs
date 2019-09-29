using System;
using RabbitMQ.Client;

namespace Donate.Shared.Queues
{
    public interface IQueueChannelManager : IDisposable
    {
        IModel GetChannel();
    }
}
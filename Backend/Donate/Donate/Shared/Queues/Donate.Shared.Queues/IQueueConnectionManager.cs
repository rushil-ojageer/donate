using System;
using RabbitMQ.Client;

namespace Donate.Shared.Queues
{
    public interface IQueueConnectionManager : IDisposable
    {
        IConnection GetConnection();
    }
}
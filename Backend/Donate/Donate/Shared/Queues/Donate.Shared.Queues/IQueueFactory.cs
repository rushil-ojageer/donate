namespace Donate.Shared.Queues
{
    public interface IQueueFactory
    {
        void SetupQueue(IQueueSettings queueSettings);
        void CreateDeadLetterQueue(IQueueSettings queueSettings);
        void CreateExchange(IQueueSettings queueSettings);
        void CreateQueue(IQueueSettings queueSettings);
        void CreateQueueExchangeBinding(IQueueSettings queueSettings);
    }
}

namespace Donate.Shared.Queues
{
    public interface IQueueSettingsResolver
    {
        IQueueSettings GetSettings(string name);
    }
}

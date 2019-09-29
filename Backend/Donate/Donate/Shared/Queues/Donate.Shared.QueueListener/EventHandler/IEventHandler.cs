using System.Threading.Tasks;

namespace Donate.Shared.QueueListener.EventHandler
{
    public interface IEventHandler
    {
        string GetService();
        string GetEvent();
        Task HandleEvent(string content);
    }
}

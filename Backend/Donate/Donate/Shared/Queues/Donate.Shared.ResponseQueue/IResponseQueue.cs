using Donate.Shared.ResponseQueue.Models;

namespace Donate.Shared.ResponseQueue
{
    public interface IResponseQueue
    {
        void PostResponse<T>(QueueResponse<T> response);
    }
}
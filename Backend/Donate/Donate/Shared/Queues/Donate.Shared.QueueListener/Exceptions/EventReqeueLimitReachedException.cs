using System;

namespace Donate.Shared.QueueListener.Exceptions
{
    public class EventRequeueLimitReachedException : Exception
    {
        public EventRequeueLimitReachedException(ulong retries, int limit)
            : base($"Event has been re-queued {retries} times - the limit is {limit}")
        {

        }
    }
}

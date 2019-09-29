using System;

namespace Donate.Shared.QueueListener.Exceptions
{
    public class UnsupportedMessageException : Exception
    {
        public UnsupportedMessageException(ulong deliveryTag)
            : base($"Message {deliveryTag} is not supported.")
        {

        }
    }
}
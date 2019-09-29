using System;
using Donate.Shared.ResponseQueue;
using Donate.Shared.ResponseQueue.Models;

namespace Donate.Shared.API.Extensions
{
    public static class ResponseQueueExtensions
    {
        public static void PostOkResponse<T>(this IResponseQueue queue, Guid correlationId, T response)
        {
            queue.PostResponse(QueueResponse<T>.Ok(correlationId, response));
        }
    }
}

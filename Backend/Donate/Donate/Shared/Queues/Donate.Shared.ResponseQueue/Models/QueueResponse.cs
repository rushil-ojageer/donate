using System;
using System.Net;

namespace Donate.Shared.ResponseQueue.Models
{
    public class QueueResponse<T>
    {
        public Guid CorrelationId { get; set; }
        public T Data { get; set; }
        public DateTime ResponseTimeUtc { get; set; }
        public HttpStatusCode StatusCode { get; set; }

        public static QueueResponse<TK> Ok<TK>(Guid correlationId, TK data)
        {
            return new QueueResponse<TK>()
            {
                CorrelationId = correlationId,
                Data = data,
                ResponseTimeUtc = DateTime.UtcNow,
                StatusCode = HttpStatusCode.OK
            };
        }

    }
}

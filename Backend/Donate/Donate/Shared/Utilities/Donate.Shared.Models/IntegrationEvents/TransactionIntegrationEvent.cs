using System;
using System.Collections.Generic;
using System.Text;

namespace Donate.Shared.Eventing.IntegrationEvents
{
    public class TransactionIntegrationEvent
    {
        public Guid TransactionIdentifier { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public Guid DonorTransactionSourceIdentifier { get; set; }
        public DateTime ReceivedAtDateTimeUtc { get; set; }
        public DateTime TransactionDateTimeUtc { get; set; }
        public string MerchantName { get; set; }
    }
}

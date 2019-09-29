using System;
using System.Collections.Generic;
using System.Text;

namespace Donate.Shared.Eventing.TransactionEvents
{
    public class TransactionEvent
    {
        public string TransactionIdentifier { get; set; }
        public string TransactionSourceIdentifier { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string MerchantIdentifier { get; set; }
        public string MerchantName { get; set; }
        public DateTime TransactionDateTimeUtc { get; set; }
    }
}
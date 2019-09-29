using System;

namespace Donate.Shared.Eventing.IntegrationEvents
{
    public class DonorTransactionSourceEvent
    {
        public string FinancialInstitution { get; set; }
        public string Type { get; set; }
        public string Identifier { get; set; }
        public long DonorId { get; set; }
        public Guid TransactionSourceIdentifier { get; set; }
    }
}
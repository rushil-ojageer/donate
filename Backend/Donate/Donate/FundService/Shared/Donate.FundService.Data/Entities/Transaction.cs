using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Donate.FundService.Data.Entities
{
    public class Transaction
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        public Guid TransactionIdentifier { get; set; }

        [Required]
        public string ExternalTransactionIdentifier { get; set; }

        [Required]
        public long DonorTransactionSourceId { get; set; }

        [Required]
        public long MerchantId { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        [StringLength(3)]
        public string Currency { get; set; }

        [Required]
        public DateTime TransactionDateTimeUtc { get; set; }

        [Required]
        public DateTime ReceivedDateTimeUtc { get; set; }

        public DonorTransactionSource DonorTransactionSource { get; set; }

        public Merchant Merchant { get; set; }
    }
}
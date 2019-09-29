using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Donate.DonorService.Data.Entities
{
    public class Donation
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        public long DonorCharityProportionId { get; set; }
        
        [Required]
        [MaxLength(3)]
        public decimal Amount { get; set; }
        
        [Required]
        [StringLength(3)]
        public string Currency { get; set; }

        [Required]
        public DateTime DonationDateTimeUtc { get; set; }

        [Required]
        public Guid TransactionIdentifier { get; set; }

        [Required]
        public string MerchantName { get; set; }

        [Required]
        public decimal TransactionAmount { get; set; }

        [Required]
        public decimal TransactionDonationPercentage { get; set; }

        [Required]
        public DateTime TransactionDateTimeUtc { get; set; }

        [ForeignKey("DonorCharityProportionId")]
        public DonorCharityProportion DonorCharityProportion { get; set; }
    }
}
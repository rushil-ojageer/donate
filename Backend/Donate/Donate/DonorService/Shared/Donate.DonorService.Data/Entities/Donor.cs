using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Donate.Shared.Data;

namespace Donate.DonorService.Data.Entities
{
    public class Donor : IDeletableEntity, IAuditableEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        [StringLength(100)]
        public string FirstName { get; set; }
        
        [Required]
        [StringLength(100)]
        public string LastName { get; set; }
        
        [Required]
        public IdentityType IdentityType { get; set; }
        
        [Required]
        [StringLength(20)]
        public string IdentityNumber { get; set; }
        
        [Required]
        [StringLength(20)]
        public string ContactNumber { get; set; }
        
        [Required]
        [StringLength(250)]
        public string EmailAddress { get; set; }

        [Required]
        public bool IsDeleted { get; set; }

        [Required]
        public DateTime UpdatedAt { get; set; }

        [Required]
        public string UpdatedBy { get; set; }

        [Required]
        public decimal TransactionDonationPercentage { get; set; }
        
        [Required]
        public decimal DonationCap { get; set; }

        public ICollection<DonorCharity> DonorCharities { get; set; }
    }
}

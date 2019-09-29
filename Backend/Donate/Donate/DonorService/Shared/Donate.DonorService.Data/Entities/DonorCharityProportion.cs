using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Donate.DonorService.Data.Entities
{
    public class DonorCharityProportion
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        public long DonorCharityId { get; set; }
        
        [Required]
        public decimal DonationPercentage { get; set; }
        
        [Required]
        public DateTime ValidFromUtc { get; set; }
        
        [Required]
        public DateTime ValidToUtc { get; set; }
        
        public DonorCharity DonorCharity { get; set; }
        
        public ICollection<Donation> Donations { get; set; }
    }
}
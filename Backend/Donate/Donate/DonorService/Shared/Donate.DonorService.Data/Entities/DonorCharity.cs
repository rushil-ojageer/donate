using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Donate.Shared.Data;

namespace Donate.DonorService.Data.Entities
{
    public class DonorCharity : IDeletableEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        public long DonorId { get; set; }
        
        [Required]
        public long CharityId { get; set; }

        [Required]
        public bool IsDeleted { get; set; }

        public Donor Donor { get; set; }
        
        public Charity Charity { get; set; }
        
        public ICollection<DonorCharityProportion> DonorCharityProportions { get; set; }
    }
}
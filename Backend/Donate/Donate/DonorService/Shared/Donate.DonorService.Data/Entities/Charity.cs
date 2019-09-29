using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Donate.Shared.Data;

namespace Donate.DonorService.Data.Entities
{
    public class Charity : IDeletableEntity, IAuditableEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        public Guid Identifier { get; set; }

        [Required]
        public string CharityName { get; set; }

        [Required]
        public bool IsDeleted { get; set; }

        [Required]
        public DateTime UpdatedAt { get; set; }

        [Required]
        public string UpdatedBy { get; set; }

        public ICollection<DonorCharity> DonorCharities { get; set; }
    }
}
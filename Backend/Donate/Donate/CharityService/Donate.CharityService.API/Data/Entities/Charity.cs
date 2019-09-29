using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Donate.Shared.Data;

namespace Donate.CharityService.API.Data.Entities
{
    public class Charity : IDeletableEntity, IAuditableEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        public Guid CharityIdentifier { get; set; }

        [Required]
        [StringLength(500)]
        public string CharityName { get; set; }

        [Required]
        [StringLength(500)]
        public string ContactPerson { get; set; }

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
        [StringLength(250)]
        public string UpdatedBy { get; set; }
    }
}

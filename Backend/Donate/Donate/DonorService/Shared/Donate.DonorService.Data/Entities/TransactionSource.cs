using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Donate.Shared.Data;

namespace Donate.DonorService.Data.Entities
{
    public class TransactionSource : IDeletableEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        public Guid TransactionSourceIdentifier { get; set; }

        [Required]
        [StringLength(1000)]
        public string FinancialInstitution { get; set; }

        [Required]
        public TransactionSourceType Type { get; set; }

        [Required]
        public string Identifier { get; set; }

        [Required]
        public long DonorId { get; set; }

        [Required]
        public bool IsDeleted { get; set; }

        public Donor Donor { get; set; }
    }
}

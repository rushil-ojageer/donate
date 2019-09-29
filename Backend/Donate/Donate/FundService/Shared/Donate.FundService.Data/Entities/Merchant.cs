using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Donate.FundService.Data.Entities
{
    public class Merchant
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        public string ExternalMerchantIdentifier { get; set; }

        [Required]
        [StringLength(2000)]
        public string MerchantName { get; set; }
    }
}
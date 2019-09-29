using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Donate.DonorService.Data;
using Microsoft.EntityFrameworkCore;

namespace Donate.DonorService.API.Models
{
    public class DonationModels
    {
        private readonly long _donorId;

        public List<DonationModel> Items { get; set; }

        public int Offset { get; set; }

        public int Count { get; set; }

        public int Total { get; set; }

        public DonationModels(long donorId, int offset, int count)
        {
            _donorId = donorId;
            Items = new List<DonationModel>();
            Offset = offset;
            Count = count;
        }

        public async Task PopulateAll(DonorContext db)
        {
            var query = db.Donations
                .Where(x => x.DonorCharityProportion.DonorCharity.DonorId == _donorId)
                .OrderBy(x => x.DonationDateTimeUtc)
                .Include(x => x.DonorCharityProportion)
                .ThenInclude(x => x.DonorCharity)
                .ThenInclude(x => x.Charity);

            var items = await query
                .Skip(Offset)
                .Take(Count)
                .ToListAsync();

            Total = await query.CountAsync();
            Items = items.Select(DonationModel.FromEntity).ToList();
        }
    }
}

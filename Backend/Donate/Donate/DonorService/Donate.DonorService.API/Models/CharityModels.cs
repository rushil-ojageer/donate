using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Donate.DonorService.Data;
using Donate.Shared.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Donate.DonorService.API.Models
{
    public class CharityModels : BaseModel<DonorContext>
    {
        public List<CharityModel> Items { get; set; }

        public long? DonorId { get; set; }

        public int Offset { get; set; }

        public int Count { get; set; }

        public int Total { get; set; }

        public CharityModels(int donorId, int offset, int count)
        {
            DonorId = donorId;
            Offset = offset;
            Count = count;
        }

        public async Task PopulateAll(DonorContext db)
        {
            if (!DonorId.HasValue) throw new Exception($"{nameof(DonorId)} is empty");

            var query = db.DonorCharities
                .Include(x => x.Charity)
                .Include(x => x.DonorCharityProportions)
                .Include(x => x.Donor)
                .Where(x => !x.IsDeleted)
                .Where(x => x.DonorId == DonorId.Value)
                .OrderBy(x => x.Charity.CharityName);

            var items = await query
                .Skip(Offset)
                .Take(Count)
                .ToListAsync();

            Total = await query.CountAsync();
            Items = items
                .Select(CharityModel.FromEntity)
                .ToList();
        }
    }
}

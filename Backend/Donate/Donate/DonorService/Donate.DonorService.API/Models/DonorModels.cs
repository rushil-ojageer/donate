using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Donate.DonorService.Data;
using Donate.Shared.API.Models;
using Donate.Shared.Data.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Donate.DonorService.API.Models
{
    public class DonorModels : BaseModel<DonorContext>
    {
        public List<DonorModel> Items { get; set; }

        public int Offset { get; set; }

        public int Count { get; set; }

        public int Total { get; set; }

        public DonorModels(int offset, int count)
        {
            Offset = offset;
            Count = count;
        }

        public async Task PopulateAll(DonorContext db)
        {
            var items = await db.Donors
                .FilterDeletedItems()
                .OrderBy(x => x.FirstName)
                .ThenByDescending(x => x.LastName)
                .Skip(Offset)
                .Take(Count)
                .ToListAsync();

            Total = await db.Donors.CountAsync();
            Items = items
                .Select(DonorModel.FromEntity)
                .ToList();
        }
    }
}

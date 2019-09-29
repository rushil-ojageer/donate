using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Donate.CharityService.API.Data;
using Donate.Shared.Data.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Donate.CharityService.API.Models
{
    public class CharityModels
    {
        public List<CharityModel> Items { get; set; }

        public int Offset { get; set; }

        public int Count { get; set; }

        public int Total { get; set; }

        public CharityModels()
        {
            Items = new List<CharityModel>();
            Offset = 0;
            Count = 20;
        }

        public CharityModels(int offset, int count)
        {
            Offset = offset;
            Count = count;
        }

        public async Task PopulateAll(CharityContext db)
        {
            var query = db.Charities
                .FilterDeletedItems()
                .OrderBy(x => x.CharityName);

            var items = await query
                .Skip(Offset)
                .Take(Count)
                .ToListAsync();

            Total = await query.CountAsync();
            Items = items
                .Select(CharityModel.FromEntity)
                .ToList();
        }

        public async Task Search(CharityContext db, string search)
        {
            if (string.IsNullOrEmpty(search))
            {
                await PopulateAll(db);
                return;
            }

            var query = db.Charities
                .FilterDeletedItems()
                .Where(x => x.CharityName.StartsWith(search));

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

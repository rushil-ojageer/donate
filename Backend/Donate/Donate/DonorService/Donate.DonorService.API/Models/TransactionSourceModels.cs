using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Donate.DonorService.Data;
using Donate.Shared.API.Models;
using Donate.Shared.Data.Extensions;
using Donate.Shared.Eventing;
using Donate.Shared.Eventing.IntegrationEvents;
using Donate.Shared.IntegrationQueue;
using Donate.Shared.IntegrationQueue.Models;
using Microsoft.EntityFrameworkCore;

namespace Donate.DonorService.API.Models
{
    public class TransactionSourceModels : BaseModel<DonorContext>
    {
        private readonly long _donorId;

        public List<TransactionSourceModel> Items { get; set; }

        public int Offset { get; set; }

        public int Count { get; set; }

        public int Total { get; set; }

        public TransactionSourceModels(long donorId, int offset, int count)
        {
            _donorId = donorId;
            Items = new List<TransactionSourceModel>();
            Offset = offset;
            Count = count;
        }

        public async Task PopulateAll(DonorContext db)
        {
            var query = db.TransactionSources
                .FilterDeletedItems()
                .Where(x => x.DonorId == _donorId)
                .OrderBy(x => x.FinancialInstitution)
                .ThenBy(x => x.Type);
                   
            var items = await query
                .Skip(Offset)
                .Take(Count)
                .ToListAsync();

            Total = await query.CountAsync();
            Items = items.Select(TransactionSourceModel.FromEntity).ToList();
        }
    }
}

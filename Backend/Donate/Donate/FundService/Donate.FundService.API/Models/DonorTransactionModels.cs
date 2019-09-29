using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Donate.FundService.Data;
using Microsoft.EntityFrameworkCore;

namespace Donate.FundService.API.Models
{
    public class DonorTransactionModels
    {
        private readonly Guid _donorTransactionSource;

        public List<DonorTransactionModel> Items { get; set; }

        public int Offset { get; set; }

        public int Count { get; set; }

        public int Total { get; set; }

        public DonorTransactionModels(Guid donorTransactionSource)
        {
            _donorTransactionSource = donorTransactionSource;
            Items = new List<DonorTransactionModel>();
            Offset = 0;
            Count = 20;
        }

        public async Task PopulateAll(FundContext db)
        {
            var query =  db.Transactions
                .Where(x => x.DonorTransactionSource.TransactionSourceIdentifier == _donorTransactionSource)
                .OrderByDescending(x => x.TransactionDateTimeUtc);

            var items = await query
                .Skip(Offset)
                .Take(Count)
                .ToListAsync();

            Total = await query.CountAsync();
            Items = items
                .Select(DonorTransactionModel.FromEntity)
                .ToList();
        }
    }
}
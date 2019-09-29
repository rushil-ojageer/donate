using System.Linq;
using System.Threading.Tasks;
using Donate.FundService.Data;
using Donate.FundService.Data.Entities;
using Donate.Shared.Eventing;
using Donate.Shared.Eventing.IntegrationEvents;
using Donate.Shared.QueueListener.EventHandler;
using Microsoft.EntityFrameworkCore;

namespace Donate.FundService.IntegrationWorker.EventHandlers
{
    public class AddDonorTransactionSourceEventHandler : BaseEventHandler<DonorTransactionSourceEvent>
    {
        private readonly FundContext _context;

        public AddDonorTransactionSourceEventHandler(FundContext context)
        {
            _context = context;
        }

        public override string GetService()
        {
            return ServiceNames.DonorService.ToString();
        }

        public override string GetEvent()
        {
            return EventNames.AddDonorTransactionSource.ToString();
        }

        protected override async Task ProcessEvent(DonorTransactionSourceEvent @event)
        {
            var existing = _context
                .DonorTransactionSources
                .SingleOrDefault(x => x.TransactionSourceIdentifier == @event.TransactionSourceIdentifier);

            if (existing != null)
                return;

            var donorTransactionSource = new DonorTransactionSource
            {
                TransactionSourceIdentifier = @event.TransactionSourceIdentifier,
                FinancialInstitution = @event.FinancialInstitution,
                Identifier = @event.Identifier
            };

            _context.DonorTransactionSources.Add(donorTransactionSource);
            await _context.SaveChangesAsync();
        }
    }
}

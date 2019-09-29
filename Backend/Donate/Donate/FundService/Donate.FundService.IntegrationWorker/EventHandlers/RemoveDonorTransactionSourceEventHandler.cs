using System.Linq;
using System.Threading.Tasks;
using Donate.FundService.Data;
using Donate.Shared.Eventing;
using Donate.Shared.Eventing.IntegrationEvents;
using Donate.Shared.QueueListener.EventHandler;
using Microsoft.EntityFrameworkCore;

namespace Donate.FundService.IntegrationWorker.EventHandlers
{
    public class RemoveDonorTransactionSourceEventHandler : BaseEventHandler<DonorTransactionSourceEvent>
    {
        private readonly FundContext _context;

        public RemoveDonorTransactionSourceEventHandler(FundContext context)
        {
            _context = context;
        }

        public override string GetService()
        {
            return ServiceNames.DonorService.ToString();
        }

        public override string GetEvent()
        {
            return EventNames.RemoveDonorTransactionSource.ToString();
        }

        protected override async Task ProcessEvent(DonorTransactionSourceEvent @event)
        {
            var existing = _context
                .DonorTransactionSources
                .SingleOrDefault(x => x.TransactionSourceIdentifier == @event.TransactionSourceIdentifier);

            if (existing == null)
            {
                return;
            }

            existing.IsDeleted = true;
            await _context.SaveChangesAsync();
        }
    }
}
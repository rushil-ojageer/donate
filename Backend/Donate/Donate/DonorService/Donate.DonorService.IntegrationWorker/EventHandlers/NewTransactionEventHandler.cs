using System;
using System.Linq;
using System.Threading.Tasks;
using Donate.DonorService.Data;
using Donate.DonorService.Data.Entities;
using Donate.Shared.Data.Extensions;
using Donate.Shared.Eventing;
using Donate.Shared.Eventing.IntegrationEvents;
using Donate.Shared.QueueListener.EventHandler;
using Microsoft.EntityFrameworkCore;

namespace Donate.DonorService.IntegrationWorker.EventHandlers
{
    public class NewTransactionEventHandler : BaseEventHandler<TransactionIntegrationEvent> 
    {
        private readonly DonorContext _donorContext;

        public NewTransactionEventHandler(DonorContext donorContext)
        {
            _donorContext = donorContext;
        }

        public override string GetService()
        {
            return ServiceNames.TransactionProcessor.ToString();
        }

        public override string GetEvent()
        {
            return EventNames.NewTransaction.ToString();
        }

        protected override async Task ProcessEvent(TransactionIntegrationEvent @event)
        {
            var transactionSource = GetTransactionSource(@event);

            if (transactionSource == null)
                return;

            AddDonation(transactionSource, @event);

            await _donorContext.SaveChangesAsync();
        }

        private TransactionSource GetTransactionSource(TransactionIntegrationEvent @event)
        {
            return _donorContext.TransactionSources
                .FilterDeletedItems()
                .SingleOrDefault(x => x.TransactionSourceIdentifier == @event.DonorTransactionSourceIdentifier);
        }

        private void AddDonation(TransactionSource transactionSource, TransactionIntegrationEvent @event)
        {
            var charities = _donorContext.DonorCharities
                .FilterDeletedItems()
                .Include(x => x.DonorCharityProportions)
                .Include(x => x.Donor)
                .Where(x => x.DonorId == transactionSource.DonorId)
                .ToList();

            if (!charities.Any())
                return;

            foreach (var charity in charities)
            {
                var donorCharityProportion = charity
                    .DonorCharityProportions
                    .LastOrDefault(x => @event.TransactionDateTimeUtc >= x.ValidFromUtc && @event.TransactionDateTimeUtc <= x.ValidToUtc);

                if (donorCharityProportion == null)
                    continue;

                var donationPercentage = donorCharityProportion.DonationPercentage;

                var donation = new Donation
                {
                    Amount = (@event.Amount * (charity.Donor.TransactionDonationPercentage / (decimal) 100.0)) * (donationPercentage / (decimal) 100.0),
                    Currency = @event.Currency,
                    DonorCharityProportionId = donorCharityProportion.Id,
                    TransactionIdentifier = @event.TransactionIdentifier,
                    DonationDateTimeUtc = DateTime.UtcNow,
                    MerchantName = @event.MerchantName,
                    TransactionAmount = @event.Amount,
                    TransactionDateTimeUtc = @event.TransactionDateTimeUtc,
                    TransactionDonationPercentage = charity.Donor.TransactionDonationPercentage
                };

                _donorContext.Donations.Add(donation);
            }
        }
    }
}

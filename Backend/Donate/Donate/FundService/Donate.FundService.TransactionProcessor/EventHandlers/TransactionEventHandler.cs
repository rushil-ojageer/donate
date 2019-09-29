using System;
using System.Linq;
using System.Threading.Tasks;
using Donate.FundService.Data;
using Donate.FundService.Data.Entities;
using Donate.Shared.Eventing;
using Donate.Shared.Eventing.IntegrationEvents;
using Donate.Shared.Eventing.TransactionEvents;
using Donate.Shared.IntegrationQueue;
using Donate.Shared.IntegrationQueue.Models;
using Donate.Shared.QueueListener.EventHandler;
using Donate.Shared.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Donate.FundService.TransactionProcessor.EventHandlers
{
    public class TransactionEventHandler : BaseEventHandler<TransactionEvent>
    {
        private readonly FundContext _fundContext;
        private readonly IIntegrationEventQueue _queue;

        public TransactionEventHandler(FundContext fundContext, IIntegrationEventQueue queue)
        {
            _fundContext = fundContext;
            _queue = queue;
        }

        public override string GetService()
        {
            return ServiceNames.TransactionFeed.ToString();
        }

        public override string GetEvent()
        {
            return EventNames.NewTransaction.ToString();
        }

        protected override async Task ProcessEvent(TransactionEvent @event)
        {
            if (HasProcessedTransaction(@event))
                return;

            var donorTransactionSource = GetDonorTransactionSource(@event);
            if (donorTransactionSource == null)
                return;

            var merchant = GetOrCreateMerchant(@event);

            var transaction = new Transaction
            {
                Amount = @event.Amount,
                Currency = @event.Currency,
                ExternalTransactionIdentifier = @event.TransactionIdentifier,
                ReceivedDateTimeUtc = DateTime.UtcNow,
                TransactionDateTimeUtc = @event.TransactionDateTimeUtc,
                TransactionIdentifier = Guid.NewGuid(),
                Merchant = merchant,
                MerchantId = merchant.Id,
                DonorTransactionSource = donorTransactionSource,
                DonorTransactionSourceId = donorTransactionSource.Id
            };

            _fundContext.Transactions.Add(transaction);
            await _fundContext.SaveChangesAsync();

            var transactionIntegrationEvent = ToTransactionIntegrationEvent(transaction);
            var integrationEvent = new IntegrationEvent<TransactionIntegrationEvent>(ServiceNames.TransactionProcessor.ToString(), EventNames.NewTransaction.ToString(), transactionIntegrationEvent);
            _queue.Post(integrationEvent);
        }

        private bool HasProcessedTransaction(TransactionEvent @event)
        {
            return _fundContext.Transactions.Any(x =>
                x.ExternalTransactionIdentifier.Equals(@event.TransactionIdentifier,
                    StringComparison.InvariantCultureIgnoreCase));
        }
        private Merchant GetOrCreateMerchant(TransactionEvent @event)
        {
            var merchant = _fundContext
                .Merchants
                .SingleOrDefault(x => x.ExternalMerchantIdentifier.Equals(@event.MerchantIdentifier, StringComparison.InvariantCultureIgnoreCase));

            if (merchant != null)
            {
                return merchant;
            }

            merchant = new Merchant
            {
                ExternalMerchantIdentifier = @event.MerchantIdentifier, 
                MerchantName = @event.MerchantName
            };

            _fundContext.Merchants.Add(merchant);

            return merchant;
        }

        private DonorTransactionSource GetDonorTransactionSource(TransactionEvent @event)
        {
            var donorTransactionSource = _fundContext
                .DonorTransactionSources
                .SingleOrDefault(x => x.Identifier.Equals(@event.TransactionSourceIdentifier, StringComparison.InvariantCultureIgnoreCase));

            return donorTransactionSource;
        }

        public TransactionIntegrationEvent ToTransactionIntegrationEvent(Transaction transaction)
        {
            var integrationEvent = new TransactionIntegrationEvent();
            integrationEvent.TransactionIdentifier = transaction.TransactionIdentifier;
            integrationEvent.DonorTransactionSourceIdentifier = transaction.DonorTransactionSource.TransactionSourceIdentifier;
            integrationEvent.Amount = transaction.Amount;
            integrationEvent.Currency = transaction.Currency;
            integrationEvent.ReceivedAtDateTimeUtc = transaction.ReceivedDateTimeUtc;
            integrationEvent.TransactionDateTimeUtc = transaction.TransactionDateTimeUtc;
            integrationEvent.MerchantName = transaction.Merchant.MerchantName;
            return integrationEvent;
        }
    }
}

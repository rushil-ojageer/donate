using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Donate.DonorService.Data;
using Donate.DonorService.Data.Entities;
using Donate.Shared.API.Models;
using Donate.Shared.Data.Extensions;
using Donate.Shared.Eventing;
using Donate.Shared.Eventing.IntegrationEvents;
using Donate.Shared.IntegrationQueue;
using Donate.Shared.IntegrationQueue.Models;
using Microsoft.EntityFrameworkCore;

namespace Donate.DonorService.API.Models
{
    public class TransactionSourceModel : BaseModel<DonorContext>
    {
        public TransactionSourceModel()
        {

        }

        public TransactionSourceModel(long id)
        {
            Id = id;
        }

        public long? Id { get; set; }

        public Guid? TransactionSourceIdentifier { get; set; }

        [Required]
        [StringLength(1000)]
        public string FinancialInstitution { get; set; }

        [Required]
        public string Type { get; set; }

        [Required]
        public string Identifier { get; set; }

        [Required]
        public long DonorId { get; set; }

        public async Task PopulateById(DonorContext db)
        {
            if (!Id.HasValue) throw new Exception("Please ensure that Id field has been populated before retrieval.");
            var transactionSource = await db.TransactionSources.FilterDeletedItems().SingleOrDefaultAsync(x => x.Id == Id);

            if (transactionSource == null)
                throw new ArgumentException($"Transaction Source with ID '{Id}' does not exist.", nameof(Id));

            MapFromEntity(transactionSource);
        }

        public async Task Create(DonorContext db, IIntegrationEventQueue queue, bool commitTransaction = true)
        {
            if (!IsValidated) throw new Exception("Please validate model before creation.");

            var donor = await db.Donors.FilterDeletedItems().SingleOrDefaultAsync(x => x.Id == DonorId);

            if (donor == null)
                throw new ArgumentException($"Donor with ID '{DonorId}' does not exist.", nameof(DonorId));

            var transactionSource = new TransactionSource();
            MapToEntity(transactionSource);
            db.TransactionSources.Add(transactionSource);
            MapFromEntity(transactionSource);

            if (commitTransaction)
            {
                await db.SaveChangesAsync();
                Id = transactionSource.Id;
                var integrationEvent = new IntegrationEvent<DonorTransactionSourceEvent>(ServiceNames.DonorService.ToString(), EventNames.AddDonorTransactionSource.ToString(), ToDonorTransactionSourceEvent());
                queue.Post(integrationEvent);
            }
        }

        public async Task Remove(DonorContext db, IIntegrationEventQueue queue)
        {
            if (!Id.HasValue) throw new Exception("Please ensure that Id field has been populated before removing.");
            var transactionSource = await db.TransactionSources.FilterDeletedItems().SingleOrDefaultAsync(x => x.Id == Id);

            if (transactionSource == null)
                throw new ArgumentException($"Transaction Source with ID '{Id}' does not exist.", nameof(Id));

            transactionSource.IsDeleted = true;
            MapFromEntity(transactionSource);
            await db.SaveChangesAsync();

            var integrationEvent = new IntegrationEvent<DonorTransactionSourceEvent>(ServiceNames.DonorService.ToString(), EventNames.RemoveDonorTransactionSource.ToString(), ToDonorTransactionSourceEvent());
            queue.Post(integrationEvent);
        }

        private void MapToEntity(TransactionSource transactionSource)
        {
            transactionSource.DonorId = DonorId;
            transactionSource.FinancialInstitution = FinancialInstitution;
            transactionSource.Identifier = Identifier;
            transactionSource.Type = Enum.Parse<TransactionSourceType>(Type, true);
            transactionSource.TransactionSourceIdentifier = TransactionSourceIdentifier ?? Guid.NewGuid();
        }

        public void MapFromEntity(TransactionSource transactionSource)
        {
            Id = transactionSource.Id;
            DonorId = transactionSource.DonorId;
            FinancialInstitution = transactionSource.FinancialInstitution;
            Identifier = transactionSource.Identifier;
            Type = transactionSource.Type.ToString();
            TransactionSourceIdentifier = transactionSource.TransactionSourceIdentifier;
        }

        public static TransactionSourceModel FromEntity(TransactionSource transactionSource)
        {
            var transactionSourceModel = new TransactionSourceModel(transactionSource.Id);
            transactionSourceModel.MapFromEntity(transactionSource);
            return transactionSourceModel;
        }

        public DonorTransactionSourceEvent ToDonorTransactionSourceEvent()
        {
            if (!TransactionSourceIdentifier.HasValue)
                throw new Exception($"Transaction Source does not contain a value for {nameof(TransactionSourceIdentifier)}");

            var donorTransactionSourceEvent = new DonorTransactionSourceEvent
            {
                DonorId = DonorId,
                FinancialInstitution = FinancialInstitution,
                Identifier = Identifier,
                Type = Type,
                TransactionSourceIdentifier = TransactionSourceIdentifier.Value
            };
            return donorTransactionSourceEvent;
        }
    }
}

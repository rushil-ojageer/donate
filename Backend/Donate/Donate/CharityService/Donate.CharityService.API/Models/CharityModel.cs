using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Donate.CharityService.API.Data;
using Donate.CharityService.API.Data.Entities;
using Donate.Shared.API.Models;
using Donate.Shared.API.Request;
using Donate.Shared.Data.Extensions;
using Donate.Shared.Eventing;
using Donate.Shared.Eventing.IntegrationEvents;
using Donate.Shared.IntegrationQueue;
using Donate.Shared.IntegrationQueue.Models;
using Microsoft.EntityFrameworkCore;

namespace Donate.CharityService.API.Models
{
    public class CharityModel : BaseModel<CharityContext>
    {
        public CharityModel()
        {
        }

        public CharityModel(long id) : this()
        {
            Id = id;
        }

        public long? Id { get; set; }

        public Guid? CharityIdentifier { get; set; }

        [Required]
        [StringLength(500)]
        public string CharityName { get; set; }

        [Required]
        [StringLength(500)]
        public string ContactPerson { get; set; }

        [StringLength(20)]
        public string ContactNumber { get; set; }

        [Required]
        [StringLength(250)]
        public string EmailAddress { get; set; }

        public DateTime UpdatedAt { get; set; }

        public override async Task<ValidationResults> Validate(CharityContext db)
        {
            var validationResults = await base.Validate(db);

            if (CharityIdentifier != null && !Id.HasValue)
            {
                var alreadyExists = await db.Charities.AnyAsync(x => x.CharityIdentifier == CharityIdentifier.Value);
                if (alreadyExists)
                {
                    validationResults.AddError($"Charity with identifier '{CharityIdentifier}' already exists.");
                }
            }

            return validationResults;
        }

        public async Task CreateOrUpdate(CharityContext db, IIntegrationEventQueue queue, IRequestContext requestContext)
        {
            if (!Id.HasValue)
            {
                await Create(db, queue, requestContext);
                return;
            }

            await Update(db, queue, requestContext);
        }

        public async Task Remove(CharityContext db, IIntegrationEventQueue queue)
        {
            if (!Id.HasValue) throw new Exception("Please ensure that Id field has been populated before removing.");
            var charity = await db.Charities.FilterDeletedItems().SingleOrDefaultAsync(x => x.Id == Id);

            if (charity == null)
                throw new ArgumentException($"Donor with ID '{Id}' does not exist.", nameof(Id));

            charity.IsDeleted = true;
            MapFromEntity(charity);
            await db.SaveChangesAsync();

            var integrationEvent = new IntegrationEvent<CharityEvent>(ServiceNames.CharityService.ToString(), EventNames.RemoveCharity.ToString(), ToCharityEvent());
            queue.Post(integrationEvent);
        }

        public async Task Update(CharityContext db, IIntegrationEventQueue queue, IRequestContext requestContext)
        {
            if (!IsValidated) throw new Exception("Please validate model before creation.");

            var charity = await db.Charities.FilterDeletedItems().SingleOrDefaultAsync(x => x.Id == Id);

            if (charity == null)
                throw new ArgumentException($"Donor with ID '{Id}' does not exist.", nameof(Id));

            MapToEntity(charity, requestContext);
            await db.SaveChangesAsync();
            MapFromEntity(charity);

            var integrationEvent = new IntegrationEvent<CharityEvent>(ServiceNames.CharityService.ToString(), EventNames.UpdateCharity.ToString(), ToCharityEvent());
            queue.Post(integrationEvent);
        }

        public async Task Create(CharityContext db, IIntegrationEventQueue queue, IRequestContext requestContext)
        {
            if (!IsValidated) throw new Exception("Please validate model before creation.");

            var donor = new Charity();
            MapToEntity(donor, requestContext);
            db.Charities.Add(donor);
            await db.SaveChangesAsync();
            Id = donor.Id;

            var integrationEvent = new IntegrationEvent<CharityEvent>(ServiceNames.CharityService.ToString(), EventNames.AddCharity.ToString(), ToCharityEvent());
            queue.Post(integrationEvent);
        }

        private void MapToEntity(Charity charity, IRequestContext requestContext)
        {
            if (CharityIdentifier == null || CharityIdentifier == Guid.Empty)
            {
                CharityIdentifier = Guid.NewGuid();
            }

            charity.CharityIdentifier = CharityIdentifier ?? Guid.NewGuid();
            charity.CharityName = CharityName;
            charity.ContactPerson = ContactPerson;
            charity.ContactNumber = ContactNumber;
            charity.EmailAddress = EmailAddress;
            charity.UpdatedBy = requestContext.User;
            charity.UpdatedAt = DateTime.UtcNow;
        }

        private void MapFromEntity(Charity charity)
        {
            Id = charity.Id;
            CharityIdentifier = charity.CharityIdentifier;
            CharityName = charity.CharityName;
            ContactPerson = charity.ContactPerson;
            ContactNumber = charity.ContactNumber;
            EmailAddress = charity.EmailAddress;
            UpdatedAt = charity.UpdatedAt;
        }

        private CharityEvent ToCharityEvent()
        {
            if (!CharityIdentifier.HasValue)
                throw new Exception($"Charity does not contain a value for {nameof(CharityIdentifier)}");

            var charityEvent = new CharityEvent
            {
                CharityIdentifier = CharityIdentifier.Value,
                CharityName = CharityName,
                ContactNumber = ContactNumber,
                ContactPerson = ContactPerson,
                EmailAddress = EmailAddress
            };
            return charityEvent;
        }

        public static CharityModel FromEntity(Charity charity)
        {
            var charityModel = new CharityModel(charity.Id);
            charityModel.MapFromEntity(charity);
            return charityModel;
        }
    }
}

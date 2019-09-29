using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Donate.DonorService.Data;
using Donate.DonorService.Data.Entities;
using Donate.Shared.API.Models;
using Donate.Shared.API.Request;
using Donate.Shared.Data.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Donate.DonorService.API.Models
{
    public class DonorModel : BaseModel<DonorContext>
    {
        public DonorModel()
        {
            
        }

        public DonorModel(long id)
        {
            Id = id;
        }

        public long? Id { get; set; }

        [Required]
        [StringLength(100)]
        public string FirstName { get; set; }
        
        [Required]
        [StringLength(100)]
        public string LastName { get; set; }
        
        [Required]
        public string IdentityType { get; set; }

        [Required]
        [StringLength(20)]
        public string IdentityNumber { get; set; }

        [Required]
        [StringLength(20)]
        public string ContactNumber { get; set; }
        
        [Required]
        [StringLength(250)]
        public string EmailAddress { get; set; }

        [Required]
        public decimal TransactionDonationPercentage { get; set; }

        [Required]
        public decimal DonationCap { get; set; }

        public DateTime UpdatedAt { get; set; }
        
        public async Task PopulateById(DonorContext db)
        {
            if (!Id.HasValue) throw new Exception("Please ensure that Id field has been populated before retrieval.");
            var donor = await db.Donors.FilterDeletedItems().SingleOrDefaultAsync(x => x.Id == Id);

            if (donor == null)
                throw new ArgumentException($"Donor with ID '{Id}' does not exist.", nameof(Id));

            MapFromEntity(donor);
        }

        public async Task CreateOrUpdate(DonorContext db, IRequestContext requestContext)
        {
            if (Id == null)
            {
                await Create(db, requestContext);
                return;
            }

            await Update(db, requestContext);
        }

        public async Task Remove(DonorContext db)
        {
            if (!Id.HasValue) throw new Exception("Please ensure that Id field has been populated before removing.");
            var donor = await db.Donors.FilterDeletedItems().SingleOrDefaultAsync(x => x.Id == Id);

            if (donor == null)
                throw new ArgumentException($"Donor with ID '{Id}' does not exist.", nameof(Id));

            donor.IsDeleted = true;
            await db.SaveChangesAsync();
        }

        public async Task Update(DonorContext db, IRequestContext requestContext)
        {
            if(!IsValidated) throw new Exception("Please validate model before creation.");

            var donor = await db.Donors.FilterDeletedItems().SingleOrDefaultAsync(x => x.Id == Id);

            if (donor == null)
                throw new ArgumentException($"Donor with ID '{Id}' does not exist.", nameof(Id));

            MapToEntity(donor, requestContext);
            await db.SaveChangesAsync();
            MapFromEntity(donor);
        }

        public async Task Create(DonorContext db, IRequestContext requestContext)
        {
            if (!IsValidated) throw new Exception("Please validate model before creation.");

            var donor = new Donor();
            MapToEntity(donor, requestContext);
            db.Donors.Add(donor);
            await db.SaveChangesAsync();
            Id = donor.Id;
        }

        private void MapToEntity(Donor donor, IRequestContext requestContext)
        {
            donor.FirstName = FirstName;
            donor.LastName = LastName;
            donor.ContactNumber = ContactNumber;
            donor.EmailAddress = EmailAddress;
            donor.IdentityNumber = IdentityNumber;
            donor.IdentityType = (IdentityType) Enum.Parse(typeof(IdentityType), IdentityType);
            donor.UpdatedBy = requestContext.User;
            donor.UpdatedAt = DateTime.UtcNow;
            donor.TransactionDonationPercentage = TransactionDonationPercentage;
            donor.DonationCap = DonationCap;
        }

        public void MapFromEntity(Donor donor)
        {
            Id = donor.Id;
            FirstName = donor.FirstName;
            LastName = donor.LastName;
            ContactNumber = donor.ContactNumber;
            EmailAddress = donor.EmailAddress;
            IdentityNumber = donor.IdentityNumber;
            IdentityType = donor.IdentityType.ToString();
            UpdatedAt = donor.UpdatedAt;
            TransactionDonationPercentage = donor.TransactionDonationPercentage;
            DonationCap = donor.DonationCap;
        }

        public static DonorModel FromEntity(Donor donor)
        {
            var donorModel = new DonorModel(donor.Id);
            donorModel.MapFromEntity(donor);
            return donorModel;
        }
    }
}
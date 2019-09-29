using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Donate.DonorService.Data;
using Donate.DonorService.Data.Entities;
using Donate.Shared.API.Models;
using Donate.Shared.Data.Exceptions;
using Donate.Shared.Data.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Donate.DonorService.API.Models
{
    public class CharityModel : BaseModel<DonorContext>
    {
        [Required]
        public long? DonorId { get; set; }

        [Required]
        public Guid? CharityIdentifier { get; set; }

        [Required]
        public decimal DonationPercentage { get; set; }

        public string CharityName { get; set; }
        
        public async Task<DonorCharity> CreateOrUpdate(DonorContext db, bool commitTransaction = true)
        {
            if (!IsValidated) throw new Exception("Please validate model before creation or modification.");

            var charity = await GetOrAddCharity(db);
            var donorCharity = await GetOrAddDonorCharity(db, charity.Id);
            UpdateDonorCharityProportion(donorCharity);

            if (commitTransaction)
            {
                await db.SaveChangesAsync();
            }

            MapFromEntity(donorCharity);

            return donorCharity;
        }

        public async Task Remove(DonorContext db)
        {
            var donorCharity = await GetDonorCharity(db);
            
            if (donorCharity == null)
                throw new DbRecordNotFoundException(nameof(db.DonorCharities), $"(DonorId={DonorId};CharityIdentifier={CharityIdentifier})");

            donorCharity.IsDeleted = true;

            await db.SaveChangesAsync();
        }

        private void UpdateDonorCharityProportion(DonorCharity donorCharity)
        {
            var utcNow = DateTime.UtcNow;
            var existingDonorCharityProportion = donorCharity.DonorCharityProportions.LastOrDefault();

            if (existingDonorCharityProportion != null)
            {
                existingDonorCharityProportion.ValidToUtc = utcNow;
            }

            var donorCharityProportion = new DonorCharityProportion
            {
                DonationPercentage = DonationPercentage, 
                ValidFromUtc = utcNow, 
                ValidToUtc = DateTime.MaxValue
            };

            donorCharity.DonorCharityProportions.Add(donorCharityProportion);
        }

        private async Task<DonorCharity> GetDonorCharity(DonorContext db)
        {
            return await db.DonorCharities
                .FilterDeletedItems()
                .Include(x => x.DonorCharityProportions)
                .SingleOrDefaultAsync(x => x.DonorId == DonorId && x.Charity.Identifier == CharityIdentifier);
        }

        private async Task<DonorCharity> GetOrAddDonorCharity(DonorContext db, long charityId)
        {
            if (!DonorId.HasValue)
                throw new Exception($"'{nameof(DonorId)}' is empty");

            var donorCharity = await GetDonorCharity(db);

            if (donorCharity != null) 
                return donorCharity;

            donorCharity = new DonorCharity
            {
                DonorId = DonorId.Value,
                CharityId = charityId,
                DonorCharityProportions = new List<DonorCharityProportion>()
            };
            db.DonorCharities.Add(donorCharity);
            return donorCharity;
        }

        private async Task<Charity> GetCharity(DonorContext db)
        {
            return await db.Charities
                .FilterDeletedItems()
                .SingleOrDefaultAsync(x => x.Identifier == CharityIdentifier);
        }

        private async Task<Charity> GetOrAddCharity(DonorContext db, bool commitTransaction = true)
        {
            if (!CharityIdentifier.HasValue)
                throw new Exception($"'{nameof(CharityIdentifier)}' is empty.");

            var charity = await GetCharity(db);

            if (charity != null)
                return charity;

            charity = new Charity
            {
                Identifier = CharityIdentifier.Value,
                CharityName = CharityName
            };
            db.Charities.Add(charity);
    
            if(commitTransaction)
                await db.SaveChangesAsync();
            
            return charity;
        }

        public void MapFromEntity(DonorCharity donorCharity)
        {
            var utcNow = DateTime.UtcNow;

            DonorId = donorCharity.DonorId;
            CharityIdentifier = donorCharity.Charity.Identifier;
            CharityName = donorCharity.Charity.CharityName;
            DonationPercentage = donorCharity.DonorCharityProportions
                .LastOrDefault(x => utcNow >= x.ValidFromUtc && utcNow <= x.ValidToUtc)?
                .DonationPercentage ?? 0;
        }

        public static CharityModel FromEntity(DonorCharity donor)
        {
            var charityModel = new CharityModel();
            charityModel.MapFromEntity(donor);
            return charityModel;
        }
    }
}
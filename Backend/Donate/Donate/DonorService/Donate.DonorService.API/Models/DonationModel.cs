using System;
using Donate.DonorService.Data.Entities;

namespace Donate.DonorService.API.Models
{
    public class DonationModel
    {
        public long Id { get; set; }
        public long DonorId { get; set; }
        public string Charity { get; set; }
        public string Donor { get; set; }
        public string Merchant { get; set; }
        public decimal Amount { get; set; }
        public decimal TransactionAmount { get; set; }
        public string Currency { get; set; }
        public DateTime DonationDateTimeUtc { get; set; }
        public DateTime TransactionDateTimeUtc { get; set; }

        public void PopulateModel(Donation model)
        {
            Id = model.Id;
            DonorId = model.DonorCharityProportion.DonorCharity.DonorId;
            Charity = model.DonorCharityProportion.DonorCharity.Charity.CharityName;
            Merchant = model.MerchantName;
            Amount = model.Amount;
            TransactionAmount = model.TransactionAmount;
            Currency = model.Currency;
            DonationDateTimeUtc = model.DonationDateTimeUtc;
            TransactionDateTimeUtc = model.TransactionDateTimeUtc;
        }

        public static DonationModel FromEntity(Donation model)
        {
            var donationModel = new DonationModel();
            donationModel.PopulateModel(model);
            return donationModel;
        }
    }
}

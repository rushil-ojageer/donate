using System;
using Transaction = Donate.FundService.Data.Entities.Transaction;

namespace Donate.FundService.API.Models
{
    public class DonorTransactionModel
    {
        public Guid TransactionSourceIdentifier { get; set; }

        public Guid TransactionIdentifier { get; set; }

        public DateTime TransactionDateTimeUtc { get; set; }

        public DateTime ReceivedDateTimeUtc { get; set; }

        public string MerchantName { get; set; }

        public string FinancialInstitution { get; set; }

        public decimal Amount { get; set; }

        public string Currency { get; set; }

        public void PopulateModel(Transaction transaction)
        {
            TransactionSourceIdentifier = transaction.DonorTransactionSource.TransactionSourceIdentifier;
            TransactionIdentifier = transaction.TransactionIdentifier;
            TransactionDateTimeUtc = transaction.TransactionDateTimeUtc;
            ReceivedDateTimeUtc = transaction.ReceivedDateTimeUtc;
            MerchantName = transaction.Merchant.MerchantName;
            FinancialInstitution = transaction.DonorTransactionSource.FinancialInstitution;
            Amount = transaction.Amount;
            Currency = transaction.Currency;
        }

        public static DonorTransactionModel FromEntity(Transaction arg)
        {
            var model = new DonorTransactionModel();
            model.PopulateModel(arg);
            return model;
        }
    }
}

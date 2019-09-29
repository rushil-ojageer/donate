using System;
using System.Threading.Tasks;
using Donate.FundService.API.Models;
using Donate.FundService.Data;
using Microsoft.AspNetCore.Mvc;

namespace Donate.FundService.API.Controllers
{
    [ApiController]
    [Route("api/Donor/TransactionSource/{donorTransactionSourceIdentifier}/Transactions")]
    public class TransactionsController : Controller
    {
        private readonly FundContext _fundContext;

        public TransactionsController(FundContext fundContext)
        {
            _fundContext = fundContext;
        }

        [HttpGet]
        public async Task<ActionResult<DonorTransactionModels>> Get(Guid donorTransactionSourceIdentifier)
        {
            var donorTransactionModels = new DonorTransactionModels(donorTransactionSourceIdentifier);
            await donorTransactionModels.PopulateAll(_fundContext);
            return donorTransactionModels;
        }
    }
}
using System.Threading.Tasks;
using Donate.DonorService.API.Models;
using Donate.DonorService.Data;
using Donate.Shared.IntegrationQueue;
using Microsoft.AspNetCore.Mvc;

namespace Donate.DonorService.API.Controllers
{
    [Route("api/Donor/{donorId}/TransactionSource")]
    [ApiController]
    public class TransactionSourceController : Controller
    {
        private readonly DonorContext _db;
        private readonly IIntegrationEventQueue _queue;

        public TransactionSourceController(DonorContext db, IIntegrationEventQueue queue)
        {
            _db = db;
            _queue = queue;
        }

        [HttpPost]
        public async Task<ActionResult<TransactionSourceModel>> Create(int donorId, [FromBody] TransactionSourceModel transactionSourceModel)
        {
            transactionSourceModel.DonorId = donorId;
            var validationResults = await transactionSourceModel.Validate(_db);

            if (!validationResults.IsValid)
            {
                return BadRequest(validationResults.ToString());
            }

            await transactionSourceModel.Create(_db, _queue);

            return Ok(transactionSourceModel);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> Delete(int donorId, long id)
        {
            var transactionSourceModel = new TransactionSourceModel { Id = id, DonorId = donorId };
            await transactionSourceModel.Remove(_db, _queue);
            return Ok();
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<TransactionSourceModel>> Get(int donorId, long id)
        {
            var transactionSourceModel = new TransactionSourceModel { Id = id, DonorId = donorId };
            await transactionSourceModel.PopulateById(_db);
            return Ok(transactionSourceModel);
        }

        [HttpGet]
        public async Task<ActionResult<TransactionSourceModels>> GetAll(int donorId, int offset, int count)
        {
            var transactionSourceModels = new TransactionSourceModels(donorId, offset, count);
            await transactionSourceModels.PopulateAll(_db);
            return Ok(transactionSourceModels);
        }
    }
}
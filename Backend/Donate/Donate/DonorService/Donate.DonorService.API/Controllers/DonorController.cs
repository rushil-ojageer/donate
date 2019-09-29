using System.Threading.Tasks;
using Donate.DonorService.API.Models;
using Donate.DonorService.Data;
using Donate.Shared.API.Request;
using Microsoft.AspNetCore.Mvc;

namespace Donate.DonorService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DonorController : ControllerBase
    {
        private readonly DonorContext _db;
        private readonly IRequestContext _requestContext;

        public DonorController(DonorContext db, IRequestContext requestContext)
        {
            _db = db;
            _requestContext = requestContext;
        }

        [HttpPost]
        public async Task<ActionResult<DonorModel>> Create([FromBody] DonorModel donorModel)
        {
            var validationResults = await donorModel.Validate(_db);

            if (!validationResults.IsValid)
            {
                return BadRequest(validationResults.ToString());
            }

            await donorModel.CreateOrUpdate(_db, _requestContext);

            return Ok(donorModel);
        }

        [HttpPut]
        public async Task<ActionResult<DonorModel>> Update([FromBody] DonorModel donorModel)
        {
            var validationResults = await donorModel.Validate(_db);

            if (!validationResults.IsValid)
            {
                return BadRequest(validationResults.ToString());
            }

            await donorModel.CreateOrUpdate(_db, _requestContext);

            return Ok(donorModel);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> Delete(long id)
        {
            var donorModel = new DonorModel { Id = id };
            await donorModel.Remove(_db);
            return Ok();
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<DonorModel>> Get(long id)
        {
            var donorModel = new DonorModel {Id = id};
            await donorModel.PopulateById(_db);
            return Ok(donorModel);
        }

        [HttpGet]
        public async Task<ActionResult<DonorModels>> GetAll(int offset, int count)
        {
            var donorModels = new DonorModels(offset, count);
            await donorModels.PopulateAll(_db);
            return Ok(donorModels);
        }
    }
}

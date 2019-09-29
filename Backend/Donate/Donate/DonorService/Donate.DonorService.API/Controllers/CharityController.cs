using System;
using System.Threading.Tasks;
using Donate.DonorService.API.Models;
using Donate.DonorService.Data;
using Microsoft.AspNetCore.Mvc;

namespace Donate.DonorService.API.Controllers
{
    [Route("api/Donor/{donorId}/Charity")]
    [ApiController]
    public class CharityController : ControllerBase
    {
        private readonly DonorContext _db;

        public CharityController(DonorContext db)
        {
            _db = db;
        }

        [HttpPost]
        [HttpPut]
        public async Task<ActionResult<DonorModel>> CreateOrUpdate(long donorId, [FromBody] CharityModel charityModel)
        {
            charityModel.DonorId = donorId;
            var validationResults = await charityModel.Validate(_db);

            if (!validationResults.IsValid)
            {
                return BadRequest(validationResults.ToString());
            }

            await charityModel.CreateOrUpdate(_db);

            return Ok(charityModel);
        }

        [HttpDelete]
        [Route("{charityIdentifier}")]
        public async Task<ActionResult> Delete(long donorId, Guid charityIdentifier)
        {
            var charityModel = new CharityModel { DonorId = donorId, CharityIdentifier = charityIdentifier };
            await charityModel.Remove(_db);
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<DonorModels>> GetAll(int donorId, int offset, int count)
        {
            var charityModels = new CharityModels(donorId, offset, count);
            await charityModels.PopulateAll(_db);
            return Ok(charityModels);
        }
    }
}
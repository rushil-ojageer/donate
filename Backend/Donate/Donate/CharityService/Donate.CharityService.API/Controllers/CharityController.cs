using System;
using System.Threading.Tasks;
using Donate.CharityService.API.Data;
using Donate.CharityService.API.Models;
using Donate.Shared.API.Request;
using Donate.Shared.IntegrationQueue;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Donate.CharityService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CharityController : ControllerBase
    {
        private readonly CharityContext _db;
        private readonly IIntegrationEventQueue _queue;
        private readonly IRequestContext _requestContext;

        public CharityController(CharityContext db, IIntegrationEventQueue queue, IRequestContext requestContext)
        {
            _db = db;
            _queue = queue;
            _requestContext = requestContext;
        }

        [HttpPost]
        public async Task<ActionResult<CharityModel>> Create([FromBody] CharityModel charityModel)
        {
            var validationResults = await charityModel.Validate(_db);

            if (!validationResults.IsValid)
            {
                return BadRequest(validationResults.ToString());
            }

            await charityModel.CreateOrUpdate(_db, _queue, _requestContext);

            return Ok(charityModel);
        }

        [HttpPut]
        public async Task<ActionResult<CharityModel>> Update([FromBody] CharityModel charityModel)
        {
            var validationResults = await charityModel.Validate(_db);

            if (!validationResults.IsValid)
            {
                return BadRequest(validationResults.ToString());
            }

            await charityModel.CreateOrUpdate(_db, _queue, _requestContext);

            return Ok(charityModel);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> Delete(long id)
        {
            var charityModel = new CharityModel {Id = id};
            await charityModel.Remove(_db, _queue);
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<CharityModels>> GetAll(int offset, int count)
        {
            var charityModels = new CharityModels(offset, count);
            await charityModels.PopulateAll(_db);
            return Ok(charityModels);
        }

        [HttpGet]
        [Route("search")]
        public async Task<ActionResult<CharityModels>> Search(string token, int offset, int count)
        {
            var charityModels = new CharityModels(offset, count);
            await charityModels.Search(_db, token);
            return Ok(charityModels);
        }
    }
}

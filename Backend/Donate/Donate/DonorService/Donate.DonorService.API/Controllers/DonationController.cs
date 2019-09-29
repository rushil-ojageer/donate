using System.Threading.Tasks;
using Donate.DonorService.API.Models;
using Donate.DonorService.Data;
using Microsoft.AspNetCore.Mvc;

namespace Donate.DonorService.API.Controllers
{
    [Route("api/Donor/{donorId}/Donations")]
    [ApiController]
    public class DonationController : ControllerBase
    {
        private readonly DonorContext _db;

        public DonationController(DonorContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<ActionResult<DonationModels>> GetAll(int donorId, int offset, int count)
        {
            var donationModels = new DonationModels(donorId, offset, count);
            await donationModels.PopulateAll(_db);
            return Ok(donationModels);
        }
    }
}

using FCF.Services;
using FCF.Entities;
using FCF.Models;
using Microsoft.AspNetCore.Mvc;

namespace FCF.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VenueController : ControllerBase
    {
        private readonly IVenueService venueService;
        public VenueController(IVenueService venueService) 
        {
            this.venueService = venueService;
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<Venue> GetVenueByIdAsync(int id)
        {
            return await venueService.GetByIdAsync(id);
        }

        [HttpGet]
        public async Task<List<Venue>> GetVenuesAsync()
        {
            return await venueService.GetAllAsync();
        }

        [HttpPost]
        public async Task<IActionResult> AddVenueAsync([FromBody] VenueDto newVenue)
        {
            var venue = await venueService.AddAsync(newVenue);
            if (venue == null)
            {
                return BadRequest(new { message = "Failed to add venue!" });
            }
            return Ok(venue);
        }


        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateVenueAsync([FromRoute]int id, [FromBody] VenueDto updatedVenue)
        {
            var venue = await venueService.UpdateAsync(id, updatedVenue);
            if (venue == null)
            {
                return BadRequest(new { message = "Failed to update venue!" });
            }
            return Ok(venue);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteVenueAsync(int id)
        {
            if (await venueService.DeleteAsync(id))
            {
                return Ok();
            }
            return BadRequest(new {message = "Failed to delete venue!"});
        }
    }
}

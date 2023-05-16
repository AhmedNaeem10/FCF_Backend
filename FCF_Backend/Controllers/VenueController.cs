using FCF.Entities;
using Microsoft.AspNetCore.Mvc;
using FCF.Services.Interfaces;
using FCF.Models.Requests.VenueDtos;
using FCF.Models.Responses.ResponseDto;
using System;

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
        public async Task<IActionResult> GetVenueByIdAsync(int id)
        {
            var venue = await venueService.GetByIdAsync(id);
            GenericResponse<Venue> response = new GenericResponse<Venue>(venue);
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllVenuesAsync()
        {
            var venues = await venueService.GetAllAsync();
            GenericResponse<List<Venue>> response = new GenericResponse<List<Venue>>(venues);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> AddVenueAsync([FromBody] VenueDto newVenue)
        {
            var venue = await venueService.AddAsync(newVenue);
            GenericResponse<Venue> response = new GenericResponse<Venue>(venue);
            return Ok(response);
        }


        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateVenueAsync([FromRoute]int id, [FromBody] VenueDto updatedVenue)
        {
            var venue = await venueService.UpdateAsync(id, updatedVenue);
            GenericResponse<Venue> response = new GenericResponse<Venue>(venue);
            return Ok(response);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteVenueAsync(int id)
        {
            var result = await venueService.DeleteAsync(id);
            GenericResponse<bool> response = new GenericResponse<bool>(result);
            return Ok(response);
        }
    }
}

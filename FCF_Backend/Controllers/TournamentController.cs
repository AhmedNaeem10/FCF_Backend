using FCF.Entities;
using FCF.Models.Requests.TournamentDtos;
using FCF.Models.Responses.ResponseDto;
using FCF.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FCF.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TournamentController : ControllerBase
    {
        private readonly ITournamentService tournamentService;

        public TournamentController(ITournamentService tournamentService)
        {
            this.tournamentService = tournamentService;
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            Tournament tournament = await tournamentService.GetAsync(id);
            GenericResponse<Tournament> response = new GenericResponse<Tournament>(tournament);
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var tournaments = await tournamentService.GetAllAsync();
            GenericResponse<List<Tournament>> response = new GenericResponse<List<Tournament>>(tournaments);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateTournamentDto tournament)
        {
            Tournament newTournament = await tournamentService.CreateAsync(tournament);
            GenericResponse<Tournament> response = new GenericResponse<Tournament>(newTournament);
            return Ok(response);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await tournamentService.DeleteAsync(id);
            GenericResponse<bool> response = new GenericResponse<bool>(true);
            return Ok(response);
        }
    }
}

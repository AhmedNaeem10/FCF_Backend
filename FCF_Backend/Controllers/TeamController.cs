using Microsoft.AspNetCore.Mvc;
using FCF.Entities;
using FCF.Services.Interfaces;
using FCF.Models.Requests.TeamDtos;
using FCF.Models.Responses.ResponseDto;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        private ITeamService _teamService;
        public TeamController(ITeamService teamService)
        {
            _teamService = teamService;
        }


        //[Authorize(Role = "admin,player,captain")]
        [HttpGet]
        public async Task<IActionResult> GetTeams()
        {
            var teams = await _teamService.GetAllTeamsAsync();
            GenericResponse<List<Team>> response = new GenericResponse<List<Team>>(teams);
            return Ok(response);
        }

        //[Authorize(Role = "admin,player,captain")]
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetTeamAsync([FromRoute] int id)
        {
            var team = await _teamService.GetByIdAsync(id);
            GenericResponse<Team> response = new GenericResponse<Team>(team);
            return Ok(response);
        }

        // [Authorize(Role = "admin")]
        [HttpPost]
        public async Task<IActionResult> AddTeamAsync(TeamDto team)
        {
            Team team_ = await _teamService.AddTeamAsync(team);
            GenericResponse<Team> response = new GenericResponse<Team>(team_);
            return Ok(response);
        }

        [HttpPut]
        [Route("RegisterTeamInTournament/{teamId:int}/{tourId:int}")]
        public async Task<IActionResult> RegisterTeamInTournament(int teamId, int tourId)
        {
            Team team = await _teamService.RegisterTeamInTournament(teamId, tourId);
            return Ok(team);
        }
    }

}

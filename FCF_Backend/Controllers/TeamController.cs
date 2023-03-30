using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FCF.Data;
using FCF.Entities;
using FCF.Helpers;
using FCF.Models;
using FCF.Services;

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
            return Ok(await _teamService.GetAllTeamsAsync());
        }

        //[Authorize(Role = "admin,player,captain")]
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetTeamAsync([FromRoute] int id)
        {
            var team = await _teamService.GetByIdAsync(id);
            if (team == null)
                return NotFound();
            return Ok(team);
        }

       // [Authorize(Role = "admin")]
        [HttpPost]
        public async Task<IActionResult> AddTeamAsync(TeamDto team)
        {
            Team team_ = await _teamService.AddTeamAsync(team);

            if (team_ != null)
            {
                return Ok(team_);
            }
            else
            {
                return BadRequest(new { message =  "Failed to add team!" });
            }
        }
    }

}

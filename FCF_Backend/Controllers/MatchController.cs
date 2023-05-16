using Microsoft.AspNetCore.Mvc;
using FCF.Services;
using FCF.Services.Interfaces;
using FCF.Models.Requests.MatchDtos;
using FCF.Entities;
using FCF.Models.Responses.ResponseDto;
using FCF.Services.Implementations;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatchController : ControllerBase
    {
        private readonly IMatchService matchService;
        public MatchController(IMatchService matchService)
        {
            this.matchService = matchService;
        }

        [HttpGet]
        public async Task<IActionResult> GetMatches()
        {
            var matches = await matchService.GetAllMatchesAsync();
            GenericResponse<List<Match>> response = new GenericResponse<List<Match>>(matches);
            return Ok(response);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetMatch(int id)
        {
            var match = await matchService.GetByIdAsync(id);
            GenericResponse<Match> response = new GenericResponse<Match>(match);
            return Ok(response);
        }

      //  [Authorize(Role="admin")]
        [HttpPost]
        public async Task<IActionResult> CreateMatchAsync([FromBody] MatchDto match)
        {
            var match_ = await matchService.CreateMatchAsync(match);
            GenericResponse<Match> response = new GenericResponse<Match>(match_);
            return Ok(response);
        }
    }
}

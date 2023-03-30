using Microsoft.AspNetCore.Mvc;
using FCF.Services;
using FCF.Models;

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
            return Ok(await matchService.GetAllMatchesAsync());
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetMatch(int id)
        {
            var match = await matchService.GetByIdAsync(id);
            if (match == null)
            {
                return NotFound();
            }
            return Ok(match);
        }

      //  [Authorize(Role="admin")]
        [HttpPost]
        public async Task<IActionResult> CreateMatchAsync([FromBody] MatchDto match)
        {
            var match_ = await matchService.CreateMatchAsync(match);
            if (match_ != null)
            {
                return Ok(match_);
            }
            else
            {
                return BadRequest(new { message = "Failed to create the match!" });
            }
        }
    }
}

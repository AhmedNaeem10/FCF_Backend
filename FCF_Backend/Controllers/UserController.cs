using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FCF.Data;
using FCF.Entities;
using FCF.Models;
using FCF.Services;
using FCF.Helpers;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }


        //[Authorize(Role="admin")]
        [HttpGet]
        public async Task<IActionResult> GetUsersAsync()
        {
            return Ok(await _userService.GetAllUsersAsync());
        }

        //[Authorize(Role="admin,player,captain")]
        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<User>> GetUserAsync([FromRoute] int id)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> AddUserAsync(userDto addUser)
        {
            var newUser = await _userService.RegisterUserAsync(addUser);
            if (newUser != null)
            {
                return Ok(newUser);
            }
            return BadRequest(new { message = "Failed to add user!" });
        }

        //[Authorize(Role="player,captain")]
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateUserAsync([FromRoute] int id, userDto updatedUser)
        {
            User user = await _userService.UpdateInfoAsync(id, updatedUser);
            if (user != null)
            {
                return Ok(user);
            }
            else
            {
                return BadRequest(new {message = "Failed to update user info!" });
            }
        }

        //[Authorize(Role="player,admin,captain")]
        [HttpPut]
        [Route("ChangePassword")]
        public async Task<IActionResult> UpdatePasswordAsync([FromBody] UpdatePasswordDto info )
        {
            User user = await _userService.UpdateUserPasswordAsync(info);
            if (user != null)
            {
                return Ok(user);
            }
            else
            {
                return BadRequest(new { message = "Failed to update user password!" });
            }
        }

        //[Authorize(Role="admin,player,captain")]
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteUserAsync([FromRoute] int id)
        {
            var deleted = await _userService.DeleteByIdAsync(id);
            if (deleted)
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        //[Authorize(Role="admin,player,captain")]
        [HttpPut]
        [Route("RegisterInTeam/{uid}/{tid}")]
        public async Task<IActionResult> RegitserUserInTeam([FromRoute] int uid, int tid)
        {
            User user = await _userService.UpdateTeamAsync(uid, tid);
            if (user != null)
            {
                return Ok(user);
            }
            return BadRequest(new { message = "Failed to register user into a team!" });
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginUserDto user)
        {
            var response = await _userService.AuthenticateAsync(user);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }
        

    }
}

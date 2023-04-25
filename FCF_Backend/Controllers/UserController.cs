using Microsoft.AspNetCore.Mvc;
using FCF.Entities;
using FCF.Services.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FCF.Models.Requests.UserDtos;
using FCF.Models.Responses.ResponseDto;

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
            var users = await _userService.GetAllUsersAsync();
            GenericResponse<List<User>> response = new GenericResponse<List<User>>(users);
            return Ok(response);
        }

        //[Authorize(Role="admin,player,captain")]
        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<User>> GetUserAsync([FromRoute] int id)
        {
            var user = await _userService.GetByIdAsync(id);
            GenericResponse<User> response = new GenericResponse<User>(user);
            if (user == null)
            {
                response.AddError("User not found!");
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> AddUserAsync(UserDto addUser)
        {
            var newUser = await _userService.RegisterUserAsync(addUser);
            GenericResponse<User> response = new GenericResponse<User>(newUser);
            if (newUser != null)
            {
                return Ok(response);
            }
            response.AddError("Failed to add user!");
            return BadRequest(response);
        }

        //[Authorize(Role="player,captain")]
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateUserAsync([FromRoute] int id, [FromBody] UserDto updatedUser)
        {
            User user = await _userService.UpdateInfoAsync(id, updatedUser);
            GenericResponse<User> response = new GenericResponse<User>(user);
            if (user != null)
            {
                return Ok(response);
            }
            response.AddError("Failed to update user info!");
            return BadRequest(response);
        }

        //[Authorize(Role="player,admin,captain")]
        [HttpPut]
        [Route("ChangePassword")]
        public async Task<IActionResult> UpdatePasswordAsync([FromBody] UpdatePasswordDto info )
        {
            User user = await _userService.UpdateUserPasswordAsync(info);
            GenericResponse<User> response = new GenericResponse<User>(user);
            if (user != null)
            {
                return Ok(response);
            }
            response.AddError("Failed to update user password!");
            return BadRequest(response);
        }

        //[Authorize(Role="admin,player,captain")]
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteUserAsync([FromRoute] int id)
        {
            var deleted = await _userService.DeleteByIdAsync(id);
            GenericResponse<User> response = new GenericResponse<User>();
            if (deleted)
            {
                return Ok(response);
            }
            response.AddError("Failed to delete user!");
            return BadRequest(response);
        }

        //[Authorize(Role="admin,player,captain")]
        [HttpPut]
        [Route("RegisterInTeam/{uid}/{tid}")]
        public async Task<IActionResult> RegitserUserInTeam([FromRoute] int uid, int tid)
        {
            User user = await _userService.UpdateTeamAsync(uid, tid);
            GenericResponse<User> response = new GenericResponse<User>(user);
            if (user != null)
            {
                return Ok(response);
            }
            response.AddError("Failed to register user into a team!");
            return BadRequest(response);
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginUserDto user)
        {
            var result = await _userService.AuthenticateAsync(user);
            GenericResponse<UserInfo> response = new GenericResponse<UserInfo>(result);
            if (result != null)
                return Ok(response);
            response.AddError("Username or password is incorrect");
            return BadRequest(response);

            
        }
        

    }
}

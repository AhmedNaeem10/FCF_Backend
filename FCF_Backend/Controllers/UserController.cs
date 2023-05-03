using Microsoft.AspNetCore.Mvc;
using FCF.Entities;
using FCF.Services.Interfaces;
using FCF.Models.Requests.UserDtos;
using FCF.Models.Responses.ResponseDto;
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


        [Authorize(Role="admin")]
        [HttpGet]
        public async Task<IActionResult> GetUsersAsync()
        {
            var users = await _userService.GetAllUsersAsync();
            GenericResponse<List<User>> response = new GenericResponse<List<User>>(users);
            return Ok(response);
        }

        [Authorize(Role = "admin,player,captain")]
        [HttpGet]
        [Route("Paginated/{pageNum:int}/{chunkSize:int}")]
        public async Task<IActionResult> GetPaginatedUsers([FromRoute] int pageNum, int chunkSize)
        {
            var users = await _userService.GetPaginatedUsersAsync(pageNum, chunkSize);
            return Ok(users);
        }

        [Authorize(Role="admin,player,captain")]
        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<User>> GetUserAsync([FromRoute] int id)
        {
            var user = await _userService.GetByIdAsync(id);
            GenericResponse<User> response = new GenericResponse<User>(user);
            return Ok(response);
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> AddUserAsync(UserDto addUser)
        {
            var newUser = await _userService.RegisterUserAsync(addUser);
            GenericResponse<User> response = new GenericResponse<User>(newUser);
            return Ok(response);
        }

        [Authorize(Role="admin,player,captain")]
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateUserAsync([FromRoute] int id, [FromBody] UserDto updatedUser)
        {
            User user = await _userService.UpdateInfoAsync(id, updatedUser);
            GenericResponse<User> response = new GenericResponse<User>(user);
            return Ok(response);
           
        }

        [Authorize(Role="player,admin,captain")]
        [HttpPut]
        [Route("ChangePassword")]
        public async Task<IActionResult> UpdatePasswordAsync([FromBody] UpdatePasswordDto info )
        {
            User user = await _userService.UpdateUserPasswordAsync(info);
            GenericResponse<User> response = new GenericResponse<User>(user);
            return Ok(response);
            
        }

        [Authorize(Role="admin,player,captain")]
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteUserAsync([FromRoute] int id)
        {
            await _userService.DeleteByIdAsync(id);
            GenericResponse<User> response = new GenericResponse<User>();
            return Ok(response);
        }

        [Authorize(Role="admin,player,captain")]
        [HttpPut]
        [Route("RegisterInTeam/{uid}/{tid}")]
        public async Task<IActionResult> RegitserUserInTeam([FromRoute] int uid, int tid)
        {
            User user = await _userService.UpdateTeamAsync(uid, tid);
            GenericResponse<User> response = new GenericResponse<User>(user);
            return Ok(response);
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginUserDto user)
        {
            var result = await _userService.AuthenticateAsync(user);
            GenericResponse<UserInfo> response = new GenericResponse<UserInfo>(result);
            return Ok(response);
        }
    }
}

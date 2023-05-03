using FCF.Entities;
using FCF.Models.Requests.UserDtos;

namespace FCF.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserInfo> AuthenticateAsync(LoginUserDto model);
        Task<User> GetByIdAsync(int id);
        Task<List<User>> GetPaginatedUsersAsync(int pageNum = 0, int chunkSize = 10);
        Task<List<User>> GetAllUsersAsync();
        Task<User> RegisterUserAsync(UserDto addUser);

        Task<User> UpdateInfoAsync(int id, UserDto updatedUser);

        Task<User> UpdateTeamAsync(int uid, int tid);

        Task<User> UpdateUserPasswordAsync(UpdatePasswordDto info);

        Task<bool> DeleteByIdAsync(int id);
    }
}

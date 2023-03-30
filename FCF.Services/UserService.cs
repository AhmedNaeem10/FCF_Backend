using FCF.Models;
using FCF.Entities;
using FCF.Data;
using Microsoft.EntityFrameworkCore;

namespace FCF.Services
{
    public interface IUserService
    {
        Task<UserInfo> AuthenticateAsync(LoginUserDto model);
        Task<User> GetByIdAsync(int id);

        Task<List<User>> GetAllUsersAsync();
        Task<User> RegisterUserAsync(userDto addUser);

        Task<User> UpdateInfoAsync(int id, userDto updatedUser);

        Task<User> UpdateTeamAsync(int uid, int tid);

        Task<User> UpdateUserPasswordAsync(UpdatePasswordDto info);

        Task<bool> DeleteByIdAsync(int id);
    }
    public class UserService : IUserService
    {
        private readonly MainDBContext dbContext;
        private readonly IJwtService _jwtService;

        public UserService(MainDBContext dbContext, IJwtService jwtService)
        {
            this.dbContext = dbContext;
            _jwtService = jwtService;
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await dbContext.Users.
                   FirstOrDefaultAsync(x => x.UserId == id);
        }

        public async Task<List<User>> GetAllUsersAsync() {
            return await dbContext.Users.ToListAsync();
        }

        public async Task<UserInfo> AuthenticateAsync(LoginUserDto model)
        {
            var user = await dbContext.Users.
                             Include(x => x.Team_).
                             SingleOrDefaultAsync(x => x.Email == model.Email && x.Password == model.Password);

            if (user == null) return null;

            var token = _jwtService.GenerateJwtToken(user);
            return new UserInfo(user, token);
        }

        public async Task<User> RegisterUserAsync(userDto addUser)
        {
            try
            {
                var newUser = new User()
                {
                    Name = addUser.Name,
                    Password = addUser.Password,
                    Email = addUser.Email,
                    Division = addUser.Division,
                    Designation = addUser.Designation,
                    Role = addUser.Role,
                };
                await dbContext.Users.AddAsync(newUser);
                await dbContext.SaveChangesAsync();
                return newUser;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<User> UpdateInfoAsync(int id, userDto updatedUser)
        {
            try
            {
                var user = await dbContext.Users.SingleOrDefaultAsync(x => x.UserId == id);
                if (user != null)
                {
                    user.Name = updatedUser.Name;
                    user.Password = updatedUser.Password;
                    user.Designation = updatedUser.Designation;
                    user.Email = updatedUser.Email;
                    user.Division = updatedUser.Division;
                    await dbContext.SaveChangesAsync();
                    return user;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<User> UpdateTeamAsync(int uid, int tid)
        {
            try
            {
                var user = await dbContext.Users.SingleOrDefaultAsync(x => x.UserId == uid);
                if (user != null)
                {
                    user.TeamId = tid;
                    await dbContext.SaveChangesAsync();
                    return user;
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<User> UpdateUserPasswordAsync(UpdatePasswordDto info)
        {
            try
            {
                var user = await dbContext.Users.SingleOrDefaultAsync(x => x.UserId == info.uid);
                if (user != null)
                {
                    user.Password = info.password;
                    await dbContext.SaveChangesAsync();
                    return user;
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<bool> DeleteByIdAsync(int id)
        {
            try
            {
                var user = await dbContext.Users.FindAsync(id);
                if (user == null)
                    return false;
                dbContext.Remove(user);
                await dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}

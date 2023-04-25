using FCF.Entities;
using FCF.Data;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using FCF.Services.Interfaces;
using FCF.Models.Requests.UserDtos;

namespace FCF.Services.Services
{
    public class UserService : IUserService
    {
        private readonly MainDBContext dbContext;
        private readonly IJwtService _jwtService;
        private readonly IMapper _mapper;

        public UserService(IMapper mapper, MainDBContext dbContext, IJwtService jwtService)
        {
            _mapper = mapper;
            this.dbContext = dbContext;
            _jwtService = jwtService;
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await dbContext.Users.
                   FirstOrDefaultAsync(x => x.UserId == id);
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
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


        public async Task<User> RegisterUserAsync(UserDto addUser)
        {
            try
            {
                var _mappedUser = _mapper.Map<User>(addUser);
                await dbContext.Users.AddAsync(_mappedUser);
                await dbContext.SaveChangesAsync();
                return _mappedUser;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<User> UpdateInfoAsync(int id, UserDto updatedUser)
        {
            try
            {
                var user = await dbContext.Users.SingleOrDefaultAsync(x => x.UserId == id);
                if (user != null)
                {

                    var updatedUser_ = _mapper.Map<User>(updatedUser);
                    updatedUser_.UserId = id;
                    user = updatedUser_;
                    await dbContext.SaveChangesAsync();
                    return updatedUser_;
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

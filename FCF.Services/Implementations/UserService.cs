using FCF.Entities;
using FCF.Data;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using FCF.Services.Interfaces;
using FCF.Models.Requests.UserDtos;
using FCF.Models.Validators.Regexes;

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
            var user = await dbContext.Users.FindAsync(id);
            if(user == null) throw new Exception("User not found!");
            return user;
        }

        public async Task<List<User>> GetPaginatedUsersAsync(int pageNum = 0, int chunkSize=10)
        {
            return await dbContext.Users.Skip((pageNum-1)*chunkSize).Take(chunkSize).ToListAsync();
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            return await dbContext.Users.ToListAsync();
        }

        public async Task<UserInfo> AuthenticateAsync(LoginUserDto model)
        {
            var user = await dbContext.Users.
                             Include(x => x.Team_).
                             FirstOrDefaultAsync(x => x.Email == model.Email && x.Password == model.Password);

            if (user == null) throw new Exception("Username or password is incorrect");

            var token = _jwtService.GenerateJwtToken(user);
            return new UserInfo(user, token);
        }


        public async Task<User> RegisterUserAsync(UserDto addUser)
        {
            var _mappedUser = _mapper.Map<User>(addUser);
            await dbContext.Users.AddAsync(_mappedUser);
            await dbContext.SaveChangesAsync();
            return _mappedUser;       
        }

        public async Task<User> UpdateInfoAsync(int id, UserDto updatedUser)
        {
            var user = await dbContext.Users.SingleOrDefaultAsync(x => x.UserId == id);
            if (user == null) throw new Exception("Failed to update user info!");
         
            var updatedUser_ = _mapper.Map<User>(updatedUser);
            updatedUser_.UserId = id;
            user = updatedUser_;
            await dbContext.SaveChangesAsync();
            return updatedUser_;
        }

        public async Task<User> UpdateTeamAsync(int uid, int tid)
        {
            var user = await dbContext.Users.SingleOrDefaultAsync(x => x.UserId == uid);
            if (user == null) throw new Exception("Failed to register user into a team!");

            user.TeamId = tid;
            await dbContext.SaveChangesAsync();
            return user;
        }

        public async Task<User> UpdateUserPasswordAsync(UpdatePasswordDto info)
        {
            var user = await dbContext.Users.SingleOrDefaultAsync(x => x.UserId == info.uid);
            if (user == null) throw new Exception("Failed to update user password!");

            user.Password = info.password;
            await dbContext.SaveChangesAsync();
            return user;
        }

        public async Task<bool> DeleteByIdAsync(int id)
        {
            var user = await dbContext.Users.FindAsync(id);
            dbContext.Remove(user);
            await dbContext.SaveChangesAsync();
            return true;
        }
    }
}

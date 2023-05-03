using FCF.Services.Interfaces;
using FCF.Models.Requests.UserDtos;
using FCF.Data;
using FCF.Services.Services;
using AutoMapper;
using FCF.Api.Profiles;
using FCF.Config;
using Microsoft.Extensions.Options;

namespace FCF.Test.UserTests
{
    public class User : IClassFixture<UserServiceFixture>
    {
        private UserServiceFixture fixture { get; }

        public User(UserServiceFixture fixture)
        {
            this.fixture = fixture;  
        }

        [Theory]
        [InlineData("ahmednaeem@folio3.com", "ahmed123", false)]
        [InlineData("admin@folio3.com", "admin123", false)]
        public void Login(string email, string password, bool expected)
        {
            UserService userService = fixture.CreateService();
            LoginUserDto loginUserDto = new LoginUserDto()
            {
                Email = email,
                Password = password
            };

            var response = userService.AuthenticateAsync(loginUserDto);
            Assert.Equal(expected, response == null);
        }

        [Fact]
        public async void AddUser()
        {
            var userService = fixture.CreateService();
            UserDto user = new UserDto()
            {
                Name = "Test",
                Email = "test@test.com",
                Password = "password",
                Division = "test dev",
                Designation = "test engineer",
                Role = "test"
            };
            var newUser = await userService.RegisterUserAsync(user);
            Assert.NotNull(newUser);
        }

        [Fact]
        public async void GetUsers()
        {
            var userService = fixture.CreateService();
            var users = await userService.GetAllUsersAsync();
            Assert.True(users.Count > 0);
        }

        [Fact]
        public async void GetUser()
        {
            var userService = fixture.CreateService();
            var user = await userService.GetByIdAsync(1);
            Assert.True(user==null);
        }
    }
}

using FCF.Models.Requests.UserDtos;
using FCF.Services.Services;
using NuGet.ContentModel;
using Xunit.Abstractions;

namespace FCF.Test.UserTests
{
    public class UserTest : IClassFixture<UserServiceFixture>
    {
        private UserServiceFixture fixture { get; }
        private readonly ITestOutputHelper _testOutputHelper;

        public UserTest(UserServiceFixture fixture, ITestOutputHelper testOutputHelper)
        {
            this.fixture = fixture;
            _testOutputHelper = testOutputHelper;
        }

        [Theory]
        [InlineData("ahmednaeem@folio3.com", "ahmed123", false)]
        [InlineData("admin@folio3.com", "admin123", true)]
        [InlineData("ahmedkhan@folio3.com", "ahmedkhan123Ak!", true)]
        public async void Login(string email, string password, bool expected)
        {
            UserService userService = fixture.CreateService();
            LoginUserDto loginUserDto = new LoginUserDto()
            {
                Email = email,
                Password = password
            };
            try
            {
                var response = await userService.AuthenticateAsync(loginUserDto);
                Assert.Equal(expected, response != null);
            }
            catch (Exception ex)
            {
                Assert.False(false);
            }
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
            var user = await userService.GetByIdAsync(5);
            Assert.NotNull(user);
        }
    }
}

using FCF.Services.Interfaces;
using FCF.Models.Requests.UserDtos;

namespace FCF.Test.UserTests
{
    public class UserTests : IClassFixture<IUserService>
    {
        private IUserService userService;

        public UserTests(IUserService userService)
        {
            this.userService = userService;
        }

        [Theory]
        [InlineData("ahmednaeem@folio3.com", "ahmed123", true)]
        [InlineData("admin@folio3.com", "admin123", false)]
        public void Login(string email, string password, bool expected)
        {
            LoginUserDto loginUserDto = new LoginUserDto()
            {
                Email = email,
                Password = password
            };

            var response = userService.AuthenticateAsync(loginUserDto);
            Assert.Equal(expected, response == null);
        }
    }
}

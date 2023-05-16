using AutoMapper;
using FCF.Api.Profiles;
using FCF.Config;
using FCF.Data;
using FCF.Services.Implementations;
using FCF.Services.Interfaces;
using FCF.Services.Services;
using FCF.Test.Mocks;

namespace FCF.Test.UserTests
{
    public class UserServiceFixture: IDisposable
    {
        public IMapper Mapper { get; }
        public MockContext<MainDBContext> MockDbContext { get; set; }

        public IJwtService jwtService { get; set; }
        public IEmailService emailService { get; set; }

        public UserServiceFixture()
        {
            var mappingConfig = new MapperConfiguration(mc => mc.AddProfile(new UserProfile()));
            Mapper = mappingConfig.CreateMapper();

            MockDbContext = new MockContext<MainDBContext>(creator: options => new MainDBContext(
                options: options
            ));
            AppSettings appSettings = new AppSettings()
            {
                Secret = "THIS IS USED TO SIGN AND VERIFY JWT TOKENS",
            };
            jwtService = new JwtService(appSettings);
            emailService = new EmailService();
        }

        public UserService CreateService()
        {
            return new UserService(Mapper, MockDbContext.Context, jwtService, emailService);
        }

        public void Dispose()
        {
            MockDbContext.Dispose();
        }
    }
}

using AutoMapper;
using FCF.Entities;
using FCF.Models.Requests.UserDtos;

namespace FCF.Api.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserDto, User>();

        }
    }
}

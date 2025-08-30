using AutoMapper;
using Storage.Entities;

namespace Storage.Mapping;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<UserEntity, UserProfile>().ReverseMap();
    }
}

using AutoMapper;
using Domain.UseCases.AccountOperations.Models;
using Storage.Entities;

namespace Storage.Mapping;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<UserEntity, UserModel>().ReverseMap();
    }
}

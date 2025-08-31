using API.Dtos.Tasks;
using API.Dtos.Users;
using AutoMapper;
using Domain.Queries;
using Domain.UseCases.AccountOperations.Commands.Login;
using Domain.UseCases.AccountOperations.Commands.Registration;
using Domain.UseCases.TaskOperations.Commands.CreateTask;
using Domain.UseCases.TaskOperations.Commands.UpdateTask;
using Domain.UseCases.TaskOperations.Models;

namespace API.Mapping;

public class AutoMappingProfile : Profile
{
    public AutoMappingProfile()
    {
        CreateMap(typeof(PaginationList<>), typeof(PaginationListDto<>));
        CreateMap<RegistrationDto, RegistrationCommand>();
        CreateMap<LoginDto, LoginCommand>();
        CreateMap<UserLoginResultModel, ResultLoginDto>();
        CreateMap<TaskDto, TaskModel>();
        CreateMap<TaskModel, TaskDto>();
        CreateMap<CreateTaskDto, TaskModel>();
        CreateMap<UpdateTaskDto, TaskModel>();
        CreateMap<CreateTaskDto, CreateTaskCommand>();
        CreateMap<TaskDto, CreateTaskCommand>();
        CreateMap<UpdateTaskDto, UpdateTaskCommand>();
    }
}

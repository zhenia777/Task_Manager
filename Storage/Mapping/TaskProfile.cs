using AutoMapper;
using Domain.UseCases.TaskOperations.Models;
using Storage.Entities;

namespace Storage.Mapping;

public class TaskProfile : Profile
{
    public TaskProfile()
    {
        CreateMap<TaskEntity, TaskModel>().ReverseMap();
    }
}

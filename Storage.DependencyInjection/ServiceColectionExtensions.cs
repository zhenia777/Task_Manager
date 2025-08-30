using Domain.UseCases.AccountOperations.Commands.Login;
using Domain.UseCases.AccountOperations.Commands.Registration;
using Domain.UseCases.TaskOperations.Commands.CreateTask;
using Domain.UseCases.TaskOperations.Commands.DeleteTask;
using Domain.UseCases.TaskOperations.Commands.UpdateTask;
using Domain.UseCases.TaskOperations.Queries.GetAllTasks;
using Domain.UseCases.TaskOperations.Queries.GetTaskById;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Storage.Storages.AccountStorage;
using Storage.Storages.TaskStorage;
using System.Reflection;

namespace Storage.DependencyInjection;

public static class ServiceColectionExtensions
{
    public static IServiceCollection AddStorage(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<ILoginStorage, LoginStorage>()
                .AddScoped<IRegistrationStorage, RegistrationStorage>()
                .AddScoped<ICreateTaskStorage, CreateTaskStorage>()
                .AddScoped<IDeleteTaskStorage, DeleteTaskStorage>()
                .AddScoped<IGetTasksStorage, GetTasksStorage>()
                .AddScoped<IGetTaskByIdStorage, GetTaskByIdStorage>()
                .AddScoped<IUpdateTaskStorage, UpdateTaskStorage>();

        services
            .AddDbContextPool<TaskManagerDbContext>(opt =>
                opt.UseNpgsql(configuration.GetConnectionString("Postgres"),
                b => b.MigrationsAssembly(typeof(TaskManagerDbContext).Assembly.FullName)));

        services
            .AddAutoMapper(conf => conf.AddMaps(Assembly.GetAssembly(typeof(TaskManagerDbContext))));

        return services;
    }
}

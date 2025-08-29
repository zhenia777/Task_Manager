using Domain.UseCases.AccountOperations.Commands.Login;
using Domain.UseCases.AccountOperations.Commands.Registration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Storage.Storages.AccountStorage;
using System.Reflection;

namespace Storage.DependencyInjection;

public static class ServiceColectionExtensions
{
    public static IServiceCollection AddStorage(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<ILoginStorage, LoginStorage>()
                .AddScoped<IRegistrationStorage, RegistrationStorage>();

        services
            .AddDbContextPool<TaskManagerDbContext>(opt =>
                opt.UseNpgsql(configuration.GetConnectionString("Postgres"),
                b => b.MigrationsAssembly(typeof(TaskManagerDbContext).Assembly.FullName)));

        services
            .AddAutoMapper(conf => conf.AddMaps(Assembly.GetAssembly(typeof(TaskManagerDbContext))));

        return services;
    }
}

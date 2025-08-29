using Domain.Exceptions;
using Domain.Services.TokenService;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Domain.DependencyInjection;

public static class ServiceCollectionExtentions
{
    public static IServiceCollection AddDomain(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddScoped<ITokenService, TokenService>();


        services.AddMediatR(opt => opt.RegisterServicesFromAssembly(typeof(DomainException).Assembly));
        return services;
    }
}

using DataAccess.Repositories;
using DataAccess.Repositories.Implementation;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccess;

public static class ServiceRegistry
{
    public static IServiceCollection LoadDataServices(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        
        return services;
    }
}
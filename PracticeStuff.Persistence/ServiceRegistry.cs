using Microsoft.Extensions.DependencyInjection;
using PracticeStuff.Persistence.Repositories;
using PracticeStuff.Persistence.Repositories.Implementation;

namespace PracticeStuff.Persistence;

public static class ServiceRegistry
{
    public static IServiceCollection LoadDataServices(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        
        return services;
    }
}
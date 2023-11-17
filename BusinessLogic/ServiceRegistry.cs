using System.Reflection;
using BusinessLogic.Services;
using BusinessLogic.Services.Implementation;
using Microsoft.Extensions.DependencyInjection;

namespace BusinessLogic;

public static class ServiceRegistry
{
    public static IServiceCollection LoadBusinessServices(this IServiceCollection services)
    {
        services.AddScoped<IStackService, StackService>();
        services.AddScoped<IFlashCardService, FlashCardService>();
        services.AddScoped<IStudyAreaService, StudyAreaService>();
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        
        return services;
    }
}
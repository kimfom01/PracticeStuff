using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using PracticeStuff.Application.Services;
using PracticeStuff.Application.Services.Implementation;

namespace PracticeStuff.Application;

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
using DataAccess.DataContext;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccess;

public static class SetupDatabase
{
    public static async Task ResetDatabase(IServiceScope scope)
    {
        var context = scope.ServiceProvider.GetRequiredService<Context>();
        await context.Database.EnsureDeletedAsync();
        await context.Database.EnsureCreatedAsync();
    }
}
using Microsoft.Extensions.DependencyInjection;
using PracticeStuff.Persistence.DataContext;

namespace PracticeStuff.Persistence;

public static class SetupDatabase
{
    public static async Task ResetDatabase(IServiceScope scope)
    {
        var context = scope.ServiceProvider.GetRequiredService<Context>();
        await context.Database.EnsureDeletedAsync();
        await context.Database.EnsureCreatedAsync();
    }
}
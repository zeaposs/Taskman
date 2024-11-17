using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Taskman.Data
{
    public static class ServiceCollectionExtensions
    {
        internal static void AddDataServicesInternal<DbContextImplementation>(this IServiceCollection services, Action<DbContextOptionsBuilder>? optionsAction) where DbContextImplementation : TaskDbContext
        {
            services.AddDbContext<TaskDbContext, DbContextImplementation>(optionsAction);
        }

        public static void InitializeDatabase(this IServiceProvider serviceProvider)
        {
            var scopeFactory = serviceProvider.GetRequiredService<IServiceScopeFactory>();
            using (var scope = scopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<TaskDbContext>();

                var pendingMigrations = context.Database.GetPendingMigrations().ToList();
                if (pendingMigrations.Any())
                {
                    context.Database.Migrate();
                }
            }
        }
    }
}

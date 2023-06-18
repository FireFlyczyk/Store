using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace OrderService.Data
{
    public static class PrepDb
    {
        public static void PrepMigrations(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetRequiredService<AppDbContext>();
                Migrate(dbContext);
            }
        }

        private static void Migrate(AppDbContext context)
        {
            Console.WriteLine("--> Attempting to apply migrations...");
            try
            {
                context.Database.Migrate();
                Console.WriteLine("--> Migrations applied successfully.");
                // Perform additional seeding if needed
            }
            catch (Exception ex)
            {
                Console.WriteLine($"--> Error applying migrations: {ex.Message}");
            }
        }
    }
}


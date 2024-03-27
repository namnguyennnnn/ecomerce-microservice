﻿using Microsoft.EntityFrameworkCore;

namespace Product.API.Extensions
{
    public static class HostExtentions
    {
        public static IHost MigrateDatabase<TContext>(this IHost host, Action<TContext,IServiceProvider> seeder) where TContext : DbContext
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
               
                var logger = services.GetRequiredService<ILogger<TContext>>();
                var context = services.GetService<TContext>();

                try 
                {
                    logger.LogInformation("Migrating Product database...");                    
                    ExecuteMigrations(context);
                    logger.LogInformation("Migarted Product database.");
                    InvokeSeeder(seeder, context, services);
                }catch (Exception ex) { }
            }

            return host;
        }


        private static void ExecuteMigrations<TContext>(TContext context) where TContext:DbContext 
        {
            context.Database.Migrate();
        }

        private static void InvokeSeeder<TContext>(Action<TContext, IServiceProvider> seeder, TContext? context, IServiceProvider services) where TContext : DbContext
        {
            seeder(context, services);
        }
    }
}

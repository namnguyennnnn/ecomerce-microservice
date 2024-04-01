using Basket.API.Repository;
using Basket.API.Repository.Interface;

namespace Basket.API.Extensions
{
    public static class ServiceExtentions
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services)
        {
            services.AddScoped<IBasketRepository, BasketRepository>();

            return services;
        }

        public static void ConfigureResdis(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetValue<string>("CacheSettings:ConnectionString");
            if (string.IsNullOrEmpty(connectionString))
                throw new ArgumentNullException("Redis connection string is not configured");

            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = connectionString;
            });
        }
    }

  
}

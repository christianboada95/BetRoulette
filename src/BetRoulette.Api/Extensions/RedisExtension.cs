namespace BetRoulette.Api.Extensions;

public static class RedisExtension
{
    public static IServiceCollection AddRedisConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        string connectionString = configuration.GetConnectionString("Redis");

        services.AddStackExchangeRedisCache(o =>
        {
            o.Configuration = connectionString;
            o.InstanceName = configuration.GetSection("Redis")["InstanceName"];
        });

        services.AddDistributedMemoryCache();

        return services;
    }
}

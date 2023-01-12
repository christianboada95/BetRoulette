using BetRoulette.Domain.Entities;
using BetRoulette.Domain.Interfaces;
using BetRoulette.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace BetRoulette.Infrastructure;

public static class StartupSetup
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddRedis(config.GetConnectionString("Redis")!);

        // Repositories
        services.AddScoped<IRepository<Roulette>, RouletteRepository>();
        //services.AddScoped<ICacheProvider<Roulette>>();

        return services;
    }

    public static void AddRedis(this IServiceCollection services, string connectionString) =>
        services.AddSingleton<IConnectionMultiplexer>(sp => ConnectionMultiplexer.Connect(connectionString));
}

using Microsoft.Extensions.DependencyInjection;

namespace BetRoulette.Infrastructure;

public static class StartupSetup
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        // Services
        //services.AddScoped<ICacheProvider<Roulette>>();

        return services;
    }
}

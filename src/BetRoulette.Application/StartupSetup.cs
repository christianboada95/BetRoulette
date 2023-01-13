using BetRoulette.Application.Interfaces;
using BetRoulette.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BetRoulette.Application;

public static class StartupSetup
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // Services
        services.AddScoped<IRouletteService, RouletteService>();
        services.AddScoped<IBetService, BetService>();

        // Mappings
        //services.AddAutoMapper(typeof(MappingProfile).Assembly);

        return services;
    }
}

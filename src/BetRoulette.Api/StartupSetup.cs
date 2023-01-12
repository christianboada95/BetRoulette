using System.Text.Json.Serialization;

namespace BetRoulette.Api;

public static class StartupSetup
{
    public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration config)
    {
        // Cache
        services.AddMemoryCache();
        services.AddRedisConfiguration(config);

        services.AddControllers()
            .ConfigureApiBehaviorOptions(op => op.SuppressModelStateInvalidFilter = true)
            .AddJsonOptions(op =>
            {
                op.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                op.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        return services;
    }
}

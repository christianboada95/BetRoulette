using BetRoulette.Domain.Entities;
using BetRoulette.Infrastructure.Common;
using StackExchange.Redis;

namespace BetRoulette.Infrastructure.Repositories;

sealed class RouletteRepository : RepositoryBase<Roulette>//, IDisposable
{
    public RouletteRepository(IConnectionMultiplexer redis)
        : base(redis.GetDatabase(), "Roulettes")
    {
    }

    //public void Dispose() => redis.Dispose();
}

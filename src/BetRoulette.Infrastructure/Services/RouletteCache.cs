using BetRoulette.Domain.Entities;
using BetRoulette.Domain.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace BetRoulette.Infrastructure.Services;

internal class RouletteCache : ICacheProvider<Roulette>
{
    private readonly IDistributedCache _distributedCache;

    public RouletteCache(IDistributedCache distributedCache)
    {
        _distributedCache = distributedCache;
    }

    public async Task<Roulette> GetAsync(string key)
    {
        CancellationTokenSource cancellationTokenSource = new();
        CancellationToken cancellationToken = cancellationTokenSource.Token;
        string json = await _distributedCache.GetStringAsync(key, cancellationToken).ConfigureAwait(false);
        return JsonSerializer.Deserialize<Roulette>(json)!;
    }

    public async Task SetAsync(string key, Roulette value)
    {
        CancellationTokenSource cancellationTokenSource = new();
        CancellationToken cancellationToken = cancellationTokenSource.Token;
        string json = JsonSerializer.Serialize(value);
        await _distributedCache.SetStringAsync(key, json, cancellationToken).ConfigureAwait(false);
    }
}

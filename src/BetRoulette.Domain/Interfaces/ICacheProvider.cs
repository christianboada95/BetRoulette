using BetRoulette.Domain.Common;

namespace BetRoulette.Domain.Interfaces;
public interface ICacheProvider<T> where T : EntityBase
{
    Task SetAsync(string key, T value);
    Task<T> GetAsync(string key);
}
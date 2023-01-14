using BetRoulette.Domain.Common;

namespace BetRoulette.Domain.Interfaces;

public interface IRepository<T> where T : EntityBase
{
    Task<T> AddAsync(T entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(T entity, CancellationToken cancellationToken = default);
    Task<T?> GetByIdAsync<TId>(TId id, CancellationToken cancellationToken = default) where TId : notnull;
    Task<List<T>?> ListAsync(CancellationToken cancellationToken = default);
}

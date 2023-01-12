using BetRoulette.Domain.Common;
using BetRoulette.Domain.Interfaces;
using StackExchange.Redis;
using System.Text.Json;

namespace BetRoulette.Infrastructure.Common
{
    public abstract class RepositoryBase<T> : IRepository<T> where T : EntityBase
    {
        private readonly IDatabaseAsync _databaseAsync;
        private readonly string _HashTableName = "Default";

        protected RepositoryBase(IDatabaseAsync database) =>
            _databaseAsync = database;

        public RepositoryBase(IDatabaseAsync database, string TableName)
            : this(database) => _HashTableName = TableName;

        public virtual async Task<T> AddAsync(T entity, CancellationToken cancellationToken = default)
        {
            var value = JsonSerializer.Serialize(entity);
            var entry = new HashEntry(entity.Id.ToString(), value);

            await _databaseAsync.HashSetAsync(_HashTableName, new HashEntry[]
                {entry}).ConfigureAwait(false);

            return entity;
        }

        public virtual async Task<T?> GetByIdAsync<TId>(TId id, CancellationToken cancellationToken = default) where TId : notnull
        {
            if (!await _databaseAsync.HashExistsAsync(_HashTableName, id.ToString()))
                return null;

            var entity = await _databaseAsync.HashGetAsync(_HashTableName, id.ToString()).ConfigureAwait(false);
            return JsonSerializer.Deserialize<T>(entity);
        }

        public virtual async Task<List<T>?> ListAsync(CancellationToken cancellationToken = default)
        {
            var list = await _databaseAsync.HashGetAllAsync(_HashTableName);

            if (!list.Any())
                return null;

            var obj = Array.ConvertAll(list, val =>
                    JsonSerializer.Deserialize<T>(val.Value)).ToList();

            return obj;
        }

        public virtual async Task UpdateAsync(T entity, CancellationToken cancellationToken = default)
        {
            if (await _databaseAsync.HashExistsAsync(_HashTableName, entity.Id.ToString()))
            {
                var value = JsonSerializer.Serialize(entity);
                var entry = new HashEntry(entity.Id.ToString(), value);

                await _databaseAsync.HashSetAsync(_HashTableName, new HashEntry[]
                    {entry}).ConfigureAwait(false);
            }
        }
    }
}

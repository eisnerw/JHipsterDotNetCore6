using System.Threading;
using System.Threading.Tasks;

namespace JHipsterDotNetCore6.Domain.Repositories.Interfaces
{
    public interface INoSqlGenericRepository<TEntity, TKey> : INoSqlReadOnlyGenericRepository<TEntity, TKey> where TEntity : MongoBaseEntity<TKey>
    {
        Task<TEntity> CreateOrUpdateAsync(TEntity entity);
        Task DeleteByIdAsync(TKey id);
        Task DeleteAsync(TEntity entity);
        Task Clear();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
        TEntity Add(TEntity entity);
        bool AddRange(params TEntity[] entities);
        TEntity Update(TEntity entity);
        bool UpdateRange(params TEntity[] entities);
    }
}

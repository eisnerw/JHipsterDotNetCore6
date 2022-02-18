using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using JHipsterNet.Core.Pagination;
using Microsoft.EntityFrameworkCore.Query;

namespace JHipsterDotNetCore6.Domain.Repositories.Interfaces
{
    public interface INoSqlReadOnlyGenericRepository<TEntity, TKey> where TEntity : MongoBaseEntity<TKey>
    {
        Task<TEntity> GetOneAsync(TKey id);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<IPage<TEntity>> GetPageAsync(IPageable pageable);
        Task<bool> Exists(Expression<Func<TEntity, bool>> predicate);
        Task<int> CountAsync();
        INoSqlFluentRepository<TEntity> QueryHelper();
    }
}

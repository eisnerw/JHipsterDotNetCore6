using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using JHipsterNet.Core.Pagination;
using JHipsterNet.Core.Pagination.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using JHipsterDotNetCore6.Domain.Repositories.Interfaces;
using JHipsterDotNetCore6.Domain;
using MongoDB.Bson;
using MongoDB.Driver;

namespace JHipsterDotNetCore6.Infrastructure.Data.Repositories
{
    public abstract class MongoReadOnlyGenericRepository<TEntity, TKey> : INoSqlReadOnlyGenericRepository<TEntity, TKey>, IDisposable where TEntity : MongoBaseEntity<TKey>
    {
        protected readonly IMongoDatabaseContext _context;
        protected IMongoCollection<TEntity> _dbSet;

        protected MongoReadOnlyGenericRepository(IMongoDatabaseContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>(typeof(TEntity).Name);
        }

        public virtual async Task<TEntity> GetOneAsync(TKey id)
        {
            var objectId = new ObjectId(id.ToString());
            FilterDefinition<TEntity> filter = Builders<TEntity>.Filter.Eq("_id", objectId);
            return await _dbSet.FindAsync(filter).Result.FirstOrDefaultAsync();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            var all = await _dbSet.FindAsync(Builders<TEntity>.Filter.Empty);
            return await all.ToListAsync();
        }

        public async Task<IPage<TEntity>> GetPageAsync(IPageable pageable)
        {
            return await _dbSet.AsQueryable().UsePageableAsync(pageable);
        }

        public virtual async Task<bool> Exists(Expression<Func<TEntity, bool>> predicate)
        {
            var result = await _dbSet.FindAsync(predicate);
            return result.Any();
        }

        public virtual Task<int> CountAsync()
        {
            return Task.FromResult(Convert.ToInt32(_dbSet.CountDocuments(Builders<TEntity>.Filter.Empty)));
        }

        public virtual INoSqlFluentRepository<TEntity> QueryHelper()
        {
            var fluentRepository = new MongoFluentRepository<TEntity>(_dbSet.AsQueryable());
            return fluentRepository;
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}

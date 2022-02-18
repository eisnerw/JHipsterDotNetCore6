using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using JHipsterNet.Core.Pagination;
using JHipsterNet.Core.Pagination.Extensions;
using Microsoft.EntityFrameworkCore.Query;
using JHipsterDotNetCore6.Domain.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver.Linq;

namespace JHipsterDotNetCore6.Infrastructure.Data.Repositories
{
    public class MongoFluentRepository<TEntity> : INoSqlFluentRepository<TEntity> where TEntity : class
    {
        private readonly IMongoQueryable<TEntity> _dbSet;
        private Expression<Func<TEntity, bool>> _filter;
        private Func<IMongoQueryable<TEntity>, IMongoQueryable<TEntity>> _orderBy;

        public MongoFluentRepository(IMongoQueryable<TEntity> dbset)
        {
            _dbSet = dbset;
        }

        public INoSqlFluentRepository<TEntity> Filter(Expression<Func<TEntity, bool>> filter)
        {
            _filter = filter;
            return this;
        }

        public INoSqlFluentRepository<TEntity> OrderBy(Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy)
        {
            _orderBy = (Func<IMongoQueryable<TEntity>, IMongoQueryable<TEntity>>)orderBy;
            return this;
        }

        public async Task<TEntity> GetOneAsync(Expression<Func<TEntity, bool>> filter)
        {
            _filter = filter;
            IMongoQueryable<TEntity> query = BuildQuery();
            return await query.SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            IMongoQueryable<TEntity> query = BuildQuery();
            return await query.ToListAsync();
        }

        public async Task<IPage<TEntity>> GetPageAsync(IPageable pageable)
        {
            IMongoQueryable<TEntity> query = BuildQuery();
            return await query.UsePageableAsync(pageable);
        }

        private IMongoQueryable<TEntity> BuildQuery()
        {
            IMongoQueryable<TEntity> query = _dbSet;

            if (_filter != null)
            {
                query = query.Where(_filter);
            }

            if (_orderBy != null)
            {
                query = _orderBy(query);
            }

            return query;
        }
    }
}

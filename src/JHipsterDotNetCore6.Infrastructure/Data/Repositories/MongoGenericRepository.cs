using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JHipsterDotNetCore6.Domain.Repositories.Interfaces;
using System.Linq.Expressions;
using JHipsterNet.Core.Pagination;
using JHipsterNet.Core.Pagination.Extensions;
using MongoDB.Driver.Linq;
using JHipsterDotNetCore6.Domain;

namespace JHipsterDotNetCore6.Infrastructure.Data.Repositories
{
    public abstract class MongoGenericRepository<TEntity, TKey> : MongoReadOnlyGenericRepository<TEntity, TKey>, INoSqlGenericRepository<TEntity, TKey>, IDisposable where TEntity : MongoBaseEntity<TKey>
    {
        protected MongoGenericRepository(IMongoDatabaseContext context) : base(context)
        {
        }

        public async Task<TEntity> CreateOrUpdateAsync(TEntity bankAccountTestA)
        {
            bool exists = await Exists(x => x.Id.Equals(bankAccountTestA.Id));

            if (bankAccountTestA.Id != null && exists)
            {
                Update(bankAccountTestA);
            }
            else
            {
                Add(bankAccountTestA);
            }
            return bankAccountTestA;
        }

        public virtual TEntity Update(TEntity entity)
        {
            // _context.AddCommand(async () => await _dbSet.ReplaceOneAsync(Builders<TEntity>.Filter.Eq("_id", obj.Id), obj));
            _dbSet.FindOneAndReplace(m => m.Id.Equals(entity.Id), entity);
            return entity;
        }

        public virtual async Task DeleteByIdAsync(TKey id)
        {
            var objectId = new ObjectId(id.ToString());
            // _context.AddCommand(async () => await _dbSet.DeleteOneAsync(Builders<TEntity>.Filter.Eq("_id", objectId)));
            await _dbSet.DeleteOneAsync(Builders<TEntity>.Filter.Eq("_id", objectId));
        }

        public async Task DeleteAsync(TEntity entity)
        {
            // _context.AddCommand(async () => await _dbSet.DeleteOneAsync(Builders<TEntity>.Filter.Eq("_id", entity.Id)));
            await _dbSet.DeleteOneAsync(Builders<TEntity>.Filter.Eq("_id", entity.Id));
        }

        public virtual TEntity Add(TEntity entity)
        {
            // _context.AddCommand(async () => await _dbSet.InsertOneAsync(entity));
            _dbSet.InsertOne(entity);
            return entity;
        }

        public virtual bool AddRange(params TEntity[] entities)
        {
            // _context.AddCommand(async () => await _dbSet.InsertManyAsync(entities));
            _dbSet.InsertMany(entities);
            return true;
        }

        public virtual bool UpdateRange(params TEntity[] entities)
        {
            foreach (TEntity entity in entities)
                this.Update(entity);
            return true;
        }

        public virtual async Task Clear()
        {
            await _dbSet.DeleteManyAsync(Builders<TEntity>.Filter.Empty);
        }

        public virtual async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync();
        }
    }
}

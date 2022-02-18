using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;
using Microsoft.Extensions.Options;
using JHipsterDotNetCore6.Infrastructure.Configuration;

namespace JHipsterDotNetCore6.Infrastructure.Data
{
    public class MongoDatabaseContext : IMongoDatabaseContext, IDisposable
    {
        private IMongoDatabase _db { get; set; }
        private MongoClient _mongoClient { get; set; }
        public IClientSessionHandle Session { get; set; }

        protected List<Func<Task>> _commands;
        public MongoDatabaseContext(IOptions<MongoDatabaseConfig> configuration)
        {
            _mongoClient = new MongoClient(configuration.Value.ConnectionString);
            _db = _mongoClient.GetDatabase(configuration.Value.DatabaseName);
            _commands = new List<Func<Task>>();
        }

        public IMongoCollection<T> Set<T>(string name)
        {
            return _db.GetCollection<T>(name);
        }

        public void AddCommand(Func<Task> func)
        {
            _commands.Add(func);
        }

        public async Task<int> SaveChangesAsync()
        {
            var commandTasks = _commands.Select(c => c());
            await Task.WhenAll(commandTasks);
            return _commands.Count;
        }

        public void Dispose()
        {
            Session?.Dispose();
        }
    }
}

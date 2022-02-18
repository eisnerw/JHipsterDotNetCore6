using System;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace JHipsterDotNetCore6.Infrastructure.Data
{
    public interface IMongoDatabaseContext
    {
        IClientSessionHandle Session { get; set; }
        IMongoCollection<T> Set<T>(string name);
        Task<int> SaveChangesAsync();
        void AddCommand(Func<Task> p);
        void Dispose();
    }
}

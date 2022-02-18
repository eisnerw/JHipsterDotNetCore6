using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace JHipsterDotNetCore6.Domain
{
    public class MongoBaseEntity<TKey>
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public TKey Id { get; set; }
    }
}

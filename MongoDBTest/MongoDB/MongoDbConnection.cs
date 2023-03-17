using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDBTest.Models;

namespace MongoDBTest.MongoDB
{
    public class MongoDbConnection<T>
    {
        public IMongoDatabase MongoDb { get; }

        public MongoDbConnection(IOptions<DooProjectMongoDbSetting> options)
        {
            var client = new MongoClient(options.Value.ConnectionString);
            MongoDb = client.GetDatabase(options.Value.DatabaseName);
        }

        public IMongoCollection<T> GetCollection(CollectionsList collectionName)
        {
            return MongoDb.GetCollection<T>(collectionName.ToString());
        }
    }
}

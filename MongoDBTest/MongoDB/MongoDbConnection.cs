using MongoDB.Driver;

namespace MongoDBTest.MongoDB
{
    public class MongoDbConnection<T>
    {
        private readonly string DatabaseName = "DooProject";

        public IMongoDatabase MongoDb { get; }

        public MongoDbConnection()
        {
            var client = new MongoClient();
            MongoDb = client.GetDatabase(DatabaseName);
        }
        public IMongoCollection<T> GetCollection(CollectionsList collectionName)
        {
            return MongoDb.GetCollection<T>(collectionName.ToString());
        }
    }
}

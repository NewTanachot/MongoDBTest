using MongoDB.Bson;
using MongoDB.Driver;
using MongoDBTest.Models;
using MongoDBTest.MongoDB;
using System.Collections;
using System.Text;

namespace MongoDBTest.Services
{
    public class CRUDServices<T>
    {
        private readonly MongoDbConnection<T> mongoDb;
        private readonly ILogger<CRUDServices<T>> logger;

        public CRUDServices(MongoDbConnection<T> mongoDbConnection, ILogger<CRUDServices<T>> logger)
        {
            mongoDb = mongoDbConnection;
            this.logger = logger;
        }

        public async Task<List<T>> LoadRecordAsync(CollectionsList collectionName, string? id = null)
        {
            var collection = mongoDb.GetCollection(collectionName);

            if (id != null)
            {
                var filter = Builders<T>.Filter.Eq("_id", ObjectId.Parse(id));

                return await collection.Find(filter).ToListAsync();
            }

            return await collection.Find(x => true).ToListAsync();
        }

        public async Task InsertRecordAsync(CollectionsList collectionName, T record)
        {
            var collection = mongoDb.GetCollection(collectionName);
            await collection.InsertOneAsync(record);
        }

        public async Task InsertRecordAsync(CollectionsList collectionName, List<T> records)
        {
            var collection = mongoDb.GetCollection(collectionName);
            await collection.InsertManyAsync(records, new InsertManyOptions { IsOrdered = false });
        }

        public async Task CreateOrUpdateRecordAsync(CollectionsList collectionName, string id, T record)
        {
            var collection = mongoDb.GetCollection(collectionName);
            var filter = Builders<T>.Filter.Eq("_id", ObjectId.Parse(id));

            var updateOption = new ReplaceOptions
            {
                IsUpsert = true
            };

            await collection.ReplaceOneAsync(filter, record, updateOption);
        }

        public async Task DeleteRecordAsync(CollectionsList collectionName, string id)
        {
            var collection = mongoDb.GetCollection(collectionName);
            var filter = Builders<T>.Filter.Eq("_id", ObjectId.Parse(id));

            await collection.DeleteOneAsync(filter);
        }

        public async Task ClearRecordAsync(CollectionsList collectionName)
        {
            await mongoDb.MongoDb.DropCollectionAsync(collectionName.ToString());
        }
    }
}

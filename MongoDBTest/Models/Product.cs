using MongoDB.Bson.Serialization.Attributes;

namespace MongoDBTest.Models
{
    public class Product
    {
        [BsonId]
        public Guid ProductId { get; set; }

        public string? ProductName { get; set; }

        public string? ProductDescription { get; set; }
    }
}

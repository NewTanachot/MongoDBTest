using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoDBTest.Models
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ProductId { get; set; } = string.Empty;

        public string? ProductName { get; set; }

        public string? ProductDescription { get; set; }

        public string? ProductType { get; set;}
    }
}

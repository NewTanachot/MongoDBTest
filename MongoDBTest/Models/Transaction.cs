using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoDBTest.Models
{
    public class Transaction
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string TransactionId { get; set; } = string.Empty;

        public string? TransactionAction { get; set; }

        public DateTime TransactionDate { get; set; } = DateTime.Now;
    }
}

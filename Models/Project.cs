using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace KitchenEstimatorAPI.Models
{
    public class Project
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public string Name { get; set; } = null!;
        public string ClientName { get; set; } = null!;
        public string Location { get; set; } = null!;
        public double KitchenSize { get; set; }
        public string Status { get; set; } = "Pending"; // Pending, Approved, Rejected
        public double TotalEstimate { get; set; }
    }
}

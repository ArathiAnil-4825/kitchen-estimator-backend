using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace KitchenEstimatorAPI.Models
{
    public enum ProductType
    {
        Appliance,
        Sink,
        Faucet,
        Disposal,
        Hood,
        Hardware,
        Accessory
    }

    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonRepresentation(BsonType.String)]
        public ProductType Type { get; set; }

        public string Name { get; set; } = null!;
        public string? Brand { get; set; }
        public string? Model { get; set; }
        public string? Sku { get; set; }

        public double Price { get; set; } // currency per item

        // Arbitrary specs (e.g., wattage, dimensions, finish)
        public Dictionary<string, string>? Specs { get; set; }

        public bool Active { get; set; } = true;
    }
}

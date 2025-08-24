using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace KitchenEstimatorAPI.Models
{
    public enum LaborRole
    {
        Installation,
        Demolition,
        Plumbing,
        Electrical,
        Carpentry,
        Tiling,
        Painting
    }

    public class LaborRate
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonRepresentation(BsonType.String)]
        public LaborRole Role { get; set; }

        public string? Location { get; set; }   // city/region if rates vary
        public double RatePerHour { get; set; } // currency per hour

        public DateTime EffectiveFrom { get; set; } = DateTime.UtcNow;
        public DateTime? EffectiveTo { get; set; }

        public bool Active { get; set; } = true;
    }
}

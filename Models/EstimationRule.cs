using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace KitchenEstimatorAPI.Models
{
    /// <summary>
    /// Stores human-readable formulas and parameters for the transparency page.
    /// Example: Category = \"Flooring\", Formula = \"(PricePerSqft * Area) + (LaborPerSqft * Area)\"
    /// </summary>
    public class EstimationRule
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public string Category { get; set; } = null!;    // e.g., Cabinetry, Flooring, Electrical
        public string Formula { get; set; } = null!;     // human-readable math string
        public string? Description { get; set; }

        // Default parameters to show in UI (optional)
        public Dictionary<string, double>? DefaultParameters { get; set; }
    }
}

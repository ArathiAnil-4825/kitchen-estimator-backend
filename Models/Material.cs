using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace KitchenEstimatorAPI.Models
{
    public enum MaterialCategory
    {
        Cabinetry,
        Countertops,
        Flooring,
        Backsplash,
        Plumbing,
        Electrical,
        Paint,
        Hardware,
        Miscellaneous
    }

    public class Material
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonRepresentation(BsonType.String)]
        public MaterialCategory Category { get; set; }

        public string Name { get; set; } = null!;
        public string Unit { get; set; } = "unit"; // e.g., unit, sqft, meter
        public double PricePerUnit { get; set; }   // currency per Unit
        public string? Supplier { get; set; }
        public string? Notes { get; set; }

        // Optional SKU/code if you maintain catalogs
        public string? Sku { get; set; }
        public bool Active { get; set; } = true;
    }
}

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace KitchenEstimatorAPI.Models
{
    /// <summary>
    /// A line item tied to a Project. Can reference a Material OR a Product, plus labor.
    /// </summary>
    public class ProjectComponent
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string ProjectId { get; set; } = null!;

        // Friendly name shown in UI (e.g., "Granite Countertop - 30mm")
        public string Name { get; set; } = null!;

        // High-level category to help with grouping in breakdowns (e.g., Countertops, Flooring)
        public string Category { get; set; } = "Miscellaneous";

        // OPTIONAL references to catalog items (one or none)
        [BsonRepresentation(BsonType.ObjectId)]
        public string? MaterialId { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string? ProductId { get; set; }

        // Quantity & pricing
        public double Quantity { get; set; } = 1;   // e.g., 120 (sq.ft) or 1 (unit)
        public string Unit { get; set; } = "unit";  // unit, sqft, meter, etc.
        public double UnitPrice { get; set; } = 0;  // material/product price per Unit

        // Labor for this line
        public double LaborHours { get; set; } = 0;
        public double LaborRate { get; set; } = 0;

        // Precomputed subtotals (you can compute on read, but storing helps reporting)
        public double SubtotalMaterial { get; set; } = 0;
        public double SubtotalLabor { get; set; } = 0;
        public double Total { get; set; } = 0;

        // Helper to compute totals server-side if you want
        public void Recalculate()
        {
            SubtotalMaterial = Quantity * UnitPrice;
            SubtotalLabor = LaborHours * LaborRate;
            Total = SubtotalMaterial + SubtotalLabor;
        }
    }
}

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace KitchenEstimatorAPI.Models
{
    public enum ApprovalStatus
    {
        Pending,
        Approved,
        Rejected
    }

    public class Approval
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        public string ProjectId { get; set; } = string.Empty;
        public string Reviewer { get; set; } = string.Empty;
        public ApprovalStatus Status { get; set; } = ApprovalStatus.Pending;

        public string? Comments { get; set; }

        public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
        public DateTime? ReviewedAtUtc { get; set; }
    }
}


//using MongoDB.Bson;
//using MongoDB.Bson.Serialization.Attributes;
//using System;

//namespace KitchenEstimatorAPI.Models
//{
//    public enum ApprovalStatus
//    {
//        Pending,
//        Approved,
//        Rejected
//    }

//    public class Approval
//    {
//        [BsonId]
//        [BsonRepresentation(BsonType.ObjectId)]
//        public string? Id { get; set; }

//        [BsonRepresentation(BsonType.ObjectId)]
//        public string ProjectId { get; set; } = null!;

//        // Who submitted and who reviewed (optional if not using auth yet)
//        [BsonRepresentation(BsonType.ObjectId)]
//        public string? SubmittedByUserId { get; set; }

//        [BsonRepresentation(BsonType.ObjectId)]
//        public string? ReviewedByUserId { get; set; }

//        [BsonRepresentation(BsonType.String)]
//        public ApprovalStatus Status { get; set; } = ApprovalStatus.Pending;

//        public string? Comments { get; set; }

//        public DateTime SubmittedAtUtc { get; set; } = DateTime.UtcNow;
//        public DateTime? ReviewedAtUtc { get; set; }
//    }
//}

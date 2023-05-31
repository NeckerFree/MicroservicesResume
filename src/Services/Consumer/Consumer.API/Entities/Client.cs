using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Consumer.API.Entities
{
    public class Client
    {
        [BsonRepresentation(BsonType.String)]
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Status { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? Region { get; set; }
        public string? PostalCode { get; set; }
        public string? Country { get; set; }
        public string? Phone { get; set; }
        public string? Fax { get; set; }
        [BsonRepresentation(BsonType.DateTime)]
        public DateTimeOffset? Created { get; set; }
    }
}

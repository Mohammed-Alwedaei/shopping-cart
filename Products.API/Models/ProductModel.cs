using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Products.API.Models;

public class ProductModel
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public decimal Price { get; set; }

    public string CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }
}
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BookStore.Web.Entities;

public class Book
{
    public Book()
    {
        AddedOn = DateTime.UtcNow;
    }

    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    [BsonElement("Name")]
    public string Name { get; set; }

    [BsonElement("Description")]
    public string Description { get; set; }

    [BsonElement("ISBN")]
    public string ISBN { get; set; }

    [BsonElement("Price")]
    public double Price { get; set; }

    [BsonElement("AuthorName")]
    public string AuthorName { get; set; }

    [BsonElement("AddedOn")]
    public DateTime AddedOn { get; set; }
}

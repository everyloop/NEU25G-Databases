using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

[BsonIgnoreExtraElements]
class Movie()
{
    [BsonId]
    public ObjectId Id { get; set; }

    [BsonElement("title")]
    public string Title { get; set; }

    [BsonElement("year")]
    public int Year { get; set; }

    [BsonElement("imdb")]
    public IMDB IMDB { get; set; }
}

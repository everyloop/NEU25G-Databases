using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

[BsonIgnoreExtraElements]
class IMDB()
{
    [BsonElement("rating")]
    public double Rating { get; set; }

    [BsonElement("votes")]
    public int Votes { get; set; }
}
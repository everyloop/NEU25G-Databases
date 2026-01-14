
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Driver;


//var doc = new BsonDocument { 
//    { "FirstName", "Fredrik" },
//    { "LastName", "Johansson" },
//    { "Contacts", new BsonDocument { 
//        { "Phone", "070234768764" }, 
//        { "Email", "fredrik@gmail.com" } 
//    } },
//    { "List", new BsonArray { 1, 4, 8, "Hello", new BsonDocument { { "Key", "Value" } } } }
//};

//doc.Add("Color", "Blue");

//doc.Set("FirstName", "Anders");

//doc.Remove("LastName");

//doc["FirstName"] = "Karl";

var jsonSettings = new JsonWriterSettings() { Indent = true };

//Console.WriteLine(doc.ToJson(jsonSettings));


//Console.Write("\nEnter key: ");
//var myKey = Console.ReadLine();

//if (doc.TryGetValue(myKey, out BsonValue myValue))
//{
//    Console.WriteLine(myValue);
//}
//else
//{
//    Console.WriteLine($"The key '{myKey}' does not exists.");
//}



var connectionString = "mongodb+srv://fredrik:fredrik@cluster0.9zffbjc.mongodb.net/";

var client = new MongoClient(connectionString);

var movieCollection = client.GetDatabase("sample_mflix").GetCollection<BsonDocument>("movies");

var filter = Builders<BsonDocument>.Filter.Regex("title", "/matrix/i");
var projection = Builders<BsonDocument>.Projection
    .Include("title")
    .Include("year")
    .Include("plot")
    .Include("imdb.rating")
    .Exclude("_id");

var movies = movieCollection.Find(filter).Project(projection).ToList();

foreach (var movie in movies)
{
    Console.WriteLine(new string('*', 100));
    Console.WriteLine(movie.ToJson(jsonSettings));
}



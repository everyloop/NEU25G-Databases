
using MongoDB.Driver;

var connectionString = "mongodb+srv://fredrik:fredrik@cluster0.9zffbjc.mongodb.net/";

var client = new MongoClient(connectionString);

Console.WriteLine($"Databases: {string.Join(", ", client.ListDatabaseNames().ToList())}");

var database = client.GetDatabase("sample_mflix");

Console.WriteLine($"\nCollections in database sample_mflix: {string.Join(", ", database.ListCollectionNames().ToList())}");

var collection = database.GetCollection<Movie>("movies");

var filter = Builders<Movie>.Filter.Empty;

var result = collection.Find(filter).Limit(10).ToList();

Console.WriteLine();

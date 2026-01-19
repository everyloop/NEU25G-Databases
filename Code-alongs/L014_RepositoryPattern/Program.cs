
using L014_RepositoryPattern.Domain;
using L014_RepositoryPattern.Repositories.Mongo.Mapping;
using MongoDB.Driver;

MongoMappings.Register();

var client = new MongoClient("mongodb+srv://fredrik:fredrik@cluster0.9zffbjc.mongodb.net/");
var database = client.GetDatabase("sample_mflix");

var collection = database.GetCollection<Movie>("movies");

var movie = await collection.Find(Builders<Movie>.Filter.Empty).FirstOrDefaultAsync();

Console.WriteLine($"{movie.Title} ({movie.Year})");
Console.WriteLine(movie.Plot);


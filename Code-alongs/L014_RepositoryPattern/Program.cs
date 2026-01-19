
using L014_RepositoryPattern.Domain;
using L014_RepositoryPattern.Repositories.Interfaces;
using L014_RepositoryPattern.Repositories.Mongo;
using L014_RepositoryPattern.Repositories.Mongo.Mapping;
using MongoDB.Bson;
using MongoDB.Driver;

MongoMappings.Register();

var client = new MongoClient("mongodb+srv://fredrik:fredrik@cluster0.9zffbjc.mongodb.net/");
var database = client.GetDatabase("sample_mflix");

//var collection = database.GetCollection<Movie>("movies");

//var movie = await collection.Find(Builders<Movie>.Filter.Empty).FirstOrDefaultAsync();


IRepository<Movie, ObjectId> repository = new MongoRepository<Movie, ObjectId>(database, "movies");

var movie = await repository.GetByIdAsync(new ObjectId("573a1392f29313caabcd9a10"));
Console.WriteLine($"{movie.Title} ({movie.Year})");
Console.WriteLine(movie.Plot);


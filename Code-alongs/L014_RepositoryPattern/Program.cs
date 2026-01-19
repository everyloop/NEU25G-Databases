
using L014_RepositoryPattern.Domain;
using L014_RepositoryPattern.Repositories.Interfaces;
using L014_RepositoryPattern.Repositories.Mongo;
using L014_RepositoryPattern.Repositories.Mongo.Mapping;
using L014_RepositoryPattern.Repositories.Sql;
using L014_RepositoryPattern.Repositories.Sql.Config;
using MongoDB.Bson;
using MongoDB.Driver;

MongoMappings.Register();

var client = new MongoClient("mongodb+srv://fredrik:fredrik@cluster0.9zffbjc.mongodb.net/");
var database = client.GetDatabase("sample_mflix");

//var collection = database.GetCollection<Movie>("movies");

//var movie = await collection.Find(Builders<Movie>.Filter.Empty).FirstOrDefaultAsync();


IMovieRepository repository = new MongoMovieRepository(database);

//var movie = await repository.GetByIdAsync(new ObjectId("573a1392f29313caabcd9a10"));
//var movie = await repository.GetByImdbIdAsync(133093);
//Console.WriteLine($"{movie.Title} ({movie.Year})");
//Console.WriteLine(movie.Plot);

//var newMovie = new Movie() { Id = ObjectId.GenerateNewId(), Title = "mongo", Imdb = new ImdbInfo() };
//await repository.AddAsync(newMovie);
//newMovie.Plot = "A movie about mongodb.";
//await repository.ReplaceAsync(newMovie);
//await repository.RemoveAsync(newMovie);

//var topRatedMovies = await repository.GetTopRatedMoviesAsync(5, 1995);

//foreach (var movie in topRatedMovies)
//{
//    Console.WriteLine($"{movie.Title} - {movie.Imdb.Rating}");
//}

using var db = new MovieDbContext();

var sqlRepository = new SqlMovieRepository(db);


Console.Write("\nSearch: ");
string query = Console.ReadLine();

var searchResult = await repository.SearchByTitleAsync(query);

foreach (var movie in searchResult)
{
    Console.Write($"{movie.Title} ({movie.Year}) - ");

    var existing = await sqlRepository.GetByIdAsync(movie.Id);

    if (existing is null)
    {
        await sqlRepository.AddAsync(movie);
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Copied!");
    }
    else
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Skipped!");
    }

    Console.ResetColor();
}


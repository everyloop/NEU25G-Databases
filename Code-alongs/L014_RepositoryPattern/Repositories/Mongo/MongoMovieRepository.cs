using L014_RepositoryPattern.Domain;
using L014_RepositoryPattern.Repositories.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Text.RegularExpressions;

namespace L014_RepositoryPattern.Repositories.Mongo;

internal class MongoMovieRepository : MongoRepository<Movie, ObjectId>, IMovieRepository
{
    public MongoMovieRepository(IMongoDatabase database) : base(database, "movies")
    {        
    }

    public async Task<Movie?> GetByImdbIdAsync(int imdbId)
    {
        var filter = Builders<Movie>.Filter.Eq(m => m.Imdb.ImdbId, imdbId);
        return await _collection.Find(filter).FirstOrDefaultAsync();
    }

    public async Task<IReadOnlyList<Movie>> GetTopRatedMoviesAsync(int count, int? year = null)
    {
        var filter = year is null
            ? Builders<Movie>.Filter.Empty
            : Builders<Movie>.Filter.Eq(m => m.Year, year);

        return await _collection
            .Find(filter)
            .SortByDescending(m => m.Imdb.Rating)
            .Limit(count)
            .ToListAsync();
    }

    public async Task<IReadOnlyList<Movie>> SearchByTitleAsync(string query, int limit = 20)
    {
        var escapedQuery = Regex.Escape(query);
        var regex = new Regex(escapedQuery, RegexOptions.IgnoreCase);

        var filter = Builders<Movie>.Filter.Regex(m => m.Title, regex);

        return await _collection
            .Find(filter)
            .SortBy(m => m.Year)
            .Limit(limit)
            .ToListAsync();
    }
}

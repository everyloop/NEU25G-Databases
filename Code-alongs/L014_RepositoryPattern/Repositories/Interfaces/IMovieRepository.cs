using L014_RepositoryPattern.Domain;
using MongoDB.Bson;

namespace L014_RepositoryPattern.Repositories.Interfaces;

internal interface IMovieRepository : IRepository<Movie, ObjectId>
{
    Task<Movie?> GetByImdbIdAsync(int imdbId);
    Task<IReadOnlyList<Movie>> SearchByTitleAsync(string query, int limit = 20);
    Task<IReadOnlyList<Movie>> GetTopRatedMoviesAsync(int count, int? year = null);
}

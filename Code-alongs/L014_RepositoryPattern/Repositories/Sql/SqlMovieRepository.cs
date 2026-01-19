using L014_RepositoryPattern.Domain;
using L014_RepositoryPattern.Repositories.Interfaces;
using L014_RepositoryPattern.Repositories.Mongo;
using L014_RepositoryPattern.Repositories.Sql.Config;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;

namespace L014_RepositoryPattern.Repositories.Sql;

internal class SqlMovieRepository : SqlRepository<Movie, ObjectId>, IMovieRepository
{
    public SqlMovieRepository(MovieDbContext context) : base(context)
    {        
    }

    public async Task<Movie?> GetByImdbIdAsync(int imdbId)
    {
        return await _context.Movies.FirstOrDefaultAsync(m => m.Imdb.ImdbId == imdbId);
    }

    public async Task<IReadOnlyList<Movie>> GetTopRatedMoviesAsync(int count, int? year = null)
    {
        var query = _context.Movies.AsQueryable();

        if (year is not null) query = query.Where(m => m.Year == year);

        return await query
            .OrderByDescending(m => m.Imdb.Rating)
            .Take(count)
            .ToListAsync();
    }

    public async Task<IReadOnlyList<Movie>> SearchByTitleAsync(string query, int limit = 20)
    {
        return await _context.Movies
            .Where(m => EF.Functions.Like(m.Title, $"%{query}%"))
            .OrderBy(m => m.Year)
            .Take(limit)
            .ToListAsync();
    }
}

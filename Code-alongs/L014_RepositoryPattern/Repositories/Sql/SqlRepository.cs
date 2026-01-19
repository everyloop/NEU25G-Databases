using L014_RepositoryPattern.Domain;
using L014_RepositoryPattern.Repositories.Interfaces;
using L014_RepositoryPattern.Repositories.Sql.Config;
using Microsoft.EntityFrameworkCore;

namespace L014_RepositoryPattern.Repositories.Sql;

internal abstract class SqlRepository<TEntity, TId> : IRepository<TEntity, TId> where TEntity : class, IHasId<TId>
{
    protected readonly MovieDbContext _context;
    protected readonly DbSet<TEntity> _set;

    protected SqlRepository(MovieDbContext context)
    {
        _context = context;
        _set = context.Set<TEntity>();
    }

    public async Task<TEntity?> GetByIdAsync(TId id)
    {
        return await _set.FirstOrDefaultAsync(e => e.Id.Equals(id));
    }

    public async Task AddAsync(TEntity entity)
    {
        _set.Add(entity);
        await _context.SaveChangesAsync();
    }

    public async Task RemoveAsync(TEntity entity)
    {
        _set.Remove(entity);
        await _context.SaveChangesAsync();
    }

    public async Task ReplaceAsync(TEntity entity)
    {
        _set.Update(entity);
        await _context.SaveChangesAsync();
    }
}

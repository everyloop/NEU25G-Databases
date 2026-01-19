using L014_RepositoryPattern.Domain;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MongoDB.Bson;

namespace L014_RepositoryPattern.Repositories.Sql.Config;

internal class MovieDbContext : DbContext
{
    public DbSet<Movie> Movies { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = new SqlConnectionStringBuilder()
        {
            ServerSPN = "localhost",
            InitialCatalog = "MovieDB",
            TrustServerCertificate = true,
            IntegratedSecurity = true
        }.ToString();

        optionsBuilder.UseSqlServer(connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var objectIdToStringConverter = new ValueConverter<ObjectId, string>(
            v => v.ToString(),
            v => ObjectId.Parse(v));
        
        modelBuilder.Entity<Movie>(entity =>
        {
            entity.ToTable("Movies");

            entity.HasKey(m => m.Id);

            entity.Property(m => m.Id)
                .HasConversion(objectIdToStringConverter)
                .HasColumnType("char(24)")
                .IsRequired();

            entity.Property(m => m.Title)
                .HasMaxLength(500)
                .IsRequired();

            entity.Property(m => m.Year);

            entity.Property(m => m.Length);

            entity.Property(m => m.Plot)
                .HasColumnType("nvarchar(max)");

            entity.OwnsOne(m => m.Imdb, imdb =>
            {
                imdb.Property(i => i.ImdbId).HasColumnName("ImdbId");
                imdb.Property(i => i.Rating).HasColumnName("ImdbRating");
                imdb.Property(i => i.Votes).HasColumnName("ImdbVotes");
            });

        });
    }
}

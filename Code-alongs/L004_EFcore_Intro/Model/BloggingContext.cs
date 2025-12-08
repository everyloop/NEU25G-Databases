using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L004_EFcore_Intro.Model;

internal class BloggingContext : DbContext
{
    public DbSet<Blog> Blogs { get; set; }
    public DbSet<Post> Posts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // var connectionString = "Data Source=localhost;Database=everyloop;Integrated Security=True;TrustServerCertificate=True;";

        var connectionString = new SqlConnectionStringBuilder()
        {
            ServerSPN = "localhost",
            InitialCatalog = "BlogDB",
            TrustServerCertificate = true,
            IntegratedSecurity = true,
        }.ToString();

        optionsBuilder.UseSqlServer(connectionString);
    }
}

public class Blog
{
    public int Id { get; set; }
    public string? Url { get; set; }
    public int? RatingRenamed { get; set; }
    public List<Post> Posts { get; set; }
}

public class Post
{
    public int Id { get; set; }
    public string? Caption { get; set; }
    public string? Body { get; set; }
    public string? Author { get; set; }
    public DateTime CreatedAt { get; set; }
    public int BlogId { get; set; }
    public Blog Blog { get; set; }
}




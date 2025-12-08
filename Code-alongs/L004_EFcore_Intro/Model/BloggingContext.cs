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
    public string Url { get; set; }
    public int Rating { get; set; }
}



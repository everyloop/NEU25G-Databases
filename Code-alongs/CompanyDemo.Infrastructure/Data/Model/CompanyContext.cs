using CompanyDemo.Domain;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Reflection.Emit;

namespace CompanyDemo.Infrastructure.Data.Model;

public partial class CompanyContext : DbContext
{
    public CompanyContext()
    {
    }

    public CompanyContext(DbContextOptions<CompanyContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<EmployeeTerritory> EmployeeTerritories { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Region> Regions { get; set; }

    public virtual DbSet<Supplier> Suppliers { get; set; }

    public virtual DbSet<Territory> Territories { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var config = new ConfigurationBuilder().AddUserSecrets<CompanyContext>().Build();

        var connectionString = new SqlConnectionStringBuilder()
        {
            ServerSPN = config["ServerName"],
            InitialCatalog = config["DatabaseName"],
            TrustServerCertificate = true,
            IntegratedSecurity = true
        }.ToString();
        
        //var connectionString = config["ConnectionString"];
        
        optionsBuilder.UseSqlServer(connectionString);
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("Finnish_Swedish_CI_AS");

        new CustomerEntityTypeConfiguration().Configure(modelBuilder.Entity<Customer>());
        new EmployeeEntityTypeConfiguration().Configure(modelBuilder.Entity<Employee>());
        new OrderEntityTypeConfiguration().Configure(modelBuilder.Entity<Order>());
        new OrderDetailEntityTypeConfiguration().Configure(modelBuilder.Entity<OrderDetail>());
        new ProductEntityTypeConfiguration().Configure(modelBuilder.Entity<Product>());
        new RegionEntityTypeConfiguration().Configure(modelBuilder.Entity<Region>());
        new SupplierEntityTypeConfiguration().Configure(modelBuilder.Entity<Supplier>());
        new TerritoryEntityTypeConfiguration().Configure(modelBuilder.Entity<Territory>());

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

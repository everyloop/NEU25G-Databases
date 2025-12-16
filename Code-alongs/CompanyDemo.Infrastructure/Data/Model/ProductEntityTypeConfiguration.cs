using CompanyDemo.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CompanyDemo.Infrastructure.Data.Model;

public class ProductEntityTypeConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(e => e.Id).HasName("PK_Products");

        builder.ToTable("products", "company");

        builder.Property(e => e.Id).ValueGeneratedNever();
        builder.Property(e => e.ProductName).HasMaxLength(100);
        builder.Property(e => e.QuantityPerUnit).HasMaxLength(50);

        builder.HasOne(d => d.Category).WithMany(p => p.Products)
            .HasForeignKey(d => d.CategoryId)
            .HasConstraintName("FK_Products_Categories");

        builder.HasOne(d => d.Supplier).WithMany(p => p.Products)
            .HasForeignKey(d => d.SupplierId)
            .HasConstraintName("FK_Products_Suppliers");
    }
}

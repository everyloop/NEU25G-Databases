using CompanyDemo.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CompanyDemo.Infrastructure.Data.Model;

public class CategoryEntityTypeConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasKey(e => e.Id).HasName("PK_Categories");

        builder.ToTable("categories", "company");

        builder.Property(e => e.Id).ValueGeneratedNever();
        builder.Property(e => e.CategoryName).HasMaxLength(50);
        builder.Property(e => e.Description).HasMaxLength(100);
    }
}

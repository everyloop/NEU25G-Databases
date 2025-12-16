using CompanyDemo.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CompanyDemo.Infrastructure.Data.Model;

public class TerritoryEntityTypeConfiguration : IEntityTypeConfiguration<Territory>
{
    public void Configure(EntityTypeBuilder<Territory> builder)
    {
        builder.HasKey(e => e.Id).HasName("PK_Territories");

        builder.ToTable("territories", "company");

        builder.Property(e => e.Id).ValueGeneratedNever();
        builder.Property(e => e.TerritoryDescription).HasMaxLength(50);

        builder.HasOne(d => d.Region).WithMany(p => p.Territories)
            .HasForeignKey(d => d.RegionId)
            .HasConstraintName("FK_Territories_Regions");
    }
}
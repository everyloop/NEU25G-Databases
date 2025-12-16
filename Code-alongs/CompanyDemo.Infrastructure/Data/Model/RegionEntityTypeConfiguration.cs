using CompanyDemo.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CompanyDemo.Infrastructure.Data.Model;

public class RegionEntityTypeConfiguration : IEntityTypeConfiguration<Region>
{
    public void Configure(EntityTypeBuilder<Region> builder)
    {
        builder.HasKey(e => e.Id).HasName("PK_Regions");

        builder.ToTable("regions", "company");

        builder.Property(e => e.Id).ValueGeneratedNever();
        builder.Property(e => e.RegionDescription).HasMaxLength(20);
    }
}

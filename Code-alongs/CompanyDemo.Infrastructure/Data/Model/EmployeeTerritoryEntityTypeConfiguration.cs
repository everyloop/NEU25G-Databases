using CompanyDemo.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CompanyDemo.Infrastructure.Data.Model;

public class EmployeeTerritoryEntityTypeConfiguration : IEntityTypeConfiguration<EmployeeTerritory>
{
    public void Configure(EntityTypeBuilder<EmployeeTerritory> builder)
    {
        builder.HasKey(e => e.Id).HasName("PK_EmployeeTerritory");

        builder.ToTable("employee_territory", "company");

        builder.Property(e => e.Id).HasMaxLength(7);

        builder.HasOne(d => d.Employee).WithMany(p => p.EmployeeTerritories)
            .HasForeignKey(d => d.EmployeeId)
            .HasConstraintName("FK_EmployeeTerritory_Employees");

        builder.HasOne(d => d.Territory).WithMany(p => p.EmployeeTerritories)
            .HasForeignKey(d => d.TerritoryId)
            .HasConstraintName("FK_EmployeeTerritory_Territories");
    }
}

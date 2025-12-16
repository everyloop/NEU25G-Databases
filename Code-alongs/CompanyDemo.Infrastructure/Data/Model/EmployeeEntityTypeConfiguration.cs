using CompanyDemo.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CompanyDemo.Infrastructure.Data.Model;

public class EmployeeEntityTypeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.HasKey(e => e.Id).HasName("PK_Employees");

        builder.ToTable("employees", "company");

        builder.Property(e => e.Id).ValueGeneratedNever();
        builder.Property(e => e.Address).HasMaxLength(50);
        builder.Property(e => e.BirthDate).HasMaxLength(10);
        builder.Property(e => e.City).HasMaxLength(20);
        builder.Property(e => e.Country).HasMaxLength(10);
        builder.Property(e => e.Extension).HasMaxLength(10);
        builder.Property(e => e.FirstName).HasMaxLength(20);
        builder.Property(e => e.HireDate).HasMaxLength(10);
        builder.Property(e => e.HomePhone).HasMaxLength(50);
        builder.Property(e => e.LastName).HasMaxLength(20);
        builder.Property(e => e.Notes).HasMaxLength(500);
        builder.Property(e => e.PhotoPath).HasMaxLength(100);
        builder.Property(e => e.PostalCode).HasMaxLength(20);
        builder.Property(e => e.Region).HasMaxLength(13);
        builder.Property(e => e.Title).HasMaxLength(50);
        builder.Property(e => e.TitleOfCourtesy).HasMaxLength(10);
    }
}

using CompanyDemo.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CompanyDemo.Infrastructure.Data.Model;

public class OrderEntityTypeConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(e => e.Id).HasName("PK_Orders");

        builder.ToTable("orders", "company");

        builder.Property(e => e.Id).ValueGeneratedNever();
        builder.Property(e => e.CustomerId).HasMaxLength(10);
        builder.Property(e => e.OrderDate).HasMaxLength(10);
        builder.Property(e => e.RequiredDate).HasMaxLength(10);
        builder.Property(e => e.ShipAddress).HasMaxLength(100);
        builder.Property(e => e.ShipCity).HasMaxLength(50);
        builder.Property(e => e.ShipCountry).HasMaxLength(50);
        builder.Property(e => e.ShipName).HasMaxLength(100);
        builder.Property(e => e.ShipPostalCode).HasMaxLength(20);
        builder.Property(e => e.ShipRegion).HasMaxLength(50);
        builder.Property(e => e.ShippedDate).HasMaxLength(10);

        builder.HasOne(d => d.Customer).WithMany(p => p.Orders)
            .HasForeignKey(d => d.CustomerId)
            .HasConstraintName("FK_Orders_Customers");

        builder.HasOne(d => d.Employee).WithMany(p => p.Orders)
            .HasForeignKey(d => d.EmployeeId)
            .HasConstraintName("FK_Orders_Employees");
    }
}

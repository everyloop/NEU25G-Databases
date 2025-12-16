using CompanyDemo.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CompanyDemo.Infrastructure.Data.Model;

public class OrderDetailEntityTypeConfiguration : IEntityTypeConfiguration<OrderDetail>
{
    public void Configure(EntityTypeBuilder<OrderDetail> builder)
    {
        builder.HasKey(e => e.Id).HasName("PK_OrderDetails");

        builder.ToTable("order_details", "company");

        builder.Property(e => e.Id).HasMaxLength(10);

        builder.HasOne(d => d.Order).WithMany(p => p.OrderDetails)
            .HasForeignKey(d => d.OrderId)
            .HasConstraintName("FK_OrderDetails_Orders");

        builder.HasOne(d => d.Product).WithMany(p => p.OrderDetails)
            .HasForeignKey(d => d.ProductId)
            .HasConstraintName("FK_OrderDetails_Products");
    }
}

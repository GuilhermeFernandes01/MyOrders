using MyOrders.Domain.Models;

namespace MyOrders.Infrastructure.Mappings;

public class OrderEntityConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("Orders");
        builder.HasKey(o => o.OrderId);
        builder.Property(p => p.ProductName).IsRequired();
        builder.Property(p => p.Quantity).IsRequired();
    }
}
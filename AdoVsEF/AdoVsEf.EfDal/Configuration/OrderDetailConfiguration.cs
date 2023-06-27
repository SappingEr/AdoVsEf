using AdoVsEf.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdoVsEf.EfDal.Configuration
{
    internal class OrderDetailConfiguration : IEntityTypeConfiguration<OrderDetail>
    {
        public void Configure(EntityTypeBuilder<OrderDetail> builder)
        {
            builder.HasKey(d => new { d.OrderId, d.ProductId }).HasName("PK_Order_Details");
            builder.ToTable("Order Details");
            builder.HasIndex(d => d.OrderId, "OrderID");
            builder.HasIndex(d => d.OrderId, "OrdersOrder_Details");
            builder.HasIndex(d => d.ProductId, "ProductID");
            builder.HasIndex(d => d.ProductId, "ProductsOrder_Details");
            builder.Property(d => d.OrderId).HasColumnName("OrderID");
            builder.Property(d => d.ProductId).HasColumnName("ProductID");
            builder.Property(d => d.Quantity).HasDefaultValueSql("((1))");
            builder.Property(d => d.UnitPrice).HasColumnType("money");
            builder.HasOne(d => d.Order)
                .WithMany(o => o.OrderDetails)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Order_Details_Orders");
            builder.HasOne(d => d.Product)
                .WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Order_Details_Products");
        }
    }
}

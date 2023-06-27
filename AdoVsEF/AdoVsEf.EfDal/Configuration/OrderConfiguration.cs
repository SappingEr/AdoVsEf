using AdoVsEf.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdoVsEf.EfDal.Configuration
{
    internal class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasIndex(o => o.CustomerId, "CustomerID");
            builder.HasIndex(o => o.CustomerId, "CustomersOrders");
            builder.HasIndex(o => o.EmployeeId, "EmployeeID");
            builder.HasIndex(o => o.EmployeeId, "EmployeesOrders");
            builder.HasIndex(o => o.OrderDate, "OrderDate");
            builder.HasIndex(o => o.ShipPostalCode, "ShipPostalCode");
            builder.HasIndex(o => o.ShippedDate, "ShippedDate");
            builder.HasIndex(o => o.ShipVia, "ShippersOrders");
            builder.Property(o => o.OrderId).HasColumnName("OrderID");
            builder.Property(o => o.CustomerId).HasMaxLength(5).HasColumnName("CustomerID").IsFixedLength(true);
            builder.Property(o => o.EmployeeId).HasColumnName("EmployeeID");
            builder.Property(o => o.Freight).HasColumnType("money").HasDefaultValueSql("((0))");
            builder.Property(o => o.OrderDate).HasColumnType("datetime");
            builder.Property(o => o.RequiredDate).HasColumnType("datetime");
            builder.Property(o => o.ShipAddress).HasMaxLength(60);
            builder.Property(o => o.ShipCity).HasMaxLength(15);
            builder.Property(o => o.ShipCountry).HasMaxLength(15);
            builder.Property(o => o.ShipName).HasMaxLength(40);
            builder.Property(o => o.ShipPostalCode).HasMaxLength(10);
            builder.Property(o => o.ShipRegion).HasMaxLength(15);
            builder.Property(o => o.ShippedDate).HasColumnType("datetime");
            builder.HasOne(o => o.Customer)
                .WithMany(c => c.Orders)
                .HasForeignKey(o => o.CustomerId)
                .HasConstraintName("FK_Orders_Customers");
            builder.HasOne(o => o.Employee)
                .WithMany(e => e.Orders)
                .HasForeignKey(o => o.EmployeeId)
                .HasConstraintName("FK_Orders_Employees");
        }
    }
}

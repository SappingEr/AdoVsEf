using AdoVsEf.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdoVsEf.EfDal.Configuration
{
    internal class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasIndex(p => p.CategoryId, "CategoriesProducts");
            builder.HasIndex(p => p.CategoryId, "CategoryID");
            builder.HasIndex(p => p.ProductName, "ProductName");
            builder.HasIndex(p => p.SupplierId, "SupplierID");
            builder.HasIndex(p => p.SupplierId, "SuppliersProducts");
            builder.Property(p => p.ProductId).HasColumnName("ProductID");
            builder.Property(p => p.CategoryId).HasColumnName("CategoryID");
            builder.Property(p => p.ProductName).IsRequired().HasMaxLength(40);
            builder.Property(p => p.QuantityPerUnit).HasMaxLength(20);
            builder.Property(p => p.ReorderLevel).HasDefaultValueSql("((0))");
            builder.Property(p => p.SupplierId).HasColumnName("SupplierID");
            builder.Property(p => p.UnitPrice).HasColumnType("money").HasDefaultValueSql("((0))");
            builder.Property(p => p.UnitsInStock).HasDefaultValueSql("((0))");
            builder.Property(p => p.UnitsOnOrder).HasDefaultValueSql("((0))");
            builder.HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId)
                .HasConstraintName("FK_Products_Categories");
            builder.HasOne(p => p.Supplier)
                .WithMany(s => s.Products)
                .HasForeignKey(p => p.SupplierId)
                .HasConstraintName("FK_Products_Suppliers");
        }
    }
}

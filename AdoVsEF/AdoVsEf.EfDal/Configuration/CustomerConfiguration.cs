using AdoVsEf.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdoVsEf.EfDal.Configuration
{
    internal class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasIndex(c => c.City, "City");
            builder.HasIndex(c => c.CompanyName, "CompanyName");
            builder.HasIndex(c => c.PostalCode, "PostalCode");
            builder.HasIndex(c => c.Region, "Region");
            builder.Property(c => c.CustomerId).HasMaxLength(5).HasColumnName("CustomerID").IsFixedLength(true);
            builder.Property(c => c.Address).HasMaxLength(60);
            builder.Property(c => c.City).HasMaxLength(15);
            builder.Property(c => c.CompanyName).IsRequired().HasMaxLength(40);
            builder.Property(c => c.ContactName).HasMaxLength(30);
            builder.Property(c => c.ContactTitle).HasMaxLength(30);
            builder.Property(c => c.Country).HasMaxLength(15);
            builder.Property(c => c.Fax).HasMaxLength(24);
            builder.Property(c => c.Phone).HasMaxLength(24);
            builder.Property(c => c.PostalCode).HasMaxLength(10);
            builder.Property(c => c.Region).HasMaxLength(15);
        }
    }
}

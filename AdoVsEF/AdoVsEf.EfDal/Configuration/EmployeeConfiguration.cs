using AdoVsEf.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdoVsEf.EfDal.Configuration
{
    internal class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasIndex(e => e.LastName, "LastName");
            builder.HasIndex(e => e.PostalCode, "PostalCode");
            builder.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            builder.Property(e => e.Address).HasMaxLength(60);
            builder.Property(e => e.BirthDate).HasColumnType("datetime");
            builder.Property(e => e.City).HasMaxLength(15);
            builder.Property(e => e.Country).HasMaxLength(15);
            builder.Property(e => e.Extension).HasMaxLength(4);
            builder.Property(e => e.FirstName).IsRequired().HasMaxLength(10);
            builder.Property(e => e.HireDate).HasColumnType("datetime");
            builder.Property(e => e.HomePhone).HasMaxLength(24);
            builder.Property(e => e.LastName).IsRequired().HasMaxLength(20);
            builder.Property(e => e.Notes).HasColumnType("ntext");
            builder.Property(e => e.PostalCode).HasMaxLength(10);
            builder.Property(e => e.Region).HasMaxLength(15);
            builder.Property(e => e.Title).HasMaxLength(30);
            builder.Property(e => e.TitleOfCourtesy).HasMaxLength(25);
            builder.HasOne(e => e.HeadEmployee)
                .WithMany(e => e.Hierarchy)
                .HasForeignKey(e => e.ReportsTo)
                .HasConstraintName("FK_Employees_Employees");
        }
    }
}

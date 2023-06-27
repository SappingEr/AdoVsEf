using AdoVsEf.EfDal.Configuration;
using AdoVsEf.Models;
using Microsoft.EntityFrameworkCore;

namespace AdoVsEf.EfDal.Context
{
    public class StoreDbContext : DbContext
    {
        private static readonly Func<StoreDbContext, int, Product?> GetProductById
            = EF.CompileQuery((StoreDbContext context, int id) =>
                context.Set<Product>().FirstOrDefault(x => x.ProductId == id));
        
        public StoreDbContext(DbContextOptions<StoreDbContext> options)
            : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; } = null!;
        public DbSet<Customer> Customers { get; set; } = null!;
        public DbSet<Employee> Employees { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<OrderDetail> OrderDetails { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<Supplier> Suppliers { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new CustomerConfiguration());
            modelBuilder.ApplyConfiguration(new EmployeeConfiguration());
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
            modelBuilder.ApplyConfiguration(new OrderDetailConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new SupplierConfiguration());
        }
        
        public Product? GetProductByIdCompiled(int id) => GetProductById(this, id);
    }
}

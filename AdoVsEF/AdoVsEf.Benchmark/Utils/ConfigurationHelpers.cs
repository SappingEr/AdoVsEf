using AdoVsEf.EfDal.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace AdoVsEf.Benchmark.Utils
{
    internal static class ConfigurationHelpers
    {
        public static StoreDbContext GetStoreDbContext()
        {
            var optionsBuilder = GetDbContextOptionsBuilder();
            return new StoreDbContext(optionsBuilder.Options);
        }

        public static DbContextOptionsBuilder<StoreDbContext> GetDbContextOptionsBuilder()
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
                .Build();

            var connectionString = config.GetConnectionString("DefaultConnection");

            if (string.IsNullOrEmpty(connectionString))
                throw new Exception("Invalid data provider value supplied.");

            var optionsBuilder = new DbContextOptionsBuilder<StoreDbContext>();
            optionsBuilder.UseSqlServer(connectionString);
            return optionsBuilder;
        }

        public static string GetConnectionString()
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
                .Build();

            var connectionString = config.GetConnectionString("DefaultConnection");

            if (!string.IsNullOrEmpty(connectionString))
                return connectionString;

            throw new InvalidOperationException("Invalid data provider value supplied.");
        }
    }
}

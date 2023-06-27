using Microsoft.Extensions.Configuration;

namespace AdoVsEf.Dapper.Tests
{
    public class TestHelpers
    {
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

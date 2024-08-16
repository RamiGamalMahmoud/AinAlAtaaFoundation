using Microsoft.EntityFrameworkCore;

namespace AinAlAtaaFoundation.Data
{
    public class AppDbContextFactory(string connectionString) : IAppDbContextFactory
    {
        public AppDbContext CreateDbContext()
        {
            return new AppDbContext(new DbContextOptionsBuilder()
                .UseSqlite(_connectionString)
                .UseSnakeCaseNamingConvention()
                .Options);
        }

        private readonly string _connectionString = connectionString;
    }
}

using AinAlAtaaFoundation.Data;
using Microsoft.EntityFrameworkCore;

namespace AinAlAtaaFoundation.Tests
{
    public class TestDbContextFactory : IAppDbContextFactory
    {
        public AppDbContext CreateDbContext()
        {
            return new AppDbContext(new DbContextOptionsBuilder()
                .UseSqlite("DataSource = data.db")
                .UseSnakeCaseNamingConvention()
                .Options);
        }
    }
}

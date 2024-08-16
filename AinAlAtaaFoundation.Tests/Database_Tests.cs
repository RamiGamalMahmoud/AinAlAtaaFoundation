using AinAlAtaaFoundation.Data;
using FluentAssertions;

namespace AinAlAtaaFoundation.Tests
{
    [Collection("AppDbFactoryCollection")]
    public class Database_Tests
    {
        public Database_Tests(TestDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        [Fact]
        public void ShouldConnect()
        {
            using (AppDbContext dbContext = _dbContextFactory.CreateDbContext())
            {
                dbContext.Database.CanConnect().Should().BeTrue();
            }
        }

        readonly TestDbContextFactory _dbContextFactory;
    }
}

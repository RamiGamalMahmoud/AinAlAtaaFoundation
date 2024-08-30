using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;

namespace AinAlAtaaFoundation.Data
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            Console.WriteLine(args.Length.ToString());
            DbContextOptionsBuilder<AppDbContext> builder = new DbContextOptionsBuilder<AppDbContext>();
            builder
                .UseSqlite("Data Source = .\\DB\\al_ain.db")
                .UseSnakeCaseNamingConvention();

            AppDbContext dbContext = new AppDbContext(builder.Options);
            return dbContext;
        }
    }
}

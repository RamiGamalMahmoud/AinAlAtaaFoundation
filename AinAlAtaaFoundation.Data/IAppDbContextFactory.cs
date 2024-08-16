namespace AinAlAtaaFoundation.Data
{
    public interface IAppDbContextFactory
    {
        AppDbContext CreateDbContext();
    }
}

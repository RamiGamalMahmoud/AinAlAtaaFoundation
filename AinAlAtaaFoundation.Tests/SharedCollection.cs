using AinAlAtaaFoundation.Data;

namespace AinAlAtaaFoundation.Tests
{
    [CollectionDefinition("AppDbFactoryCollection")]
    public class SharedCollection : ICollectionFixture<TestDbContextFactory>
    {
    }
}

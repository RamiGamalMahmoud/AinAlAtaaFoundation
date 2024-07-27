using AinAlAtaaFoundation.Models;

namespace AinAlAtaaFoundation.Shared
{
    public static class Messages
    {
        public record LoginSuccededMessage(User User);
        public record LoginFailedMessage;
        public record EntityCreated<TEntity>(TEntity Entity);
        public record EntityUpdated<TEntity>(TEntity Entity);
        public record EntityRemoved<TEntity>(TEntity Entity);
    }
}

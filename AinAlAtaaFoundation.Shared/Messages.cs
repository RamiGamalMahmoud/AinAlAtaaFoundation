using AinAlAtaaFoundation.Models;
using CommunityToolkit.Mvvm.Messaging.Messages;

namespace AinAlAtaaFoundation.Shared
{
    public static class Messages
    {
        public record LoginSuccededMessage(User User);
        public record LoginFailedMessage(string UserName);
        public record EntityCreated<TEntity>(TEntity Entity);
        public record EntityUpdated<TEntity>(TEntity Entity);
        public record EntityRemoved<TEntity>(TEntity Entity);

        public class ConfirmRequestMessage(string message) : RequestMessage<bool>
        {
            public string Message { get; } = message;
        }

        public static class Database
        {
            public record BackupMessage;
            public record ResoreMessage;
            public record ResetMessage;
            public class GetCurrentDatabaseVersion : RequestMessage<DatabaseInfo>;
            public record UpdateDatabaseVersionMessage(string Description);
        }

        public static class SettingsMessages
        {
            public class GetSatrtStatusRequestMessage : RequestMessage<bool>;

            public record UpdateStartStatusMessage(bool IsFreshStart);
        }
    }
}

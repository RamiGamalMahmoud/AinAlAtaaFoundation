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
        public record EntityRestored<TEntity>();

        public class ConfirmRequestMessage(string message) : RequestMessage<bool>
        {
            public string Message { get; } = message;
        }

        public static class Application
        {
            public record RestartMessage;
            public record ShutdownMessage;
        }

        public static class Database
        {
            public record BackupMessage;
            public record RestoreMessage(string FileName);
            public record ResetMessage;
            public class GetCurrentDatabaseVersion : AsyncRequestMessage<DatabaseInfo>;
            public record UpdateDatabaseVersionMessage(string Description);
            public record DatabaseBackedupMessage;
        }

        public static class SettingsMessages
        {
            public class GetSatrtStatusRequestMessage : RequestMessage<bool>;
            public class HasBackupFileRequestMessage : RequestMessage<bool>;
            public record UpdateStartStatusMessage(bool IsFreshStart);
        }
    }
}

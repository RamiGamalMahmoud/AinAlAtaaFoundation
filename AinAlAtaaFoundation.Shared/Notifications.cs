namespace AinAlAtaaFoundation.Shared
{
    public static class Notifications
    {
        public record SuccessNotification(string Message);
        public record FailerNotification(string Message);
        public record Notification(string Message);
    }
}

namespace AinAlAtaaFoundation.Features.DisbursementManagement
{
    internal static class Messages
    {
        public record IdChangedMessage;
        public record ClearInputMessage;
        public record MessageInputFinished;
        public record MessageManualInputChanged(bool State);
    }
}

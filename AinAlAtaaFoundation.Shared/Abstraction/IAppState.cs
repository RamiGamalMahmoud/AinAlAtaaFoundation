using AinAlAtaaFoundation.Models;

namespace AinAlAtaaFoundation.Shared.Abstraction
{
    public interface IAppState
    {
        User User { get; }
        string AppDataFolder { get; }
        bool IsFeshStart { get; }
    }
}

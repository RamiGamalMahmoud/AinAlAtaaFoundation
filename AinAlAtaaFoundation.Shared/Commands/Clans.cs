using AinAlAtaaFoundation.Models;
using MediatR;

namespace AinAlAtaaFoundation.Shared.Commands
{
    public static class Clans
    {
        public record CommandShowCreate : IRequest;
        public record CommandShowUpdate(Clan Clan) : IRequest;
    }
}

using AinAlAtaaFoundation.Models;
using MediatR;

namespace AinAlAtaaFoundation.Shared.Commands
{
    public static class Districts
    {
        public record CommandShowCreate : IRequest;
        public record CommandShowUpdate(District District) : IRequest;
    }
}

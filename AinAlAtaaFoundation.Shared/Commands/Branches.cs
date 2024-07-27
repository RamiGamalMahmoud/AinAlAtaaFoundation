using AinAlAtaaFoundation.Models;
using MediatR;

namespace AinAlAtaaFoundation.Shared.Commands
{
    public static class Branches
    {
        public record CommandShowCreate : IRequest;
        public record CommandShowUpdate(Branch Branch) : IRequest;
    }
}

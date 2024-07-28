using AinAlAtaaFoundation.Models;
using MediatR;

namespace AinAlAtaaFoundation.Shared.Commands
{
    public static class Families
    {
        public record CommandShowCreate : IRequest;
        public record CommandShowUpdate(Family Family) : IRequest;

        public record GetFamilyCommand(int Id) : IRequest<Family>;
    }
}

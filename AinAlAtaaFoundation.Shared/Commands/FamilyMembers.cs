using AinAlAtaaFoundation.Models;
using MediatR;

namespace AinAlAtaaFoundation.Shared.Commands
{
    public static class FamilyMembers
    {
        public record CommandShowCreate : IRequest;
        public record CommandShowUpdate(FamilyMember FamilyMember) : IRequest;
    }
}

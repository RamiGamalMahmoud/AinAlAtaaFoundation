using AinAlAtaaFoundation.Models;
using MediatR;

namespace AinAlAtaaFoundation.Shared.Commands
{
    public static class BranchRepresentatives
    {
        public record ShowCreate : IRequest<BranchRepresentative>;
        public record ShowUpdate(BranchRepresentative BranchRepresentative) : IRequest<bool>;
    }
}

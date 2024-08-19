using AinAlAtaaFoundation.Models;
using MediatR;
using System.Collections.Generic;

namespace AinAlAtaaFoundation.Shared.Commands
{
    public static class BranchRepresentatives
    {
        public record CommandGetByClan(Clan Clan) : IRequest<IEnumerable<BranchRepresentative>>;
        public record CommandGetByBranch(Branch Branch) : IRequest<IEnumerable<BranchRepresentative>>;
    }
}

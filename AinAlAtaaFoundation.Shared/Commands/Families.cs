using AinAlAtaaFoundation.Models;
using MediatR;
using System.Collections.Generic;

namespace AinAlAtaaFoundation.Shared.Commands
{
    public static class Families
    {
        public record GetByRationCardOwner(string RationCardOwnerName) : IRequest<IEnumerable<Family>>;

        public record GetFamilyCommand(int Id) : IRequest<Family>;
        public record GetByRationCard(string RationCard) : IRequest<IEnumerable<Family>>;
    }
}

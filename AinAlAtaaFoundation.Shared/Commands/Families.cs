using AinAlAtaaFoundation.Models;
using MediatR;
using System.Collections.Generic;

namespace AinAlAtaaFoundation.Shared.Commands
{
    public static class Families
    {
        public record GetFamilyCommand(int Id) : IRequest<Family>;
        public record GetByRationCard(string RationCard) : IRequest<IEnumerable<Family>>;
    }
}

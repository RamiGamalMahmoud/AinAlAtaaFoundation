using AinAlAtaaFoundation.Models;
using MediatR;
using System.Collections.Generic;

namespace AinAlAtaaFoundation.Shared.Commands
{
    public static class Branches
    {
        public record CommandGetByClan(Clan Clan) : IRequest<IEnumerable<Branch>>;
    }
}

using AinAlAtaaFoundation.Models;
using MediatR;
using System.Collections.Generic;

namespace AinAlAtaaFoundation.Shared.Commands
{
    public static class DistrictRepresentatives
    {
        public record CommandGetByDistrict(District District) : IRequest<IEnumerable<DistrictRepresentative>>;
    }
}

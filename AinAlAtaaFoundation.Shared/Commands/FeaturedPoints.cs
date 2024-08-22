using AinAlAtaaFoundation.Models;
using MediatR;
using System.Collections.Generic;

namespace AinAlAtaaFoundation.Shared.Commands
{
    public static class FeaturedPoints
    {
        public record CommandGetByDistrict(District District) : IRequest<IEnumerable<FeaturedPoint>>;
    }
}

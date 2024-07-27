using AinAlAtaaFoundation.Models;
using MediatR;

namespace AinAlAtaaFoundation.Shared.Commands
{
    public static class DistrictRepresentatives
    {
        public record ShowCreate : IRequest<DistrictRepresentative> ;
        public record ShowUpdate(DistrictRepresentative DistrictRepresentative) : IRequest<bool>;
    }
}

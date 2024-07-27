using AinAlAtaaFoundation.Models;
using MediatR;

namespace AinAlAtaaFoundation.Shared.Commands
{
    public static class FeaturedPoints
    {
        public record CommandShowCreate : IRequest;
        public record CommandShowUpdate(FeaturedPoint FeaturedPoint) : IRequest;
    }
}

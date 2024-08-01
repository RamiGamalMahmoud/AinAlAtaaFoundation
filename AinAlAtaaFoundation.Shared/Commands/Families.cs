using AinAlAtaaFoundation.Models;
using MediatR;

namespace AinAlAtaaFoundation.Shared.Commands
{
    public static class Families
    {
        public record GetFamilyCommand(int Id) : IRequest<Family>;
    }
}

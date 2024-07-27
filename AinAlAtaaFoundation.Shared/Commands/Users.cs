using AinAlAtaaFoundation.Models;
using MediatR;

namespace AinAlAtaaFoundation.Shared.Commands
{
    public static class Users
    {
        public record GetUserCommand(string UserName, string Password) : IRequest<User>;
    }
}

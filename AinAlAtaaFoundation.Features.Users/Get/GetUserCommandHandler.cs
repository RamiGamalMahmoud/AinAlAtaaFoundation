using AinAlAtaaFoundation.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AinAlAtaaFoundation.Features.Users.Get
{
    internal class GetUserCommandHandler(Repository repository) : IRequestHandler<Shared.Commands.Users.GetUserCommand, User>
    {
        public async Task<User> Handle(Shared.Commands.Users.GetUserCommand request, CancellationToken cancellationToken)
        {
            return await _repository.GetUser(request.UserName, request.Password);
        }
     
        private readonly Repository _repository = repository;
    }
}

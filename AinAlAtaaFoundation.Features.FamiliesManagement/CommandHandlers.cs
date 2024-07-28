using AinAlAtaaFoundation.Models;
using AinAlAtaaFoundation.Shared.Commands;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AinAlAtaaFoundation.Features.FamiliesManagement
{
    internal class CommandHandlerGet(Repository repository) : IRequestHandler<Shared.Commands.Families.GetFamilyCommand, Family>
    {
        public async Task<Family> Handle(Families.GetFamilyCommand request, CancellationToken cancellationToken)
        {
            return await _repository.Get(request.Id);
        }

        private readonly Repository _repository = repository;
    }
}

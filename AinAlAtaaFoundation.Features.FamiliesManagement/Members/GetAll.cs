using AinAlAtaaFoundation.Models;
using AinAlAtaaFoundation.Shared.Commands;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AinAlAtaaFoundation.Features.FamiliesManagement.Members
{
    internal class GetAll(Repository repository) : IRequestHandler<Generic.GetAllCommand<FamilyMember>, IEnumerable<FamilyMember>>
    {
        public async Task<IEnumerable<FamilyMember>> Handle(Generic.GetAllCommand<FamilyMember> request, CancellationToken cancellationToken)
        {
            return await _repository.GetAll(request.Reload);
        }

        private readonly Repository _repository = repository;
    }
}

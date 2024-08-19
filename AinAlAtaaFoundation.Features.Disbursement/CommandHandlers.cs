using AinAlAtaaFoundation.Models;
using AinAlAtaaFoundation.Shared.Commands;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using static AinAlAtaaFoundation.Shared.Commands.Disbursements;

namespace AinAlAtaaFoundation.Features.DisbursementManagement
{
    internal class CommandHandlerGetFamilyById(Repository repository) : IRequestHandler<Shared.Commands.Disbursements.GetByFamilyId, IEnumerable<Disbursement>>
    {
        public async Task<IEnumerable<Disbursement>> Handle(Disbursements.GetByFamilyId request, CancellationToken cancellationToken)
        {
            return await _repository.GetByFamily(request.FamilyId);
        }

        private readonly Repository _repository = repository;
    }

    internal class CommandHandlerGetDisbursements(Repository repository) : IRequestHandler<GetHistory, IEnumerable<Disbursement>>
    {
        public async Task<IEnumerable<Disbursement>> Handle(GetHistory request, CancellationToken cancellationToken)
        {
            return await _repository.GetDisbursementsAsync(request);
        }
        private readonly Repository _repository = repository;
    }

    internal class CommandHandlerGetFamilyByRationCard(Repository repository) : IRequestHandler<Shared.Commands.Disbursements.GetByRationCard, IEnumerable<Disbursement>>
    {
        public async Task<IEnumerable<Disbursement>> Handle(Disbursements.GetByRationCard request, CancellationToken cancellationToken)
        {
            return await _repository.GetByRationCard(request.RationCard);
        }

        private readonly Repository _repository = repository;
    }

    internal static class CommandHandlerCreate
    {
        internal record Command(Disbursement Disbursement) : IRequest<Disbursement>;

        internal class Handler(Repository repository) : IRequestHandler<Command, Disbursement>
        {
            public async Task<Disbursement> Handle(Command request, CancellationToken cancellationToken)
            {
                return await _repository.Create(request.Disbursement);
            }

            private readonly Repository _repository = repository;
        }
    }

    internal static class CommandGetLastTicketNumber
    {
        internal record Command(DateTime Date) : IRequest<int>;

        internal class Handler(Repository repository) : IRequestHandler<Command, int>
        {
            public async Task<int> Handle(Command request, CancellationToken cancellationToken)
            {
                return await _repository.GetLastTicketNumber(request.Date);
            }

            private readonly Repository _repository = repository;
        }
    }
}

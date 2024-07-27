using AinAlAtaaFoundation.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AinAlAtaaFoundation.Features.FamiliesManagement.Print
{
    internal class CommandHandlerShowPrint
    {
        internal record Command(Family Family) : IRequest;

        internal class Handler : IRequestHandler<Command>
        {
            public Task Handle(Command request, CancellationToken cancellationToken)
            {
                View view = new View(request.Family);
                view.Show();
                return Task.CompletedTask;
            }
        }
    }
}

using MediatR;
using System.Collections.Generic;

namespace AinAlAtaaFoundation.Shared.Commands
{
    public static class Generic
    {
        public record GoToManagementCommand;
        public record GoToDisbursementCommand;
        public record GoToHomeCommand;

        public record GetAllCommand<TModel>(bool Reload = false) : IRequest<IEnumerable<TModel>>;
    }
}

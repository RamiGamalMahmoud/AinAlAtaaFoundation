using CommunityToolkit.Mvvm.ComponentModel;
using MediatR;

namespace AinAlAtaaFoundation.Features.DisbursementManagement.Create
{
    internal abstract partial class FamilyGetterBase(IMediator mediator) : ObservableObject
    {
        protected readonly IMediator _mediator = mediator;
        protected int _familyId;
        protected string _rationCard;
    }
}

using AinAlAtaaFoundation.Models;
using MediatR;
using System.Threading.Tasks;

namespace AinAlAtaaFoundation.Features.DisbursementManagement.Create
{
    internal class FamilyGetterByRationCard(IMediator mediator) : FamilyGetterBase(mediator), IFamilyGetter
    {
        public async Task<Disbursement> GetDisbursement()
        {
            return await _mediator.Send(new Shared.Commands.Disbursements.CommandGetLastDisbursementByRationCard(RationCard));
        }

        public async Task<Family> GetFamily()
        {
            Family family = await _mediator.Send(new Shared.Commands.Families.GetByRationCard(RationCard));
            FamilyId = family is not null ? family.Id : 0;
            return family;
        }

        public void Clear()
        {
            FamilyId = 0;
            RationCard = "";
        }

        public int FamilyId
        {
            get => _familyId;
            set
            {
                SetProperty(ref _familyId, value);
            }
        }

        public string RationCard
        {
            get => _rationCard;
            set
            {
                SetProperty(ref _rationCard, value);
                OnPropertyChanged(nameof(RationCard));
            }
        }
    }
}

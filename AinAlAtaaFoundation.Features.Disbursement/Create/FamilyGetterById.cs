using AinAlAtaaFoundation.Models;
using MediatR;
using System.Threading.Tasks;

namespace AinAlAtaaFoundation.Features.DisbursementManagement.Create
{
    internal class FamilyGetterById(IMediator mediator) : FamilyGetterBase(mediator), IFamilyGetter
    {
        public async Task<Disbursement> GetDisbursement()
        {
            return await _mediator.Send(new Shared.Commands.Disbursements.CommandGetLastDisbursementByFamilyId(FamilyId));
        }

        public async Task<Family> GetFamily()
        {
            Family family = await _mediator.Send(new Shared.Commands.Families.GetFamilyCommand(FamilyId));
            RationCard = family is not null ? family.RationCard : "";
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
                OnPropertyChanged(nameof(FamilyId));
            }
        }

        public string RationCard
        {
            get => _rationCard;
            set
            {
                SetProperty(ref _rationCard, value);
            }
        }
    }
}

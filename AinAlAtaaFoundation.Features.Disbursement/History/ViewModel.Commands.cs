using CommunityToolkit.Mvvm.Input;
using System.Threading.Tasks;

namespace AinAlAtaaFoundation.Features.DisbursementManagement.History
{
    internal partial class ViewModel
    {
        [RelayCommand]
        private async Task Filter()
        {
            Disbursements = await _mediator.Send(new Shared.Commands.Disbursements.GetHistory(
                Clan,
                Branch,
                BranchRepresentative,
                District,
                DistrictRepresentative,
                FamilyType,
                SocialStatus,
                OrphanType));
        }
    }
}

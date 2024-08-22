using AinAlAtaaFoundation.Models;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AinAlAtaaFoundation.Features.DisbursementManagement.History
{
    internal partial class ViewModel
    {
        //[RelayCommand]
        //private async Task Filter()
        //{
        //    IEnumerable<Disbursement> disbursements = await _mediator.Send(new Shared.Commands.Disbursements.GetHistory(
        //        TopFilterViewModel.Clan,
        //        TopFilterViewModel.Branch,
        //        TopFilterViewModel.BranchRepresentative,
        //        TopFilterViewModel.District,
        //        TopFilterViewModel.DistrictRepresentative,
        //        TopFilterViewModel.FamilyType,
        //        TopFilterViewModel.SocialStatus,
        //        TopFilterViewModel.OrphanType));
        //    Disbursements = new System.Collections.ObjectModel.ObservableCollection<Disbursement>(disbursements);
        //}

        //[RelayCommand]
        //private async Task Remove(Disbursement disbursement)
        //{
        //    await _mediator.Send(new Shared.Commands.Disbursements.CommandRemove(disbursement));
        //    Disbursements.Remove(disbursement);
        //    _messenger.Send(new Shared.Notifications.SuccessNotification("تم الحذف بنجاح"));
        //}
    }
}

using AinAlAtaaFoundation.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.Generic;

namespace AinAlAtaaFoundation.Features.DisbursementManagement.History
{
    internal partial class ViewModel : ObservableObject
    {
        [ObservableProperty]
        private IEnumerable<Disbursement> _disbursements;
    }
}

using CommunityToolkit.Mvvm.ComponentModel;
using System;

namespace AinAlAtaaFoundation.Models
{
    [ObservableObject]
    public partial class Disbursement : ModelBase
    {
        public DateTime Date { get; set; }
        public int TicketNumber { get; set; }

        public int FamilyId { get; set; }
        public Family Family
        {
            get => _family;
            set => SetProperty(ref _family, value);
        }
        private Family _family;
    }
}

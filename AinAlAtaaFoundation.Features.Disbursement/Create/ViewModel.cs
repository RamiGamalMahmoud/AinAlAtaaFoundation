using AinAlAtaaFoundation.Models;
using AinAlAtaaFoundation.Shared.Commands;
using AinAlAtaaFoundation.Shared.Helpers;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using MediatR;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using static AinAlAtaaFoundation.Features.DisbursementManagement.Messages;

namespace AinAlAtaaFoundation.Features.DisbursementManagement.Create
{
    internal partial class ViewModel : ObservableValidator
    {
        public ViewModel(IMediator mediator, IMessenger messenger)
        {
            Clock = new Clock();
            _mediator = mediator;
            _messenger = messenger;
            _insertionType = InsertionType.Id;
        }

        public bool CanPrint() => Family is not null;

        [RelayCommand(CanExecute = nameof(CanPrint))]
        private async Task ExecutePrint()
        {
            Disbursement disbursement = await Create();
            PrintTicket(disbursement);
            LastFamilyDisbursement = disbursement;

            FamilyId = 0;
            ReadingNumber = 0;
            RationCard = "";
            _messenger.Send<Messages.IdChangedMessage>();
        }

        [ObservableProperty]
        private bool _manualInput;

        [RelayCommand]
        private void SetInputToId()
        {
            _insertionType = InsertionType.Id;
        }

        [RelayCommand]
        private  void SetInputToRationCard()
        {
            _insertionType = InsertionType.RationCard;
        }

        [RelayCommand]
        private void PrintTicket(Disbursement disbursement)
        {
            Dictionary<string, List<string>> parameters = new Dictionary<string, List<string>>
            {
                { "Date", [disbursement.Date.ToString("yyyy-MM-dd")] },
                { "Time", [disbursement.Date.ToString("hh:mm:ss - tt")] },
                { "RationCard", [disbursement.Family.RationCard] },
                { "Name", [disbursement.Family.Applicant.Name] },
                { "TicketNumber", [disbursement.TicketNumber.ToString()] }
            };
            _mediator.Send(new Shared.Commands.Generic.PrintCommand("DisbursementTicket.rdlc", parameters));
        }

        private async Task<Disbursement> Create()
        {
            Disbursement disbursement = new Disbursement
            {
                TicketNumber = ++LastTicketNumber,
                Date = DateTime.Now,
                Family = Family
            };

            await _mediator.Send(new CommandHandlerCreate.Command(disbursement));
            LastFamilyDisbursement = disbursement;
            return disbursement;
        }

        public async Task LoadDataAsync()
        {
            LastTicketNumber = await _mediator.Send(new CommandGetLastTicketNumber.Command(DateTime.Now));
        }

        partial void OnManualInputChanged(bool oldValue, bool newValue)
        {
            _messenger.Send<Messages.IdChangedMessage>();
        }

        protected override async void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(FamilyId) && _insertionType == InsertionType.Id)
            {
                LastFamilyDisbursement = await _mediator.Send(new Shared.Commands.Disbursements.CommandGetLastDisbursementByFamilyId(FamilyId));
                Family = await _mediator.Send(new Shared.Commands.Families.GetFamilyCommand(FamilyId));
                RationCard = Family?.RationCard;
            }

            else if (e.PropertyName == nameof(ReadingNumber) && _insertionType == InsertionType.Id )
            {
                if (ReadingNumber >= 3)
                {
                    if (Family is not null)
                    {
                        Disbursement disbursement = await Create();
                        PrintTicket(disbursement);

                        _messenger.Send<Messages.IdChangedMessage>();
                    }

                    ReadingNumber = 0;
                }
            }

            base.OnPropertyChanged(e);
        }

        [ObservableProperty]
        private int _lastTicketNumber;

        public int FamilyId
        {
            get => _id;
            set
            {
                bool isSame = _id == value;
                SetProperty(ref _id, value);
                ReadingNumber = isSame ? ReadingNumber + 1 : 1;


                if (!ManualInput)
                {
                    _messenger.Send<IdChangedMessage>();
                }
            }
        }

        public int ReadingNumber
        {
            get => _readingNumber;
            private set => SetProperty(ref _readingNumber, value);
        }

        [ObservableProperty]
        private string _rationCard;
        async partial void OnRationCardChanged(string oldValue, string newValue)
        {
            if (_insertionType != InsertionType.RationCard) return;
            LastFamilyDisbursement = await _mediator.Send(new Shared.Commands.Disbursements.CommandGetLastDisbursementByRationCard(newValue));
            IEnumerable<Disbursement> disbursements = await _mediator.Send(new Shared.Commands.Disbursements.GetByRationCard(newValue));

            Family = await _mediator.Send(new Shared.Commands.Families.GetByRationCard(newValue))
                .ContinueWith(x => x
                .Result
                .Where(family => family.RationCard == newValue).FirstOrDefault());
            if(Family is not null)FamilyId = Family.Id;
        }

        public Disbursement LastFamilyDisbursement
        {
            get => _lastFamilyDisbursement;
            private set => SetProperty(ref _lastFamilyDisbursement, value);
        }
        private Disbursement _lastFamilyDisbursement;

        private int _id;
        private int _readingNumber = 0;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(ExecutePrintCommand))]
        private Family _family;
        private readonly IMediator _mediator;
        private readonly IMessenger _messenger;

        public Clock Clock { get; }
        private InsertionType _insertionType = InsertionType.Id;
    }

    enum InsertionType
    {
        Id,
        RationCard
    }
}

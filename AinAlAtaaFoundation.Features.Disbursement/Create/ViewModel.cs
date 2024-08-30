using AinAlAtaaFoundation.Models;
using AinAlAtaaFoundation.Shared.Helpers;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;

namespace AinAlAtaaFoundation.Features.DisbursementManagement.Create
{
    internal partial class ViewModel : ObservableValidator
    {
        public ViewModel(IMediator mediator, IMessenger messenger)
        {
            Clock = new Clock();
            _mediator = mediator;
            _messenger = messenger;
            _familyGetterById = new FamilyGetterById(mediator);
            _familyGetterByRationCard = new FamilyGetterByRationCard(mediator);
            FamilyGetter = _familyGetterById;

            _familyGetterById.PropertyChanged += FamilyGetter_PropertyChanged;
            _familyGetterByRationCard.PropertyChanged += FamilyGetter_PropertyChanged;
        }

        private async void FamilyGetter_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (!ManualInput)
            {
                _messenger.Send<Messages.MessageInputFinished>();
            }
            if (e.PropertyName is nameof(FamilyGetter.RationCard) or nameof(FamilyGetter.FamilyId))
            {
                Family = await _familyGetter.GetFamily();
                LastFamilyDisbursement = await _familyGetter.GetDisbursement();
            }
        }

        public IFamilyGetter FamilyGetter
        {
            get => _familyGetter;
            private set => SetProperty(ref _familyGetter, value);
        }

        public string InputType => !ManualInput ? "إدخال يدوي" : "قارئ البار كود";

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(InputType))]
        private bool _manualInput;
        partial void OnManualInputChanged(bool oldValue, bool newValue)
        {
            _messenger.Send(new Messages.MessageManualInputChanged(ManualInput));
        }

        public int LastTicketNumber
        {
            get => _lastTicketNumber;
            private set => SetProperty(ref _lastTicketNumber, value);
        }
        private int _lastTicketNumber;

        public Disbursement LastFamilyDisbursement
        {
            get => _lastFamilyDisbursement;
            private set => SetProperty(ref _lastFamilyDisbursement, value);
        }
        private Disbursement _lastFamilyDisbursement;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(ExecutePrintCommand))]
        [NotifyCanExecuteChangedFor(nameof(ShowFamilyDisbursementsHistoryCommand))]
        private Family _family;

        private bool HasFamily() => Family is not null;


        [RelayCommand(CanExecute = nameof(HasFamily))]
        private async Task ExecutePrint()
        {
            Disbursement disbursement = await Create(++LastTicketNumber, Family);
            PrintTicket(disbursement);
            LastFamilyDisbursement = disbursement;
            _messenger.Send<Messages.ClearInputMessage>();
        }

        private async Task<Disbursement> Create(int ticketNumber, Family family)
        {
            Disbursement disbursement = new Disbursement
            {
                TicketNumber = ticketNumber,
                Date = DateTime.Now,
                Family = family
            };

            await _mediator.Send(new CommandHandlerCreate.Command(disbursement));
            return disbursement;
        }

        public async Task LoadDataAsync()
        {
            LastTicketNumber = await _mediator.Send(new CommandGetLastTicketNumber.Command(DateTime.Now));
        }

        [RelayCommand]
        private void SetInputToRationCard()
        {
            FamilyGetter = _familyGetterByRationCard;
            FamilyGetter.Clear();
        }

        [RelayCommand]
        private void SetInputToId()
        {
            FamilyGetter = _familyGetterById;
            FamilyGetter.Clear();
        }

        [RelayCommand]
        private void PrintTicket(Disbursement disbursement)
        {
            Dictionary<string, string> keyValues = new Dictionary<string, string>()
            {
                { "Date", disbursement.Date.ToString("yyyy-MM-dd") },
                { "Time", disbursement.Date.ToString("hh:mm:ss - tt") },
                { "RationCard", disbursement.Family.RationCard },
                { "Name", disbursement.Family.Applicant.Name },
                { "TicketNumber", disbursement.TicketNumber.ToString() }
            };
            _mediator.Send(new Shared.Commands.Generic.PrintCommand("DisbursementTicket.rdlc", keyValues));
        }

        [RelayCommand(CanExecute = nameof(HasFamily))]
        private void ShowFamilyDisbursementsHistory(Family family)
        {
            _mediator.Send(new Shared.Commands.Disbursements.ShowFamilyDisbursementsHistory(family));
        }

        private readonly IMediator _mediator;
        private readonly IMessenger _messenger;
        private IFamilyGetter _familyGetter;
        private FamilyGetterById _familyGetterById;
        private FamilyGetterByRationCard _familyGetterByRationCard;

        public Clock Clock { get; }
    }
}

using AinAlAtaaFoundation.Models;
using AinAlAtaaFoundation.Shared.Helpers;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using MediatR;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AinAlAtaaFoundation.Features.DisbursementManagement
{
    internal partial class ViewModel : ObservableValidator
    {
        public ViewModel(IMediator mediator, IMessenger messenger)
        {
            Clock = new Clock();
            _mediator = mediator;
            _messenger = messenger;
        }

        public bool CanPrint() => !HasErrors;

        [RelayCommand(CanExecute = nameof(CanPrint))]
        private void ExecutePrint()
        {
            Print();
            ReadingNumber = 0;
            _messenger.Send<Messages.IdChangedMessage>();
            _messenger.Send<Messages.ClearInputMessage>();
        }

        [ObservableProperty]
        private bool _manualInput;

        private void Print()
        {
        }

        public Task LoadDataAsync()
        {
            return Task.CompletedTask;
        }

        partial void OnManualInputChanged(bool oldValue, bool newValue)
        {
            _messenger.Send<Messages.IdChangedMessage>();
        }

        public int Id
        {
            get => _id;
            set
            {
                if(ManualInput)
                {
                    SetProperty(ref _id, value);
                    return;
                }
                if (value == _id)
                {
                    ReadingNumber++;
                }
                else
                {
                    ReadingNumber = 1;
                }
                SetProperty(ref _id, value);
                _messenger.Send<Messages.IdChangedMessage>();
            }
        }

        public int ReadingNumber
        {
            get => _readingNumber;
            private set
            {
                if (value == 3)
                {
                    SetProperty(ref _readingNumber, 0);
                    Print();
                    return;
                }
                SetProperty(ref _readingNumber, value);
            }
        }

        public IEnumerable<Family> Families { get; }
        public IEnumerable<Disbursement> Disbursements { get; }

        private int _id;
        private int _readingNumber = 0;
        private readonly IMediator _mediator;
        private readonly IMessenger _messenger;

        public Clock Clock { get; }
    }
}

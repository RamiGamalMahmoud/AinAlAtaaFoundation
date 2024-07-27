using AinAlAtaaFoundation.Shared.Helpers;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using MediatR;
using System.Threading.Tasks;

namespace AinAlAtaaFoundation.Shared
{
    public abstract partial class ViewModelBase<TDataModel>(IMediator mediator, IMessenger messenger) : ObservableObject
    {
        public TDataModel DataModel { get; protected set; }

        [RelayCommand(CanExecute = nameof(CanSave))]
        protected abstract Task Save();
        public abstract Task LoadDataAsync();
        public abstract bool CanSave();
        public HasChangesObject HasChangesObject { get; protected set; }
        public DoBusyWorkObject DoBusyWorkObject => new DoBusyWorkObject();

        protected virtual void DataModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            HasChangesObject.SetHaschanges();
        }

        protected readonly IMediator _mediator = mediator;
        protected readonly IMessenger _messenger = messenger;
    }
}

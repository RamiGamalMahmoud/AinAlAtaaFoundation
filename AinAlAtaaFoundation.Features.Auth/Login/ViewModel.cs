using AinAlAtaaFoundation.Models;
using AinAlAtaaFoundation.Shared;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using MediatR;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace AinAlAtaaFoundation.Features.Auth.Login
{
    internal partial class ViewModel : ObservableValidator
    {
        public ViewModel(IMessenger messenger, IMediator mediator)
        {
#if DEBUG
            UserName = "admin";
            Password = "admin";
#endif
            ValidateAllProperties();
            _messenger = messenger;
            _mediator = mediator;
        }

        public DoBusyWorkObject DoBusyWorkObject => _busyWorkObject;

        [ObservableProperty]
        [Required(ErrorMessage = "حقل مطلوب")]
        [NotifyCanExecuteChangedFor(nameof(LoginCommand))]
        [NotifyPropertyChangedFor(nameof(HasErrors))]
        [NotifyDataErrorInfo]
        private string _userName;

        [ObservableProperty]
        [Required(ErrorMessage = "حقل مطلوب")]
        [NotifyPropertyChangedFor(nameof(HasErrors))]
        [NotifyCanExecuteChangedFor(nameof(LoginCommand))]
        [NotifyDataErrorInfo]
        private string _password;

        private readonly IMessenger _messenger;
        private readonly IMediator _mediator;

        [RelayCommand(CanExecute = nameof(CanLogin))]
        private async Task Login()
        {
            using (DoBusyWorkFactory.CreateBusyWork(DoBusyWorkObject))
            {
                User user = await _mediator.Send(new Shared.Commands.Users.GetUserCommand(UserName, Password));
                if (user is not null)
                {
                    _messenger.Send(new Messages.LoginSuccededMessage(user));
                }
                else
                {
                    _messenger.Send(new Messages.LoginFailedMessage(UserName));
                }
            }
        }

        private readonly DoBusyWorkObject _busyWorkObject = new DoBusyWorkObject();

        private bool CanLogin() => !HasErrors;
    }
}

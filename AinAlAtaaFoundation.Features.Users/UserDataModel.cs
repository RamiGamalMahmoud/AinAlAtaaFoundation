using AinAlAtaaFoundation.Models;
using AinAlAtaaFoundation.Shared;
using AinAlAtaaFoundation.Shared.Abstraction;
using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AinAlAtaaFoundation.Features.Users
{
    internal partial class UserDataModel : ObservableValidator, IDataModel<User>
    {
        public UserDataModel(User model)
        {
            if(model is not null)
            {
                Model = model;
                UserName = model.UserName;
                IsActive = (bool) model.IsActive;
                IsAdmin = model.IsAdmin;
                Password = model.Password;
            }
            ValidateAllProperties();
        }
        public bool IsValid => !HasErrors;

        public User Model { get; }

        public void UpdateModel(User model = null)
        {
            model.UserName = UserName;
            model.Password = Password;
            model.IsAdmin = IsAdmin;
            model.IsActive = IsActive;
        }

        [ObservableProperty]
        [Required(ErrorMessage = "حقل مطلوب")]
        [NotifyPropertyChangedFor(nameof(IsValid))]
        [NotifyDataErrorInfo]
        private string _userName;

        [ObservableProperty]
        private bool _isActive = true;

        [ObservableProperty]
        private bool _isAdmin = false;

        [ObservableProperty]
        [Required(ErrorMessage = "حقل مطلوب")]
        [NotifyPropertyChangedFor(nameof(IsValid))]
        [NotifyDataErrorInfo]
        private string _password;

        public void ClearInputs()
        {
            UserName = "";
            Password = "";
            IsActive = false;
            IsAdmin = false;
        }
    }
}

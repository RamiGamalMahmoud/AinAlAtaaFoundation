using AinAlAtaaFoundation.Models;
using AinAlAtaaFoundation.Shared.Abstraction;
using CommunityToolkit.Mvvm.ComponentModel;
using System;

namespace AinAlAtaaFoundation.Features.Users
{
    internal class UserDataModel : ObservableValidator, IDataModel<User>
    {
        public bool IsValid => throw new NotImplementedException();

        public User Model => throw new NotImplementedException();

        public void UpdateModel(User model = null)
        {
            throw new NotImplementedException();
        }
    }
}

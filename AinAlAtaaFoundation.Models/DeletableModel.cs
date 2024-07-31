using CommunityToolkit.Mvvm.ComponentModel;
using System;

namespace AinAlAtaaFoundation.Models
{
    public partial class DeletableModel : ObservableObject
    {
        public DateTime? DeletedAt { get => _deletedAt; private set => SetProperty(ref _deletedAt, value); }
        public bool IsDeleted { get => _isDeleted; private set => SetProperty(ref _isDeleted, value); }

        public void Delete()
        {
            DeletedAt = DateTime.Now;
            IsDeleted = true;
        }

        public void Restore()
        {
            DeletedAt = null;
            IsDeleted = false;
        }

        private DateTime? _deletedAt = null;
        private bool _isDeleted = false;
    }
}

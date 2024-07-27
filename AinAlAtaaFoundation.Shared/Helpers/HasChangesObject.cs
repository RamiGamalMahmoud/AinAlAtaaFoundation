using CommunityToolkit.Mvvm.ComponentModel;
using System;

namespace AinAlAtaaFoundation.Shared.Helpers
{
    public class HasChangesObject : ObservableObject
    {
        public HasChangesObject(Action onChange = null)
        {
            _onChange = onChange;
            SetNotHasChanges();
        }

        public void SetHaschanges() => HasChanges = true;

        public void SetNotHasChanges() => HasChanges = false;

        public bool HasChanges
        {
            get => _hasChanges;
            private set
            {
                SetProperty(ref _hasChanges, value);
                _onChange?.Invoke();
            }
        }

        private readonly Action _onChange;
        private bool _hasChanges;
    }
}

using System.ComponentModel;

namespace AinAlAtaaFoundation.Shared.Abstraction
{
    public interface IDataModel<TModel> : INotifyPropertyChanged, INotifyDataErrorInfo where TModel : class
    {
        public void UpdateModel(TModel model = null);
        public bool IsValid { get; }
        public TModel Model { get; }
    }
}

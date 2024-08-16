using AinAlAtaaFoundation.Data;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace AinAlAtaaFoundation.Shared.Abstraction
{
    public abstract class RepositoryBase<TModel, TDataModel>(IAppDbContextFactory dbContextFactory)
    {
        public abstract Task<TModel> Create(TDataModel dataModel);
        public abstract Task<bool> Update(TDataModel dataModel);

        public abstract Task<IEnumerable<TModel>> GetAll(bool reload = false);

        protected void SetEntities(IEnumerable<TModel> entities)
        {
            _entities = new ObservableCollection<TModel>(entities);
            _isLoaded = true;
        }

        protected readonly IAppDbContextFactory _dbContextFactory = dbContextFactory;
        protected static ObservableCollection<TModel> _entities = [];
        protected static bool _isLoaded;
    }
}

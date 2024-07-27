using AinAlAtaaFoundation.Shared.Abstraction;
using System;

namespace AinAlAtaaFoundation.Shared
{
    public class DoBusyWorkFactory
    {
        public static IDisposable CreateBusyWork(IDoBusyWorkObject doBusyWork) => new BusyWork(doBusyWork);

        protected class BusyWork : IDisposable
        {
            private readonly Action<bool> _busyAction;

            public BusyWork(IDoBusyWorkObject doBusyWork)
            {
                ArgumentNullException.ThrowIfNull(doBusyWork);
                _busyAction = isBusy => doBusyWork.IsBusy = isBusy;
                _busyAction(true);
            }

            public void Dispose() => _busyAction?.Invoke(false);
        }
    }
}

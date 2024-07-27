using AinAlAtaaFoundation.Shared.Helpers;

namespace AinAlAtaaFoundation.Features.Disbursement
{
    internal class ViewModel
    {
        public ViewModel()
        {
            Clock = new Clock();
        }
        public Clock Clock { get; }
    }
}

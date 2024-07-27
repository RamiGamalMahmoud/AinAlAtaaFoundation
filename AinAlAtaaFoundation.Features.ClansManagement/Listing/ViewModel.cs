namespace AinAlAtaaFoundation.Features.ClansManagement.Listing
{
    internal class ViewModel
    {
        public ViewModel()
        {
            Clans = new List<object>()
            {
                new { Id = 1, Name = "عشيرة 1" },
                new { Id = 2, Name = "عشيرة 2" }
            };
        }
        public List<object> Clans { get; }
    }
}

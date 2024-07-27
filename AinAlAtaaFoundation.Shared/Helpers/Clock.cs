using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Windows.Threading;

namespace AinAlAtaaFoundation.Shared.Helpers
{
    public class Clock : ObservableObject
    {
        public Clock()
        {
            _timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };

            _timer.Tick += Timer_Tick;
            _timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            Time = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss tt");
        }

        public string Time
        {
            get => _time;
            set => SetProperty(ref _time, value);
        }
        private string _time;

        private readonly DispatcherTimer _timer;
    }
}

using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Windows.Threading;

namespace AinAlAtaaFoundation.Shared.Helpers
{
    public class Timer : ObservableObject
    {
        public Timer(int hours, int minutes, int seconds, Action action) :this (action)
        {
            SetTime(hours, minutes, seconds);
        }

        public Timer(Action action)
        {
            _action = action;

            _timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };

            Time = new TimeSpan();
            _timer.Tick += TimerTick;
        }

        public void SetTime(int hours, int minutes, int seconds)
        {
            Time = new TimeSpan(hours, minutes, seconds);
        }

        private void TimerTick(object sender, EventArgs e)
        {
            Time -= TimeSpan.FromSeconds(1);

            if (Time <= TimeSpan.FromSeconds(0))
            {
                Stop();
                _action?.Invoke();
            }
        }

        public void Start()
        {
            _timer.Start();
        }
        public void Stop()
        {
            _timer.Stop();
        }

        public TimeSpan Time
        {
            get => _time;
            private set
            {
                SetProperty(ref _time, value);
                OnPropertyChanged(nameof(IsTimerGettingToClose));
            }
        }

        public bool IsTimerGettingToClose => Time.Seconds <= 5;

        private TimeSpan _time;
        private readonly DispatcherTimer _timer;
        private Action _action;
    }
}

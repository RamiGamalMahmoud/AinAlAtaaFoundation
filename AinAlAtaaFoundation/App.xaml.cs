using AinAlAtaaFoundation.Data;
using AinAlAtaaFoundation.Features.Auth;
using AinAlAtaaFoundation.Features.DisbursementManagement;
using AinAlAtaaFoundation.Features.FamiliesManagement;
using AinAlAtaaFoundation.Features.MainWindow;
using AinAlAtaaFoundation.Features.Management;
using AinAlAtaaFoundation.Features.Settings;
using AinAlAtaaFoundation.Features.Users;
using AinAlAtaaFoundation.Shared.Abstraction;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Windows;

namespace AinAlAtaaFoundation
{
    public partial class App : Application
    {
        public App()
        {
            ShutdownMode = ShutdownMode.OnMainWindowClose;
            

            _host = Host
                .CreateDefaultBuilder()
                .ConfigureServices(s =>
                {
                    s.AddSingleton<IMessenger>(WeakReferenceMessenger.Default);
                    s.AddSingleton<AppDbContextFactory>(new AppDbContextFactory("Data Source = .\\DB\\data.db"));
                    s.ConfigureMainWindowFeature();
                    s.ConfigureFamiliesFeature();
                    s.ConfigureManegementFeature();
                    s.ConfigureDisbursementFeature();
                    s.ConfigureUsersFeature();
                    s.ConfigureSettingsFeature();
                    s.ConfigureAuthFeature();
                })
                .Build();
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            Bold.Licensing.BoldLicenseProvider.RegisterLicense("oVgzSnMRENMQOj1LQkrqvqiHS8ja9c4BZ3W18wyYk7M=");
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MzM2Mzc2M0AzMjM2MmUzMDJlMzBjMEkyZXZObXZQQ0FvTXBvOWdTUmQ2Yk1JLzJGZEk4dWhlZmJHUVI1aUUwPQ==;MzM2Mzc2NEAzMjM2MmUzMDJlMzBnS3NqcFRVTXZtbjNLdUdYYUJvZW1KMG1rNlhaS2x6bHlYSWJYU2htVkFBPQ=="); 
            IMessenger messenger = _host.Services.GetRequiredService<IMessenger>();
            _host.Services.GetRequiredService<IAppState>();
            messenger.Register<Shared.Messages.LoginSuccededMessage>(this, (s, m) =>
            {
                Window window = MainWindow;
                MainWindow = _host.Services.GetRequiredService<Features.MainWindow.View>();
                MainWindow.Show();
                window.Close();
            });

            await _host.StartAsync();
            MainWindow = _host.Services.GetRequiredService<ILoginView>() as Window;
            //MainWindow = _host.Services.GetRequiredService<Features.MainWindow.View>();
            MainWindow.Show();
            base.OnStartup(e);
        }

        private bool CanConnect()
        {
            using (AppDbContext dbContext = _host.Services.GetRequiredService<AppDbContextFactory>().CreateDbContext())
            {
                return dbContext.Database.CanConnect();
            }
        }

        private readonly IHost _host;
    }

}

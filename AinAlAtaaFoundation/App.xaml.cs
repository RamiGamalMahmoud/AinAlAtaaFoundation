using AinAlAtaaFoundation.Data;
using AinAlAtaaFoundation.Features.Auth;
using AinAlAtaaFoundation.Features.DisbursementManagement;
using AinAlAtaaFoundation.Features.FamiliesManagement;
using AinAlAtaaFoundation.Features.MainWindow;
using AinAlAtaaFoundation.Features.Management;
using AinAlAtaaFoundation.Features.Settings;
using AinAlAtaaFoundation.Features.Users;
using AinAlAtaaFoundation.Services.Printing;
using AinAlAtaaFoundation.Shared;
using AinAlAtaaFoundation.Shared.Abstraction;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Notification.Wpf;
using System.Threading.Tasks;
using System.Windows;
using Velopack;

namespace AinAlAtaaFoundation
{
    public partial class App : Application
    {
        public App()
        {
            VelopackApp.Build().Run();
            ShutdownMode = ShutdownMode.OnMainWindowClose;

            _host = Host
                .CreateDefaultBuilder()
                .ConfigureServices(s =>
                {
                    s.AddSingleton<IMessenger>(WeakReferenceMessenger.Default);
                    s.AddSingleton<AppDbContextFactory>(new AppDbContextFactory("Data Source = .\\DB\\data.db"));
                    s.ConfigureMainWindowFeature();
                    s.ConfigureFamiliesFeature();
                    s.ConfigureServicePrint();
                    s.ConfigureManegementFeature();
                    s.ConfigureDisbursementFeature();
                    s.ConfigureUsersFeature();
                    s.ConfigureSettingsFeature();
                    s.ConfigureAuthFeature();
                })
                .Build();

            _messenger = _host.Services.GetRequiredService<IMessenger>();

            _messenger.Register<Shared.Notifications.SuccessNotification>(this, (r, m) =>
            {
                _notificationManager.Show("رسالة", m.Message, NotificationType.Success);
            });

            _messenger.Register<Shared.Notifications.FailerNotification>(this, (r, m) =>
            {
                _notificationManager.Show("رسالة", m.Message, NotificationType.Error);
            });

            _messenger.Register<Messages.LoginFailedMessage>(this, (r, m) =>
            {
                _messenger.Send(new Shared.Notifications.FailerNotification("مستخدم غير موجود أو كلمة مرور غير صحيحة"));
                _messenger.Send(new Shared.Notifications.FailerNotification("للمستخدم : " + m.UserName));
            });
        }

        protected override async void OnStartup(StartupEventArgs e)
        {

            Bold.Licensing.BoldLicenseProvider.RegisterLicense("MDAyQDM2MmUzMTJlMzBOd2hxV01OKzROclgwVE9jaFUwbk5NRHppeEUzR0lzTlZXUWxrSld3cnNrPWV5Sk1hV05sYm5ObFZHOXJaVzRpT2lKdlZtZDZVMjVOVWtWT1RWRlBhakZNVVd0eWNYWnhhVWhUT0dwaE9XTTBRbG96VnpFNGQzbFphemROUFNJc0lreHBZMlZ1YzJWUVpYSnBiMlFpT2lJNU9UazVMVEV5TFRNeFZESXpPalU1T2pVNUxqazVOeUlzSWtselJWTlZjMlZ5SWpwMGNuVmxMQ0pKYzFCbGNuQmxkSFZoYkV4cFkyVnVjMlVpT21aaGJITmxmUT09\r\n", true);
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MzM2Mzc2M0AzMjM2MmUzMDJlMzBjMEkyZXZObXZQQ0FvTXBvOWdTUmQ2Yk1JLzJGZEk4dWhlZmJHUVI1aUUwPQ==;MzM2Mzc2NEAzMjM2MmUzMDJlMzBnS3NqcFRVTXZtbjNLdUdYYUJvZW1KMG1rNlhaS2x6bHlYSWJYU2htVkFBPQ=="); 

            if (!CanConnect())
            {
                _messenger.Send(new Shared.Notifications.FailerNotification("سوف يتم الخروج من البرنامج"));
                _messenger.Send(new Shared.Notifications.FailerNotification("لا يمكن الاتصال بقاعدة البيانات, تحقق من بيانات الاتصال"));
                await Task.Delay(5000);
                Shutdown();
            }
            else _messenger.Send(new Shared.Notifications.SuccessNotification("تم الاتصال بقاعدة البيانات"));
            
            _host.Services.GetRequiredService<IAppState>();

            _messenger.Register<Shared.Messages.LoginSuccededMessage>(this, (s, m) =>
            {
                Window window = MainWindow;
                MainWindow = _host.Services.GetRequiredService<Features.MainWindow.View>();
                MainWindow.Show();
                window.Close();
            });

            _messenger.Register<Shared.Commands.Generic.CommandLogout>(this, (r, m) =>
            {
                Window window = MainWindow;
                MainWindow = _host.Services.GetRequiredService<ILoginView>() as Window;
                MainWindow.Show();
                window.Close();

            });

            await _host.StartAsync();
            MainWindow = _host.Services.GetRequiredService<ILoginView>() as Window;
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
        private readonly NotificationManager _notificationManager = new NotificationManager();
        private readonly IMessenger _messenger;


        private readonly IHost _host;
    }

}

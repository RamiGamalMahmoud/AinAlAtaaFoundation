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
using Microsoft.Win32;
using Notification.Wpf;
using System;
using System.Diagnostics;
using System.IO;
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
                    ConfigureServices(s);
                })
                .Build();

            _messenger = _host.Services.GetRequiredService<IMessenger>();

            RegisterRecipients();
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IMessenger>(WeakReferenceMessenger.Default);
            services.AddSingleton<AppDbContextFactory>(new AppDbContextFactory($"Data Source = {AppHelpers.DatabaseFile}"));
            services.AddSingleton<IAppDbContextFactory>(new AppDbContextFactory($"Data Source = {AppHelpers.DatabaseFile}"));
            services.AddTransient<Shared.Components.TopFilterViewModel>();
            services.AddSingleton<DatabaseHelpers>();
            services.ConfigureMainWindowFeature();
            services.ConfigureFamiliesFeature();
            services.ConfigureServicePrint();
            services.ConfigureManegementFeature();
            services.ConfigureDisbursementFeature();
            services.ConfigureUsersFeature();
            services.ConfigureSettingsFeature();
            services.ConfigureAuthFeature();
        }

        private void RegisterRecipients()
        {
            _messenger.Register<Shared.Messages.LoginSuccededMessage>(this, (s, m) =>
            {
                Window window = MainWindow;
                MainWindow = _host.Services.GetRequiredService<Features.MainWindow.View>();
                MainWindow.Show();
                window.Close();
            });

            _messenger.Register(this, (MessageHandler<object, Shared.Commands.Generic.CommandLogout>)((r, m) => ShowLoing()));

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

            _messenger.Register<Messages.Database.BackupMessage>(this, (r, m) =>
            {
                bool isOk = _messenger.Send(new Messages.ConfirmRequestMessage("هل تريد انشاء نسخة احتياطية ؟"));
                if (isOk)
                {
                    DatabaseHelpers.Backup(AppHelpers.BackupFolder,AppHelpers.DatabaseFile);
                    _messenger.Send(new Shared.Notifications.SuccessNotification("تم إنشاء نسخة احتياطية لقاعدة البيانات"));
                }
            });

            _messenger.Register<Messages.Database.ResoreMessage>(this, (r, m) =>
            {
                OpenFileDialog fileDialog = new OpenFileDialog();
                if (fileDialog.ShowDialog() == true)
                {
                    string file = fileDialog.FileName;
                }

            });

            _messenger.Register<Messages.ConfirmRequestMessage>(this, (r, m) =>
            {
                bool reply = MessageBox.Show(m.Message, "رسالة تأكيد", MessageBoxButton.OKCancel, MessageBoxImage.Warning) == MessageBoxResult.OK;

                m.Reply(reply);
            });

            _messenger.Register<Messages.Database.ResetMessage>(this, async (r, m) =>
            {
                bool isOk = _messenger.Send(new Messages.ConfirmRequestMessage("أنت على وشك حذف قاعدة البيانات, هل تريد الإستمرار ؟"));
                if (isOk)
                {
                    _messenger.Send(new Shared.Notifications.SuccessNotification("سوف يتم حذف قاعدة البيانات بعد إعادة تشغيل البرنامج"));
                    _messenger.Send(new Messages.SettingsMessages.UpdateStartStatusMessage(true));
                    await Task.Delay(2000);
                    _messenger.Send(new Shared.Notifications.SuccessNotification("جاري إغلاق البرنامج"));
                    await Task.Delay(2000);
                    AppHelpers.Restart(this);
                }
            });
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            LoadSettings();

            bool isfresh = _messenger.Send<Messages.SettingsMessages.GetSatrtStatusRequestMessage>();

            if (isfresh)
            {
                DatabaseHelpers.Reset(AppHelpers.DataFolder);
                _messenger.Send(new Messages.SettingsMessages.UpdateStartStatusMessage(false));
            }
            AppHelpers.CreateAppDataFolder();

            AppHelpers.CreateDatabase();
            await AppHelpers.TestDatabaseConnection(_host.Services.GetRequiredService<IAppDbContextFactory>(), _messenger, Shutdown);

            await DatabaseHelpers.ApplyMigrations(_host.Services.GetRequiredService<IAppDbContextFactory>());
            AppHelpers.RegisterLicences();

            await _host.StartAsync();
            ShowMainWindow();
            base.OnStartup(e);
        }

        private void ShowMainWindow()
        {
            MainWindow = _host.Services.GetRequiredService<ILoginView>() as Window;
            MainWindow.Show();
        }

        private void ShowLoing()
        {
            Window window = MainWindow;
            MainWindow = _host.Services.GetRequiredService<ILoginView>() as Window;
            MainWindow.Show();
            window.Close();
        }

        private void LoadSettings()
        {
            _host.Services.GetRequiredService<IAppState>();
        }

        private readonly NotificationManager _notificationManager = new NotificationManager();
        private readonly IMessenger _messenger;
        private readonly IHost _host;

    }

}

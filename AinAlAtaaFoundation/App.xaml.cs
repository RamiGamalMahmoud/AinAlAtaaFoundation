using AinAlAtaaFoundation.Data;
using AinAlAtaaFoundation.Features.Auth;
using AinAlAtaaFoundation.Features.DisbursementManagement;
using AinAlAtaaFoundation.Features.FamiliesManagement;
using AinAlAtaaFoundation.Features.MainWindow;
using AinAlAtaaFoundation.Features.Management;
using AinAlAtaaFoundation.Features.Settings;
using AinAlAtaaFoundation.Features.Users;
using AinAlAtaaFoundation.Services;
using AinAlAtaaFoundation.Models;
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
using System.Diagnostics;
using System;

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
            _appService = _host.Services.GetRequiredService<AppService>();
            _appState = _host.Services.GetRequiredService<IAppState>();
            _databaseService = _host.Services.GetRequiredService<DatabaseService>();

            RegisterRecipients();

            DispatcherUnhandledException += App_DispatcherUnhandledException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            _messenger.Send(new Shared.Notifications.FailerNotification("خطأ"));
            _messenger.Send(new Shared.Notifications.FailerNotification(e.ToString()));
        }

        private void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            _messenger.Send(new Shared.Notifications.FailerNotification("خطأ"));
            _messenger.Send(new Shared.Notifications.FailerNotification(e.Exception.Message));
            e.Handled = true;
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IMessenger>(WeakReferenceMessenger.Default);
            services.AddSingleton<AppDbContextFactory>(new AppDbContextFactory($"Data Source = {AppService.DatabaseFile}"));
            services.AddSingleton<IAppDbContextFactory>(new AppDbContextFactory($"Data Source = {AppService.DatabaseFile}"));
            services.AddTransient<Shared.Components.TopFilterViewModel>();
            services.AddSingleton<DatabaseService>();
            services.AddSingleton<AppService>();
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

            _messenger.Register<Shared.Notifications.Notification>(this, (r, m) =>
            {
                _notificationManager.Show("", m.Message);
            });

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

            _messenger.Register<Messages.Database.BackupMessage>(this, async (r, m) =>
            {
                if (_messenger.Send(new Messages.ConfirmRequestMessage("هل تريد انشاء نسخة احتياطية ؟")))
                {
                    DatabaseInfo info = await _messenger.Send(new Messages.Database.GetCurrentDatabaseVersion());
                    _databaseService.Backup(AppService.BackupFolder, AppService.DatabaseFile, info.Version);
                    _messenger.Send(new Shared.Notifications.Notification("تم إنشاء نسخة احتياطية لقاعدة البيانات"));
                }
            });

            _messenger.Register<Messages.ConfirmRequestMessage>(this, (r, m) =>
            {
                bool reply = MessageBox.Show(m.Message, "رسالة تأكيد", MessageBoxButton.OKCancel, MessageBoxImage.Warning) == MessageBoxResult.OK;

                m.Reply(reply);
            });

            _messenger.Register<Messages.Application.RestartMessage>(this, (r, m) =>
            {
                Restart();
            });

            _messenger.Register<Messages.Database.ResetMessage>(this, async (r, m) =>
            {
                bool isOk = _messenger.Send(new Messages.ConfirmRequestMessage("أنت على وشك حذف قاعدة البيانات, هل تريد الإستمرار ؟"));
                if (isOk)
                {
                    _messenger.Send(new Shared.Notifications.SuccessNotification("سوف تتم العملية بعد إعادة تشغيل البرنامج"));
                    _messenger.Send(new Messages.SettingsMessages.UpdateStartStatusMessage(true));
                    await Task.Delay(2000);
                    _messenger.Send(new Shared.Notifications.SuccessNotification("جاري إغلاق البرنامج"));
                    await Task.Delay(2000);
                    _messenger.Send(new Messages.Application.RestartMessage());
                }
            });

            // get current database version
            _messenger.Register<App, Messages.Database.GetCurrentDatabaseVersion>(this, (r, m) =>
            {
                m.Reply(_databaseService.GetDatabaseVersion());
            });

            // update database version
            _messenger.Register<Messages.Database.UpdateDatabaseVersionMessage>(this, async (r, m) =>
            {
                await _databaseService.UpdateDatabaseVersion(m.Description);
            });
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            LoadSettings();

            if (_messenger.Send<Messages.SettingsMessages.GetSatrtStatusRequestMessage>())
            {
                _databaseService.Reset(AppService.DataFolder);
                _messenger.Send(new Messages.SettingsMessages.UpdateStartStatusMessage(false));
            }

            // restore backup database
            if (_appState.HasBackupFileToRestore)
            {
                _messenger.Send(new Shared.Notifications.Notification($"جاري استعادة النسخة الإحتياطية من الملف {_appState.BackupFileToRestore}"));
                _databaseService.RestoreDatabase(_appState.BackupFileToRestore);
                _messenger.Send(new Messages.Database.DatabaseBackedupMessage());
                _messenger.Send(new Shared.Notifications.Notification($"تم استعادة النسخة الإحتياطية من الملف {_appState.BackupFileToRestore}"));
                // set backup state to false
            }

            _appService.CreateAppDataFolder();

            _appService.CreateDatabase();
            await _appService.TestDatabaseConnection(Shutdown);

            await _databaseService.ApplyMigrations();

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

        public void Restart()
        {
            Process.Start(Process.GetCurrentProcess().MainModule.FileName);
            Shutdown();
        }

        private readonly AppService _appService;
        private readonly IAppState _appState;
        private readonly DatabaseService _databaseService;
        private readonly NotificationManager _notificationManager = new NotificationManager();
        private readonly IMessenger _messenger;
        private readonly IHost _host;

    }

}

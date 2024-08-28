using AinAlAtaaFoundation.Data;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows;

namespace AinAlAtaaFoundation
{
    internal static class AppHelpers
    {
        public static void CreateAppDataFolder()
        {
            Directory.CreateDirectory(AppDataFolder);
            Directory.CreateDirectory(DataFolder);
            Directory.CreateDirectory(BackupFolder);
        }

        public static void CreateDatabase()
        {
            if (!File.Exists(DatabaseFile))
            {
                string path = Path.Combine(DataFolder, $"{DatabaseName}.db");
                string source = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DB", $"{DatabaseName}.db");
                File.Copy(source, path);
            }
        }

        public static async Task TestDatabaseConnection(IAppDbContextFactory dbContextFactory, IMessenger messenger, Action action)
        {
            if (!await DatabaseHelpers.CanConnect(dbContextFactory))
            {
                messenger.Send(new Shared.Notifications.FailerNotification("سوف يتم الخروج من البرنامج"));
                messenger.Send(new Shared.Notifications.FailerNotification("لا يمكن الاتصال بقاعدة البيانات, تحقق من بيانات الاتصال"));
                await Task.Delay(5000);
                action?.Invoke();
            }
            else messenger.Send(new Shared.Notifications.SuccessNotification("تم الاتصال بقاعدة البيانات"));
        }

        public static void RegisterLicences()
        {
            Bold.Licensing.BoldLicenseProvider.RegisterLicense("MDAyQDM2MmUzMTJlMzBOd2hxV01OKzROclgwVE9jaFUwbk5NRHppeEUzR0lzTlZXUWxrSld3cnNrPWV5Sk1hV05sYm5ObFZHOXJaVzRpT2lKdlZtZDZVMjVOVWtWT1RWRlBhakZNVVd0eWNYWnhhVWhUT0dwaE9XTTBRbG96VnpFNGQzbFphemROUFNJc0lreHBZMlZ1YzJWUVpYSnBiMlFpT2lJNU9UazVMVEV5TFRNeFZESXpPalU1T2pVNUxqazVOeUlzSWtselJWTlZjMlZ5SWpwMGNuVmxMQ0pKYzFCbGNuQmxkSFZoYkV4cFkyVnVjMlVpT21aaGJITmxmUT09\r\n", true);
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MzM2Mzc2M0AzMjM2MmUzMDJlMzBjMEkyZXZObXZQQ0FvTXBvOWdTUmQ2Yk1JLzJGZEk4dWhlZmJHUVI1aUUwPQ==;MzM2Mzc2NEAzMjM2MmUzMDJlMzBnS3NqcFRVTXZtbjNLdUdYYUJvZW1KMG1rNlhaS2x6bHlYSWJYU2htVkFBPQ==");
        }

        public static void Restart(Application application)
        {
            Process.Start(Process.GetCurrentProcess().MainModule.FileName);
            application.Shutdown();
        }

        public static string DatabaseName => "al_ain";
        public static string AppDataFolder => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "AinAlAtaaFoundation");
        public static string DataFolder => Path.Combine(AppDataFolder, "data");
        public static string DatabaseFile => Path.Combine(DataFolder, $"{DatabaseName}.db");
        public static string BackupFolder => Path.Combine(AppDataFolder, "backups");
    }
}
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.IO;
using System.Threading.Tasks;

namespace AinAlAtaaFoundation.Services
{
    internal class AppService(DatabaseService databaseService, IMessenger messenger)
    {
        private readonly DatabaseService _databaseService = databaseService;
        private readonly IMessenger _messenger = messenger;

        public void CreateAppDataFolder()
        {
            Directory.CreateDirectory(AppDataFolder);
            Directory.CreateDirectory(DataFolder);
            Directory.CreateDirectory(BackupFolder);
        }

        public void CreateDatabase()
        {
            if (!File.Exists(DatabaseFile))
            {
                string path = Path.Combine(DataFolder, $"{DatabaseName}.db");
                string source = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DB", $"{DatabaseName}.db");
                File.Copy(source, path);
            }
        }

        public async Task TestDatabaseConnection(Action action)
        {
            if (!await _databaseService.CanConnect())
            {
                _messenger.Send(new Shared.Notifications.FailerNotification("سوف يتم الخروج من البرنامج"));
                _messenger.Send(new Shared.Notifications.FailerNotification("لا يمكن الاتصال بقاعدة البيانات, تحقق من بيانات الاتصال"));
                await Task.Delay(5000);
                action?.Invoke();
            }
            else _messenger.Send(new Shared.Notifications.SuccessNotification("تم الاتصال بقاعدة البيانات"));
        }

        public static string DatabaseName => "al_ain";
        public static string AppDataFolder => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "AinAlAtaaFoundation");
        public static string DataFolder => Path.Combine(AppDataFolder, "data");
        public static string DatabaseFile => Path.Combine(DataFolder, $"{DatabaseName}.db");
        public static string BackupFolder => Path.Combine(AppDataFolder, "backups");
    }
}
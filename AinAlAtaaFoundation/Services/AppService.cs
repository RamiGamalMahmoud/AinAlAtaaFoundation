using AinAlAtaaFoundation.Shared;
using AinAlAtaaFoundation.Shared.Abstraction;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.IO;
using System.Threading.Tasks;

namespace AinAlAtaaFoundation.Services
{
    internal class AppService
    {
        private readonly DatabaseService _databaseService;
        private readonly IMessenger _messenger;
        private readonly IRandomStringGenerator _randomStringGenerator;

        public AppService(DatabaseService databaseService, IMessenger messenger, IRandomStringGenerator randomStringGenerator)
        {
            _databaseService = databaseService;
            _messenger = messenger;
            _randomStringGenerator = randomStringGenerator;

            _messenger.Register<Messages.Images.FamiliesImagesFolderRequeatMessage>(this, (r, m) => m.Reply(FamiliesImagesFolder));

            _messenger.Register<Messages.Images.SaveFamilyImageMessage>(this, (r, m) =>
            {
                string fileExtension = Path.GetExtension(m.FileName);
                string imageFileName = $"{_randomStringGenerator.Gnerate(16).ToLower()}{fileExtension}";
                File.Copy(m.FileName, Path.Combine(FamiliesImagesFolder, imageFileName));
                m.Reply(Path.Combine(FamiliesImagesFolder, imageFileName));
            });
        }
        public void CreateAppDataFolder()
        {
            Directory.CreateDirectory(AppDataFolder);
            Directory.CreateDirectory(DataFolder);
            Directory.CreateDirectory(BackupFolder);
            Directory.CreateDirectory(OutputFolder);
            Directory.CreateDirectory(FamiliesImagesFolder);
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
        public static string ImagesFolder => Path.Combine(DataFolder, "images");
        public static string FamiliesImagesFolder => Path.Combine(ImagesFolder, "families_images");
        public static string DatabaseFile => Path.Combine(DataFolder, $"{DatabaseName}.db");
        public static string BackupFolder => Path.Combine(AppDataFolder, "backups");
        public static string OutputFolder => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "AinAlAtaaFoundation");
    }
}
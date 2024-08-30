using AinAlAtaaFoundation.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AinAlAtaaFoundation.Helpers
{
    public class DatabaseHelpers
    {
        public static void Backup(string backupFolder, string databaseFile)
        {
            string backupFile = Path.Combine(backupFolder, $"{DateTime.Now:yyyy_MM_dd__HH_mm_ss__FFFF}.db");
            File.Copy(databaseFile, backupFile);
        }

        public static void Reset(string dataFolder)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(dataFolder);
            foreach (FileInfo fileInfo in directoryInfo.GetFiles())
            {
                fileInfo.Delete();
            }
        }

        public static async Task ApplyMigrations(IAppDbContextFactory dbContextFactory)
        {
            using (AppDbContext dbContext = dbContextFactory.CreateDbContext())
            {
                IEnumerable<string> pendingMigrations = await dbContext.Database.GetPendingMigrationsAsync();

                if (pendingMigrations.Any())
                {
                    await dbContext.Database.MigrateAsync();
                }
            }
        }

        public static async Task<bool> CanConnect(IAppDbContextFactory dbContextFactory)
        {
            using (AppDbContext dbContext = dbContextFactory.CreateDbContext())
            {
                return await dbContext.Database.CanConnectAsync();
            }
        }
    }
}

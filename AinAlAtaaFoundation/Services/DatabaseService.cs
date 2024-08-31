using AinAlAtaaFoundation.Data;
using AinAlAtaaFoundation.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AinAlAtaaFoundation.Services
{
    public class DatabaseService(IAppDbContextFactory dbContextFactory)
    {
        public async Task ApplyMigrations()
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

        public void Backup(string backupFolder, string databaseFile, int version)
        {
            string backupFile = Path.Combine(backupFolder, $"{DateTime.Now:yyyy_MM_dd__HH_mm_ss__FFFF} [V-{version:00}].db");
            File.Copy(databaseFile, backupFile);
        }

        public async Task<bool> CanConnect()
        {
            using (AppDbContext dbContext = dbContextFactory.CreateDbContext())
            {
                return await dbContext.Database.CanConnectAsync();
            }
        }

        public async Task<DatabaseInfo> GetDatabaseVersion()
        {
            using (AppDbContext dbContext = dbContextFactory.CreateDbContext())
            {
                return await dbContext.DatabaseInfos.OrderByDescending(x => x.DateUpdated).FirstOrDefaultAsync();
            }
        }

        public void Reset(string dataFolder)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(dataFolder);
            foreach (FileInfo fileInfo in directoryInfo.GetFiles())
            {
                fileInfo.Delete();
            }
        }

        public void RestoreDatabase(string fileName)
        {
            // delete old one
            File.Delete(AppService.DatabaseFile);
            string backupFilePath = Path.Combine(AppService.BackupFolder, $"{fileName}.db");


            File.Copy(backupFilePath, Path.Combine(AppService.DataFolder, "al_ain.db"));
            // copy new
        }

        public async Task UpdateDatabaseVersion(string description)
        {
            using (AppDbContext dbContext = dbContextFactory.CreateDbContext())
            {
                DatabaseInfo currentDatabaseInfo = await GetDatabaseVersion();
                DatabaseInfo databaseInfo = new DatabaseInfo()
                {
                    Version = currentDatabaseInfo is not null ? currentDatabaseInfo.Version + 1 : 1,
                    DateUpdated = DateTime.Now,
                    Discreption = description
                };

                dbContext.DatabaseInfos.Add(databaseInfo);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}

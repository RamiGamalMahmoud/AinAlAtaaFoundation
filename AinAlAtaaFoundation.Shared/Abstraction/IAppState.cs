﻿using AinAlAtaaFoundation.Models;

namespace AinAlAtaaFoundation.Shared.Abstraction
{
    public interface IAppState
    {
        User User { get; }
        string AppDataFolder { get; }
        bool IsFeshStart { get; }
        bool HasBackupFileToRestore { get; }
        string BackupFileToRestore { get; }
        string RecipePrinter { get; }
        string LabelPrinter { get; }
        string DefaultPrinter { get; }
    }
}

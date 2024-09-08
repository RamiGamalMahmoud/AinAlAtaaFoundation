using AinAlAtaaFoundation.Shared.Abstraction;
using MediatR;
using System.Collections.Generic;

namespace AinAlAtaaFoundation.Shared.Commands
{
    public static class Generic
    {
        public record CommandLogout : IRequest;
        public record GoToManagementCommand;
        public record GoToDisbursementCommand;
        public record GoToHomeCommand;
        public record ShowCreate<TModel> : IRequest;
        public record ShowUpdate<TModel>(TModel Model) : IRequest;
        public record CommandCreate<TModel, TDataModel>(TDataModel DataModel) : IRequest<TModel> where TDataModel : class, IDataModel<TModel> where TModel : class;
        public record CommandUpdate<TDataModel>(TDataModel DataModel) : IRequest<bool> where TDataModel : class;
        public record PrintCommand(string ReportName, Dictionary<string, string> Parameters = null, Dictionary<string, object> DataSources = null) : IRequest;
        public record ExportToPdfCommand(string ReportName, Dictionary<string, string> Parameters = null, Dictionary<string, object> DataSources = null) : IRequest;
        public record ExportToExcelCommand(): IRequest;
        public record DirectPrintCommand(string ReportName, string printerName, Dictionary<string, string> Parameters = null, Dictionary<string, object> DataSources = null) : IRequest;
        public record GetAllCommand<TModel>(bool Reload = false) : IRequest<IEnumerable<TModel>>;
        public record RemoveCommand<TModel>(TModel Model) : IRequest<bool>;
    }
}

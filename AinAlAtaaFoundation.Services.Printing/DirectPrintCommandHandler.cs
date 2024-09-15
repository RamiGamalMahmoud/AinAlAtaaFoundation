using AinAlAtaaFoundation.Shared.Commands;
using MediatR;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AinAlAtaaFoundation.Services.Printing
{
    internal class DirectPrintCommandHandler : IRequestHandler<Shared.Commands.Generic.DirectPrintCommand>
    {
        public async Task Handle(Generic.DirectPrintCommand request, CancellationToken cancellationToken)
        {
            string reportPath = System.IO.Path.Combine(Environment.CurrentDirectory, "Reports", request.ReportName);
            await Task.Run(() => Print(reportPath, request.Parameters, request.DataSources, request.printerName), cancellationToken);
        }

        private static async void Print(string reportPath, Dictionary<string, string> parameters, Dictionary<string, object> dataSources, string printer = "Default")
        {
            LocalReport localReport = await LocalReportHelpers.CreateLocalReport(reportPath, parameters, dataSources);
            using (DirectPrint directPrint = new DirectPrint(localReport, printer))
            {
                directPrint.Export().Print();
            }
        }

    }
}

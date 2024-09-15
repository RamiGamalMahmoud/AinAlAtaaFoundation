using MediatR;
using Microsoft.Reporting.WinForms;
using System;
using System.Threading;
using System.Threading.Tasks;
using static AinAlAtaaFoundation.Shared.Commands.Generic;

namespace AinAlAtaaFoundation.Services.Printing
{
    internal class ExportToExcelCommandCommandHandler : IRequestHandler<ExportToExcelCommand>
    {
        public async Task Handle(ExportToExcelCommand request, CancellationToken cancellationToken)
        {
            string reportPath = System.IO.Path.Combine(Environment.CurrentDirectory, "Reports", request.ReportName);
            LocalReport localReport = await LocalReportHelpers.CreateLocalReport(reportPath, request.Parameters, request.DataSources);
            await LocalReportHelpers.Render(localReport, request.OutputFileName, "EXCELOPENXML", "xlsx");
        }
    }
}

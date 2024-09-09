using AinAlAtaaFoundation.Shared.Commands;
using MediatR;
using Microsoft.Reporting.WinForms;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AinAlAtaaFoundation.Services.Printing
{
    internal class ExportPdfCommandHandler : IRequestHandler<Shared.Commands.Generic.ExportToPdfCommand, string>
    {
        public async Task<string> Handle(Generic.ExportToPdfCommand request, CancellationToken cancellationToken)
        {
            string reportPath = System.IO.Path.Combine(Environment.CurrentDirectory, "Reports", request.ReportName);
            LocalReport localReport = LocalReportHelpers.CreateLocalReport(reportPath, request.Parameters, request.DataSources);
            return await LocalReportHelpers.Render(localReport, request.OutputFileName, "PDF", "pdf");
        }
    }
}

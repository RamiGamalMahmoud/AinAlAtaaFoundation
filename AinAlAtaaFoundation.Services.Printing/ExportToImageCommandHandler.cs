using AinAlAtaaFoundation.Shared.Commands;
using MediatR;
using Microsoft.Reporting.WinForms;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace AinAlAtaaFoundation.Services.Printing
{
    internal class ExportToImageCommandHandler : IRequestHandler<Shared.Commands.Generic.ExportToImageCommand>
    {
        public async Task Handle(Generic.ExportToImageCommand request, CancellationToken cancellationToken)
        {
            string reportPath = System.IO.Path.Combine(Environment.CurrentDirectory, "Reports", request.ReportName);

            await Export(LocalReportHelpers.CreateLocalReport(reportPath, request.Parameters, request.DataSources), request.OutputFileName);
        }

        private async Task<string> Export(LocalReport localReport, string outputFileName)
        {
            byte[] bytes = localReport.Render("IMAGE", "<DeviceInfo><OutputFormat>TIFF</OutputFormat></DeviceInfo>", out string mimeType,
                out string encoding,
                out string filenameExtension,
                out string[] streamids,
                out Warning[] warnings);

            string renderDate = DateTime.Now.ToString("yyyy_MM_dd__hh_mm_ss");
            string outputFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "AinAlAtaaFoundation", "images");
            string fileName = $"{outputFolder}\\{outputFileName}-{renderDate}.TIFF";
            Directory.CreateDirectory(outputFolder);

            await File.WriteAllBytesAsync(fileName, bytes);

            return fileName;
        }
    }
}

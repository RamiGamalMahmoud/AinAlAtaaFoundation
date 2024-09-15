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

            await Export(await LocalReportHelpers.CreateLocalReport(reportPath, request.Parameters, request.DataSources), request.OutputFileName);
        }

        private static async Task<string> Export(LocalReport localReport, string outputFileName)
        {
            byte[] bytes = await Task.Run(() => localReport.Render("IMAGE", "<DeviceInfo><OutputFormat>PNG</OutputFormat></DeviceInfo>"));

            string outputFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "AinAlAtaaFoundation");
            if (outputFileName.Contains('\\'))
            {
                string[] outputFileNameContents = outputFileName.Split('\\');
                Directory.CreateDirectory(Path.Combine(outputFolder, outputFileNameContents[0]));
            }
            else
            {
                Directory.CreateDirectory(outputFolder);
            }
            string fileName = $"{outputFolder}\\{outputFileName}.png";

            await File.WriteAllBytesAsync(fileName, bytes);

            return fileName;
        }
    }
}

using AinAlAtaaFoundation.Shared.Commands;
using MediatR;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AinAlAtaaFoundation.Services.Printing
{
    internal class DirectPrintCommandHandler : IRequestHandler<Shared.Commands.Generic.DirectPrintCommand>
    {
        public Task Handle(Generic.DirectPrintCommand request, CancellationToken cancellationToken)
        {
            string reportPath = System.IO.Path.Combine(Environment.CurrentDirectory, "Reports", request.ReportName);
            Print(reportPath, request.Parameters, request.DataSources, request.printerName);
            return Task.CompletedTask;
        }

        private static void Print(string reportPath, Dictionary<string, string> parameters, Dictionary<string, object> dataSources, string printer = "Default")
        {
            List<Microsoft.Reporting.WinForms.ReportParameter> reportParameters = [];
            if (parameters is not null && parameters.Any())
            {
                foreach (KeyValuePair<string, string> keyValue in parameters)
                {
                    Microsoft.Reporting.WinForms.ReportParameter reportParameter = new Microsoft.Reporting.WinForms.ReportParameter(keyValue.Key, keyValue.Value);

                    reportParameters.Add(reportParameter);
                }
            }

            LocalReport localReport = new LocalReport()
            {
                ReportPath = reportPath
            };

            localReport.DataSources.Clear();

            if (dataSources is not null && dataSources.Count > 0)
            {
                foreach (KeyValuePair<string, object> keyValuePair in dataSources)
                {
                    localReport.DataSources.Add(new ReportDataSource(keyValuePair.Key, keyValuePair.Value));
                }
            }

            localReport.EnableExternalImages = true;

            localReport.SetParameters(reportParameters);

            localReport.Refresh();
            using (DirectPrint directPrint = new DirectPrint(localReport, printer))
            {
                directPrint.Export().Print();
            }
        }

    }
}

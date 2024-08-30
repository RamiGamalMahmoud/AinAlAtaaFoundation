using AinAlAtaaFoundation.Shared.Commands;
using BoldReports.RDL.DOM;
using MediatR;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace AinAlAtaaFoundation.Services.Printing
{
    internal class CommandHandlerPrint : IRequestHandler<Shared.Commands.Generic.PrintCommand>
    {
        public Task Handle(Generic.PrintCommand request, CancellationToken cancellationToken)
        {
            string reportPath = System.IO.Path.Combine(Environment.CurrentDirectory, "Reports", request.ReportName);

            ShowBoldReports(reportPath, request.Parameters, request.DataSources);
            //ShowNormalViewer(reportPath, request.Parameters, request.DataSources);
            //Print(reportPath, request.Parameters, request.DataSources);

            return Task.CompletedTask;
        }

        private void ShowBoldReports(string reportPath, Dictionary<string, string> parameters, Dictionary<string, object> dataSources)
        {
            List<BoldReports.Windows.ReportParameter> reportParameters = [];

            foreach (KeyValuePair<string, string> keyValuePair in parameters)
            {
                reportParameters.Add(new BoldReports.Windows.ReportParameter() { Name = keyValuePair.Key, Values = [keyValuePair.Value] });
            }

            new BoldReportsViewerWindow(reportPath, reportParameters, dataSources).Show();
        }

        private void ShowNormalViewer(string reportPath, Dictionary<string, string> parameters, Dictionary<string, object> dataSources)
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

            new ViewPrint(reportPath, reportParameters, dataSources).Show();
        }

        private void Print(string reportPath, Dictionary<string, string> parameters, Dictionary<string, object> dataSources)
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

            localReport.Refresh();

            localReport.SetParameters(reportParameters);

            using (DirectPrint directPrint = new DirectPrint(localReport))
            {
                directPrint.Export().Print();
            }
        }
    }
}

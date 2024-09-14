using AinAlAtaaFoundation.Shared.Abstraction;
using AinAlAtaaFoundation.Shared.Commands;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AinAlAtaaFoundation.Services.Printing
{
    internal class CommandHandlerPrint(IAppState appState) : IRequestHandler<Shared.Commands.Generic.PrintCommand>
    {
        public Task Handle(Generic.PrintCommand request, CancellationToken cancellationToken)
        {
            string reportPath = System.IO.Path.Combine(Environment.CurrentDirectory, "Reports", request.ReportName);

            ShowNormalViewer(reportPath, request.Parameters, request.DataSources);
            return Task.CompletedTask;
        }

        private void ShowBoldReports(string reportPath, Dictionary<string, string> parameters, Dictionary<string, object> dataSources)
        {
            List<BoldReports.Windows.ReportParameter> reportParameters = [];

            if (parameters is not null)
            {
                foreach (KeyValuePair<string, string> keyValuePair in parameters)
                {
                    reportParameters.Add(new BoldReports.Windows.ReportParameter() { Name = keyValuePair.Key, Values = [keyValuePair.Value] });
                }
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
    }
}

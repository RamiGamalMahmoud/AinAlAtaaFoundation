using AinAlAtaaFoundation.Shared.Commands;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AinAlAtaaFoundation.Services.Printing
{
    internal class CommandHandlerPrint() : IRequestHandler<Shared.Commands.Generic.PrintCommand>
    {
        public Task Handle(Generic.PrintCommand request, CancellationToken cancellationToken)
        {
            string reportPath = System.IO.Path.Combine(Environment.CurrentDirectory, "Reports", request.ReportName);

            ShowNormalViewer(reportPath, request.Parameters, request.DataSources);
            return Task.CompletedTask;
        }

        private static void ShowNormalViewer(string reportPath, Dictionary<string, string> parameters, Dictionary<string, object> dataSources)
        {
            List<Microsoft.Reporting.WinForms.ReportParameter> reportParameters = [];

            if (parameters is not null && parameters.Count > 0)
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

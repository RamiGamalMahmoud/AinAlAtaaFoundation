using AinAlAtaaFoundation.Shared.Commands;
using BoldReports.Windows;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AinAlAtaaFoundation.Services.Printing
{
    internal class CommandHandlerPrint : IRequestHandler<Shared.Commands.Generic.PrintCommand>
    {
        public Task Handle(Generic.PrintCommand request, CancellationToken cancellationToken)
        {
            string reportPath = System.IO.Path.Combine(Environment.CurrentDirectory, "Reports", request.ReportName);
            if (request.Parameters.Any())
            {
                List<ReportParameter> reportParameters = [];

                foreach (KeyValuePair<string, List<string>> keyValue in request.Parameters)
                {
                    ReportParameter reportParameter = new ReportParameter
                    {
                        Name = keyValue.Key,
                        Values = keyValue.Value
                    };

                    reportParameters.Add(reportParameter);
                }

                Printing.ViewPrint viewPrint = new ViewPrint(reportPath, reportParameters, request.DataSourceName, request.DataSource);
                viewPrint.Show();
            }
            return Task.CompletedTask;
        }
    }
}

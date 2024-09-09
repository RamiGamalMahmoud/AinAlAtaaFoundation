using Microsoft.Reporting.WinForms;
using System.Collections.Generic;
using System.Windows;

namespace AinAlAtaaFoundation.Services.Printing
{
    public partial class ViewPrint : Window
    {
        public ViewPrint()
        {
            InitializeComponent();
        }

        public ViewPrint(string reportPath, List<ReportParameter> reportParameters, Dictionary<string, object> dataSources) : this()
        {
            reportViewer.SetDisplayMode(DisplayMode.PrintLayout);
            reportViewer.LocalReport.ReportPath = reportPath;
            reportViewer.LocalReport.EnableExternalImages = true;
            reportViewer.LocalReport.SetParameters(reportParameters);
            reportViewer.LocalReport.DataSources.Clear();

            if (dataSources is not null)
            {
                foreach (KeyValuePair<string, object> keyValuePair in dataSources)
                {
                    reportViewer.LocalReport.DataSources.Add(new ReportDataSource(keyValuePair.Key, keyValuePair.Value));
                }
            }

            reportViewer.RefreshReport();
        }
    }
}

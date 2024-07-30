using BoldReports.Windows;
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

        public ViewPrint(string reportPath)
        {
            InitializeComponent();

            reportViewer.ReportPath = reportPath;
            reportViewer.DataSources.Clear();
            reportViewer.RefreshReport();
        }

        public ViewPrint(string reportPath, List<ReportParameter> reportParameters, string dataSourceName, object dataSource)
        {
            InitializeComponent();

            reportViewer.ReportPath = reportPath;
            reportViewer.SetParameters(reportParameters);
            reportViewer.DataSources.Clear();

            if(!string.IsNullOrEmpty(dataSourceName) && dataSource is not null)
            {
                reportViewer.DataSources.Add(new ReportDataSource(dataSourceName, dataSource));
            }

            reportViewer.RefreshReport();
        }
    }
}

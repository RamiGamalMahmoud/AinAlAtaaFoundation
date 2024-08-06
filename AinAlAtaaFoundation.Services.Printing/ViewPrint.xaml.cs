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

        public ViewPrint(string reportPath, List<ReportParameter> reportParameters, Dictionary<string, object> dataSources)
        {
            InitializeComponent();

            reportViewer.ReportPath = reportPath;
            reportViewer.SetParameters(reportParameters);
            reportViewer.DataSources.Clear();

            if(dataSources is not null)
            {
                foreach(KeyValuePair<string, object> keyValuePair in dataSources)
                {
                    reportViewer.DataSources.Add(new ReportDataSource(keyValuePair.Key, keyValuePair.Value));
                }
            }

            reportViewer.RefreshReport();
        }
    }
}

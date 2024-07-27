using AinAlAtaaFoundation.Models;
using BarcodeStandard;
using BoldReports.Windows;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows;

namespace AinAlAtaaFoundation.Features.FamiliesManagement.Print
{
    public partial class View : Window
    {
        private readonly Family _family;

        public View(Family family)
        {
            InitializeComponent();
            _family = family;
            Loaded += View_Loaded;
        }

        private void View_Loaded(object sender, RoutedEventArgs e)
        {
            Barcode b = new Barcode
            {
                IncludeLabel = true
            };
            Image img = Image
                .FromStream(b.Encode(BarcodeStandard.Type.Code128, _family.Id.ToString(), SKColors.Black, SKColors.White, 290, 120)
                .Encode()
                .AsStream());

            //string path = Path.Combine(Environment.CurrentDirectory, @"Images\img.png");

            //img.Save(path);

            string barcodeImageString = ImageToBase64(img, ImageFormat.Png);

            IList<BoldReports.Windows.ReportParameter> parametes = [];

            parametes.Add(new BoldReports.Windows.ReportParameter()
            {
                Name = "Id",
                Values = [_family.Id.ToString()]
            });

            parametes.Add(new BoldReports.Windows.ReportParameter()
            {
                Name = "img",
                Values = [barcodeImageString]
            });

            ReportDataSource reportDataSource = new ReportDataSource("FamilyDataSet", new List<FamilyDataModel>() { new FamilyDataModel(_family) });
            ReportDataSource phonesDataSource = new ReportDataSource("Phones", _family.Phones);

            parametes.Add(new BoldReports.Windows.ReportParameter() { Name = "FamilyType", Values = [_family.FamilyType.Name] });
            parametes.Add(new BoldReports.Windows.ReportParameter() { Name = "SocialStatus", Values = [_family.SocialStatus.Name] });
            parametes.Add(new BoldReports.Windows.ReportParameter() { Name = "OrphanType", Values = [_family.OrphanType?.Name] });
            parametes.Add(new BoldReports.Windows.ReportParameter() { Name = "Clan", Values = [_family.Clan.Name] });
            parametes.Add(new BoldReports.Windows.ReportParameter() { Name = "Branch", Values = [_family.Branch?.Name] });
            parametes.Add(new BoldReports.Windows.ReportParameter() { Name = "BranchRepresentative", Values = [_family.BranchRepresentative.Name] });
            parametes.Add(new BoldReports.Windows.ReportParameter() { Name = "District", Values = [_family.Address.District.Name] });
            parametes.Add(new BoldReports.Windows.ReportParameter() { Name = "FeaturedPoint", Values = [_family.Address.FeaturedPoint?.Name] });
            parametes.Add(new BoldReports.Windows.ReportParameter() { Name = "Street", Values = [_family.Address.Street] });
            parametes.Add(new BoldReports.Windows.ReportParameter() { Name = "DistrictRepresentative", Values = [_family.DistrictRepresentative.Name] });
            parametes.Add(new BoldReports.Windows.ReportParameter() { Name = "RationCard", Values = [_family.RationCard] });
            parametes.Add(new BoldReports.Windows.ReportParameter() { Name = "RationCardOwnerName", Values = [_family.RationCardOwnerName] });
            parametes.Add(new BoldReports.Windows.ReportParameter() { Name = "Notes", Values = [_family.Notes] });
            parametes.Add(new BoldReports.Windows.ReportParameter() { Name = "ApplicantName", Values = [_family.Applicant.Name] });
            parametes.Add(new BoldReports.Windows.ReportParameter() { Name = "ApplicantSurname", Values = [_family.Applicant.Surname] });

            _reportViewer.ReportPath = System.IO.Path.Combine(Environment.CurrentDirectory, @"Print\Reports\Family.rdlc");
            _reportViewer.SetParameters(parametes);
            _reportViewer.DataSources.Clear();
            _reportViewer.DataSources.Add(reportDataSource);
            _reportViewer.DataSources.Add(phonesDataSource);
            _reportViewer.RefreshReport();
        }

        public static string ImageToBase64(Image image, System.Drawing.Imaging.ImageFormat format)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                image.Save(ms, format);
                byte[] imageBytes = ms.ToArray();

                string base64String = Convert.ToBase64String(imageBytes);
                return base64String;
            }
        }
    }
}

﻿using AinAlAtaaFoundation.Models;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AinAlAtaaFoundation.Features.FamiliesManagement.Listing
{
    internal partial class ViewModel
    {
        [RelayCommand(CanExecute = nameof(CanPerformFamilyAction))]
        private void ShowUpdate(Family family)
        {
            _mediator.Send(new Shared.Commands.Generic.ShowUpdate<Family>(family));
        }

        [RelayCommand(CanExecute = nameof(CanPerformFamilyAction))]
        private async Task ShowPrintFamily(Family family)
        {
            var members = family.FamilyMembers.Select(member =>
            {
                return new
                {
                    member.Name,
                    member.YearOfBirth,
                    member.Age,
                    IsDeserves = member.IsDeserves ? "نعم" : "لا",
                    MotherName = member.Mother?.Name
                };
            });
            Dictionary<string, object> dataSources = new Dictionary<string, object>
            {
                { "Phones", family.Phones },
                { "Members", members }
            };
            await _mediator.Send(new Shared.Commands.Generic.PrintCommand("Family.rdlc", GetFamilyParameters(family), dataSources));
        }

        [RelayCommand(CanExecute = nameof(CanPerformFamilyAction))]
        private async Task PrintFamily(Family family)
        {
            var members = family.FamilyMembers.Select(member =>
            {
                return new
                {
                    member.Name,
                    member.YearOfBirth,
                    member.Age,
                    IsDeserves = member.IsDeserves ? "نعم" : "لا",
                    MotherName = member.Mother?.Name
                };
            });
            Dictionary<string, object> dataSources = new Dictionary<string, object>
            {
                { "Phones", family.Phones },
                { "Members", members }
            };
            await _mediator.Send(new Shared.Commands.Generic.DirectPrintCommand("Family.rdlc", _appState.DefaultPrinter, false, GetFamilyParameters(family), dataSources));
        }

        [RelayCommand]
        private async Task DirectPrintBarcode(Family family)
        {
            string barcodeImageString = Shared.GenerateBarCode.ToBarCodeString(family.Id);
            Dictionary<string, string> parameters = new Dictionary<string, string>
            {
                { "Barcode", barcodeImageString }
            };

            await _mediator.Send(new Shared.Commands.Generic.DirectPrintCommand("FamilyBarcode.rdlc", _appState.LabelPrinter, true, parameters));
        }

        [RelayCommand(CanExecute = nameof(CanPerformFamilyAction))]
        private async Task PrintBarcode(Family family)
        {
            string barcodeImageString = Shared.GenerateBarCode.ToBarCodeString(family.Id);
            Dictionary<string, string> parameters = new Dictionary<string, string>
            {
                { "Barcode", barcodeImageString }
            };

            await _mediator.Send(new Shared.Commands.Generic.PrintCommand("FamilyBarcode.rdlc", parameters));
        }

        [RelayCommand]
        private async Task PrintFamilies()
        {
            (string reportName, Dictionary<string, string> parameters, Dictionary<string, object> dataSource) = PrepareReport();

            await _mediator.Send(new Shared.Commands.Generic.DirectPrintCommand(reportName, _appState.DefaultPrinter, false, parameters, dataSource));
        }

        [RelayCommand]
        private async Task ShowPrintFamilies()
        {
            (string reportName, Dictionary<string, string> parameters, Dictionary<string, object> dataSource) = PrepareReport();

            await _mediator.Send(new Shared.Commands.Generic.PrintCommand(reportName, parameters, dataSource));
        }

        [RelayCommand]
        private async Task ExportToExcel()
        {
            (string reportName, Dictionary<string, string> parameters, Dictionary<string, object> dataSource) = PrepareReport();

            await _mediator.Send(new Shared.Commands.Generic.ExportToExcelCommand("العائلات", reportName, parameters, dataSource));
            _messenger.Send(new Shared.Notifications.Notification("تم انشاء التقرير"));
        }

        [RelayCommand]
        private async Task ExportToPdf()
        {
            (string reportName, Dictionary<string, string> parameters, Dictionary<string, object> dataSource) = PrepareReport();

            await _mediator.Send(new Shared.Commands.Generic.ExportToPdfCommand("العائلات", reportName, parameters, dataSource));
            _messenger.Send(new Shared.Notifications.Notification("تم انشاء التقرير"));
        }

        [RelayCommand]
        private async Task ExportAllCards()
        {
            foreach(Family family in Families)
            {
                await ExportFamilyCard(family);
            }
        }

        [RelayCommand]
        private async Task PrintAll()
        {
            foreach (Family family in Families)
            {
                await PrintFamilyCommand.ExecuteAsync(family);
            }
        }

        [RelayCommand]
        private async Task PrintAllBarcodes()
        {
            foreach (Family family in Families)
            {
                await DirectPrintBarcodeCommand.ExecuteAsync(family);
            }
        }

        [RelayCommand(CanExecute = nameof(CanPerformFamilyAction))]
        private async Task ExportFamilyCard(Family family)
        {
            string barcodeImageString = Shared.GenerateBarCode.ToBarCodeString(family.Id);
            Dictionary<string, string> parameters = new Dictionary<string, string>
            {
                { "FamilyId", family.Id.ToString() },
                { "RationCard", family.RationCard},
                { "RationCardOwnerName", family.RationCardOwnerName },
                { "ApplicantName", family.Applicant.Name },
                { "Barcode", barcodeImageString }
            };
            await _mediator.Send(new Shared.Commands.Generic.ExportToImageCommand($"بطاقات\\[ {family.RationCard} ]", "FamilyCard.rdlc", parameters, null));
            _messenger.Send(new Shared.Notifications.Notification($"تم انشاء البطاقة للعائلة {family.RationCard}"));
        }

        private (string, Dictionary<string, string>, Dictionary<string, object>) PrepareReport()
        {
            var families = Families.Select(family =>
            {
                return new
                {
                    family.Id,
                    family.RationCard,
                    family.RationCardOwnerName,
                    ApplicantName = family.Applicant.Name,
                    Clan = family.Clan.Name,
                    Branch = family.Branch?.Name,
                    BranchRepresentative = family.BranchRepresentative.Name,
                    SocialStatus = family.SocialStatus.Name,
                    family.IsSponsored
                };
            });

            Dictionary<string, object> dataSource = new Dictionary<string, object>()
            {
                { "Families", families}
            };

            Dictionary<string, string> parameters = new Dictionary<string, string>
            {
                { "Clan", TopFilterViewModel.Clan?.Name},
                { "Branch", TopFilterViewModel.Branch?.Name},
                { "BranchRepresentative", TopFilterViewModel.BranchRepresentative?.Name },
                { "District", TopFilterViewModel.District?.Name },
                { "DistrictRepresentative", TopFilterViewModel.DistrictRepresentative?.Name },
                { "FamilyType", TopFilterViewModel.FamilyType?.Name },
                { "SocialStatus", TopFilterViewModel.SocialStatus?.Name },
                { "OrphanType", TopFilterViewModel.OrphanType?.Name },
                { "SponsoredStatus", TopFilterViewModel.SponsoringStatus?.Title }
            };

            return ("FilteredFamiliesReport.rdlc", parameters, dataSource);
        }

        private static bool CanPerformFamilyAction(Family family) => family is not null;
    }
}

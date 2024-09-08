using AinAlAtaaFoundation.Models;
using CommunityToolkit.Mvvm.Input;
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
        private void ShowPrint(Family family)
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
            _mediator.Send(new Shared.Commands.Generic.PrintCommand("Family.rdlc", GetParameters(family), dataSources));
        }

        [RelayCommand]
        private async Task DirectPrintBarcode(Family family)
        {
            string barcodeImageString = Shared.GenerateBarCode.ToBarCodeString(family.Id);
            Dictionary<string, string> parameters = new Dictionary<string, string>();

            parameters.Add("Barcode", barcodeImageString);

            await _mediator.Send(new Shared.Commands.Generic.DirectPrintCommand("FamilyBarcode.rdlc", _appState.LabelPrinter, parameters));
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
            var families = Families.Select(family =>
            {
                return new
                {
                    family.Id,
                    family.RationCard,
                    family.RationCardOwnerName,
                    ApplicantName = family.Applicant.Name
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
            await _mediator.Send(new Shared.Commands.Generic.DirectPrintCommand("FilteredFamiliesReport.rdlc", _appState.DefaultPrinter, parameters, dataSource));
        }

        [RelayCommand]
        private async Task ShowPrintFamilies()
        {
            var families = Families.Select(family =>
            {
                return new
                { 
                    family.Id,
                    family.RationCard,
                    family.RationCardOwnerName,
                    ApplicantName = family.Applicant.Name
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
            await _mediator.Send(new Shared.Commands.Generic.PrintCommand("FilteredFamiliesReport.rdlc", parameters, dataSource));
        }

        [RelayCommand]
        private async Task Export()
        {
            await Task.CompletedTask;
        }

        private static bool CanPerformFamilyAction(Family family) => family is not null;
    }
}

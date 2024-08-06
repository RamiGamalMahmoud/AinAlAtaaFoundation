using AinAlAtaaFoundation.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MediatR;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AinAlAtaaFoundation.Features.FamiliesManagement.Listing
{
    internal partial class ViewModel(IMediator mediator) : ObservableObject
    {
        public async Task LoadDataAsync()
        {
            Families = await _mediator.Send(new Shared.Commands.Generic.GetAllCommand<Family>());
        }

        [RelayCommand]
        private async Task ShowCreate()
        {
            await _mediator.Send(new Shared.Commands.Generic.ShowCreate<Family>());
        }

        [RelayCommand]
        private void ShowUpdate(Family family)
        {
            _mediator.Send(new Shared.Commands.Generic.ShowUpdate<Family>(family));
        }

        [RelayCommand]
        private void ShowPrint(Family family)
        {
            Dictionary<string, object> dataSources = new Dictionary<string, object>
            {
                { "Phones", family.Phones },
                { "Members", family.FamilyMembers }
            };
            _mediator.Send(new Shared.Commands.Generic.PrintCommand("Family.rdlc", GetParameters(family), dataSources));
        }

        [RelayCommand]
        private void PrintBarcode(Family family)
        {
            string barcodeImageString = Shared.GenerateBarCode.ToBarCodeString(family.Id);
            Dictionary<string, List<string>> parameters = new Dictionary<string, List<string>>();
            parameters.Add("Barcode", [barcodeImageString]);
            parameters.Add("FamilyName", [family.Name]);
            _mediator.Send(new Shared.Commands.Generic.PrintCommand("FamilyBarcode.rdlc", parameters));
        }

        private static Dictionary<string, List<string>> GetParameters(Family family)
        {
            string barcodeImageString = Shared.GenerateBarCode.ToBarCodeString(family.Id);
            Dictionary<string, List<string>> parameters = new Dictionary<string, List<string>>();

            parameters.Add("Id", [family.Id.ToString()]);
            parameters.Add("img", [barcodeImageString]);
            parameters.Add("", []);

            parameters.Add("FamilyType", [family.FamilyType.Name]);
            parameters.Add("SocialStatus", [family.SocialStatus.Name]);
            parameters.Add("OrphanType", [family.OrphanType?.Name]);
            parameters.Add("Clan", [family.Clan.Name]);
            parameters.Add("Branch", [family.Branch?.Name]);
            parameters.Add("BranchRepresentative", [family.BranchRepresentative.Name]);
            parameters.Add("District", [family.Address.District.Name]);
            parameters.Add("FeaturedPoint", [family.Address.FeaturedPoint?.Name]);
            parameters.Add("Street", [family.Address.Street]);
            parameters.Add("DistrictRepresentative", [family.DistrictRepresentative.Name]);
            parameters.Add("RationCard", [family.RationCard]);
            parameters.Add("RationCardOwnerName", [family.RationCardOwnerName]);
            parameters.Add("Notes", [family.Notes]);
            parameters.Add("ApplicantName", [family.Applicant.Name]);
            parameters.Add("ApplicantSurname", [family.Name]);

            return parameters;
        }

        public IEnumerable<Family> Families
        {
            get => _families;
            private set => SetProperty(ref _families, value);
        }
        private IEnumerable<Family> _families;

        private readonly IMediator _mediator = mediator;
    }
}

using AinAlAtaaFoundation.Features.DisbursementManagement.Create;
using CommunityToolkit.Mvvm.Messaging;
using FluentAssertions;
using MediatR;
using Moq;

namespace AinAlAtaaFoundation.Tests.Disbursements
{
    [Collection("AppDbFactoryCollection")]
    public class DisbursementsTests
    {
        public DisbursementsTests()
        {
            _mockMediator = new Mock<IMediator>();
        }

        [Fact]
        public void Test()
        {
            ViewModel viewModel = new ViewModel(_mockMediator.Object, WeakReferenceMessenger.Default);

            viewModel.SetInputToIdCommand.Execute(null);
            viewModel.FamilyGetter.FamilyId = 4;
            viewModel.FamilyGetter.FamilyId.Should().Be(4);
            viewModel.FamilyGetter.FamilyId = 5;

            viewModel.FamilyGetter.RationCard.Should().BeEmpty();
            viewModel.SetInputToRationCardCommand.Execute(null);

            viewModel.FamilyGetter.FamilyId.Should().Be(5);
        }

        private readonly Mock<IMediator> _mockMediator;
    }
}

/*
 * read form barcode scaner
 * manual input
 * 
 * search by ration card number
 */

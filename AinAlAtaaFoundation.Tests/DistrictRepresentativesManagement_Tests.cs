using CommunityToolkit.Mvvm.Messaging;
using AinAlAtaaFoundation.Features.Management.DistrictRepresentativesManagement.Create;
using MediatR;
using Moq;
using FluentAssertions;

namespace AinAlAtaaFoundation.Tests
{
    public class DistrictRepresentativesManagement_Tests
    {
        public DistrictRepresentativesManagement_Tests()
        {
            _mediatorMock = new Mock<IMediator>();
        }

        [Fact]
        public void SaveCommand_CanNotExecute_AfterInit()
        {
            ViewModel viewModel = new ViewModel(_mediatorMock.Object, WeakReferenceMessenger.Default);
            viewModel.SaveCommand.CanExecute(null).Should().BeFalse();
        }

        private readonly Mock<IMediator> _mediatorMock;
    }
}

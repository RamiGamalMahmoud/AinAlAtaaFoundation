using CommunityToolkit.Mvvm.Messaging;
using MediatR;
using Moq;
using FluentAssertions;
using AinAlAtaaFoundation.Features.Management.DistrictRepresentativesManagement.Editor;

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
            ViewModelCreate viewModel = new ViewModelCreate(_mediatorMock.Object, WeakReferenceMessenger.Default);
            viewModel.SaveCommand.CanExecute(null).Should().BeFalse();
        }

        private readonly Mock<IMediator> _mediatorMock;
    }
}

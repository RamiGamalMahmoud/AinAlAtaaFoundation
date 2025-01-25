using AinAlAtaaFoundation.Shared.Abstraction;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace AinAlAtaaFoundation.Features.Management
{
    internal class ViewModel(IServiceProvider serviceProvider) : ObservableObject
    {
        public object ClansView { get; } = serviceProvider.GetRequiredService<IClansView>();
        public object BranchesView { get; } = serviceProvider.GetRequiredService<IBranchesView>();
        public object BranchRepresentativesView { get; } = serviceProvider.GetRequiredService<IBranchRepresentativesView>();

        public object DistrictsView { get; } = serviceProvider.GetRequiredService<IDistrictsView>();
        public object DistrictRepresentativesView { get; } = serviceProvider.GetRequiredService<IClanRepresentativesView>();
        public object FeaturedPointsView { get; } = serviceProvider.GetRequiredService<IFeaturedPointsView>();
        public object AssociationRepresentativesView { get; } = serviceProvider.GetRequiredService<IAssociationRepresentativesListingView>();

        public object UsersView { get; } = serviceProvider.GetRequiredService<IUserView>();

    }
}

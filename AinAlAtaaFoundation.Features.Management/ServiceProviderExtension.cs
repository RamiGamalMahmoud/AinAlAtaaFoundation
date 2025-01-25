using AinAlAtaaFoundation.Shared.Abstraction;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace AinAlAtaaFoundation.Features.Management
{
    public static class ServiceProviderExtension
    {
        public static IServiceCollection ConfigureManegementFeature(this IServiceCollection services)
        {
            services
                .AddMediatR((cfg) => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()))

            #region FeaturedPointsManagement
                .AddSingleton<FeaturedPointsManagement.Repository>()
                .AddSingleton<IFeaturedPointsView, FeaturedPointsManagement.View>()
                .AddSingleton<FeaturedPointsManagement.ViewModel>()
            #endregion

            #region DistrictRepresentativesManagement
                .AddSingleton<DistrictRepresentativesManagement.Repository>()
                .AddSingleton<IClanRepresentativesView, DistrictRepresentativesManagement.View>()
                .AddSingleton<DistrictRepresentativesManagement.ViewModel>()
            #endregion

            #region BranchRepresentativesManagement
                .AddSingleton<BranchRepresentativesManagement.Repository>()
                .AddSingleton<IBranchRepresentativesView, BranchRepresentativesManagement.View>()
                .AddSingleton<BranchRepresentativesManagement.ViewModel>()
            #endregion

            #region BranchesManagement
                .AddSingleton<BranchesManagement.Repository>()
                .AddSingleton<IBranchesView, BranchesManagement.View>()
                .AddSingleton<BranchesManagement.ViewModel>()
            #endregion

            #region DistrictsManagement
                .AddSingleton<DistrictsManagement.Repository>()
                .AddSingleton<IDistrictsView, DistrictsManagement.View>()
                .AddSingleton<DistrictsManagement.ViewModel>()
            #endregion

            #region ClansManagement
                .AddSingleton<ClansManagement.Repository>()
                .AddSingleton<IClansView, ClansManagement.View>()
                .AddSingleton<ClansManagement.ViewModel>()
            #endregion

            #region AssociationsManagement
                .AddSingleton<AssociationRepresentatives.Repository>()
                .AddSingleton< IAssociationRepresentativesListingView, AssociationRepresentatives.Listing.View>()
                .AddSingleton<AssociationRepresentatives.Listing.ViewModel>()
            #endregion

                .AddSingleton<IManagementView, View>()
                .AddSingleton<ViewModel>();
            return services;
        }
    }
}

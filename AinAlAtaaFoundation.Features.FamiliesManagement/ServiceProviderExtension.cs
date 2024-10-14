using AinAlAtaaFoundation.Shared.Abstraction;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace AinAlAtaaFoundation.Features.FamiliesManagement
{
    public static class ServiceProviderExtension
    {
        public static IServiceCollection ConfigureFamiliesFeature(this IServiceCollection services)
        {
            services
                .AddMediatR((cfg) => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()))

                .AddSingleton<SocialStatuses.Repository>()

                .AddSingleton<Members.Repository>()
                .AddTransient<IFamilyMembersViewWindow, Members.ListingWindow>()
                .AddSingleton<IFamilyMembersView, Members.View>()
                .AddSingleton<Members.ViewModel>()

                .AddTransient<Deleted.View>()
                .AddTransient<Deleted.ViewModel>()

                .AddSingleton<Repository>()
                .AddSingleton< IFamiliesView, Listing.View>()
                .AddSingleton<Listing.ViewModel>();
            return services;
        }
    }
}

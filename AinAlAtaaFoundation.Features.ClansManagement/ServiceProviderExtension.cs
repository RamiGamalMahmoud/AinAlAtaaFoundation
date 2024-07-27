using AinAlAtaaFoundation.Shared.Abstraction;
using Microsoft.Extensions.DependencyInjection;

namespace AinAlAtaaFoundation.Features.ClansManagement
{
    public static class ServiceProviderExtension
    {
        public static IServiceCollection ConfigureClansManagementFeature(this IServiceCollection services)
        {
            //services
            //    .AddSingleton<IClansView, Listing.View>()
            //    .AddSingleton<Listing.ViewModel>();
            return services;
        }
    }
}

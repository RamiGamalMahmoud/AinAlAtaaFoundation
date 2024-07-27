using AinAlAtaaFoundation.Shared.Abstraction;
using Microsoft.Extensions.DependencyInjection;

namespace AinAlAtaaFoundation.Features.DistrictsManagement
{
    public static class ServiceProviderExtension
    {
        public static IServiceCollection ConfigureDistrictsManagementFeature(this IServiceCollection services)
        {
            return services;
        }
    }
}

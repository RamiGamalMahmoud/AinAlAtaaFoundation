using AinAlAtaaFoundation.Shared.Abstraction;
using Microsoft.Extensions.DependencyInjection;

namespace AinAlAtaaFoundation.Features.DisbursementManagement
{
    public static class ServiceProviderExtension
    {
        public static IServiceCollection ConfigureDisbursementFeature(this IServiceCollection services)
        {
            services
                .AddSingleton<ViewModel>()
                .AddSingleton< IDisbursementView, View >();
            return services;
        }
    }
}

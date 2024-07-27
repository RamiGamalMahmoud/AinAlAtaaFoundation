using AinAlAtaaFoundation.Shared.Abstraction;
using Microsoft.Extensions.DependencyInjection;

namespace AinAlAtaaFoundation.Features.Settings
{
    public static class ServiceProviderExtension
    {
        public static IServiceCollection ConfigureSettingsFeature(this IServiceCollection services)
        {
            services
                .AddSingleton<IAppState, ViewModel>();
            return services;
        }
    }
}

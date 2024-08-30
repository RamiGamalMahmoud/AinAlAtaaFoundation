using AinAlAtaaFoundation.Shared.Abstraction;
using AinAlAtaaFoundation.Shared.Abstraction.Settings;
using Microsoft.Extensions.DependencyInjection;

namespace AinAlAtaaFoundation.Features.Settings
{
    public static class ServiceProviderExtension
    {
        public static IServiceCollection ConfigureSettingsFeature(this IServiceCollection services)
        {
            services
                .AddSingleton<Properties.Settings>()
                .AddSingleton<IAppState>(p => p.GetRequiredService<AppState>())
                .AddSingleton<AppState>()
                .AddSingleton<ViewModel>()
                .AddTransient<ISettingsView, View>();
            return services;
        }
    }
}

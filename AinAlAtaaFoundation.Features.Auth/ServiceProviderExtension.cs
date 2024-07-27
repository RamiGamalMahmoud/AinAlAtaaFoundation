using AinAlAtaaFoundation.Features.Auth.Login;
using AinAlAtaaFoundation.Shared.Abstraction;
using Microsoft.Extensions.DependencyInjection;

namespace AinAlAtaaFoundation.Features.Auth
{
    public static class ServiceProviderExtension
    {
        public static IServiceCollection ConfigureAuthFeature(this IServiceCollection services)
        {
            services
                .AddTransient<ViewModel>()
                .AddTransient<ILoginView, View>();
            return services;
        }
    }
}

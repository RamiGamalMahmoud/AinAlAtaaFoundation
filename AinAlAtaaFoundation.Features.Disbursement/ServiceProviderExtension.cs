using AinAlAtaaFoundation.Shared.Abstraction;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace AinAlAtaaFoundation.Features.DisbursementManagement
{
    public static class ServiceProviderExtension
    {
        public static IServiceCollection ConfigureDisbursementFeature(this IServiceCollection services)
        {
            services
                .AddMediatR((cfg) => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()))
                .AddSingleton<Repository>()
                .AddSingleton<ViewModel>()
                .AddSingleton<IDisbursementView, View>();
            return services;
        }
    }
}

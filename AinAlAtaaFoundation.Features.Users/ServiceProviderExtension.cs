using AinAlAtaaFoundation.Shared.Abstraction;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace AinAlAtaaFoundation.Features.Users
{
    public static class ServiceProviderExtension
    {
        public static IServiceCollection ConfigureUsersFeature(this IServiceCollection services)
        {
            services
                .AddMediatR((cfg) => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()))
                .AddSingleton<Listing.ViewModel>()
                .AddSingleton<IUserView, Listing.View>()
                .AddSingleton<Repository>();
            return services;
        }
    }
}

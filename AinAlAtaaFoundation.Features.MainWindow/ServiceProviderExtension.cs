using Microsoft.Extensions.DependencyInjection;

namespace AinAlAtaaFoundation.Features.MainWindow
{
    public static class ServiceProviderExtension
    {
        public static IServiceCollection ConfigureMainWindowFeature(this IServiceCollection services)
        {
            services
                .AddSingleton<View>()
                .AddSingleton<ViewModel>()
                .AddSingleton<WelcomeView>();
            return services;
        }
    }
}

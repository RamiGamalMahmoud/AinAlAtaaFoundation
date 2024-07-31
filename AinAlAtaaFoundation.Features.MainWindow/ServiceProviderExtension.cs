using Microsoft.Extensions.DependencyInjection;

namespace AinAlAtaaFoundation.Features.MainWindow
{
    public static class ServiceProviderExtension
    {
        public static IServiceCollection ConfigureMainWindowFeature(this IServiceCollection services)
        {
            services
                .AddTransient<View>()
                .AddTransient<ViewModel>()
                .AddSingleton<WelcomeView>();
            return services;
        }
    }
}

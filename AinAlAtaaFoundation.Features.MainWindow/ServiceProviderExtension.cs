using Microsoft.Extensions.DependencyInjection;

namespace AinAlAtaaFoundation.Features.MainWindow
{
    public static class ServiceProviderExtension
    {
        public static IServiceCollection ConfigureMainWindowFeature(this IServiceCollection services)
        {
            services
                .AddSingleton<Statistics.View>()
                .AddSingleton<Statistics.ViewModel>()
                .AddTransient<View>()
                .AddTransient<ViewModel>()
                .AddSingleton<WelcomeView>();
            return services;
        }
    }
}

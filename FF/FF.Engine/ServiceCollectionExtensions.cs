using Microsoft.Extensions.DependencyInjection;

namespace FF.Engine
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAccountant(this IServiceCollection services)
        {
            services
                .AddSingleton<IAccountant, Accountant>()
                .AddSingleton<IForecaster, Forecaster>();
            return services;
        }
    }
}
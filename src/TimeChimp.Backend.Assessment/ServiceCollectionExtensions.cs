using Microsoft.Extensions.DependencyInjection;
using TimeChimp.Backend.Assessment.Interfaces;
using TimeChimp.Backend.Assessment.Services;

namespace TimeChimp.Backend.Assessment
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Extensibility point for adding general services available to all components
        /// </summary>
        /// <param name="services"></param>
        public static void AddServices(this IServiceCollection services)
        {
            services.AddTransient<IGeneralNewsService, GeneralNewsService>();
        }

        /// <summary>
        /// Extensibility point for adding services constructed using a specialised provider e.g.: country specific services
        /// </summary>
        /// <param name="services"></param>
        public static void AddServiceProviders(this IServiceCollection services)
        {
        }
    }
}

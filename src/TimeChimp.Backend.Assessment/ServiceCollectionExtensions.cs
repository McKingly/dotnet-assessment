using Microsoft.Extensions.DependencyInjection;
using System.Text.Json.Serialization;
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
            services.AddTransient<INewsService, NewsService>();
            
            // Adds the in memory cache to the project
            services.AddMemoryCache();

            // Makes it so the enum classes appear as string in the swagger documentation
            services.AddControllers().AddJsonOptions(options =>
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
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

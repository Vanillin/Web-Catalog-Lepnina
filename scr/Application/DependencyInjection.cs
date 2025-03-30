using Application.Mappings;
using Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingProfile));
            services.AddTransient<IServiceAttachment, ServiceAttachment>();
            services.AddTransient<IServiceProduct, ServiceProduct>();
            services.AddTransient<IServiceReview, ServiceReview>();
            services.AddTransient<IServiceSection, ServiceSection>();
            services.AddTransient<IServiceUser, ServiceUser>();
            services.AddTransient<IServiceFavorites, ServiceFavorites>();

            return services;
        }
    }
}

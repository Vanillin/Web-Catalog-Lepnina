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
            services.AddSingleton<IServiceAttachment, ServiceAttachment>();
            services.AddSingleton<IServiceProduct, ServiceProduct>();
            services.AddSingleton<IServiceReview, ServiceReview>();
            services.AddSingleton<IServiceSection, ServiceSection>();
            services.AddSingleton<IServiceUser, ServiceUser>();
            services.AddSingleton<IServiceUserProduct, ServiceUserProduct>();

            return services;
        }
    }
}

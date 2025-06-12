using Application.Mappings;
using Application.Services;
using Application.Services.Interfaces;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

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
            services.AddTransient<IServiceAuth, ServiceAuth>();
            services.AddTransient<IPasswordHasher, BCryptHasher>();
            services.AddTransient<IServicePictureFile, ServicePictureFile>();
            services.AddTransient<IServiceFileStorage, ServiceFileStorage>();

            services.AddFluentValidationAutoValidation();
            services.AddFluentValidationClientsideAdapters();
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}

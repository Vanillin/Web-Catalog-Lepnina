using Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Infrostracture
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddSingleton<IRepositAttachment, RepositAttachment>();
            services.AddSingleton<IRepositProduct, RepositProduct>();
            services.AddSingleton<IRepositReview, RepositReview>();
            services.AddSingleton<IRepositSection, RepositSection>();
            services.AddSingleton<IRepositUser, RepositUser>();
            services.AddSingleton<IRepositUserProduct, RepositUserProduct>();

            return services;
        }
    }
}

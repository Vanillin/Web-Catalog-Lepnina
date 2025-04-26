using Dapper;
using FluentMigrator.Runner;
using Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using System.Reflection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddSingleton(sp =>
            {
                var configuration = sp.GetRequiredService<IConfiguration>();
                var connectionString = configuration.GetConnectionString("PostgresDB");
                return new NpgsqlDataSourceBuilder(connectionString).Build();
            });

            services.AddScoped(sp =>
            {
                var datasourse = sp.GetRequiredService<NpgsqlDataSource>();
                return datasourse.CreateConnection();
            });

            services.AddTransient<IRepositAttachment, PostgresRepositAttachment>();
            services.AddTransient<IRepositProduct, PostgresRepositProduct>();
            services.AddTransient<IRepositReview, PostgresRepositReview>();
            services.AddTransient<IRepositSection, PostgresRepositSection>();
            services.AddTransient<IRepositUser, PostgresRepositUser>();
            services.AddTransient<IRepositFavorites, PostgresRepositFavorites>();

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            services
                .AddFluentMigratorCore()
                .ConfigureRunner(
                rb => rb.AddPostgres().WithGlobalConnectionString("PostgresDB")
                .ScanIn(Assembly.GetExecutingAssembly()).For.Migrations()
                )
                .AddLogging(lb => lb.AddFluentMigratorConsole());

            services.AddScoped<Database.MigrationRunner>();

            return services;
        }
    }
}

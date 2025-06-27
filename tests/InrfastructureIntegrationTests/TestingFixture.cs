using Application;
using Domain.Entities;
using FluentMigrator.Runner;
using FluentMigrator.Runner.Processors;
using Infrastructure;
using Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Npgsql;
using Respawn;
using System.Reflection;
using MigrationRunner = Infrastructure.Database.MigrationRunner;

namespace InfrastructureIntegrationTests
{
    public sealed class TestingFixture : IAsyncLifetime
    {
        private Respawner _respawner = null!;

        public TestingFixture()
        {
            var host = Host.CreateDefaultBuilder()
                .ConfigureAppConfiguration((context, config) => { config.AddJsonFile("appsettings.json"); })
                .ConfigureServices((context, services) =>
                {
                    services.AddInfrastructure();
                    services.AddApplication();
                    var connectionString = context.Configuration.GetConnectionString("PostgresDB");
                    if (string.IsNullOrWhiteSpace(connectionString))
                        throw new ApplicationException("PostgresDB connection string is empty");

                    services.AddSingleton(_ => new NpgsqlDataSourceBuilder(connectionString).Build());

                    services.AddTransient(sp =>
                    {
                        var dataSource = sp.GetRequiredService<NpgsqlDataSource>();
                        return dataSource.CreateConnection();
                    });

                    services
                        .AddFluentMigratorCore()
                        .ConfigureRunner(rb => rb
                            .AddPostgres()
                            .WithGlobalConnectionString(connectionString)
                            .ScanIn(Assembly.GetExecutingAssembly()).For.Migrations())
                        .Configure<SelectingProcessorAccessorOptions>(options => { options.ProcessorId = "Postgres"; });
                })
                .Build();

            ServiceProvider = host.Services;
        }

        public IServiceProvider ServiceProvider { get; }

        public async Task InitializeAsync()
        {
            using var scope = ServiceProvider.CreateScope();
            var connection = scope.ServiceProvider.GetRequiredService<NpgsqlConnection>();
            await connection.OpenAsync();

            var migrationRunner = scope.ServiceProvider.GetRequiredService<MigrationRunner>();
            migrationRunner.Run();

            _respawner = await Respawner.CreateAsync(connection, new RespawnerOptions
            {
                DbAdapter = DbAdapter.Postgres,
                SchemasToInclude = ["public"],
                TablesToIgnore = ["VersionInfo"]
            });
        }

        public async Task<Section> CreateSection()
        {
            using var scope = ServiceProvider.CreateScope();
            var userRepository = scope.ServiceProvider.GetRequiredService<IRepositSection>();

            var user = new Section
            {
                Name = "Test"
            };

            int? id = await userRepository.Create(user);
            user.Id = (int)id;
            return user;
        }

        public async Task DisposeAsync()
        {
            await ResetDatabase();
        }

        private async Task ResetDatabase()
        {
            using var scope = ServiceProvider.CreateScope();
            var connection = scope.ServiceProvider.GetRequiredService<NpgsqlConnection>();
            await connection.OpenAsync();

            await _respawner.ResetAsync(connection);
        }
    }
}
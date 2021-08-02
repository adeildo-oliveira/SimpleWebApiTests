using Dapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SimpleWebApiTests;
using System;
using System.Data;
using System.IO;
using Xunit;

namespace Tests.Integration.Tools
{
    public class DatabaseFixture
    {
        private const string NAME_CONNECTION = "ApiConnection";
        private TestServer _server;
        private IServiceProvider _serviceProvider;
        private IConfiguration _configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.Development.json").Build();

        public void ConfigureServicesDatabaseFixture()
        {
            var servicesProvider = new WebHostBuilder()
                .UseStartup<Startup>()
                .UseEnvironment("Development")
                .UseConfiguration(_configuration);

            _server = new TestServer(servicesProvider);
            _serviceProvider = _server.Services;
        }

        public T GetService<T>()
        {
            ConfigureServicesDatabaseFixture();
            return _serviceProvider.GetService<T>();
        }

        public void ClearDataBase()
        {
            var script = File.ReadAllText($"{AppDomain.CurrentDomain.BaseDirectory}Script\\ScriptDB.sql");

            using var conn = new SqlConnection(_configuration.GetConnectionString(NAME_CONNECTION));
            conn.Execute(script, commandType: CommandType.Text);
        }
    }

    [CollectionDefinition(Name)]
    public class IntegrationTestsCollection : ICollectionFixture<DatabaseFixture>
    {
        public const string Name = nameof(IntegrationTestsCollection);
    }

    [Collection(IntegrationTestsCollection.Name)]
    public class IntegrationTestFixture : DatabaseFixture, IDisposable
    {
        private const string Name = nameof(IntegrationTestFixture);
        protected readonly DatabaseFixture _fixture;

        public IntegrationTestFixture(DatabaseFixture fixture) => _fixture = fixture;

        public void Dispose() => ClearDataBase();
    }
}

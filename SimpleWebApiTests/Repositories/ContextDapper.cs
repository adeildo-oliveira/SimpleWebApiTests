using Microsoft.Data.SqlClient;
using SimpleWebApiTests.Interfaces;
using System.Data;

namespace SimpleWebApiTests.Repositories
{
    public class ContextDapper : IContextDapper
    {
        private readonly string _connectionString;

        public ContextDapper(string connectionString) => _connectionString = connectionString;

        public IDbConnection Connection() => new SqlConnection(_connectionString);
    }
}

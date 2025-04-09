using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Data;

namespace Infrastructure.Data
{
    public class DapperContext
    {
       // private readonly IDbConnection _dbConnection;
        private readonly string _connectionString;

        public DapperContext(IConfiguration configuration)
        {
            _connectionString = configuration.GetSection("Connections:PostgreSQL").Value;  
        }

        public IDbConnection CreateConnection() => new NpgsqlConnection(_connectionString);

    }
}

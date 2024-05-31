using Microsoft.Data.SqlClient;
using System.Data;

namespace App.Database
{
    public class SqlConnectionFactory : ISqlConnectionFactory
    {
        private readonly IConfiguration _configuration;

        public SqlConnectionFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IDbConnection Create()
        {
            return new SqlConnection(_configuration.GetConnectionString("DbConnection"));
        }
    }
}

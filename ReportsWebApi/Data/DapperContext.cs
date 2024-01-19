using Microsoft.Data.SqlClient;
using System.Data;

namespace ReportsWebApi.Data
{
    public class DapperContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        public DapperContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DefaultConnection") ?? throw new ApplicationException("DefaultConnection Not Found");
        }
        public IDbConnection CreateConnection()
            => new SqlConnection(_connectionString);
    }
}

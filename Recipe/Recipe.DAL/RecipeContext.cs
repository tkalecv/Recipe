using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Recipe.DAL
{
    public class RecipeContext : IRecipeContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public RecipeContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
        }

        public IDbConnection CreateConnection()
            => new SqlConnection(_connectionString);
    }
}

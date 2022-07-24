using AzureSqlApp.Models;
using Microsoft.Data.SqlClient;
using Microsoft.FeatureManagement;
using System.Text.Json;

namespace AzureSqlApp.Services
{
    public class ProductService : IProductService
    {
        readonly IConfiguration _configuration;
        readonly IFeatureManager _featureManager;

        public ProductService(IConfiguration configuration, IFeatureManager featureManager)
        {
            _configuration = configuration;
            _featureManager = featureManager;
        }

        public async Task<bool> IsBeta() => await _featureManager.IsEnabledAsync("beta");

        SqlConnection GetConnection()
        {
            //Configuration
            //var key = "SQLAZURECONNSTR_DB_CONNECT";
            //var connectionString = Environment.GetEnvironmentVariable(key);
            //return new SqlConnection(_configuration.GetConnectionString("DB_CONNECT"));

            return new SqlConnection(_configuration["DB_CONNECT"]);
        }

        public async Task<List<Product>> GetProductsAsync()
        {
            var functionUrl = "{functionUrl}";

            using(var client = new HttpClient())
            {
                var response = await client.GetAsync(functionUrl);

                var content = await response.Content.ReadAsStringAsync();

                return JsonSerializer.Deserialize<List<Product>>(content) ?? new List<Product>();
            }
        }
    }
}
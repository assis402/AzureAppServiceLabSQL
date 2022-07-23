using AzureSqlApp.Models;
using Microsoft.Data.SqlClient;

namespace AzureSqlApp.Services
{
    public class ProductService
    {
        static readonly string db_source = "assissql.database.windows.net";
        static readonly string db_user = "assis";
        static readonly string db_password = "32833113Mt32833113";
        static readonly string db_database = "assissql";

        static SqlConnection GetConnection()
        {
            var builder = new SqlConnectionStringBuilder
            {
                DataSource = db_source,
                UserID = db_user,
                Password = db_password,
                InitialCatalog = db_database
            };

            var key = "SQLAZURECONNSTR_DB_CONNECT";
            var connectionString = Environment.GetEnvironmentVariable(key);

            return new SqlConnection(connectionString);
        }

        public List<Product> GetProducts()
        {
            SqlConnection connection = GetConnection();

            var productList = new List<Product>();
            var statement = "SELECT ProductID, ProductName, Quantity FROM Products";

            connection.Open();

            var command = new SqlCommand(statement, connection);

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var product = new Product()
                    {
                        ProductId = reader.GetInt32(0),
                        ProductName = reader.GetString(1),
                        Quantity = reader.GetInt32(2)
                    };

                    productList.Add(product);
                }
            }

            connection.Close();

            return productList;
        }
    }
}

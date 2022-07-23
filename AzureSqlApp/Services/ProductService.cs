﻿using AzureSqlApp.Models;
using Microsoft.Data.SqlClient;

namespace AzureSqlApp.Services
{
    public class ProductService : IProductService
    {
        readonly IConfiguration _configuration;

        public ProductService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        SqlConnection GetConnection()
        {
            //var key = "SQLAZURECONNSTR_DB_CONNECT";
            //var connectionString = Environment.GetEnvironmentVariable(key);
            return new SqlConnection(_configuration.GetConnectionString("DB_CONNECT"));
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

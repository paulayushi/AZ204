using AZ204.WebApp.Models;
using Microsoft.Data.SqlClient;

namespace AZ204.WebApp.Services
{
    public class ProductService: IProductService
    {
        private readonly IConfiguration _configuration;

        public ProductService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<List<Product>> GetProductsAzync()
        {
            var products = new List<Product>();
            var connection = GetConnection();

            var statement = "select ProductID, ProductName, Quantity from products";
            SqlCommand command = new SqlCommand(statement, connection);

            connection.Open();

            using(SqlDataReader reader = await command.ExecuteReaderAsync()) 
            {
                while(reader.Read())
                {
                    var product = new Product
                    {
                        ProductID = reader.GetInt32(0),
                        ProductName = reader.GetString(1),
                        Quantity = reader.GetInt32(2)
                    };
                    products.Add(product);
                }
            }

            return products;
        }

        private SqlConnection GetConnection()
        {
            var builder = new SqlConnectionStringBuilder(_configuration.GetConnectionString("SqlConnection"));
            return new SqlConnection(builder.ConnectionString);
        }
    }
}

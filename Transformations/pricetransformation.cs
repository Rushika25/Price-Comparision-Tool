using Microsoft.Extensions.Configuration;
using PriceComparisiontool.Models;
using System.Data;
using System.Data.SqlClient;

namespace PriceComparisiontool.Transformations
{
    

    public class ProductRepository
    {
        private readonly string _connectionString;

        public ProductRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public void AddProduct(Product product)
        {
            using var conn = new SqlConnection(_connectionString);
            conn.Open();

            using var cmd = new SqlCommand("INSERT INTO Products (Name, LastUpdated) OUTPUT INSERTED.Id VALUES (@Name, @LastUpdated)", conn);
            cmd.Parameters.AddWithValue("@Name", product.Name);
            cmd.Parameters.AddWithValue("@LastUpdated", product.LastUpdated);

            var productId = (int)cmd.ExecuteScalar();

            foreach (var price in product.Prices)
            {
                var priceCmd = new SqlCommand("INSERT INTO Prices (Store, Amount, Url, ProductId) VALUES (@Store, @Amount, @Url, @ProductId)", conn);
                priceCmd.Parameters.AddWithValue("@Store", price.Store);
                priceCmd.Parameters.AddWithValue("@Amount", price.Amount);
                priceCmd.Parameters.AddWithValue("@Url", price.Url ?? (object)DBNull.Value);
                priceCmd.Parameters.AddWithValue("@ProductId", productId);
                priceCmd.ExecuteNonQuery();
            }
        }

        public Product? GetProductByName(string name)
        {
            
            using var conn = new SqlConnection(_connectionString);
            conn.Open();

            var productCmd = new SqlCommand("SELECT Id, Name, LastUpdated FROM Products WHERE Name = @Name", conn);
            productCmd.Parameters.AddWithValue("@Name", name);

            using var reader = productCmd.ExecuteReader();
            if (!reader.Read()) return null;

            var product = new Product
            {
                Id = reader.GetInt32(0),
                Name = reader.GetString(1),
                LastUpdated = reader.GetDateTime(2),
                Prices = new List<Price>()
            };

            reader.Close();

            var priceCmd = new SqlCommand("SELECT Id, Store, Amount, Url FROM Prices WHERE ProductId = @ProductId", conn);
            priceCmd.Parameters.AddWithValue("@ProductId", product.Id);

            using var priceReader = priceCmd.ExecuteReader();
            while (priceReader.Read())
            {
                product.Prices.Add(new Price
                {
                    Id = priceReader.GetInt32(0),
                    Store = priceReader.GetString(1),
                    Amount = priceReader.GetDecimal(2),
                    Url = priceReader.IsDBNull(3) ? null : priceReader.GetString(3)
                });
            }

            return product;
        }
    }

}

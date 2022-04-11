using Dapper;
using Microsoft.Extensions.Options;
using ProductTurnover.Infra;
using ProductTurnover.Infra.Record;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace ProductTurnover.DAL
{
    public class ProductRepository : IProductRepository, IDisposable
    {
        private readonly IDbConnection _conn;

        public ProductRepository(IOptions<ProductTurnoverConfig> conf)
        {
            _conn = new SqlConnection(conf.Value.ConnectionString);
            _conn.Open();
        }

        public Product Read(int EAN)
        {
            var sql = $"SELECT * FROM Product p JOIN Category c ON p.CategoryId = c.Id WHERE p.EAN = {EAN}";

            var product = _conn.Query<Product, Category, Product>(sql, (product, category) => {
                product.Category = category;
                return product;
            },
            splitOn: "CategoryId").FirstOrDefault();

            return product;
        }

        public void Dispose()
        {
            _conn?.Dispose();
        }
    }
}

using Dapper;
using ProductTurnover.Infra;
using System.Data;

namespace ProductTurnover.DAL
{
    public class ProductRepository : IProductRepository
    {
        private readonly IDbConnection _connection = null;

        public ProductRepository(IDbConnection conn)
        {
            _connection = conn;
        }

        public decimal ReadTaxation(int EAN)
        {
            var sql = $"SELECT c.VAT FROM Product p JOIN Category c ON P.Id = C.Id WHERE p.EAN = {EAN}";

            var vat = _connection.QuerySingleOrDefault<decimal>(sql);

            return vat;
        }
    }
}

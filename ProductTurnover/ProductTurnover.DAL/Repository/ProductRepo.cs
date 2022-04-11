using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductTurnover.DAL.Repository
{
    public class ProductRepo
    {
        private readonly IDbConnection Connection = null;

        public ProductRepo(IDbConnection conn)
        {
            Connection = conn;
        }

        public int ReadTaxation(int EAN)
        {
            var sql = "";

            var vat = Connection.QuerySingle<int>(sql);

            return vat;
        }
    }
}

using IGL.Core.Entities.CoreContext;
using IGL.Core.Entities.ProductTransaction;
using IGL.Core.Repository.MaterialDetail;
using IGL.Infrastructure.Repository.SqlHelper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IGL.Infrastructure.Repository.MaterialDetail
{
    public class ProductReturnRepository : IProductReturnRepository
    {
        private readonly IGLContext baseContext = null;
        private readonly string _connectionString;

        public ProductReturnRepository()
        {
            baseContext = new IGLContext();
            _connectionString = baseContext.Database.GetDbConnection().ConnectionString;
        }

        public async Task<List<ProductIssueDetail>> GetProductIssueDetail(int Id)
        {
            var models = new List<ProductIssueDetail>();

            SqlParameter[] sqlParams = {
                 new SqlParameter("@mtId",Id)
            };

            var reader = await SqlHelperExtension.ExecuteReader(_connectionString, SqlConstant.ProcGetIssueProduct, System.Data.CommandType.StoredProcedure, sqlParams);

            while (reader.Read())
            {
                ProductIssueDetail model = new ProductIssueDetail();
                model.Id = reader.DefaultIfNull<int>("Id");
                model.ProductName = reader.DefaultIfNull<string>("ProductName");
                model.EmployeeName = reader.DefaultIfNull<string>("EmployeeName");
                model.TransactionDate = reader.DefaultIfNull<DateTime>("TransactionDate");
                model.TransactionType = reader.DefaultIfNull<string>("TransactionType");
                model.SlipNumber = reader.DefaultIfNull<string>("SlipNumber");
                model.ItemId = reader.DefaultIfNull<int>("ItemId");
                model.ItemNumber = reader.DefaultIfNull<string>("ItemNumber");
                model.Quantity = reader.DefaultIfNull<decimal>("Quantity");
                models.Add(model);
            }

            return models;
        }
    }
}

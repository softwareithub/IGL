
using IGL.Core.Entities.CoreContext;
using IGL.Core.Entities.Reports;
using IGL.Core.Repository.ReportsRepository;
using IGL.Infrastructure.Repository.SqlHelper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IGL.Infrastructure.Repository.Reports
{
    public class ProductReportRepository : IProductReportRepository
    {
        private IGLContext baseContext = null;
        private readonly string _connectionString;

        public ProductReportRepository()
        {
            baseContext = new IGLContext();
            _connectionString = baseContext.Database.GetDbConnection().ConnectionString;
        }
        public async Task<List<ProductReport>> GetProductReport()
        {
            var models = new List<ProductReport>();
            var reader = await SqlHelperExtension.ExecuteReader(_connectionString, SqlConstant.ProcGetProductReport, System.Data.CommandType.StoredProcedure, null);

            while (reader.Read())
            {
                ProductReport model = new ProductReport();
                model.Code = reader.DefaultIfNull<string>("Code");
                model.Name = reader.DefaultIfNull<string>("Name");
                model.PeUnitCost = reader.DefaultIfNull<decimal>("PerUnitCost");
                model.Quantity = reader.DefaultIfNull<int>("Quantity");
                model.ThresholdValue = reader.DefaultIfNull<int>("ThresholdValue");
                model.Unit = reader.DefaultIfNull<string>("Unit");                
                models.Add(model);
            }

            return models;
        }

        public async Task<List<ProductTransactionStatusReport>> GetProductTransactionStatusReport(int? EmployeeId)
        {
            var models = new List<ProductTransactionStatusReport>();
            
            var reader = await SqlHelperExtension.ExecuteReader(_connectionString, SqlConstant.ProcGetProductTransactionStatusReport, System.Data.CommandType.StoredProcedure, null);

            while (reader.Read())
            {
                ProductTransactionStatusReport model = new ProductTransactionStatusReport();
                model.EmailId = reader.DefaultIfNull<string>("EmailId");
                model.EmployeeId = reader.DefaultIfNull<int>("EmployeeId");
                model.EmployeeName = reader.DefaultIfNull<string>("EmployeeName");
                model.Phone = reader.DefaultIfNull<string>("Phone");
                model.ProductName = reader.DefaultIfNull<string>("ProductName");
                model.Quantity = reader.DefaultIfNull<int>("Quantity");
                model.TotalPrice = reader.DefaultIfNull<decimal>("TotalPrice");
                model.TransactionType = reader.DefaultIfNull<string>("TransactionType"); 
                model.UnitPrice = reader.DefaultIfNull<decimal>("UnitPrice"); ;
                models.Add(model);
            }

            return models;
        }
    }
}


using IGL.Core.Entities.CoreContext;
using IGL.Core.Entities.Reports;
using IGL.Core.Repository.ReportsRepository;
using IGL.Infrastructure.Repository.SqlHelper;
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
    }
}

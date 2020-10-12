using IGL.Core.Entities.Reports;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IGL.Core.Repository.ReportsRepository
{
    public interface IProductReportRepository
    {
        Task<List<ProductReport>> GetProductReport();
        Task<List<ProductTransactionStatusReport>> GetProductTransactionStatusReport(int? EmployeeId);
    }
}

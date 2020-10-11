using IGL.Core.Entities.Reports;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IGL.Core.Service.ReportsService
{
    public interface IProductReportService
    {
        Task<List<ProductReport>> GetProductReport();
    }
}

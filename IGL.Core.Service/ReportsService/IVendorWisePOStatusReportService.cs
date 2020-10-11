using IGL.Core.Entities.Reports;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IGL.Core.Service.ReportsService
{
    public interface IVendorWisePOStatusReportService
    {
        Task<List<VendorWisePOStatus>> GetVendorWisePoStatusReport();
        Task<List<VenderWisePoStatusDetail>> GetVendorWisePoStatusDetailReport(int vendorId, string status);
    }
}

using IGL.Core.Entities.Reports;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IGL.Core.Repository.ReportsRepository
{
    public interface IVendorWisePoStatusReportRepository
    {
        Task<List<VendorWisePOStatus>> GetVendorWisePOStatusReport();
        Task<List<VenderWisePoStatusDetail>> GetVendorWisePoStatusDetailReport(int vendorId , string status);
    }
}

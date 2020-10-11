using IGL.Core.Entities.Reports;
using IGL.Core.Repository.ReportsRepository;
using IGL.Core.Service.ReportsService;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IGL.Infastructure.Service.ReportService
{
    public class VendorWisePoStatusReportService : IVendorWisePOStatusReportService
    {
        private readonly IVendorWisePoStatusReportRepository _IVendorWisePOStatusReportRepository;

        public VendorWisePoStatusReportService(IVendorWisePoStatusReportRepository vendorWisePoStatusReportRepository)
        {
            _IVendorWisePOStatusReportRepository = vendorWisePoStatusReportRepository;
        }


        public Task<List<VenderWisePoStatusDetail>> GetVendorWisePoStatusDetailReport(int vendorId, string status) =>
            _IVendorWisePOStatusReportRepository.GetVendorWisePoStatusDetailReport(vendorId, status);
       
        public Task<List<VendorWisePOStatus>> GetVendorWisePoStatusReport() => _IVendorWisePOStatusReportRepository.GetVendorWisePOStatusReport();
    }
}

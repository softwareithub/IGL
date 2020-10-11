using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IGL.Core.Service.ReportsService;
using Microsoft.AspNetCore.Mvc;

namespace IGL.UI.Controllers.Reports
{
    public class VendorWisePoStatusReport : Controller
    {
        private readonly IVendorWisePOStatusReportService _IVendorWisePOStatusReportService;
        public VendorWisePoStatusReport(IVendorWisePOStatusReportService vendorWisePOStatusReportService)
        {
            _IVendorWisePOStatusReportService = vendorWisePOStatusReportService;
        }
        public async Task<IActionResult> Index()
        {
            var models = await _IVendorWisePOStatusReportService.GetVendorWisePoStatusReport();
            return View("~/Views/Reports/VendorWisePoStatusReport.cshtml", models);
        }

        public async Task<IActionResult> GetPODetailReport(int vendorId, string status)
        {
            var models = await _IVendorWisePOStatusReportService.GetVendorWisePoStatusDetailReport(vendorId, status);
            return PartialView("~/Views/Reports/_VendorWisePoStatusDetailPartial.cshtml", models);
        }
    }
}

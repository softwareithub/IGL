using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IGL.Core.Service.ReportsService;
using Microsoft.AspNetCore.Mvc;

namespace IGL.UI.Controllers.Reports
{
    public class ProductReport : Controller
    {
        private readonly IProductReportService _IProductReportService;

        public ProductReport(IProductReportService productReportService)
        {
            _IProductReportService = productReportService;
        }
        public async Task<IActionResult> Index()
        {
            var models =await  _IProductReportService.GetProductReport();
            return View("~/Views/Reports/ProductReport.cshtml", models);
        }
    }
}

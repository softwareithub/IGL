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

        public async Task<IActionResult> GetProductTransactionStatusReport(int? EmployeeId)
        {
            var models = await _IProductReportService.GetProductTransactionStatusReport(EmployeeId);
            return View("~/Views/Reports/ProductTransactionStatusReport.cshtml", models);
        }

        public async Task<IActionResult> GetLowQuantityProductReport()
        {
            var models = await _IProductReportService.GetLowQuantityProductReport();
            return View("~/Views/Reports/LowQuantityProductReport.cshtml", models);
        }
    }
}

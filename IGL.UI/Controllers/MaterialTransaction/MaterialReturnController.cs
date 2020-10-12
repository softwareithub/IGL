using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IGL.Core.Entities.Transaction;
using IGL.Core.Service.GenericService;
using IGL.Core.Service.MaterialDetail;
using Microsoft.AspNetCore.Mvc;

namespace IGL.UI.Controllers.MaterialTransaction
{
    public class MaterialReturnController : Controller
    {
        private readonly IProductReturnService _IProductReturnService;
        private readonly IGenericService<MaterialTransction, int> _IMaterialTransactionService;
        public MaterialReturnController(IProductReturnService productReturnService, IGenericService<MaterialTransction, int> materialTransaction)
        {
            _IProductReturnService = productReturnService;
            _IMaterialTransactionService = materialTransaction;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.TransactionDetail = await _IMaterialTransactionService.GetList(x => x.IsActive == 1);
            return View("~/Views/MaterialTransaction/_MaterialReturnIndex.cshtml");
        }

        public async Task<IActionResult> GetProductIssueDetail(int id)
        {
            var model = await _IProductReturnService.GetProductIssueDetail(id);
            return PartialView("~/Views/MaterialTransaction/MaterialReturnPartial.cshtml", model);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IGL.Core.Entities.Transaction;
using IGL.Core.Service.GenericService;
using IGL.Core.Service.MaterialDetail;
using Microsoft.AspNetCore.Http;
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
            HttpContext.Session.SetInt32("TransactionId", id);
            var model = await _IProductReturnService.GetMaterialIssueDetail(id);
            return PartialView("~/Views/MaterialTransaction/MaterialReturnPartial.cshtml", model);
        }

        [HttpPost]
        public async Task<IActionResult> ProductReturn(string [] matId,string [] ItemNumber, string [] qty,string slipNumber)
        {
            var models = new List<MaterialReturn>();
            for(int i=0; i<matId.Count();i++)
            {
                var model = new MaterialReturn();
                model.Id = Convert.ToInt32(HttpContext.Session.GetInt32("TransactionId"));
                model.ProductId = Convert.ToInt32(matId[i]);
                model.UniqueItemId =Convert.ToInt32(ItemNumber[i].ToString()??"0");
                model.Quantity = Convert.ToDecimal(qty[i]);
                model.SlipNumber = slipNumber;
                model.TransactionType = "Return";
                model.TransactionDate = DateTime.Now;
                models.Add(model);
            }
            var response = await _IProductReturnService.MaterialReturn(models);
            return Json("");
        }
    }
}
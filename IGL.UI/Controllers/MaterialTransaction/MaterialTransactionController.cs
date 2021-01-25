using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IGL.Core.Comman.Helper;
using IGL.Core.Entities.Master;
using IGL.Core.Entities.ProductTransaction;
using IGL.Core.Entities.Transaction;
using IGL.Core.Service.GenericService;
using IGL.Core.Service.MaterialDetail;
using IGL.Core.ViewModelEntities.MasterVm;
using IGL.Core.ViewModelEntities.MasterVm.TransactionVm;
using IGL.UI.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IGL.UI.Controllers.MaterialTransaction
{
    [Authorize]
    public class MaterialTransactionController : Controller
    {
        private readonly IGenericService<MaterialTransction, int> _IMaterialTransactionService;
        private readonly IGenericService<EmployeeDetail, int> _IEmployeeDetailService;
        private readonly IGenericService<MaterialMaster, int> _IMaterialMasterService;
        private readonly IGenericService<UnitMaster, int> _IUnitMasterRepo;
        private readonly IGenericService<TransactionItems, int> _ITransactionItemService;
        private readonly IMaterialDetailService _materialDetailService;
        private readonly IGenericService<ProductTransactionDetail, int> _IProductDetailService;
        private readonly IProductReturnService _productReturnService;

        public MaterialTransactionController(IGenericService<MaterialTransction, int> materialTransactionService, 
            IGenericService<EmployeeDetail, int> employeeDetailService, IGenericService<MaterialMaster, int> materialMasterService,
            IGenericService<UnitMaster, int> unitMasterRepo, IGenericService<TransactionItems, int> _transactionService,
            IMaterialDetailService materialDetailService, IGenericService<ProductTransactionDetail, int> productDetailService,
            IProductReturnService productReturnService
            )
        {
            _IMaterialTransactionService = materialTransactionService;
            _IEmployeeDetailService = employeeDetailService;
            _IMaterialMasterService = materialMasterService;
            _IUnitMasterRepo = unitMasterRepo;
            _ITransactionItemService = _transactionService;
            _materialDetailService = materialDetailService;
            _IProductDetailService = productDetailService;
            _productReturnService = productReturnService;
        }
        public async Task<IActionResult> Index()
        {
            return await Task.Run(() => View("~/Views/MaterialTransaction/MaterialTransactionIndex.cshtml"));
        }
        public async Task<IActionResult> ReturnIssue()
        {
            ViewBag.EmployeeList = await _IEmployeeDetailService.GetList(x => x.IsActive == 1);
            ViewBag.ProductList = await _IMaterialMasterService.GetList(x => x.IsActive == 1);
            return PartialView("~/Views/MaterialTransaction/_MaterialIssueReturnPartial.cshtml");
        }
        public async Task<IActionResult> GetMaterialDetails()
        {
            var models =await _materialDetailService.GetMaterialDetail();
            return PartialView("~/Views/MaterialTransaction/_MaterialDetailList.cshtml", models);
        }

        public async Task<IActionResult> GetMaterialTransactionDetail()
        {
            var employeeDetails = await _IEmployeeDetailService.GetList(x => x.IsActive == 1);
            var transactionDetail = await _IMaterialTransactionService.GetList(x => x.IsActive == 1);

            var models = (from TM in transactionDetail
                          join EM in employeeDetails
                          on TM.EmployeeId equals EM.Id
                          select new MaterialTransactionModel
                          {
                              Id = TM.Id,
                              EmployeeName = EM.Name,
                              TransactionDate = TM.TransactionDate,
                              TransactionType = TM.TransactionType,
                              SlipNumber = TM.SlipNumber
                          }).OrderByDescending(x=>x.Id).ToList();
            return PartialView("~/Views/MaterialTransaction/_MaterialTransactionDetailPartial.cshtml", models);
        }

        [HttpPost]
        public async Task<IActionResult> CreateReturnIssue(MaterialTransction model, string[] matId, string[] remarks, string[] qty, string[] prodNumber)
        {
            var listData = await _IMaterialTransactionService.GetList(x => x.IsActive == 1);
            string slipNumber = "#0001";

            if (listData.Count() > 0)
                slipNumber += listData.Max(x => x.Id).ToString();

            model.SlipNumber = slipNumber;

            List<TransactionItems> itemModels = new List<TransactionItems>();
            for (int i = 0; i < matId.Count(); i++)
            {
                TransactionItems item = new TransactionItems();
                item.ItemId = Convert.ToInt32(matId[i]?? "0");
                item.Remarks = remarks[i]?? string.Empty;
                item.Quantity = Convert.ToDecimal(qty[i]??"0");
                item.UniqueItemId = Convert.ToInt32(prodNumber[i]??"0");
                itemModels.Add(item);
            }
            var response = await _productReturnService.CreateMaterialIssue(model, itemModels);
            var responseMessage= response.responseStatus == 1 ? "Material Issue Created" : "Error in Material Issue creation.";
            return Json(responseMessage);
        }

        public async Task<IActionResult> GetMaterialSlip(int id)
        {
            var response = await _productReturnService.GetMaterialSlipDetail(id);

            return PartialView("~/Views/MaterialTransaction/_MaterialSlipPartial.cshtml", response);
        }

        public async Task<IActionResult> GetProductNumber(int prodId)
        {
            var models = await _IProductDetailService.GetList(x => x.IsActive == 1 && x.MaterialId == prodId);
            return Json(models);
        }

        public async Task<IActionResult> ValidateProductQuantity(int prodId,int qty)
        {
            var model = await _IMaterialMasterService.GetSingle(x => x.Id == prodId);
            if(model.OpeningQuantity<qty)
            {
                return Json("-1");
            }
            if(model.OpeningQuantity == qty)
            {
                return Json("-2");
            }
            return Json("");
        }

    }
}
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

        public MaterialTransactionController(IGenericService<MaterialTransction, int> materialTransactionService, IGenericService<EmployeeDetail, int> employeeDetailService, IGenericService<MaterialMaster, int> materialMasterService, IGenericService<UnitMaster, int> unitMasterRepo, IGenericService<TransactionItems, int> _transactionService, IMaterialDetailService materialDetailService, IGenericService<ProductTransactionDetail, int> productDetailService)
        {
            _IMaterialTransactionService = materialTransactionService;
            _IEmployeeDetailService = employeeDetailService;
            _IMaterialMasterService = materialMasterService;
            _IUnitMasterRepo = unitMasterRepo;
            _ITransactionItemService = _transactionService;
            _materialDetailService = materialDetailService;
            _IProductDetailService = productDetailService;
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
                          }).ToList();
            return PartialView("~/Views/MaterialTransaction/_MaterialTransactionDetailPartial.cshtml", models);
        }

        [HttpPost]
        public async Task<IActionResult> CreateReturnIssue(MaterialTransction model, string[] matId, string[] remarks, string[] qty, string[] prodNumber)
        {
            var listData = await _IMaterialTransactionService.GetList(x => x.IsActive == 1);
            string slipNumber = "#0001";
            if (listData.Count() > 0)
            {
                slipNumber += listData.Max(x => x.Id).ToString();
            }
            model.SlipNumber = slipNumber;
            var transactionMdoel = CommanCRUDHelper.CommanCreateCode(model, 1);
            var transactionMasterResponse = await _IMaterialTransactionService.CreateEntity(transactionMdoel);
            var materialId = (await _IMaterialTransactionService.GetList(x => x.IsActive == 1)).Max(x => x.Id);

            List<TransactionItems> itemModels = new List<TransactionItems>();
            List<MaterialMaster> prodList = new List<MaterialMaster>();
            for (int i = 0; i < matId.Count(); i++)
            {
                string itemNumber = string.Empty;
                if( prodNumber[i]!="0")
                {
                    var prodTranModel = await _IProductDetailService.GetSingle(x => x.Id == Convert.ToInt32(prodNumber[i]));
                    prodTranModel.IsActive = 0; prodTranModel.IsDeleted = 1;
                    var transResponse = await _IProductDetailService.Update(prodTranModel);
                    var productMasterModel = await _IMaterialMasterService.GetSingle(x => x.Id == Convert.ToInt32(matId[i]) && x.IsActive == 1);
                    productMasterModel.OpeningQuantity = productMasterModel.OpeningQuantity - 1;
                    await _IMaterialMasterService.Update(productMasterModel);

                    itemNumber = prodTranModel.ItemNumber;
                }

                TransactionItems itemModel = new TransactionItems();
                itemModel.MaterialTransctionId = materialId;
                itemModel.ItemId = Convert.ToInt32(matId[i]);
                itemModel.Quantity = Convert.ToDecimal(qty[i]);
                itemModel.ItemNumber = itemNumber;
                itemModel.Remarks = remarks[i];
                itemModel.IsActive = 1;
                itemModel.IsDeleted = 0;
                itemModel.CreatedBy = 1;
                itemModel.CreatedDate = DateTime.Now.Date;

                MaterialMaster prodModel = await _IMaterialMasterService.GetSingle(x => x.Id == Convert.ToInt32(matId[i]));
                prodModel.OpeningQuantity =Convert.ToInt32(Convert.ToDecimal(prodModel.OpeningQuantity) - Convert.ToDecimal(qty[i]));
                prodList.Add(prodModel);
                itemModels.Add(itemModel);
            }

            var response = await _ITransactionItemService.Add(itemModels.ToArray());
            var updateProdQuantity = await _IMaterialMasterService.Update(prodList.ToArray());
            return Json(ResponseHelper.GetResponseMessage(response));
        }

        public async Task<IActionResult> GetMaterialSlip(int id)
        {
            var materials = await _IMaterialTransactionService.GetList(x => x.IsActive == 1);
            var items = await _ITransactionItemService.GetList(x => x.IsActive == 1);

            var models = (from mt in materials
                          join im in items
                          on mt.Id equals im.MaterialTransctionId
                          join em in await _IEmployeeDetailService.GetList(x => x.IsActive == 1)
                          on mt.EmployeeId equals em.Id
                          join md in await _IMaterialMasterService.GetList(x => x.IsActive == 1)
                          on im.ItemId equals md.Id
                          join um in await _IUnitMasterRepo.GetList(x => x.IsActive == 1)
                          on md.UnitId equals um.Id
                          where mt.Id == id
                          select new MaterialSlipVm
                          {
                              Id = mt.Id,
                              SlipNumber = mt.SlipNumber,
                              SlipName = mt.TransactionType.ToLower().Trim() == "return" ? "Material Return Slip" : "Material Issue Slip",
                              SlipDate = mt.TransactionDate,
                              EmployeeName = em.Name,
                              MaterialCode = md.Code,
                              MaterialName = md.Name,
                              UnitPrice = md.PerUnitCost,
                              Quantity = im.Quantity,
                              Remarks = im.Remarks,
                              UnitName = um.Name,
                              TotalPrice = im.Quantity * md.PerUnitCost
                          }).ToList();

            return PartialView("~/Views/MaterialTransaction/_MaterialSlipPartial.cshtml", models);
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
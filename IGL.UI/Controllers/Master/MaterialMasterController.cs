using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IGL.Core.Comman.Helper;
using IGL.Core.Entities.Master;
using IGL.Core.Entities.ProductTransaction;
using IGL.Core.Service.GenericService;
using IGL.Core.ViewModelEntities.MasterVm;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IGL.UI.Controllers.Master
{
    [Authorize]
    public class MaterialMasterController : Controller
    {
        private readonly IGenericService<MaterialMaster, int> _IMaterialMasterService;
        private readonly IGenericService<UnitMaster, int> _IUnitMasterService;
        private readonly IGenericService<ProductTransactionDetail, int> _IProductTransactionService;
        private readonly IGenericService<RateMaster, int> _IRateMasterService;
        
        public MaterialMasterController(IGenericService<MaterialMaster, int> _matetialService, IGenericService<UnitMaster, int> _unitMasterService, IGenericService<ProductTransactionDetail, int> productTransactionService, IGenericService<RateMaster, int> rateMasterService)
        {
            _IMaterialMasterService = _matetialService;
            _IUnitMasterService = _unitMasterService;
            _IProductTransactionService = productTransactionService;
            _IRateMasterService = rateMasterService;
        }
        public async Task<IActionResult> Index()
        {
            return await Task.Run(() => View("~/Views/Master/Product/ProductIndex.cshtml"));
        }

        public async Task<IActionResult> GetProductDetails()
        {
            var unitModels = await _IUnitMasterService.GetList(x => x.IsActive == 1);
            var materialModels = await _IMaterialMasterService.GetList(x => x.IsActive == 1);


            var models = (from mt in materialModels
                          join UM in unitModels
                          on mt.UnitId equals UM.Id
                          select new MaterialMasterVm
                          {
                              Id = mt.Id,
                              MaterialName = mt.Name,
                              Code = mt.Code,
                              Unit = UM.Name,
                              PerUnitCost = mt.PerUnitCost,
                              IsPayable = mt.IsPayable == 1 ? "Payable" : "Not Payable",
                              ThresholdValue = mt.ThresholdValue,
                              Quantity = mt.OpeningQuantity,
                              HSNCode = mt.HSNCode,
                              IsUniqe = mt.IsUnique == 1
                          }).ToList();

            return PartialView("~/Views/Master/Product/_ProductList.cshtml", models);

        }

        public async Task<IActionResult> CreateMaterial(int id)
        {
            ViewBag.UnitMaster = await _IUnitMasterService.GetList(x => x.IsActive == 1);
            var materialModel = await _IMaterialMasterService.GetSingle(x => x.Id == id);
            return PartialView("~/Views/Master/Product/_ProductCreate.cshtml", materialModel);
        }

        [HttpPost]
        public async Task<IActionResult> UpSertMaterial(MaterialMaster model)
        {
            if (model.Id == 0)
            {
                var createModel = CommanCRUDHelper.CommanCreateCode(model, 1);
                var createResponse = await _IMaterialMasterService.CreateEntity(createModel);

                var createdPrdctId = (await _IMaterialMasterService.GetList(x => x.IsActive == 1)).Max(x => x.Id);
                var rateMasterResponse = await UpdateRateMaster(createdPrdctId, model.PerUnitCost);

                return Json(ResponseHelper.GetResponseMessage(createResponse));
            }
            else
            {
                var updateModel = CommanCRUDHelper.CommanUpdateCode(model, 1);
                var updateResponse = await _IMaterialMasterService.Update(updateModel);

                var rateMasterResponse = await UpdateRateMaster(model.Id, model.PerUnitCost);

                return Json(ResponseHelper.GetResponseMessage(updateResponse));
            }

            
        }

        public async Task<IActionResult> AssignProductNumber(int ProductId, int count)
        {
            ViewBag.prodId = ProductId;
            ViewBag.prodCount = count;
            
            var models = await _IProductTransactionService.GetList(x => x.IsActive == 1 && x.MaterialId == ProductId);

            if (models.Any())
            {
                var itemNumber = new List<string>();
                models.ToList().ForEach(item =>
                {
                    itemNumber.Add(item.ItemNumber);
                });

                ViewBag.ItemNumbes = itemNumber;

            }
            return await Task.Run(() => PartialView("~/Views/Master/Product/UniqueProductNumberPartial.cshtml"));
        }

        [HttpGet]
        public async Task<IActionResult> UpSertProductNumber(int prodId, string[] number)
        {
            var models = await _IProductTransactionService.GetList(x => x.IsActive == 1 && x.MaterialId == prodId);
            models.ToList().ForEach(item =>
            {
                item.IsActive = 0;
                item.IsDeleted = 1;
                item.UpdatedBy = 1;
                item.UpdatedDate = DateTime.Now.Date;
            });
            var response = await _IProductTransactionService.Delete(models.ToArray());
            var prodList = new List<ProductTransactionDetail>();
            for (int i = 0; i < number.Count(); i++)
            {
                ProductTransactionDetail prod = new ProductTransactionDetail()
                {
                    ItemNumber = number[i],
                    MaterialId = prodId,
                    IsActive = 1,
                    IsDeleted = 0,
                    CreatedBy = 1,
                    CreatedDate = DateTime.Now.Date
                };
                prodList.Add(prod);
            }
            var updateResponse = await _IProductTransactionService.Add(prodList.ToArray());
            return Json(ResponseHelper.GetResponseMessage(updateResponse));
        }
        public async Task<IActionResult> Delete(int id)
        {
            var model = await _IMaterialMasterService.GetSingle(x => x.Id == id);
            var deleteModel = CommanCRUDHelper.CommanDeleteCode(model, 1);
            await _IMaterialMasterService.CreateNewContext();
            var deleteResponse = await _IMaterialMasterService.Update(deleteModel);
            return Json(ResponseHelper.GetResponseMessage(deleteResponse));

        }

        private async  Task<bool> UpdateRateMaster(int prodId,decimal price)
        {
            //Check product exists in RateMaster
            var rateModel = (await _IRateMasterService.GetList(x => x.ProductId == prodId && x.ToDate == null)).FirstOrDefault();

            if(rateModel!=null)
            {
                rateModel.ToDate = DateTime.Now.Date;
                var updateResponse = await _IRateMasterService.Update(rateModel);
            }
            RateMaster model = new RateMaster();
            model.ProductId = prodId;
            model.Rate = Convert.ToDecimal(price);
            model.FromDate = DateTime.Now.Date;
            model.ToDate = null;
            var createResponse = await _IRateMasterService.CreateEntity(model);
            return createResponse == Core.Comman.Comman.ResponseMessage.AddedSuccessfully ? true : false;
        }

        public async Task<IActionResult> GetProductPriceDetail(int productId)
        {
            var response = (await _IRateMasterService.GetList(x => x.IsActive == 1 && x.IsDeleted == 0)).ToList();
            return PartialView("~/Views/Master/Product/_ProductRateDetailPartial.cshtml", response.Where(x=>x.ProductId==productId).ToList());
        }
    }
}
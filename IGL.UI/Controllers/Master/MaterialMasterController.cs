using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IGL.Core.Comman.Helper;
using IGL.Core.Entities.Master;
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
        public MaterialMasterController(IGenericService<MaterialMaster, int> _matetialService, IGenericService<UnitMaster, int> _unitMasterService)
        {
            _IMaterialMasterService = _matetialService;
            _IUnitMasterService = _unitMasterService;
        }
        public async Task<IActionResult> Index()
        {
            return await Task.Run(()=> View("~/Views/Master/Product/ProductIndex.cshtml"));
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
                              IsPayable = mt.IsPayable==1? "Payable":"Not Payable",
                              ThresholdValue= mt.ThresholdValue,
                              Quantity= mt.OpeningQuantity,
                              HSNCode= mt.HSNCode
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
            if(model.Id==0)
            {
                var createModel = CommanCRUDHelper.CommanCreateCode(model, 1);
                var createResponse = await _IMaterialMasterService.CreateEntity(createModel);
                return Json(ResponseHelper.GetResponseMessage(createResponse));

            }
            var updateModel = CommanCRUDHelper.CommanUpdateCode(model, 1);
            var updateResponse = await _IMaterialMasterService.Update(updateModel);
            return Json(ResponseHelper.GetResponseMessage(updateResponse));

        }

        public async Task<IActionResult> Delete(int id)
        {
            var model =await  _IMaterialMasterService.GetSingle(x => x.Id == id);
            var deleteModel = CommanCRUDHelper.CommanDeleteCode(model, 1);
            await _IMaterialMasterService.CreateNewContext();
            var deleteResponse = await _IMaterialMasterService.Update(deleteModel);
            return Json(ResponseHelper.GetResponseMessage(deleteResponse));

        }
    }
}
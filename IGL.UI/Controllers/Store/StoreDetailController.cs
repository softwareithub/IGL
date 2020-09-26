using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IGL.Core.Comman.Helper;
using IGL.Core.Entities.StoreMaster;
using IGL.Core.Service.GenericService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace IGL.UI.Controllers.Store
{
    public class StoreDetailController : Controller
    {
        private readonly IGenericService<StoreDetail, int> _IStoreDetailService;
        public StoreDetailController(IGenericService<StoreDetail, int> _storeDetailService)
        {
            _IStoreDetailService = _storeDetailService;
        }
        public IActionResult Index()
        {
            return View("~/Views/Store/StoreIndex.cshtml");
        }

        public async Task<IActionResult> GetStoreDetails()
        {
            var models = await _IStoreDetailService.GetList(x => x.IsActive == 1);
            return PartialView("~/Views/Store/_StoreDetailPartial.cshtml", models);
        }

        public async Task<IActionResult> CreateStoreDetail(int id)
        {
            var model = await _IStoreDetailService.GetSingle(x => x.Id == id);
            return PartialView("~/Views/Store/_CreateStorePartial.cshtml", model);
        }

        [HttpPost]
        public async Task<IActionResult> UpSertStoreDetail(StoreDetail model)
        {
            if(model.Id>0)
            {
                var updateModel = CommanCRUDHelper.CommanUpdateCode(model, 1);
                var updateResponse = await _IStoreDetailService.Update(updateModel);
                return Json(ResponseHelper.GetResponseMessage(updateResponse));
            }
            var createModel = CommanCRUDHelper.CommanCreateCode(model, 1);
            var createResponse = await _IStoreDetailService.CreateEntity(createModel);
            return Json(ResponseHelper.GetResponseMessage(createResponse));

        }

        
        public async Task<IActionResult> DeleteStoreDetail(int id)
        {
            var model = await _IStoreDetailService.GetSingle(x => x.Id == id);
            var updateModel = CommanCRUDHelper.CommanDeleteCode(model, 1);
            var updateResponse = await _IStoreDetailService.Update(updateModel);
            return Json(ResponseHelper.GetResponseMessage(updateResponse));

        }
    }
}
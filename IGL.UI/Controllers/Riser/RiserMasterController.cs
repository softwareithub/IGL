using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IGL.Core.Comman.Helper;
using IGL.Core.Entities.Transaction;
using IGL.Core.Service.GenericService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IGL.UI.Controllers.Riser
{
    [Authorize]
    public class RiserMasterController : Controller
    {
        private readonly IGenericService<RiserMaster, int> _IRiserMasterService;

        public RiserMasterController(IGenericService<RiserMaster, int> riserMasterService)
        {
            _IRiserMasterService = riserMasterService;
        }
        public async Task<IActionResult> Index()
        {
            return await Task.Run(()=> View("~/Views/RiserMaster/RiserIndex.cshtml"));
        }
        public async Task<IActionResult> GetRiserList()
        {
            var models = await _IRiserMasterService.GetList(x => x.IsActive == 1);
            return PartialView("~/Views/RiserMaster/_RiserMasterList.cshtml", models);
        }
        public async Task<IActionResult> CreateRiser(int id)
        {
            var riserModel = await _IRiserMasterService.GetList(x => x.IsActive == 1);
            string riserNumber = "#rs0001";
            if(riserModel.Count()>0)
            {
                riserNumber = "#rsr00" + (riserModel.Max(x => x.Id) + 1).ToString();
            }
            var model = await _IRiserMasterService.GetSingle(x => x.Id == id);
            ViewData["RiserNumber"] = riserNumber;
            return PartialView("~/Views/RiserMaster/_CreateRiserMaster.cshtml", model);
        }

        [HttpPost]
        public async Task<IActionResult> UpSertRiserMaster(RiserMaster model)
        {
            if(model.Id>0)
            {
                var updateModel = CommanCRUDHelper.CommanUpdateCode(model, 1);
                var updateResponse = await _IRiserMasterService.Update(updateModel);
                return Json(ResponseHelper.GetResponseMessage(updateResponse));
            }

            var createModel = CommanCRUDHelper.CommanCreateCode(model, 1);
            var createResponse = await _IRiserMasterService.CreateEntity(createModel);
            return Json(ResponseHelper.GetResponseMessage(createResponse));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var model = await _IRiserMasterService.GetSingle(x => x.Id == id);
            var deleteModel = CommanCRUDHelper.CommanDeleteCode(model, 1);
            var response = await _IRiserMasterService.Update(deleteModel);
            return Json(ResponseHelper.GetResponseMessage(response));
        }
    }
}
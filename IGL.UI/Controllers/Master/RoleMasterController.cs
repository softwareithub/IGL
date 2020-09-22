using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IGL.Core.Comman.Comman;
using IGL.Core.Comman.Helper;
using IGL.Core.Entities.Master;
using IGL.Core.Service.GenericService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IGL.UI.Controllers.Master
{
    [Authorize]
    public class RoleMasterController : Controller
    {
        private readonly IGenericService<RoleMaster, int> _IRoleMasterService;

        public RoleMasterController(IGenericService<RoleMaster, int> roleMasterService)
        {
            _IRoleMasterService = roleMasterService;
        }
        public async Task<IActionResult> Index()
        {
            var models = await _IRoleMasterService.GetList(x => x.IsActive == 1);
            return View("~/Views/UserManagement/RoleIndex.cshtml",models);
        }

        public async Task<IActionResult> CreateRole(int id)
        {
            var model = await _IRoleMasterService.GetSingle(x => x.Id == id);
            return PartialView("~/Views/UserManagement/_RoleCreatePartial.cshtml", model);
        }

        [HttpPost]
        public async Task<IActionResult> UpSertRole(RoleMaster modelEntity)
        {
            if(modelEntity.Id==0)
            {
                var createModel = CommanCRUDHelper.CommanCreateCode(modelEntity, 1);
                var createResponse = await _IRoleMasterService.CreateEntity(createModel);
                return Json(ResponseHelper.GetResponseMessage(createResponse));
            }

            var updateModel = CommanCRUDHelper.CommanUpdateCode(modelEntity, 1);
            var updateResponse = await _IRoleMasterService.Update(updateModel);
            return Json(ResponseHelper.GetResponseMessage(updateResponse));
        }
        public async Task<IActionResult> Delete(int id)
        {
            var model = await _IRoleMasterService.GetSingle(x => x.Id == id);
            var deleteModel = CommanCRUDHelper.CommanDeleteCode(model, 1);
            await _IRoleMasterService.CreateNewContext();
            var response = await _IRoleMasterService.Update(deleteModel);
            return Json(ResponseHelper.GetResponseMessage(response));
        }
    }
}
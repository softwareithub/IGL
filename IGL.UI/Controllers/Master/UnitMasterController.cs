using IGL.Core.Comman.Helper;
using IGL.Core.Entities.Master;
using IGL.Core.Service.GenericService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace IGL.UI.Controllers.Master
{
    [Authorize]
    public class UnitMasterController : Controller
    {
        private readonly IGenericService<UnitMaster, int> _IUnitMasterService;

        public UnitMasterController(IGenericService<UnitMaster,int> _unitMasterService)
        {
            _IUnitMasterService = _unitMasterService;
        }
        public async Task<IActionResult> Index()
        {
            var models = await _IUnitMasterService.GetList(x => x.IsActive == 1);
            return View("~/Views/Master/UnitMaster/UnitMaster.cshtml", models.OrderBy(x => x.CreatedDate).ThenBy(x => x.UpdatedDate).ToList());
        }
        public async Task<IActionResult> GetUnitList()
        {
            var models = await _IUnitMasterService.GetList(x => x.IsActive == 1);
            return PartialView("~/Views/Master/UnitMaster/_UnitListPartial.cshtml", models.OrderBy(x => x.CreatedDate).ThenBy(x => x.UpdatedDate).ToList());
        }


        public async Task<IActionResult> CreateUnit(int id)
        {
            var model = await _IUnitMasterService.GetSingle(x => x.Id == id);
            return PartialView("~/Views/Master/UnitMaster/_UnitMasterCreatePartial.cshtml", model);
        }

        [HttpPost]
        public async Task<IActionResult> UpSertUnitMaster(UnitMaster model)
        {
            if(model.Id>0)
            {
                var responseUpdate = await _IUnitMasterService.Update(model);
                return Json(ResponseHelper.GetResponseMessage(responseUpdate));

            }
            var responseCreate = await _IUnitMasterService.CreateEntity(model);
            return Json(ResponseHelper.GetResponseMessage(responseCreate));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var model = await _IUnitMasterService.GetSingle(x => x.Id == id);
            var deleteModel = CommanCRUDHelper.CommanDeleteCode<UnitMaster>(model, 1);
            await _IUnitMasterService.CreateNewContext();
            var responseData = await _IUnitMasterService.Update(deleteModel);
            return Json(ResponseHelper.GetResponseMessage(responseData));
        }

       
    }
}
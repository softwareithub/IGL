using System.Linq;
using System.Threading.Tasks;
using IGL.Core.Comman.Helper;
using IGL.Core.Entities.Inventory;
using IGL.Core.Service.GenericService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IGL.UI.Controllers.Inventory
{
    [Authorize]
    public class VendorMasterController : Controller
    {
        private readonly IGenericService<VendorMaster, int> _IVendorMasterService;

        public VendorMasterController(IGenericService<VendorMaster,int> _vendorMasterService)
        {
            _IVendorMasterService = _vendorMasterService;
        }
        public async Task<IActionResult>  Index()
        {
            return await Task.Run(()=> View("~/Views/Inventory/VendorIndex.cshtml"));
        }

        public async Task<IActionResult> GetVendorList(string vendorName)
        {
            var models = await _IVendorMasterService.GetList(x => x.IsActive == 1);
            return PartialView("~/Views/Inventory/_VendorListPartial.cshtml", models.OrderByDescending(x => x.Id).ThenBy(x => x.UpdatedDate).ToList());
        }

        public async Task<IActionResult> CreateVendor(int id)
        {
            var model = await _IVendorMasterService.GetSingle(x => x.Id == id);
            return PartialView("~/Views/Inventory/_CreateVendorPartial.cshtml", model);
        }
        [HttpPost]
        public async Task<IActionResult> UpSertVendor(VendorMaster model)
        {
            if(model.Id==0)
            {
                var createModel = CommanCRUDHelper.CommanCreateCode(model, 1);
                var response = await _IVendorMasterService.CreateEntity(createModel);
                return Json(ResponseHelper.GetResponseMessage(response));
            }

            var updateModel = CommanCRUDHelper.CommanUpdateCode(model, 1);
            var updateRespose = await _IVendorMasterService.Update(updateModel);
            return Json(ResponseHelper.GetResponseMessage(updateRespose));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var model = await _IVendorMasterService.GetSingle(x => x.Id == id);
            var deleteModel = CommanCRUDHelper.CommanDeleteCode(model, 1);
            var response = await _IVendorMasterService.Update(deleteModel);
            return Json(ResponseHelper.GetResponseMessage(response));
        }
    }
}
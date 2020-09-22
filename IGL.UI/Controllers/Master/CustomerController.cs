using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IGL.Core.Comman.Helper;
using IGL.Core.Entities.Master;
using IGL.Core.Service.GenericService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IGL.UI.Controllers.Master
{
    [Authorize]
    public class CustomerController : Controller
    {
        private readonly IGenericService<CustomerMaster, int> _ICustomerMasterService;
        public CustomerController(IGenericService<CustomerMaster, int> _customerService)
        {
            _ICustomerMasterService = _customerService;
        }
        public async Task<IActionResult> Index()
        {
            return await Task.Run(() => View("~/Views/Master/CustomerMaster/CustomerIndex.cshtml"));
        }
        public async Task<IActionResult> CreateCustomer(int id)
        {   
            var model = await _ICustomerMasterService.GetSingle(x => x.Id == id);
            return PartialView("~/Views/Master/CustomerMaster/_customerCreatePartial.cshtml", model);
        }
        public async Task<IActionResult> GetCustomerList()
        {
            var models = await _ICustomerMasterService.GetList(x => x.IsActive == 1);
            return PartialView("~/Views/Master/CustomerMaster/_CustomerListPartial.cshtml", models.OrderByDescending(x => x.CreatedDate).ThenBy(x => x.UpdatedDate));
        }

        [HttpPost]
        public async Task<IActionResult> UpSertCustomer(CustomerMaster modelEntity)
        {
            if(modelEntity.Id==0)
            {
                var createHelperModel = CommanCRUDHelper.CommanCreateCode(modelEntity, 1);
                var createResponse = await _ICustomerMasterService.CreateEntity(createHelperModel);
                return Json(ResponseHelper.GetResponseMessage(createResponse));
            }
            var updateHelperModel = CommanCRUDHelper.CommanUpdateCode(modelEntity, 1);
            var updateResponse = await _ICustomerMasterService.Update(updateHelperModel);
            return Json(ResponseHelper.GetResponseMessage(updateResponse));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var model = await _ICustomerMasterService.GetSingle(x => x.Id == id);
            var deleteModel = CommanCRUDHelper.CommanDeleteCode(model, 1);
            await _ICustomerMasterService.CreateNewContext();
            var deleteResponse = await _ICustomerMasterService.Update(deleteModel);
            return Json(ResponseHelper.GetResponseMessage(deleteResponse));
        }
    }
}
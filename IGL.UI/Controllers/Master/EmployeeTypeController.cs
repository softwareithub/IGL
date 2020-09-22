using System.Threading.Tasks;
using IGL.Core.Comman.Helper;
using IGL.Core.Entities.Master;
using IGL.Core.Service.GenericService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IGL.UI.Controllers.Master
{
    [Authorize]
    public class EmployeeTypeController : Controller
    {
        private readonly IGenericService<EmployeeType, int> _IEmployeeTypeService;

        public EmployeeTypeController(IGenericService<EmployeeType, int> employeeTypeService)
        {
            _IEmployeeTypeService = employeeTypeService;
        }
        public async Task<IActionResult> Index()
        {
            return await Task.Run(()=> View("~/Views/Master/EmployeeType/EmployeeTypeIndex.cshtml"));
        }

        public async Task<IActionResult> GetEmployeeTypeDetails()
        {
            var models = await _IEmployeeTypeService.GetList(x => x.IsActive == 1);
            return PartialView("~/Views/Master/EmployeeType/_EmployeeTypeList.cshtml", models);

        }

        public async Task<IActionResult> UpSertEmployeeType(int id)
        {
            var model = await _IEmployeeTypeService.GetSingle(x => x.Id == id);
            return PartialView("~/Views/Master/EmployeeType/_EmployeeTypeCreate.cshtml", model);
        }

        [HttpPost]
        public async Task<IActionResult> UpSertEmployeeType(EmployeeType model)
        {
            if(model.Id>0)
            {
                var updateModel = CommanCRUDHelper.CommanUpdateCode(model, 1);
                var updateResponse = await _IEmployeeTypeService.Update(updateModel);
                return Json(ResponseHelper.GetResponseMessage(updateResponse));
            }
            var createModel = CommanCRUDHelper.CommanCreateCode(model, 1);
            var createResponse = await _IEmployeeTypeService.CreateEntity(createModel);
            return Json(ResponseHelper.GetResponseMessage(createResponse));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var model = await _IEmployeeTypeService.GetSingle(x => x.Id == id);
            var deleteModel = CommanCRUDHelper.CommanDeleteCode(model, 1);
            await _IEmployeeTypeService.CreateNewContext();
            var response = await _IEmployeeTypeService.Update(deleteModel);
            return Json(ResponseHelper.GetResponseMessage(response));
        }
    }
}